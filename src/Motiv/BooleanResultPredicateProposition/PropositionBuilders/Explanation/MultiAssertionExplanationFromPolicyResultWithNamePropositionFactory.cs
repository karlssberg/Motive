﻿using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.BooleanResultPredicateProposition.PropositionBuilders.Explanation;

/// <summary>
/// A factory for creating propositions based on the supplied proposition and explanation factories.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the proposition.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiAssertionExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata>(
    [MultipleFluentMethods(typeof(PolicyResultBuildOverloads))]Func<TModel, PolicyResultBase<TMetadata>> predicate,
    [FluentMethod("WhenTrue")]string trueBecause,
    [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, PolicyResultBase<TMetadata>, IEnumerable<string>> falseBecause)
{
    /// <summary>
    /// Creates a proposition and names it with the propositional statement provided.
    /// </summary>
    /// <param name="statement">The proposition statement of what the proposition represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A proposition for the model.</returns>
    public SpecBase<TModel, string> Create(string statement)
    {
        statement.ThrowIfNullOrWhitespace(nameof(statement));
        return new PolicyResultPredicateMultiMetadataProposition<TModel, string, TMetadata>(
            predicate,
            trueBecause
                .ToEnumerable()
                .ToFunc<TModel, PolicyResultBase<TMetadata>, IEnumerable<string>>(),
            falseBecause,
            new SpecDescription(statement)
        );
    }

    /// <summary>
    /// Creates a proposition with explanations for when the condition is true or false. The propositional statement
    /// will be obtained from the .WhenTrue() assertion.
    /// </summary>
    /// <returns>A proposition for the model.</returns>
    public SpecBase<TModel, string> Create() =>
        new PolicyResultPredicateMultiMetadataProposition<TModel, string, TMetadata>(
            predicate,
            trueBecause
                .ToEnumerable()
                .ToFunc<TModel, PolicyResultBase<TMetadata>, IEnumerable<string>>(),
            falseBecause,
            new SpecDescription(trueBecause));
}
