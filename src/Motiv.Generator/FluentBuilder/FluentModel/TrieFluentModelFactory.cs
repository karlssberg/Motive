using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.Generation;
using Motiv.Generator.FluentBuilder.Generation.Shared;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public class TrieFluentModelFactory(Compilation compilation)
{
    public FluentFactoryCompilationUnit CreateFluentFactoryCompilationUnit(
        string fullname,
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        var usings = GetUsingStatements(fluentConstructorContexts);

        var stepTrie = CreateFluentStepTrie(fluentConstructorContexts);

        var rootFluentStep = ConvertNodeToFluentStep(stepTrie.Root);

        var childFluentSteps = rootFluentStep?.FluentMethods
            .Select(m => m.ReturnStep)
            .OfType<FluentStep>() ?? [];

        var fluentBuilderSteps =
            GetDescendentFluentSteps(childFluentSteps)
                .DistinctBy(step => step.KnownConstructorParameters)
                .Select((step, index) =>
                {
                    step.Name = $"Step_{index}__{fullname.Replace(".", "_")}";
                    return step;
                })
                .ToImmutableArray();

        var sampleConstructorContext = fluentConstructorContexts.First();
        return new FluentFactoryCompilationUnit(
            fullname,
            [..rootFluentStep.FluentMethods],
            fluentBuilderSteps,
            usings)
        {
            IsStatic = sampleConstructorContext.IsStatic,
            TypeKind = sampleConstructorContext.TypeKind,
            Accessibility = sampleConstructorContext.Accessibility,
            IsRecord = sampleConstructorContext.IsRecord
        };
    }

    private static IEnumerable<FluentStep> GetDescendentFluentSteps(IEnumerable<FluentStep> fluentSteps)
    {
        foreach (var fluentStep in fluentSteps)
        {
            yield return fluentStep;

            var childSteps = fluentStep.FluentMethods
                .Select(m => m.ReturnStep)
                .OfType<FluentStep>();

            foreach (var underlyingFluentStep in GetDescendentFluentSteps(childSteps))
                yield return underlyingFluentStep;
        }
    }

    private FluentStep? ConvertNodeToFluentStep(Trie<FluentParameter, ConstructorMetadata>.Node node)
    {
        FluentMethod[] fluentMethods =
        [
            ..ConvertNodeToFluentMethods(node),
            ..ConvertNodeToCreationMethods(node)
        ];

        if (fluentMethods.Length == 0)
            return null;

        var fluentStep = new FluentStep
        {
            Name = "Step",
            KnownConstructorParameters = new KnownConstructorParameters(node.Key),
            FluentMethods = fluentMethods
        };

        var methodReturnSteps = fluentMethods
            .Select(m => m.ReturnStep)
            .OfType<FluentStep>();

        foreach (var methodReturnStep in methodReturnSteps) methodReturnStep.Parent = fluentStep;

        return fluentStep;
    }

    private IEnumerable<FluentMethod> ConvertNodeToFluentMethods(Trie<FluentParameter, ConstructorMetadata>.Node node)
    {
        FluentMethod[] intermediateStepMethods =
        [
            ..
            from child in node.Children.Values
            let nextStep = ConvertNodeToFluentStep(child)
            let fluentParameters = child.EncounteredKeyParts
            select CreateFluentMethod(fluentParameters, nextStep, child.Values)
        ];
        var intermediateStepMethodsLookup = new HashSet<(string, ITypeSymbol)>(
            intermediateStepMethods
                .Select(m => m.FluentParameter)
                .OfType<FluentParameter>()
                .Select(p => (p.Name, p.ParameterSymbol.Type)));

        foreach (var intermediateStepMethod in intermediateStepMethods)
        {
            yield return intermediateStepMethod;

            var overloadedFluentMethods =
                from converter in intermediateStepMethod.ParameterConverters
                let normalizedConverter = NormalizedConverterMethod(converter, intermediateStepMethod)
                let fluentParameter = new FluentParameter(normalizedConverter.Parameters.First())
                where !intermediateStepMethodsLookup.Contains((intermediateStepMethod.MethodName, fluentParameter.ParameterSymbol.Type))
                select intermediateStepMethod with
                {
                    FluentParameter = fluentParameter,
                    ParameterConverter = normalizedConverter,
                    ReturnStep = intermediateStepMethod.ReturnStep
                };

            foreach (var overloadedFluentMethod in overloadedFluentMethods) yield return overloadedFluentMethod;
        }

        yield break;

        FluentMethod CreateFluentMethod(
            ICollection<FluentParameter> fluentParameterInstances,
            FluentStep? nextStep,
            IList<ConstructorMetadata> constructorMetadata)
        {

            var fluentParameter = fluentParameterInstances.First();
            var constructor = nextStep is null
                ? constructorMetadata.First().Constructor
                : null;

            if (nextStep is null)
            {
                return new FluentMethod(fluentParameter.Name, constructor)
                {
                    FluentParameter = new FluentParameter(fluentParameter.ParameterSymbol),
                    KnownConstructorParameters = new KnownConstructorParameters(node.Key.Select(p => p.ParameterSymbol)),
                    ParameterConverters =
                    [
                        ..fluentParameterInstances
                            .SelectMany(p => compilation.GetFluentMethodOverloads(p.ParameterSymbol))
                            .Distinct(SymbolEqualityComparer.Default)
                            .OfType<IMethodSymbol>()
                    ]
                };
            }

            return new FluentMethod(fluentParameter.Name, nextStep, constructor)
            {
                FluentParameter = new FluentParameter(fluentParameter.ParameterSymbol),
                KnownConstructorParameters = new KnownConstructorParameters(node.Key.Select(p => p.ParameterSymbol)),
                ParameterConverters =
                [
                    ..fluentParameterInstances
                        .SelectMany(p => compilation.GetFluentMethodOverloads(p.ParameterSymbol))
                        .Distinct(SymbolEqualityComparer.Default)
                        .OfType<IMethodSymbol>()
                ]
            };
        }
    }

    private IEnumerable<FluentMethod> ConvertNodeToCreationMethods(Trie<FluentParameter, ConstructorMetadata>.Node node)
    {
        if (!node.IsEnd) yield break;

        var createMethods =
            from value in node.Values
            where value.Constructor.Parameters.Length == node.Key.Length
            where !value.Options.HasFlag(FluentFactoryGeneratorOptions.NoCreateMethod)
            select new FluentMethod("Create", value.Constructor)
            {
                KnownConstructorParameters = new KnownConstructorParameters(value.Constructor.Parameters)
            };

        foreach (var createMethod in createMethods) yield return createMethod;
    }

    private static Trie<FluentParameter, ConstructorMetadata> CreateFluentStepTrie(
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        var trie = new Trie<FluentParameter, ConstructorMetadata>();
        foreach (var constructorContext in fluentConstructorContexts)
        {
            var fluentParameters = constructorContext
                .FluentMethodContexts
                .Select(ctx => new FluentParameter(ctx.SourceParameter))
                .ToImmutableArray();

            trie.Insert(
                fluentParameters,
                new ConstructorMetadata(
                    constructorContext.Constructor,
                    constructorContext.Options,
                    fluentParameters));
        }

        return trie;
    }

    private static IMethodSymbol NormalizedConverterMethod(IMethodSymbol converter, FluentMethod method)
    {
        var returnType = converter.ReturnType;
        var sourceParameter = method.SourceParameterSymbol;

        var mapping = TypeMapper.MapGenericArguments(returnType, sourceParameter!.Type);

        var normalizedMethod = converter.NormalizeMethodTypeParameters(mapping);

        return normalizedMethod;
    }

    private record ConstructorMetadata(
        IMethodSymbol Constructor,
        FluentFactoryGeneratorOptions Options,
        ImmutableArray<FluentParameter> FluentParameters)
    {
        public IMethodSymbol Constructor { get; } = Constructor;
        public FluentFactoryGeneratorOptions Options { get; } = Options;
        public ImmutableArray<FluentParameter> FluentParameters { get; } = FluentParameters;
    }

    private static ImmutableArray<INamespaceSymbol> GetUsingStatements(ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        return [
            ..fluentConstructorContexts
                .SelectMany(ctx => ctx.Constructor.Parameters)
                .Select(parameter => parameter.Type)
                .Select(type => type.ContainingNamespace)
                .Concat(fluentConstructorContexts.Select(ctx => ctx.Constructor.ContainingType.ContainingNamespace))
                .DistinctBy(ns => ns.ToDisplayString())
                .OrderBy(ns => ns.ToDisplayString())
        ];
    }
}
