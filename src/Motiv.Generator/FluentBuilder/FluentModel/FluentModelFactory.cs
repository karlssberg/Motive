using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.FluentModel.Methods;
using Motiv.Generator.FluentBuilder.FluentModel.Steps;
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

        var fluentRootMethods = ConvertNodeToFluentFluentMethods(rootType, stepTrie.Root);

        var childFluentSteps = fluentRootMethods
            .Select(m => m.Return)
            .OfType<IFluentStep>();

        var descendentFluentSteps = GetDescendentFluentSteps(childFluentSteps);
        var fluentBuilderSteps = descendentFluentSteps
            .DistinctBy(step => step.KnownConstructorParameters)
            .Select((step, index) =>
            {
                if (step is not RegularFluentStep) return step;

                var name = rootType.ToIdentifier();
                step.Name = $"Step_{index}__{name}";

                return step;
            })
            .ToImmutableArray();

        var sampleConstructorContext = fluentConstructorContexts.First();
        return new FluentFactoryCompilationUnit(
            rootType,
            fluentRootMethods,
            fluentBuilderSteps,
            usings)
        {
            IsStatic = sampleConstructorContext.IsStatic,
            TypeKind = sampleConstructorContext.TypeKind,
            Accessibility = sampleConstructorContext.Accessibility,
            IsRecord = sampleConstructorContext.IsRecord
        };
    }

    private ImmutableArray<IFluentMethod> ConvertNodeToFluentFluentMethods(
        INamedTypeSymbol type,
        Trie<FluentMethodParameter, ConstructorMetadata>.Node node)
    {
        ImmutableArray<IFluentMethod> fluentMethods =
        [
            ..ConvertNodeToFluentMethods(type, node),
            ..ConvertNodeToCreationMethods(type, node)
        ];

        return fluentMethods;
    }

    private IEnumerable<IFluentMethod> ConvertNodeToFluentMethods(
        INamedTypeSymbol rootType,
        Trie<FluentMethodParameter, ConstructorMetadata>.Node node)
    {
        IFluentMethod[] fluentMethods =
        [
            ..
            from child in node.Children.Values
            let nextStep = ConvertNodeToFluentStep(rootType, child)
            let fluentParameters = child.EncounteredKeyParts
            from fluentMethod in CreateFluentMethods(rootType, node, fluentParameters, nextStep, child.Values)
            select fluentMethod
        ];

        var fluentMethodGroups = fluentMethods.GroupBy(m => m, FluentMethodSignatureEqualityComparer.Default);

        foreach (var fluentMethodGroup in fluentMethodGroups)
        {
            var nonRegularMethod = fluentMethodGroup.OfType<RegularMethod>().FirstOrDefault();

            yield return nonRegularMethod ?? fluentMethodGroup.First();
        }
    }

    private IFluentStep? ConvertNodeToFluentStep(
        INamedTypeSymbol rootType,
        Trie<FluentMethodParameter, ConstructorMetadata>.Node node)
    {
        var fluentMethods = ConvertNodeToFluentFluentMethods(rootType, node);
        if (fluentMethods.Length == 0)
            return null;

        var constructorMetadata = node.EndValues.FirstOrDefault();
        var useExistingTypeAsStep = UseExistingTypeAsStep();

        return (useExistingTypeAsStep, constructorMetadata) switch
        {
            (true, { } metadata) =>
                new ExistingTypeFluentStep(rootType, metadata.Constructor)
                {
                    Name = constructorMetadata.Constructor.ContainingType.ToDisplayString(),
                    KnownConstructorParameters = new ParameterSequence(node.Key),
                    FluentMethods = fluentMethods,
                    Accessibility = metadata.Constructor.ContainingType.DeclaredAccessibility,
                    TypeKind = metadata.Constructor.ContainingType.TypeKind,
                    IsRecord = metadata.Constructor.ContainingType.IsRecord,
                    ParameterStoreMembers = metadata.ParameterStoreMembers
                },
            _ =>
                new RegularFluentStep(rootType)
                {
                    Name = "Step",
                    KnownConstructorParameters = new ParameterSequence(node.Key),
                    FluentMethods = fluentMethods,
                    IsEndStep = node.IsEnd
                }
        };

        bool UseExistingTypeAsStep()
        {
            if (constructorMetadata is null) return false;

            var containingType = constructorMetadata.Constructor.ContainingType;
            var isPartial = containingType.IsPartial();
            var isStatic = containingType.IsStatic;
            var doNotGenerateCreateMethod = constructorMetadata.Options.HasFlag(NoCreateMethod);

            // it is necessary for the target type to be partial and instantiatable.  To is so avoid hiding other
            // constructors that begin with the same build steps, but have additional steps
            return isPartial && !isStatic && doNotGenerateCreateMethod;
        }
    }

    private IEnumerable<IFluentMethod> CreateFluentMethods(
        INamedTypeSymbol rootType,
        Trie<FluentMethodParameter, ConstructorMetadata>.Node node,
        ICollection<FluentMethodParameter> fluentParameterInstances,
        IFluentStep? nextStep,
        IList<ConstructorMetadata> constructorMetadataList)
    {
        var constructorMetadata = MergeConstructorMetadata(node, constructorMetadataList);
        IFluentReturn methodReturn = nextStep switch
        {
            null => new TargetTypeReturn(
                constructorMetadata.Constructor,
                new ParameterSequence(node.Key.Select(p => p.ParameterSymbol))),
            _ => nextStep
        };

        foreach (var parameter in fluentParameterInstances)
        {
            var multipleFluentMethodSymbols = compilation
                .GetMultipleFluentMethodSymbols(parameter.ParameterSymbol)
                .Select(method => NormalizedConverterMethod(method, parameter.ParameterSymbol.Type));

            foreach (var multipleFluentMethodSymbol in multipleFluentMethodSymbols)
            {
                yield return new MultiMethod(
                    multipleFluentMethodSymbol.Name,
                    parameter.ParameterSymbol,
                    methodReturn,
                    rootType.ContainingNamespace,
                    multipleFluentMethodSymbol,
                    node.Key);
            }

            var hasMultipleFluentMethodsAttribute = parameter.ParameterSymbol
                .GetAttribute(TypeName.MultipleFluentMethodsAttribute) != null;

            var hasFluentMethodAttribute = parameter.ParameterSymbol
                .GetAttribute(TypeName.FluentMethodAttribute) != null;

            if (!hasFluentMethodAttribute && hasMultipleFluentMethodsAttribute) continue;

            var fluentParameter = fluentParameterInstances.First();
            foreach (var name in fluentParameter.Names)
            {
                yield return new RegularMethod(
                    name,
                    fluentParameter.ParameterSymbol,
                    methodReturn,
                    rootType.ContainingNamespace,
                    node.Key);
            }
        }
    }

    private static IEnumerable<IFluentStep> GetDescendentFluentSteps(IEnumerable<IFluentStep> fluentSteps)
    {
        foreach (var fluentStep in fluentSteps)
        {
            yield return fluentStep;

            var childSteps = fluentStep.FluentMethods
                .Select(m => m.Return)
                .OfType<IFluentStep>();

            foreach (var underlyingFluentStep in GetDescendentFluentSteps(childSteps))
                yield return underlyingFluentStep;
        }
    }

    private static ConstructorMetadata MergeConstructorMetadata(
        Trie<FluentMethodParameter, ConstructorMetadata>.Node node, IList<ConstructorMetadata> constructorMetadataList)
    {
        return constructorMetadataList.Skip(1).Aggregate(constructorMetadataList.First().Clone(), (merged, metadata) =>
        {
            merged.Options |= metadata.Options;
            if (metadata.Constructor.Parameters.Length - 1 != node.Key.Length)
                return merged;

            merged.Constructor = metadata.Constructor;
            var mergeableConstructors = metadata.CandidateConstructors
                .Except<IMethodSymbol>(merged.CandidateConstructors, SymbolEqualityComparer.Default);

            merged.CandidateConstructors.AddRange(mergeableConstructors);

            return merged;
        });
    }

    private IEnumerable<IFluentMethod> ConvertNodeToCreationMethods(
        INamedTypeSymbol rootType,
        Trie<FluentMethodParameter, ConstructorMetadata>.Node node)
    {
        if (!node.IsEnd) yield break;

        var createMethods =
            from value in node.Values
            where value.Constructor.Parameters.Length == node.Key.Length
            where !value.Options.HasFlag(NoCreateMethod)
            select new CreateMethod(
                rootType.ContainingNamespace,
                value.Constructor,
                node.Key,
                compilation);

        foreach (var createMethod in createMethods) yield return createMethod;
    }

    private Trie<FluentMethodParameter, ConstructorMetadata> CreateFluentStepTrie(
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        var trie = new Trie<FluentMethodParameter, ConstructorMetadata>();
        foreach (var constructorContext in fluentConstructorContexts)
        {
            var fluentParameters =
                constructorContext.Constructor.Parameters
                    .Select(parameter =>
                    {
                        var methodNames = compilation
                            .GetMultipleFluentMethodSymbols(parameter)
                            .Select(symbol => symbol.Name)
                            .DefaultIfEmpty(parameter.GetFluentMethodName());

                        return new FluentMethodParameter(parameter, methodNames);
                    });


            trie.Insert(
                fluentParameters,
                new ConstructorMetadata(constructorContext));
        }

        return trie;
    }

    private static IMethodSymbol NormalizedConverterMethod(IMethodSymbol converter, ITypeSymbol targetType)
    {
        var mapping = TypeMapper.MapGenericArguments(converter.ReturnType, targetType);

        return converter.NormalizeMethodTypeParameters(mapping);
    }

    private static ImmutableArray<INamespaceSymbol> GetUsingStatements(
        ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        return
        [
            ..fluentConstructorContexts
                .SelectMany(ctx => ctx.Constructor.Parameters)
                .Select(parameter => parameter.Type)
                .Select(type => type.ContainingNamespace)
                .Concat(fluentConstructorContexts.Select(ctx => ctx.Constructor.ContainingType.ContainingNamespace))
                .DistinctBy(ns => ns.ToDisplayString())
                .OrderBy(ns => ns.ToDisplayString())
        ];
    }

    private class ConstructorMetadata(FluentConstructorContext constructorContext)
    {
        public IMethodSymbol Constructor { get; set; } = constructorContext.Constructor;

        public IList<IMethodSymbol> CandidateConstructors { get; } = [constructorContext.Constructor];

        public FluentFactoryGeneratorOptions Options { get; set; } = constructorContext.Options;

        public IReadOnlyDictionary<IParameterSymbol, FluentParameterResolution> ParameterStoreMembers { get; } =
            constructorContext.ParameterStores;

        public ConstructorMetadata Clone()
        {
            return new ConstructorMetadata(constructorContext);
        }
    }
}
