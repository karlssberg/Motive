﻿using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.PolicyResultPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Policy;

/// <summary>
/// A factory for creating specifications based on a predicate and explanations for true and false conditions. This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create a specification that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the specification.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiAssertionExplanationWithNameHigherOrderPolicyResultPropositionFactory<TModel, TMetadata>(
    [FluentMethod("Build", Overloads = typeof(PolicyResultBuildOverloads))]Func<TModel, PolicyResultBase<TMetadata>> resultResolver,
    [MultipleFluentMethods(typeof(HigherOrderPredicatePolicyMethods))]HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation,
    [FluentMethod("WhenTrue")]string trueBecause,
    [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderPolicyResultEvaluation<TModel, TMetadata>, IEnumerable<string>> falseBecause)
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
        return new HigherOrderFromPolicyResultMultiMetadataProposition<TModel, string, TMetadata>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            trueBecause
                .ToEnumerable()
                .ToFunc<HigherOrderPolicyResultEvaluation<TModel, TMetadata>, IEnumerable<string>>(),
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
        new HigherOrderFromPolicyResultMultiMetadataProposition<TModel, string, TMetadata>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            trueBecause
                .ToEnumerable()
                .ToFunc<HigherOrderPolicyResultEvaluation<TModel, TMetadata>, IEnumerable<string>>(),
            falseBecause,
            new SpecDescription(trueBecause),
            higherOrderOperation.CauseSelector);
}
