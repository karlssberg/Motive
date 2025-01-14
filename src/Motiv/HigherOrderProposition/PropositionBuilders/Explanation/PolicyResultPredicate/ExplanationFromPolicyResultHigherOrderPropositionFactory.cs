﻿using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.PolicyResultPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate;

/// <summary>
/// A factory for creating specifications based on a predicate and explanations for true and false conditions. This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create a specification that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the specification.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct ExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata>(
    [FluentMethod("Build")]Func<TModel, PolicyResultBase<TMetadata>> resultResolver,
    [MultipleFluentMethods(typeof(HigherOrderPredicatePolicyMethods))]HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation,
    [FluentMethod("WhenTrue", Overloads = typeof(AllConverters))]Func<HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> trueBecause,
    [FluentMethod("WhenFalse", Overloads = typeof(AllConverters))]Func<HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> falseBecause)
{
    /// <summary>
    /// Creates a specification with explanations for when the condition is true or false, and names it with the propositional statement provided.
    /// </summary>
    /// <param name="statement">The proposition statement of what the specification represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>An instance of <see cref="SpecBase{TModel, TMetadata}" />.</returns>
    public PolicyBase<IEnumerable<TModel>, string> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new HigherOrderFromPolicyResultExplanationProposition<TModel, TMetadata>(
            resultResolver,
            higherOrderOperation.HigherOrderPredicate,
            trueBecause,
            falseBecause,
            new SpecDescription(statement),
            higherOrderOperation.CauseSelector);
    }
}
