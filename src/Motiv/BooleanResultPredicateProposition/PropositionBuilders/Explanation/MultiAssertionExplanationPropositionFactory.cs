using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.BooleanResultPredicateProposition.PropositionBuilders.Explanation;

/// <summary>
/// A factory for creating propositions based on the supplied proposition and explanation factories.
/// This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create a proposition that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the proposition.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(
    [MultipleFluentMethods(typeof(BooleanResultBuildOverloads))]Func<TModel, BooleanResultBase<TMetadata>> predicate,
    [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, BooleanResultBase<TMetadata>, IEnumerable<string>> trueBecause,
    [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, BooleanResultBase<TMetadata>, IEnumerable<string>> falseBecause)
{
    /// <summary>
    /// A factory for creating propositions based on the supplied proposition and explanation factories.
    /// </summary>
    /// <param name="predicate">The predicate to evaluate.</param>
    /// <param name="trueBecause">The explanations for when the predicate is true.</param>
    /// <param name="falseBecause">The explanation for when the predicate is false.</param>
    [FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiAssertionExplanationPropositionFactory(
        [FluentMethod("Build")]Func<TModel, BooleanResultBase<TMetadata>> predicate,
        [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, BooleanResultBase<TMetadata>, IEnumerable<string>> trueBecause,
        [FluentMethod("WhenFalse", Overloads = typeof(WhenOverloads))]Func<TModel, BooleanResultBase<TMetadata>, string> falseBecause)
        : this(predicate, trueBecause, falseBecause.ToEnumerableReturn())
    {
    }

    /// <summary>
    /// A factory for creating propositions based on the supplied proposition and explanation factories.
    /// </summary>
    /// <param name="predicate">The predicate to evaluate.</param>
    /// <param name="trueBecause">The explanation for when the predicate is true.</param>
    /// <param name="falseBecause">The explanations for when the predicate is false.</param>
    [FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiAssertionExplanationPropositionFactory(
        [FluentMethod("Build")]Func<TModel, BooleanResultBase<TMetadata>> predicate,
        [FluentMethod("WhenTrue", Overloads = typeof(WhenOverloads))]Func<TModel, BooleanResultBase<TMetadata>, string> trueBecause,
        [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, BooleanResultBase<TMetadata>, IEnumerable<string>> falseBecause)
        : this(predicate, trueBecause.ToEnumerableReturn(), falseBecause)
    {
    }

    /// <summary>
    /// Creates a proposition and names it with the propositional statement provided.
    /// </summary>
    /// <param name="statement">The proposition statement of what the proposition represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A proposition for the model.</returns>
    public SpecBase<TModel, string> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new BooleanResultPredicateMultiMetadataProposition<TModel, string, TMetadata>(
            predicate,
            trueBecause,
            falseBecause,
            new SpecDescription(statement)
        );
    }
}
