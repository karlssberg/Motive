﻿using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.PolicyResultPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Policy;

/// <summary>
/// A factory for creating specifications based on a predicate and explanations for true and false conditions. This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create a specification that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TUnderlyingMetadata">The type of the underlying metadata associated with the specification.</typeparam>
[FluentConstructor(typeof(Motiv.Spec))]
public readonly ref struct MultiAssertionExplanationWithNameHigherOrderPolicyResultPropositionFactory<TModel, TUnderlyingMetadata>(
    [FluentMethod("Build")]Func<TModel, PolicyResultBase<TUnderlyingMetadata>> resultResolver,
    [FluentMethod("As")]Func<IEnumerable<PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate,
    [FluentMethod("WithCauses")]Func<bool, IEnumerable<PolicyResult<TModel, TUnderlyingMetadata>>, IEnumerable<PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector,
    [FluentMethod("WhenTrue")]string trueBecause,
    [FluentMethod("WhenFalse")]Func<HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, IEnumerable<string>> falseBecause)
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
        return new HigherOrderFromPolicyResultMultiMetadataProposition<TModel, string, TUnderlyingMetadata>(
            resultResolver,
            higherOrderPredicate,
            trueBecause
                .ToEnumerable()
                .ToFunc<HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, IEnumerable<string>>(),
            falseBecause,
            new SpecDescription(statement),
            causeSelector);
    }

    /// <summary>
    /// Creates a specification with explanations for when the condition is true or false. The propositional statement
    /// will be obtained from the .WhenTrue() assertion.
    /// </summary>
    /// <returns>An instance of <see cref="SpecBase{TModel, TMetadata}" />.</returns>
    public SpecBase<IEnumerable<TModel>, string> Create() =>
        new HigherOrderFromPolicyResultMultiMetadataProposition<TModel, string, TUnderlyingMetadata>(
            resultResolver,
            higherOrderPredicate,
            trueBecause
                .ToEnumerable()
                .ToFunc<HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, IEnumerable<string>>(),
            falseBecause,
            new SpecDescription(trueBecause),
            causeSelector);
}
