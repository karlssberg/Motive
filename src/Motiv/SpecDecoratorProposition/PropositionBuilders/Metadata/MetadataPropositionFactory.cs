using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.SpecDecoratorProposition.PropositionBuilders.Metadata;

/// <summary>
/// A factory for creating propositions based on the supplied proposition and metadata factories.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TReplacementMetadata">The type of the metadata associated with the proposition.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the proposition.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(
    [FluentMethod("Build", Overloads = typeof(BuildOverloads))]SpecBase<TModel, TMetadata> spec,
    [FluentMethod("WhenTrue", Overloads = typeof(WhenOverloads))]Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> whenTrue,
    [FluentMethod("WhenFalse", Overloads = typeof(WhenOverloads))]Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> whenFalse)
{
    /// <summary>Creates a proposition and names it with the propositional statement provided.</summary>
    /// <param name="statement">The proposition statement of what the proposition represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A proposition for the model.</returns>
    public PolicyBase<TModel, TReplacementMetadata> Create(string statement) =>
        new SpecDecoratorMetadataProposition<TModel, TReplacementMetadata, TMetadata>(
            spec,
            whenTrue,
            whenFalse,
            new SpecDescription(statement.ThrowIfNullOrWhitespace(nameof(statement)), spec.Description));
}
