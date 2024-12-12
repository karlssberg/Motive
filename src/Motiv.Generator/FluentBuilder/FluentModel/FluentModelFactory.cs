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

        var mergedFluentBuilderSteps = MergedFluentBuilderSteps(initialFluentBuilderSteps)
            .ToImmutableArray();

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

        var sampleConstructorContext = fluentConstructorContexts.First();
        return new FluentBuilderFile(
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
                .Select(constructorContext =>
                {
                    return constructorContext.FluentMethodContexts
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
                                KnownConstructorParameters = [..constructorParameters],
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
                        });
                })
                .Where(step => step is not null)
        ];
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
