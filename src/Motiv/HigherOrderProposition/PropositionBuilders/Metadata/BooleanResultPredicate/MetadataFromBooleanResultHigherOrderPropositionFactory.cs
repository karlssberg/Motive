using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.BooleanResultPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate;

/// <summary>
/// A factory for creating propositions based on a predicate and metadata factories. This is particularly useful
/// for handling edge-case scenarios where it would be impossible or impractical to create a proposition that covers
/// every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TReplacementMetadata">The type of the metadata associated with the specification.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the specification.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(
    [FluentMethod("Build")]Func<TModel, BooleanResultBase<TMetadata>> resultResolver,
    [MultipleFluentMethods(typeof(HigherOrderPredicateSpecMethods))]HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation,
    [FluentMethod("WhenTrue", Overloads = typeof(AllConverters))]Func<HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue,
    [FluentMethod("WhenFalse", Overloads = typeof(AllConverters))]Func<HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
{
    /// <summary>Creates a specification and names it with the propositional statement provided.</summary>
    /// <param name="statement">The proposition statement of what the specification represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A specification for the model.</returns>
    public PolicyBase<IEnumerable<TModel>, TReplacementMetadata> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new HigherOrderFromBooleanResultMetadataProposition<TModel, TReplacementMetadata, TMetadata>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            whenTrue,

            whenFalse,
            new SpecDescription(statement),
            higherOrderOperation.CauseSelector);
    }
}
