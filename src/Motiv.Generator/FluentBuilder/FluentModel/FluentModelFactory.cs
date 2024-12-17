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

        var initialFluentBuilderSteps = GetStartingFluentBuilderSteps(fluentConstructorContexts);

        var mergedFluentBuilderSteps = MergedFluentBuilderSteps(initialFluentBuilderSteps)
            .ToImmutableArray();

        var builderSteps = mergedFluentBuilderSteps
            .SelectMany(step => step.FluentMethods.Select(method => method.ReturnStep))
            .Where(step => step is not null)
            .ToImmutableArray();

        var fluentBuilderSteps = GetAllFluentSteps(builderSteps);

        foreach (var (step, index) in fluentBuilderSteps.Select((step, index) => (step, index)))
        {
            step.Name = $"Step_{index}__{fullname.Replace(".", "_")}";
        }

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

    private static ImmutableArray<FluentBuilderStep> GetAllFluentSteps(ImmutableArray<FluentBuilderStep?> mergedFluentBuilderSteps)
    {
        return
        [
            ..mergedFluentBuilderSteps,
            ..mergedFluentBuilderSteps.SelectMany(step =>
                GetAllFluentSteps([
                    ..step.FluentMethods
                        .Select(method => method.ReturnStep)
                        .Where(returnStep => returnStep is not null)
                ]))
        ];
    }

    private static ImmutableArray<FluentBuilderStep> GetStartingFluentBuilderSteps(ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
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
            method.PriorSourceParameters
                .Select(methodContext => methodContext.SourceParameter)
                .ToImmutableArray();

        var fluentBuilderMethod =
            new FluentBuilderMethod(
                fluentMethodName,
                nextStep ?? MaybeCreateCreationMethodStep(nextStep, constructorContext))
            {
                SourceParameterSymbol = method.SourceParameter,
                Constructor = constructorContext.Constructor,
                KnownConstructorParameters = constructorParameters,
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
                new FluentBuilderMethod("Create", nextStep)
                {
                    Constructor = constructorContext.Constructor,
                    KnownConstructorParameters = constructorContext.Constructor.Parameters,
                }
            ]
        };
    }

    private static IEnumerable<FluentBuilderStep> MergedFluentBuilderSteps(IEnumerable<FluentBuilderStep> fluentBuilderSteps)
    {
        var groupedSteps = fluentBuilderSteps
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

    private static IEnumerable<FluentBuilderMethod> MergeFluentMethods(IEnumerable<FluentBuilderMethod> fluentMethods)
    {
        return
        [
            ..fluentMethods
                .GroupBy(method => method, FluentBuilderMethod.FluentBuilderMethodComparer)
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
