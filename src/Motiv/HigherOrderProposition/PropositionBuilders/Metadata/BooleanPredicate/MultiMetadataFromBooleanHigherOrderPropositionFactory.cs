using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.BooleanPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate;

/// <summary>
/// A factory for creating propositions based on a predicate and metadata factories. This is particularly useful
/// for handling edge-case scenarios where it would be impossible or impractical to create a proposition that covers
/// every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the metadata associated with the specification.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(
    [FluentMethod("Build")]Func<TModel, bool> resultResolver,
    [MultipleFluentMethods(typeof(HigherOrderBooleanPredicateSpecMethods))]HigherOrderSpecBooleanPredicateOperation<TModel> higherOrderOperation,
    [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderBooleanEvaluation<TModel>, IEnumerable<TMetadata>> whenTrue,
    [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderBooleanEvaluation<TModel>, IEnumerable<TMetadata>> whenFalse)
{
    /// <summary>
    /// Creates a specification with metadata for when the condition is true or false, and names it with the propositional statement provided.
    /// </summary>
    /// <param name="resultResolver">The predicate to evaluate.</param>
    /// <param name="higherOrderOperation">The higher-order operation to perform.</param>
    /// <param name="whenTrue">The metadata for when the predicate is true.</param>
    /// <param name="whenFalse">The metadata for when the predicate is false.</param>
    [FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiMetadataFromBooleanHigherOrderPropositionFactory(
        [FluentMethod("Build")]Func<TModel, bool> resultResolver,
        [MultipleFluentMethods(typeof(HigherOrderBooleanPredicateSpecMethods))]HigherOrderSpecBooleanPredicateOperation<TModel> higherOrderOperation,
        [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderBooleanEvaluation<TModel>, IEnumerable<TMetadata>> whenTrue,
        [FluentMethod("WhenFalse", Overloads = typeof(WhenOverloads))]Func<HigherOrderBooleanEvaluation<TModel>, TMetadata> whenFalse)
        : this(resultResolver, higherOrderOperation, whenTrue, whenFalse.ToEnumerableReturn())
    {
    }

    /// <summary>
    /// Creates a specification with metadata for when the condition is true or false, and names it with the propositional statement provided.
    /// </summary>
    /// <param name="resultResolver">The predicate to evaluate.</param>
    /// <param name="higherOrderOperation">The higher-order operation to perform.</param>
    /// <param name="whenTrue">The metadata for when the predicate is true.</param>
    /// <param name="whenFalse">The metadata for when the predicate is false.</param>
    [FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiMetadataFromBooleanHigherOrderPropositionFactory(
        [FluentMethod("Build")]Func<TModel, bool> resultResolver,
        [MultipleFluentMethods(typeof(HigherOrderBooleanPredicateSpecMethods))]HigherOrderSpecBooleanPredicateOperation<TModel> higherOrderOperation,
        [FluentMethod("WhenTrue", Overloads = typeof(WhenOverloads))]Func<HigherOrderBooleanEvaluation<TModel>, TMetadata> whenTrue,
        [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderBooleanEvaluation<TModel>, IEnumerable<TMetadata>> whenFalse)
        : this(resultResolver, higherOrderOperation, whenTrue.ToEnumerableReturn(), whenFalse)
    {
    }

    /// <summary>Creates a specification and names it with the propositional statement provided.</summary>
    /// <param name="statement">The proposition statement of what the specification represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A specification for the model.</returns>
    public SpecBase<IEnumerable<TModel>, TMetadata> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new HigherOrderFromBooleanPredicateMultiMetadataProposition<TModel,TMetadata>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            whenTrue,
            whenFalse,
            new SpecDescription(statement),
            higherOrderOperation.CauseSelector);
    }
}
