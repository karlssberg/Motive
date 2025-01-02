using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.Generation;
using Motiv.Generator.FluentBuilder.Generation.Shared;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public class FluentModelFactory(Compilation compilation)
{
    private OrderedDictionary<KnownConstructorParameters, FluentStep> _steps = new();

    public FluentFactoryCompilationUnit CreateFluentFactoryCompilationUnit(string fullname,
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        _steps = new OrderedDictionary<KnownConstructorParameters, FluentStep>();

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
                    .Aggregate(default(FluentStep?), (nextStep, method) =>
                        CreateFluentBuilderStep(method, nextStep, constructorContext)))
                .Where(step => step is not null)
        ];
    }

    private static FluentStep? CreateFluentBuilderStep(
        FluentMethodContext method,
        FluentStep? methodReturnStep,
        FluentConstructorContext constructorContext)
    {
        var knownConstructorParameters =
            method
                .PriorMethodContexts
                .Select(methodContext => methodContext.SourceParameter)
                .ToImmutableArray();

        var fluentBuilderMethod =
            new FluentMethod(
                method.Name,
                methodReturnStep ?? MaybeCreateStepContainingCreateMethod(constructorContext),
                constructorContext.Constructor)
            {
                FluentParameter = new FluentParameter(method.SourceParameter),
                KnownConstructorParameters = new KnownConstructorParameters(knownConstructorParameters),
                ParameterConverters = method.ParameterConverters
            };

        var fluentBuilderStep = new FluentStep
        {
            KnownConstructorParameters = new KnownConstructorParameters(knownConstructorParameters),
            FluentMethods =
            [
                fluentBuilderMethod,
                ..fluentBuilderMethod.ParameterConverters
                    .Select<IMethodSymbol, FluentMethod>(converter =>
                    {
                        var normalizedConverter = NormalizedConverterMethod(converter, fluentBuilderMethod);
                        return fluentBuilderMethod with
                        {
                            FluentParameter = new FluentParameter(normalizedConverter.Parameters.First()),
                            ParameterConverter = normalizedConverter,
                            ReturnStep = fluentBuilderMethod.ReturnStep?.Clone()
                        };
                    })
            ]
        };

        if (fluentBuilderMethod.ReturnStep is not null) fluentBuilderMethod.ReturnStep.Parent = fluentBuilderStep;

        return fluentBuilderStep;
    }

    private static FluentStep? MaybeCreateStepContainingCreateMethod(FluentConstructorContext constructorContext)
    {
        if (constructorContext.Options.HasFlag(FluentFactoryGeneratorOptions.NoCreateMethod))
            return null;

        return new FluentStep
        {
            KnownConstructorParameters = new KnownConstructorParameters(constructorContext.Constructor.Parameters),
            FluentMethods =
            [
                new FluentMethod("Create", constructorContext.Constructor)
                {
                    KnownConstructorParameters = new KnownConstructorParameters(constructorContext.Constructor.Parameters)
                }
            ]
        };
    }

    private IEnumerable<FluentStep> MergeFluentSteps(IEnumerable<FluentStep> fluentBuilderSteps)
    {
        var groupedSteps = fluentBuilderSteps
            .GroupBy(step => step.KnownConstructorParameters);

        foreach (var group in groupedSteps)
        {
            var mergedStep = group.First();
            var mergeableMethods = group.SelectMany(s => s.FluentMethods);
            mergedStep.FluentMethods = [..MergeFluentMethods(mergeableMethods)];

            // update parent reference
            foreach (var method in mergedStep.FluentMethods.Where(method => method.ReturnStep is not null))
                method.ReturnStep!.Parent = mergedStep;

            // if exists
            if (_steps.TryGetValue(mergedStep.KnownConstructorParameters, out var step))
            {
                yield return step;
                yield break;
            }

            // remember the merged step
            _steps.Add(mergedStep.KnownConstructorParameters, mergedStep);
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
            .GroupBy(method => method, FluentMethod.FluentMethodComparer);

        foreach (var group in groups)
        {
            var mergedMethod = CreateMergeMethod(group)!;

            var allConverters = new List<IMethodSymbol>();
            var allReturnStepFluentMethods = new List<FluentMethod>();

            var allAreOverloads  = group.All(m => m.OverloadParameter is not null);
            foreach (var nextMethod in group)
            {
                if (nextMethod.ReturnStep is not null
                    && nextMethod.ReturnStep.KnownConstructorParameters
                        .Equals(mergedMethod.ReturnStep?.KnownConstructorParameters))
                    allReturnStepFluentMethods.AddRange(nextMethod.ReturnStep.FluentMethods);

                allConverters.AddRange(
                    nextMethod
                        .ParameterConverters
                        .Where(c => !compilation.IsAssignable(c.Parameters.FirstOrDefault()?.Type, nextMethod.SourceParameterSymbol?.Type) ));
            }

            if (mergedMethod.ReturnStep is not null && allReturnStepFluentMethods.Any())
                mergedMethod.ReturnStep.FluentMethods.AddRange(allReturnStepFluentMethods);

            mergedMethod.ParameterConverters = [..allConverters];

            var returnSteps = group
                .Where(m => allAreOverloads || m.OverloadParameter is null)
                .Select(m => m.ReturnStep)
                .OfType<FluentStep>()
                .ToImmutableArray();

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

    private static FluentMethod? CreateMergeMethod(IEnumerable<FluentMethod> fluentMethods)
    {
        var firstMethod = default(FluentMethod?);
        foreach (var fluentMethod in fluentMethods)
        {
            if (fluentMethod.ParameterConverter is null)
                return fluentMethod;

            if (firstMethod is null)
                firstMethod = fluentMethod;
        }

        return firstMethod is null
            ? null
            : firstMethod with {};
    }
}
