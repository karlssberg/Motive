using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;


public static class FluentModelFactory
{
    public static FluentBuilderFile CreateFluentBuilderFile(string fullname, ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        ImmutableArray<INamespaceSymbol> usings =
            [
                ..fluentConstructorContexts
                    .SelectMany(ctx => ctx.Constructor.Parameters)
                    .Select(parameter => parameter.Type)
                    .Select(type => type.ContainingNamespace)
                    .Distinct(SymbolEqualityComparer.Default)
                    .OfType<INamespaceSymbol>()
            ];

        var initialFluentBuilderSteps = GetStartingFluentBuilderSteps(fluentConstructorContexts);

        var mergedFluentBuilderSteps = MergedFluentBuilderSteps(initialFluentBuilderSteps);

        var builderSteps = mergedFluentBuilderSteps
            .SelectMany(step => step.FluentMethods.Select(method => method.ReturnStep))
            .Where(step => step is not null)
            .ToImmutableArray();

        var fluentBuilderSteps = GetAllFluentSteps(builderSteps);

        foreach (var (step, index) in fluentBuilderSteps.Select((step, index) => (step, index)))
        {
            step.Name = $"Step_{index}";
        }

        var fluentBuilderMethods = mergedFluentBuilderSteps
            .SelectMany(step => step.FluentMethods)
            .ToImmutableArray();

        return new FluentBuilderFile(
            fullname,
            [..fluentBuilderMethods],
            fluentBuilderSteps,
            usings);
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
                    {
                        var fluentMethodName = method.SourceParameter.GetFluentMethodName();

                        var constructorParameters =
                            method.PriorSourceParameters
                                .Select(methodContext => methodContext.SourceParameter)
                                .ToImmutableArray();

                        var fluentBuilderStep = new FluentBuilderStep
                        {
                            ConstructorParameters = [..constructorParameters],
                            FluentMethods =
                            [
                                new FluentBuilderMethod(fluentMethodName, method.SourceParameter, previousStep)
                                {
                                    Constructor = constructorContext.Constructor,
                                    ConstructorParameters = constructorParameters,
                                }
                            ]
                        };

                        if (previousStep is not null)
                        {
                            previousStep.Parent = fluentBuilderStep;
                        }

                        return fluentBuilderStep;
                    }))
                .Where(step => step is not null)
        ];
    }

    private static ImmutableArray<FluentBuilderStep> MergedFluentBuilderSteps(ImmutableArray<FluentBuilderStep> fluentBuilderSteps)
    {
        return
        [
            ..fluentBuilderSteps
                .GroupBy(step => step, FluentBuilderStep.ConstructorParametersComparer)
                .Select(group =>
                {
                    var mergedStep = group
                        .Aggregate((prev, next) =>
                        {
                            prev.FluentMethods = prev.FluentMethods.AddRange(next.FluentMethods);
                            return prev;
                        });
                    mergedStep.FluentMethods = MergeFluentMethods(mergedStep.FluentMethods);

                    return mergedStep;
                })
        ];
    }

    private static ImmutableArray<FluentBuilderMethod> MergeFluentMethods(ImmutableArray<FluentBuilderMethod> fluentMethods)
    {
        return
        [
            ..fluentMethods
                .GroupBy(method => method, FluentBuilderMethod.ConstructorParametersComparer)
                .Select(group =>
                {
                    var method = group
                        .Aggregate((prev, next) =>
                        {
                            prev.ReturnStep!.FluentMethods = prev.ReturnStep.FluentMethods.AddRange(next.ReturnStep!.FluentMethods);
                            prev.ReturnStep = next.ReturnStep;
                            return prev;
                        });
                    if (method.ReturnStep is not null)
                    {
                        method.ReturnStep.FluentMethods = MergeFluentMethods(method.ReturnStep.FluentMethods);
                    }

                    return method;
                })
        ];
    }

}
