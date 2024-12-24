using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.Generation;
using Motiv.Generator.FluentBuilder.Generation.Shared;

namespace Motiv.Generator.FluentBuilder.FluentModel;


public static class FluentModelFactory
{
    public static FluentFactoryCompilationUnit CreateFluentFactoryCompilationUnit(string fullname, ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
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

        var mergedFluentBuilderSteps = MergedFluentSteps(startingFluentSteps)
            .ToImmutableArray();

        var builderSteps = mergedFluentBuilderSteps
            .SelectMany(step => step.FluentMethods.Select(method => method.ReturnStep))
            .Where(step => step is not null)
            .ToImmutableArray();

        var fluentBuilderSteps = GetAllFluentSteps(builderSteps)
            .Distinct(FluentStep.ConstructorParametersComparer)
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

    private static ImmutableArray<FluentStep> GetAllFluentSteps(ImmutableArray<FluentStep> mergedFluentBuilderSteps)
    {
        IEnumerable<FluentStep> concatenated =
        [
            ..mergedFluentBuilderSteps,
            ..mergedFluentBuilderSteps.SelectMany(step =>
                GetAllFluentSteps([
                    ..step
                        .FluentMethods
                        .Select(method => method.ReturnStep)
                        .Where(returnStep => returnStep is not null)
                ]))
        ];

        return [..MergedFluentSteps(concatenated)];
    }

    private static ImmutableArray<FluentStep> GetStartingFluentSteps(ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
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
            FluentMethods = [
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

        if (fluentBuilderMethod.ReturnStep is not null)
            fluentBuilderMethod.ReturnStep.Parent = fluentBuilderStep;

        return fluentBuilderStep;
    }

    private static FluentStep? MaybeCreateCreationMethodStep(FluentStep? nextStep, FluentConstructorContext constructorContext)
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
                    KnownConstructorParameters = constructorContext.Constructor.Parameters,
                }
            ]
        };
    }

    private static IEnumerable<FluentStep> MergedFluentSteps(IEnumerable<FluentStep> fluentBuilderSteps)
    {
        var groupedSteps = fluentBuilderSteps
            .GroupBy(step => step, FluentStep.ConstructorParametersComparer)
            .ToImmutableArray();

        return groupedSteps
                .Select(group =>
                {
                    var mergedStep = group
                        .Aggregate((prev, next) =>
                        {
                            prev.FluentMethods = prev.FluentMethods.AddRange(next.FluentMethods);
                            return prev;
                        });
                    mergedStep.FluentMethods = [..MergeFluentMethods(mergedStep.FluentMethods)];

                    return mergedStep;
                })
                .ToImmutableArray();
    }

    private static IMethodSymbol NormalizedConverterMethod(IMethodSymbol converter, FluentMethod method)
    {

        var returnType = converter.ReturnType;
        var sourceParameter = method.SourceParameterSymbol;

        var mapping = TypeMapper.MapGenericArguments(returnType, sourceParameter!.Type);

        var normalizedMethod = converter.NormalizeMethodTypeParameters(mapping);

        return normalizedMethod;
    }

    private static IEnumerable<FluentMethod> MergeFluentMethods(IEnumerable<FluentMethod> fluentMethods)
    {
        return
        [
            ..fluentMethods
                .GroupBy(method => method, FluentMethod.FluentBuilderMethodComparer)
                .Select(group =>
                {
                    var method = group
                        .Aggregate((prev, next) =>
                        {
                            if (prev.ReturnStep is not null && next.ReturnStep is not null)
                            {
                                prev.ReturnStep!.FluentMethods =
                                    prev.ReturnStep.FluentMethods.AddRange(next.ReturnStep!.FluentMethods);
                            }

                            return prev;
                        });
                    if (method.ReturnStep is not null)
                    {
                        method.ReturnStep.FluentMethods = [..MergeFluentMethods(method.ReturnStep.FluentMethods)];
                    }

                    return method;
                })
        ];
    }

}
