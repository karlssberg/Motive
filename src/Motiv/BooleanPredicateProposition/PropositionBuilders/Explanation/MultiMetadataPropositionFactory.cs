﻿using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.BooleanPredicateProposition.PropositionBuilders.Explanation;

/// <summary>
/// A factory for creating propositions based on the supplied predicate and metadata factories.
/// </summary>
/// <typeparam name="TModel">The type of the model the proposition is for.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiAssertionExplanationPropositionFactory<TModel>(
    [FluentMethod("Build")]Func<TModel, bool> predicate,
    [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))] Func<TModel, IEnumerable<string>> whenTrue,
    [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))] Func<TModel, IEnumerable<string>> whenFalse)
{
    /// <summary>
    /// A factory for creating propositions based on the supplied predicate and metadata factories.
    /// </summary>
    /// <typeparam name="TModel">The type of the model the proposition is for.</typeparam>
    [FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiAssertionExplanationPropositionFactory(
        [FluentMethod("Build")] Func<TModel, bool> predicate,
        [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))] Func<TModel, IEnumerable<string>> whenTrue,
        [FluentMethod("WhenFalse", Overloads = typeof(WhenOverloads))] Func<TModel, string> whenFalse)
    : this(predicate, whenTrue, whenFalse.ToEnumerableReturn())
    {
    }

    /// <summary>
    /// A factory for creating propositions based on the supplied predicate and metadata factories.
    /// </summary>
    /// <typeparam name="TModel">The type of the model the proposition is for.</typeparam>
    [FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiAssertionExplanationPropositionFactory(
        [FluentMethod("Build")] Func<TModel, bool> predicate,
        [FluentMethod("WhenTrue", Overloads = typeof(WhenOverloads))] Func<TModel, string> whenTrue,
        [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))] Func<TModel, IEnumerable<string>> whenFalse)
        : this(predicate, whenTrue.ToEnumerableReturn(), whenFalse)
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
        return new MultiMetadataProposition<TModel, string>(
            predicate,
            whenTrue,
            whenFalse,
            new SpecDescription(statement));
    }
}
