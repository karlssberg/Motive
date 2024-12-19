using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.Generation;

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
            .Distinct(FluentBuilderStep.ConstructorParametersComparer)
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

    private static ImmutableArray<FluentBuilderStep> GetAllFluentSteps(ImmutableArray<FluentBuilderStep> mergedFluentBuilderSteps)
    {
        IEnumerable<FluentBuilderStep> concatenated =
        [
            ..mergedFluentBuilderSteps,
            ..mergedFluentBuilderSteps.SelectMany(step =>
                GetAllFluentSteps([
                    ..step.FluentMethods
                        .Select(method => method.ReturnStep)
                        .Where(returnStep => returnStep is not null)
                ]))
        ];

        return [..MergedFluentSteps(concatenated)];
    }

    private static ImmutableArray<FluentBuilderStep> GetStartingFluentSteps(ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        return
        [
            ..fluentConstructorContexts
                .Select(constructorContext => constructorContext.FluentMethodContexts
                    .Reverse()
                    .Aggregate(default(FluentBuilderStep?), (previousStep, method) =>
                        CreateFluentBuilderStep(method, previousStep, constructorContext)))
                .Where(step => step is not null)
        ];
    }

    private static FluentBuilderStep? CreateFluentBuilderStep(
        FluentMethodContext method,
        FluentBuilderStep? nextStep,
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
                nextStep ?? MaybeCreateCreationMethodStep(nextStep, constructorContext))
            {
                SourceParameterSymbol = method.SourceParameter,
                Constructor = constructorContext.Constructor,
                KnownConstructorParameters = constructorParameters,
                ParameterConverters = method.ParameterConverters
            };

        var fluentBuilderStep = new FluentBuilderStep
        {
            KnownConstructorParameters = [..constructorParameters],
            FluentMethods = [fluentBuilderMethod]
        };

        if (fluentBuilderMethod.ReturnStep is not null)
            fluentBuilderMethod.ReturnStep.Parent = fluentBuilderStep;

        return fluentBuilderStep;
    }

    private static FluentBuilderStep? MaybeCreateCreationMethodStep(FluentBuilderStep? nextStep, FluentConstructorContext constructorContext)
    {
        if (constructorContext.Options.HasFlag(FluentFactoryGeneratorOptions.NoCreateMethod))
            return null;

        return new FluentBuilderStep
        {
            KnownConstructorParameters = [..constructorContext.Constructor.Parameters],
            FluentMethods =
            [
                new FluentMethod("Create", nextStep)
                {
                    Constructor = constructorContext.Constructor,
                    KnownConstructorParameters = constructorContext.Constructor.Parameters,
                }
            ]
        };
    }

    private static IEnumerable<FluentBuilderStep> MergedFluentSteps(IEnumerable<FluentBuilderStep> fluentBuilderSteps)
    {
        var groupedSteps = fluentBuilderSteps
            .Select(step =>
            {
                step.FluentMethods = [
                    ..step.FluentMethods.SelectMany<FluentMethod, FluentMethod>(method =>
                    [
                        method,
                        ..method.ParameterConverters
                            .Select<IMethodSymbol, FluentMethod>(converter => method with
                            {
                                SourceParameterSymbol = converter.Parameters.First(),
                                ParameterConverter = converter
                            })
                    ])
                ];
                return step;
            })
            .GroupBy(step => step, FluentBuilderStep.ConstructorParametersComparer);

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
                });
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
