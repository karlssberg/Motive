using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.Generation;
using Motiv.Generator.FluentBuilder.Generation.Shared;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public class FluentModelFactory
{
    private OrderedDictionary<FluentStep, FluentStep> _steps = new(FluentStep.ConstructorParametersComparer);

    public FluentFactoryCompilationUnit CreateFluentFactoryCompilationUnit(string fullname,
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        _steps = new OrderedDictionary<FluentStep, FluentStep>(FluentStep.ConstructorParametersComparer);

        ImmutableArray<INamespaceSymbol> usings =
        [
            ..fluentConstructorContexts
                .SelectMany(ctx => ctx.Constructor.Parameters)
                .Select(parameter => parameter.Type)
                .Select(type => type.ContainingNamespace)
                .Concat(fluentConstructorContexts.Select(ctx => ctx.Constructor.ContainingType.ContainingNamespace))
                .DistinctBy(ns => ns.ToDisplayString())
                .OrderBy(ns => ns.ToDisplayString())
        ];

        var startingFluentSteps = GetStartingFluentSteps(fluentConstructorContexts);

        var mergedFluentBuilderSteps = MergeFluentSteps(startingFluentSteps).ToImmutableArray();

        var fluentBuilderSteps = _steps.Values
            .Except(startingFluentSteps)
            .Reverse()
            .Select((step, index) =>
            {
                step.Name = $"Step_{index}__{fullname.Replace(".", "_")}";
                return step;
            })
            .ToImmutableArray();

        var fluentBuilderMethods = mergedFluentBuilderSteps
            .SelectMany(step => step.FluentMethods)
            .ToImmutableArray();

        var sampleConstructorContext = fluentConstructorContexts.First();
        return new FluentFactoryCompilationUnit(
            fullname,
            [..fluentBuilderMethods],
            fluentBuilderSteps,
            usings)
        {
            IsStatic = sampleConstructorContext.IsStatic,
            TypeKind = sampleConstructorContext.TypeKind,
            Accessibility = sampleConstructorContext.Accessibility,
            IsRecord = sampleConstructorContext.IsRecord
        };
    }

    private static ImmutableArray<FluentStep> GetStartingFluentSteps(
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        return
        [
            ..fluentConstructorContexts
                .Select(constructorContext => constructorContext.FluentMethodContexts
                    .Reverse()
                    .Aggregate(default(FluentStep?), (previousStep, method) =>
                        CreateFluentBuilderStep(method, previousStep, constructorContext)))
                .Where(step => step is not null)
        ];
    }

    private static FluentStep? CreateFluentBuilderStep(
        FluentMethodContext method,
        FluentStep? nextStep,
        FluentConstructorContext constructorContext)
    {
        var fluentMethodName = method.SourceParameter.GetFluentMethodName();

        var constructorParameters =
            method.PriorMethodContexts
                .Select(methodContext => methodContext.SourceParameter)
                .ToImmutableArray();

        var fluentBuilderMethod =
            new FluentMethod(
                fluentMethodName,
                nextStep ?? MaybeCreateCreationMethodStep(nextStep, constructorContext),
                constructorContext.Constructor)
            {
                SourceParameterSymbol = method.SourceParameter,
                KnownConstructorParameters = constructorParameters,
                ParameterConverters = method.ParameterConverters
            };

        var fluentBuilderStep = new FluentStep
        {
            KnownConstructorParameters = [..constructorParameters],
            FluentMethods =
            [
                fluentBuilderMethod,
                ..fluentBuilderMethod.ParameterConverters
                    .Select<IMethodSymbol, FluentMethod>(converter =>
                    {
                        var normalizedConverter = NormalizedConverterMethod(converter, fluentBuilderMethod);
                        return fluentBuilderMethod with
                        {
                            SourceParameterSymbol = normalizedConverter.Parameters.First(),
                            ParameterConverter = normalizedConverter
                        };
                    })
            ]
        };

        if (fluentBuilderMethod.ReturnStep is not null) fluentBuilderMethod.ReturnStep.Parent = fluentBuilderStep;

        return fluentBuilderStep;
    }

    private static FluentStep? MaybeCreateCreationMethodStep(FluentStep? nextStep,
        FluentConstructorContext constructorContext)
    {
        if (constructorContext.Options.HasFlag(FluentFactoryGeneratorOptions.NoCreateMethod))
            return null;

        return new FluentStep
        {
            KnownConstructorParameters = [..constructorContext.Constructor.Parameters],
            FluentMethods =
            [
                new FluentMethod("Create", nextStep, constructorContext.Constructor)
                {
                    KnownConstructorParameters = constructorContext.Constructor.Parameters
                }
            ]
        };
    }

    private IEnumerable<FluentStep> MergeFluentSteps(IEnumerable<FluentStep> fluentBuilderSteps)
    {
        var groupedSteps = fluentBuilderSteps
            .GroupBy(step => step, FluentStep.ConstructorParametersComparer);

        foreach (var group in groupedSteps)
        {
            var mergedStep = group.First();
            mergedStep.FluentMethods = [..MergeFluentMethods(group.SelectMany(s => s.FluentMethods))];

            foreach (var method in mergedStep.FluentMethods.Where(method => method.ReturnStep is not null))
                method.ReturnStep!.Parent = mergedStep;

            if (_steps.TryGetValue(mergedStep, out var step))
            {
                yield return step;
                yield break;
            }

            _steps.Add(mergedStep, mergedStep);
            yield return mergedStep;
        }
    }

    private static IMethodSymbol NormalizedConverterMethod(IMethodSymbol converter, FluentMethod method)
    {
        var returnType = converter.ReturnType;
        var sourceParameter = method.SourceParameterSymbol;

        var mapping = TypeMapper.MapGenericArguments(returnType, sourceParameter!.Type);

        var normalizedMethod = converter.NormalizeMethodTypeParameters(mapping);

        return normalizedMethod;
    }

    private IEnumerable<FluentMethod> MergeFluentMethods(IEnumerable<FluentMethod> fluentMethods)
    {
        var groups = fluentMethods
            .GroupBy(method => method, FluentMethod.FluentBuilderMethodComparer);

        foreach (var group in groups)
        {
            var method = group.First();
            var mergedMethod = method with { };

            var allConverters = new List<IMethodSymbol>();
            var allFluentMethods = new List<FluentMethod>();

            foreach (var nextMethod in group.Skip(1))
            {
                if (mergedMethod.ReturnStep is not null && nextMethod.ReturnStep is not null)
                    allFluentMethods.AddRange(nextMethod.ReturnStep.FluentMethods);

                allConverters.AddRange(nextMethod.ParameterConverters);
            }

            if (mergedMethod.ReturnStep is not null && allFluentMethods.Any())
                mergedMethod.ReturnStep.FluentMethods =
                    mergedMethod.ReturnStep.FluentMethods.AddRange(allFluentMethods);

            mergedMethod.ParameterConverters =
            [
                ..allConverters
                    .Union(method.ParameterConverters, SymbolEqualityComparer.Default)
                    .OfType<IMethodSymbol>()
            ];

            var returnSteps = group
                .Select(m => m.ReturnStep)
                .OfType<FluentStep>();

            var mergedReturnSteps = MergeFluentSteps(returnSteps).ToArray();

            if (mergedReturnSteps.Length > 1)
            {
                var sameName = mergedReturnSteps.Select(s => s.Name).Distinct().Count() == 1;
                var sameKnownParameters = mergedReturnSteps.Select(s => s.KnownConstructorParameters).Distinct().Count() == 1;

                var parameters =
                    string.Join("\n\n",
                        mergedReturnSteps.Select(s => string.Join(" -> ", s.KnownConstructorParameters)));
                throw new FluentException(
                    $"""
                    When merging fluent methods, {mergedReturnSteps.Length} return step were discovered when only one was expected.
                    The following methods were found:
                      Same Name: {sameName} ({string.Join(", ", mergedReturnSteps.Select(s => $"\"{s.Name}\"").Distinct())})
                      Same Known Parameters: {sameKnownParameters}

                      {parameters}
                    """);
            }

            mergedMethod.ReturnStep = mergedReturnSteps.FirstOrDefault();

            yield return mergedMethod;
        }
    }
}
