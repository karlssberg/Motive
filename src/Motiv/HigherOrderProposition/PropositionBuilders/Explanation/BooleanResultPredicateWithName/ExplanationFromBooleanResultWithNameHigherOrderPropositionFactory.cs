using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.BooleanResultPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName;

/// <summary>
/// A factory for creating specifications based on a predicate and explanations for true and false conditions.
/// This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create a
/// specification that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the specification.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct ExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata>(
    [FluentMethod("Build")]Func<TModel, BooleanResultBase<TMetadata>> resultResolver,
    [MultipleFluentMethods(typeof(HigherOrderPredicateSpecMethods))]HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation,
    [FluentMethod("WhenTrue")]string trueBecause,
    [FluentMethod("WhenFalse", Overloads = typeof(WhenOverloads))]Func<HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string> falseBecause)
{
    /// <summary>
    /// Creates a specification with explanations for when the condition is true or false. The propositional statement
    /// will be obtained from the .WhenTrue() assertion.
    /// </summary>
    /// <returns>An instance of <see cref="SpecBase{TModel, TMetadata}" />.</returns>
    public PolicyBase<IEnumerable<TModel>, string> Create() =>
        new HigherOrderFromBooleanResultExplanationProposition<TModel, TMetadata>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            trueBecause.ToFunc<HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string>(),
            falseBecause,
            new SpecDescription(trueBecause),
            higherOrderOperation.CauseSelector);

    /// <summary>
    /// Creates a specification with descriptive assertions, but using the supplied proposition to succinctly explain
    /// the decision.
    /// </summary>
    /// <param name="statement">The proposition statement of what the specification represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>An instance of <see cref="SpecBase{TModel, TMetadata}" />.</returns>
    public PolicyBase<IEnumerable<TModel>, string> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new HigherOrderFromBooleanResultExplanationProposition<TModel, TMetadata>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            trueBecause.ToFunc<HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string>(),
            falseBecause,
            new SpecDescription(statement),
            higherOrderOperation.CauseSelector);
    }
}
