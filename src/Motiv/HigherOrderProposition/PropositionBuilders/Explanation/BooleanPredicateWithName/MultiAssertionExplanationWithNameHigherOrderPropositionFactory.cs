using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.BooleanPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName;

/// <summary>
/// A factory for creating specifications based on a predicate and explanations for true and false conditions. This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create a specification that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiAssertionExplanationWithNameHigherOrderPropositionFactory<TModel>(
    [FluentMethod("Build")]Func<TModel, bool> resultResolver,
    [MultipleFluentMethods(typeof(HigherOrderBooleanPredicateSpecMethods))]HigherOrderSpecBooleanPredicateOperation<TModel> higherOrderOperation,
    [FluentMethod("WhenTrue", Overloads = typeof(AllConverters))]string trueBecause,
    [FluentMethod("WhenFalseYield", Overloads = typeof(AllConverters))]Func<HigherOrderBooleanEvaluation<TModel>, IEnumerable<string>> falseBecause)
{
    /// <summary>
    /// Creates a specification with explanations for when the condition is true or false, and names it with the propositional statement provided.
    /// </summary>
    /// <param name="statement">The proposition statement of what the specification represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>An instance of <see cref="SpecBase{TModel, TMetadata}" />.</returns>
    public SpecBase<IEnumerable<TModel>, string> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new HigherOrderFromBooleanPredicateMultiMetadataProposition<TModel, string>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            trueBecause
                .ToEnumerable()
                .ToFunc<HigherOrderBooleanEvaluation<TModel>, IEnumerable<string>>(),
            falseBecause,
            new SpecDescription(statement),
            higherOrderOperation.CauseSelector);
    }

    /// <summary>
    /// Creates a specification with explanations for when the condition is true or false. The propositional statement
    /// will be obtained from the .WhenTrue() assertion.
    /// </summary>
    /// <returns>An instance of <see cref="SpecBase{TModel, TMetadata}" />.</returns>
    public SpecBase<IEnumerable<TModel>, string> Create() =>
        new HigherOrderFromBooleanPredicateMultiMetadataProposition<TModel, string>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            trueBecause
                .ToEnumerable()
                .ToFunc<HigherOrderBooleanEvaluation<TModel>, IEnumerable<string>>(),
            falseBecause,
            new SpecDescription(trueBecause),
            higherOrderOperation.CauseSelector);
}
