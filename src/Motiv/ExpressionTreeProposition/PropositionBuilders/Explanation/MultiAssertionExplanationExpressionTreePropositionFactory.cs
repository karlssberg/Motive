﻿using System.Linq.Expressions;
using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.ExpressionTreeProposition.PropositionBuilders.Explanation;

/// <summary>
/// A factory for creating propositions based on the supplied proposition and explanation factories.
/// This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create
/// a proposition that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TPredicateResult">The return type of the predicate expression.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(
    [FluentMethod("From")]Expression<Func<TModel, TPredicateResult>> expression,
    [FluentMethod("WhenTrueYield", Overloads = typeof(AllConverters))]Func<TModel, BooleanResultBase<string>, IEnumerable<string>> trueBecause,
    [FluentMethod("WhenFalseYield", Overloads = typeof(AllConverters))]Func<TModel, BooleanResultBase<string>, IEnumerable<string>> falseBecause)
{
    /// <summary>
    /// Creates a proposition and names it with the propositional statement provided.
    /// </summary>
    /// <param name="statement">The proposition statement of what the proposition represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A proposition for the model.</returns>
    public SpecBase<TModel, string> Create(string statement) =>
        new ExpressionTreeMultiMetadataProposition<TModel, string, TPredicateResult>(
            expression,
            trueBecause,
            falseBecause,
            new SpecDescription(statement.ThrowIfNullOrWhitespace(nameof(statement))));
}
