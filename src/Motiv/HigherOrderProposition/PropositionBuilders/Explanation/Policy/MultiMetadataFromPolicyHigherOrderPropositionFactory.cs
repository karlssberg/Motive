﻿using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.PolicyResultPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Policy;

/// <summary>
/// A factory for creating propositions based on a predicate and metadata factories. This is particularly useful
/// for handling edge-case scenarios where it would be impossible or impractical to create a proposition that covers
/// every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TReplacementMetadata">The type of the metadata associated with the specification.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata>(
    [MultipleFluentMethods(typeof(PolicyBuildOverloads))]PolicyBase<TModel, string> policy,
    [MultipleFluentMethods(typeof(HigherOrderPredicatePolicyMethods))]HigherOrderPolicyPredicateOperation<TModel, string> higherOrderOperation,
    [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderPolicyResultEvaluation<TModel, string>, IEnumerable<TReplacementMetadata>> whenTrue,
    [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderPolicyResultEvaluation<TModel, string>, IEnumerable<TReplacementMetadata>> whenFalse)
{
    /// <summary>
    /// Creates a specification and names it with the propositional statement provided.
    /// </summary>
    /// <param name="policy">The policy to evaluate.</param>
    /// <param name="higherOrderOperation">The higher-order operation to perform.</param>
    /// <param name="whenTrue">The metadata to yield when the policy is satisfied.</param>
    /// <param name="whenFalse">The metadata to yield when the policy is not satisfied.</param>
    [FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiMetadataFromPolicyHigherOrderPropositionFactory(
        [MultipleFluentMethods(typeof(PolicyBuildOverloads))]PolicyBase<TModel, string> policy,
        [MultipleFluentMethods(typeof(HigherOrderPredicatePolicyMethods))]HigherOrderPolicyPredicateOperation<TModel, string> higherOrderOperation,
        [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderPolicyResultEvaluation<TModel, string>, IEnumerable<TReplacementMetadata>> whenTrue,
        [FluentMethod("WhenFalse", Overloads = typeof(WhenOverloads))]Func<HigherOrderPolicyResultEvaluation<TModel, string>, TReplacementMetadata> whenFalse)
        : this(policy, higherOrderOperation, whenTrue, whenFalse.ToEnumerableReturn())
    {
    }

    /// <summary>
    /// Creates a specification and names it with the propositional statement provided.
    /// </summary>
    /// <param name="policy">The policy to evaluate.</param>
    /// <param name="higherOrderOperation">The higher-order operation to perform.</param>
    /// <param name="whenTrue">The metadata to yield when the policy is satisfied.</param>
    /// <param name="whenFalse">The metadata to yield when the policy is not satisfied.</param>
    [FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiMetadataFromPolicyHigherOrderPropositionFactory(
        [MultipleFluentMethods(typeof(PolicyBuildOverloads))]PolicyBase<TModel, string> policy,
        [MultipleFluentMethods(typeof(HigherOrderPredicatePolicyMethods))]HigherOrderPolicyPredicateOperation<TModel, string> higherOrderOperation,
        [FluentMethod("WhenTrue", Overloads = typeof(WhenOverloads))]Func<HigherOrderPolicyResultEvaluation<TModel, string>, TReplacementMetadata> whenTrue,
        [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<HigherOrderPolicyResultEvaluation<TModel, string>, IEnumerable<TReplacementMetadata>> whenFalse)
        : this(policy, higherOrderOperation, whenTrue.ToEnumerableReturn(), whenFalse)
    {
    }

    /// <summary>Creates a specification and names it with the propositional statement provided.</summary>
    /// <param name="statement">The proposition statement of what the specification represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A specification for the model.</returns>
    public SpecBase<IEnumerable<TModel>, TReplacementMetadata> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new HigherOrderFromPolicyResultMultiMetadataProposition<TModel, TReplacementMetadata, string>(
            policy.IsSatisfiedBy,
            higherOrderOperation.HigherOrderPredicate,
            whenTrue,
            whenFalse,
            new SpecDescription(statement, policy.Description),
            higherOrderOperation.CauseSelector);
    }
}
