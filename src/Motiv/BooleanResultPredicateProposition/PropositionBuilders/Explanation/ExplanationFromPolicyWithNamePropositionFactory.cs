﻿using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.BooleanResultPredicateProposition.PropositionBuilders.Explanation;

/// <summary>
/// A factory for creating propositions based on the supplied proposition and explanation factories.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the proposition.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct ExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata>(
    [FluentMethod("Build")]Func<TModel, PolicyResultBase<TMetadata>> predicate,
    [FluentMethod("WhenTrue", Overloads = typeof(AllConverters))]string trueBecause,
    [FluentMethod("WhenFalse", Overloads = typeof(AllConverters))]Func<TModel, PolicyResultBase<TMetadata>, string> falseBecause)
{
    /// <summary>
    /// Creates a proposition with explanations for when the condition is true or false. The propositional statement
    /// will be obtained from the .WhenTrue() assertion.
    /// </summary>
    /// <returns>A proposition for the model.</returns>
    public PolicyBase<TModel, string> Create() =>
        new PolicyResultPredicateWithSingleAssertionProposition<TModel, TMetadata>(
            predicate,
            trueBecause,
            falseBecause,
            new SpecDescription(trueBecause));

    /// <summary>
    /// Creates a proposition with descriptive assertions, but using the supplied proposition to succinctly explain
    /// the decision.
    /// </summary>
    /// <param name="statement">The proposition statement of what the proposition represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A proposition for the model.</returns>
    public PolicyBase<TModel, string> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new PolicyResultPredicateExplanationProposition<TModel,TMetadata>(
            predicate,
            trueBecause.ToFunc<TModel, PolicyResultBase<TMetadata>, string>(),
            falseBecause,
            new SpecDescription(statement)
        );
    }
}
