using System.Linq.Expressions;
using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.ExpressionTreeProposition.PropositionBuilders.Metadata;

/// <summary>
/// A factory for creating propositions based on the supplied proposition and metadata factories.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the metadata associated with the proposition.</typeparam>
/// <typeparam name="TPredicateResult">The return type of the predicate expression.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(
    [FluentMethod("From")]Expression<Func<TModel, TPredicateResult>> expression,
    [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, BooleanResultBase<string>, IEnumerable<TMetadata>> whenTrue,
    [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, BooleanResultBase<string>, IEnumerable<TMetadata>> whenFalse)
{
    /// <summary>
    /// A factory for creating propositions based on the supplied proposition and metadata factories.
    /// </summary>
    /// <param name="expression">The predicate expression.</param>
    /// <param name="whenTrue">The function that returns the metadata when the predicate is true.</param>
    /// <param name="whenFalse">The function that returns the metadata when the predicate is false.</param>
    [FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiMetadataPropositionExpressionTreeFactory(
        [FluentMethod("From")]Expression<Func<TModel, TPredicateResult>> expression,
        [FluentMethod("WhenTrue", Overloads = typeof(WhenOverloads))]Func<TModel, BooleanResultBase<string>, TMetadata> whenTrue,
        [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, BooleanResultBase<string>, IEnumerable<TMetadata>> whenFalse)
        : this(expression, whenTrue.ToEnumerableReturn(), whenFalse)
    {
    }

    /// <summary>
    /// A factory for creating propositions based on the supplied proposition and metadata factories.
    /// </summary>
    /// <param name="expression">The predicate expression.</param>
    /// <param name="whenTrue">The function that returns the metadata when the predicate is true.</param>
    /// <param name="whenFalse">The function that returns the metadata when the predicate is false.</param>
    [FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiMetadataPropositionExpressionTreeFactory(
        [FluentMethod("From")]Expression<Func<TModel, TPredicateResult>> expression,
        [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, BooleanResultBase<string>, IEnumerable<TMetadata>> whenTrue,
        [FluentMethod("WhenFalse", Overloads = typeof(WhenOverloads))]Func<TModel, BooleanResultBase<string>, TMetadata> whenFalse)
        : this(expression, whenTrue, whenFalse.ToEnumerableReturn())
    {
    }

    /// <summary>Creates a proposition and names it with the propositional statement provided.</summary>
    /// <param name="statement">The proposition statement of what the proposition represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A proposition for the model.</returns>
    public SpecBase<TModel, TMetadata> Create(string statement) =>
        new ExpressionTreeMultiMetadataProposition<TModel, TMetadata, TPredicateResult>(
            expression,
            whenTrue,
            whenFalse,
            new SpecDescription(statement.ThrowIfNullOrWhitespace(nameof(statement))));
}
