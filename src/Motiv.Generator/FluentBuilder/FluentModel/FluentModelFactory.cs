using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.Generation;
using Motiv.Generator.FluentBuilder.Generation.Shared;
using static Motiv.Generator.FluentBuilder.FluentFactoryGeneratorOptions;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public class FluentModelFactory(Compilation compilation)
{

    public FluentFactoryCompilationUnit CreateFluentFactoryCompilationUnit(
        INamedTypeSymbol rootType,
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        var usings = GetUsingStatements(fluentConstructorContexts);

        var stepTrie = CreateFluentStepTrie(fluentConstructorContexts);

        var rootFluentStep = ConvertNodeToFluentStep(rootType, stepTrie.Root);

        var childFluentSteps = rootFluentStep?.FluentMethods
            .Select(m => m.ReturnStep)
            .OfType<FluentStep>() ?? [];

        var descendentFluentSteps = GetDescendentFluentSteps(childFluentSteps);
        var fluentBuilderSteps = descendentFluentSteps
                .DistinctBy(step => step.KnownConstructorParameters)
                .Select((step, index) =>
                {
                    if (step.IsExistingPartialType) return step;

                    var name = rootType.ToIdentifier();
                    step.Name = $"Step_{index}__{name}";

                    return step;
                })
                .ToImmutableArray();

        var sampleConstructorContext = fluentConstructorContexts.First();
        return new FluentFactoryCompilationUnit(
            rootType,
            [..rootFluentStep?.FluentMethods ?? []],
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

    private FluentStep? ConvertNodeToFluentStep(
        INamedTypeSymbol rootType,
        Trie<FluentParameter, ConstructorMetadata>.Node node)
    {
        FluentMethod[] fluentMethods =
        [
            ..ConvertNodeToFluentMethods(rootType, node),
            ..ConvertNodeToCreationMethods(rootType, node)
        ];

        if (fluentMethods.Length == 0)
            return null;

        var metadata = node.EndValues.FirstOrDefault();
        var useExistingTypeAsStep = UseExistingTypeAsStep();

        var fluentStep = new FluentStep(rootType)
        {
            Name = ResolveStepName(),
            KnownConstructorParameters = new ParameterSequence(node.Key),
            FluentMethods = fluentMethods,
            Accessibility = metadata?.Constructor.ContainingType.DeclaredAccessibility ?? Accessibility.Public,
            TypeKind = metadata?.Constructor.ContainingType.TypeKind ?? TypeKind.Struct,
            IsEndStep = node.IsEnd,
            IsRecord = metadata?.Constructor.ContainingType.IsRecord ?? false,
            IsExistingPartialType = useExistingTypeAsStep,
            ParameterStoreMembers = metadata?.ParameterStoreMembers ?? new Dictionary<IParameterSymbol, FluentParameterResolution>(FluentParameterComparer.Default),
            ExistingStepConstructor = useExistingTypeAsStep
                ? metadata?.Constructor
                : null
        };

        return fluentStep;

        string ResolveStepName()
        {
            return metadata is not null && useExistingTypeAsStep
                ? metadata.Constructor.ContainingType.ToDisplayString()
                : "Step";
        }

        bool UseExistingTypeAsStep()
        {
            if (metadata is null) return false;

            var containingType = metadata.Constructor.ContainingType;
            var isPartial = containingType.IsPartial();
            var isStatic = containingType.IsStatic;
            var doNotGenerateCreateMethod = metadata.Options.HasFlag(NoCreateMethod);

            // it is necessary for the target type to be partial and instantiatable.  To is so avoid hiding other
            // constructors that begin with the same build steps, but have additional steps
            return isPartial && !isStatic && doNotGenerateCreateMethod;
        }
    }

    private IEnumerable<FluentMethod> ConvertNodeToFluentMethods(
        INamedTypeSymbol rootType,
        Trie<FluentParameter, ConstructorMetadata>.Node node)
    {
        FluentMethod[] intermediateStepMethods =
        [
            ..
            from child in node.Children.Values
            let nextStep = ConvertNodeToFluentStep(rootType, child)
            let fluentParameters = child.EncounteredKeyParts
            from intermediateStepMethod in CreateIntermediateStepMethod(rootType, node, fluentParameters, nextStep, child.Values)
            select intermediateStepMethod
        ];

        foreach (var intermediateStepMethodGroup in intermediateStepMethods.GroupBy(m => m, FluentMethod.FluentMethodSignatureComparer))
        {
            var intermediateStepMethod = intermediateStepMethodGroup.FirstOrDefault(m => m.ParameterConverter is null)
                                         ?? intermediateStepMethodGroup.First();
            yield return intermediateStepMethod;

            var overloadedFluentMethods = CreateOverloadedFluentMethods(intermediateStepMethod);

            foreach (var overloadedFluentMethod in overloadedFluentMethods) yield return overloadedFluentMethod;
        }

        yield break;

        bool HasMethodSignatureClash(ImmutableArray<FluentParameter> fluentParameters)
        {
            return intermediateStepMethods
                .Any(m =>
                {
                    var existingParameterSequence = m.FluentParameters.Select(mp => mp.ParameterSymbol.Type);
                    var overloadedParameterSequence = fluentParameters.Select(p => p.ParameterSymbol.Type);

                    return existingParameterSequence.SequenceEqual(overloadedParameterSequence, SymbolEqualityComparer.Default);
                });
        }

        IEnumerable<FluentMethod> CreateOverloadedFluentMethods(FluentMethod intermediateStepMethod)
        {
            return from converter in intermediateStepMethod.ParameterConverters
                let normalizedConverter = NormalizedConverterMethod(converter, intermediateStepMethod.SourceParameterSymbol!.Type)
                let fluentParameters = normalizedConverter.Parameters.Select(p => new FluentParameter(p)).ToImmutableArray()
                where !HasMethodSignatureClash(fluentParameters)
                select new FluentMethod(
                    intermediateStepMethod.MethodName,
                    intermediateStepMethod.ReturnStep,
                    intermediateStepMethod.RootNamespace,
                    intermediateStepMethod.TargetConstructor,
                    normalizedConverter)
                {
                    FluentParameters = fluentParameters,
                    ReturnStep = intermediateStepMethod.ReturnStep,
                    ParameterConverters = intermediateStepMethod.ParameterConverters,
                    KnownConstructorParameters = intermediateStepMethod.KnownConstructorParameters
                };
        }
    }

    private IEnumerable<FluentMethod> ConvertNodeToCreationMethods(
        INamedTypeSymbol rootType,
        Trie<FluentParameter, ConstructorMetadata>.Node node)
    {
        if (!node.IsEnd) yield break;

        var createMethods =
            from value in node.Values
            where value.Constructor.Parameters.Length == node.Key.Length
            where !value.Options.HasFlag(NoCreateMethod)
            select new FluentMethod("Create", rootType.ContainingNamespace, value.Constructor)
            {
                KnownConstructorParameters = new ParameterSequence(value.Constructor.Parameters)
            };

        foreach (var createMethod in createMethods) yield return createMethod;
    }

    private IEnumerable<FluentMethod> CreateIntermediateStepMethod(
        INamedTypeSymbol rootType,
        Trie<FluentParameter, ConstructorMetadata>.Node node,
        ICollection<FluentParameter> fluentParameterInstances,
        FluentStep? nextStep,
        IList<ConstructorMetadata> constructorMetadataList)
    {
        var constructorMetadata = MergeConstructorMetadata(node, constructorMetadataList);
        var constructor = nextStep is null
            ? constructorMetadata.Constructor
            : null;

        var fluentParameter = fluentParameterInstances.First();
        var multipleFluentMethodSymbols = compilation
            .GetMultipleFluentMethodSymbols(fluentParameter.ParameterSymbol)
            .Select(method => NormalizedConverterMethod(method, fluentParameter.ParameterSymbol.Type))
            .ToArray();

        if (multipleFluentMethodSymbols.Length > 0)
        {
            foreach (var fluentMethodSymbol in multipleFluentMethodSymbols)
            {
                yield return new FluentMethod(fluentMethodSymbol.Name, nextStep, rootType.ContainingNamespace, constructor, fluentMethodSymbol)
                {
                    FluentParameters = [..fluentMethodSymbol.Parameters.Select(p => new FluentParameter(p))],
                    KnownConstructorParameters = CreateKnownConstructorParameters()
                };
            }
            yield break;
        }

        if (nextStep is null)
        {
            yield return new FluentMethod(fluentParameter.Name, rootType.ContainingNamespace, constructor)
            {
                FluentParameters = [new FluentParameter(fluentParameter.ParameterSymbol)],
                KnownConstructorParameters = CreateKnownConstructorParameters(),
                ParameterConverters = CreateParameterConverters()
            };
            yield break;
        }

        yield return new FluentMethod(fluentParameter.Name,  nextStep, rootType.ContainingNamespace,  constructor)
        {
            FluentParameters = [new FluentParameter(fluentParameter.ParameterSymbol)],
            KnownConstructorParameters = CreateKnownConstructorParameters(),
            ParameterConverters = CreateParameterConverters()
        };

        yield break;

        ImmutableArray<IMethodSymbol> CreateParameterConverters()
        {
            ImmutableArray<IMethodSymbol> immutableArray =
            [
                ..fluentParameterInstances
                    .SelectMany(p => compilation.GetFluentMethodOverloads(p.ParameterSymbol))
                    .Distinct(SymbolEqualityComparer.Default)
                    .OfType<IMethodSymbol>()
            ];
            return immutableArray;
        }

        ParameterSequence CreateKnownConstructorParameters()
        {
            return new ParameterSequence(node.Key.Select(p => p.ParameterSymbol));
        }
    }

    private static ConstructorMetadata MergeConstructorMetadata(Trie<FluentParameter, ConstructorMetadata>.Node node, IList<ConstructorMetadata> constructorMetadataList)
    {
        return constructorMetadataList.Skip(1).Aggregate(constructorMetadataList.First().Clone(), (merged, metadata) =>
        {
            merged.Options |= metadata.Options;
            if (metadata.Constructor.Parameters.Length - 1 != node.Key.Length)
                return merged;

            merged.Constructor = metadata.Constructor;
            var mergeableConstructors = metadata.CandidateConstructors
                .Except(merged.CandidateConstructors, SymbolEqualityComparer.Default)
                .OfType<IMethodSymbol>();

            merged.CandidateConstructors.AddRange(mergeableConstructors);

            return merged;
        });
    }

    private Trie<FluentParameter, ConstructorMetadata> CreateFluentStepTrie(
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        var trie = new Trie<FluentParameter, ConstructorMetadata>();
        foreach (var constructorContext in fluentConstructorContexts)
        {
            var fluentParameters =
                constructorContext.Constructor.Parameters
                    .Select(parameter => new FluentParameter(parameter))
                    .ToImmutableArray();

            trie.Insert(
                fluentParameters,
                new ConstructorMetadata(constructorContext));
        }

        return trie;
    }

    private static IMethodSymbol NormalizedConverterMethod(IMethodSymbol converter, ITypeSymbol targetType)
    {
        var returnType = converter.ReturnType;;

        var mapping = TypeMapper.MapGenericArguments(returnType, targetType);

        var normalizedMethod = converter.NormalizeMethodTypeParameters(mapping);

        return normalizedMethod;
    }

    private class ConstructorMetadata(FluentConstructorContext constructorContext)
    {
        public IMethodSymbol Constructor { get; set; } = constructorContext.Constructor;

        public IList<IMethodSymbol> CandidateConstructors { get; set; } = [constructorContext.Constructor];

        public FluentFactoryGeneratorOptions Options { get; set;  } = constructorContext.Options;
        public IReadOnlyDictionary<IParameterSymbol, FluentParameterResolution> ParameterStoreMembers { get; set; } = constructorContext.ParameterStores;

        public INamedTypeSymbol RootType { get; } = constructorContext.RootType;

        public ConstructorMetadata Clone()
        {
            return new ConstructorMetadata(constructorContext);
        }
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
