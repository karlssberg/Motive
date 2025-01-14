﻿using System.Linq.Expressions;
using Motiv.ExpressionTreeProposition;
using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.ExpressionTree;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree;

/// <summary>
/// A builder for creating propositions based on a predicate and explanations for true and false conditions.
/// This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create a
/// proposition that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TPredicateResult">The return type of the predicate expression.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>(
    [FluentMethod("From")]Expression<Func<TModel, TPredicateResult>> expression,
    [MultipleFluentMethods(typeof(HigherOrderPredicateSpecMethods))]HigherOrderSpecPredicateOperation<TModel, string> higherOrderOperation)
{
    /// <summary>Creates a proposition and names it with the propositional statement provided.</summary>
    /// <param name="statement">The proposition statement of what the specification represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A specification for the model.</returns>
    public SpecBase<IEnumerable<TModel>, string> Create(string statement) =>
        new HigherOrderFromBooleanResultExpressionTreeProposition<TModel, TPredicateResult>(
            expression,
            higherOrderOperation.HigherOrderPredicate,
            new SpecDescription(statement.ThrowIfNullOrWhitespace(nameof(statement))),
            higherOrderOperation.CauseSelector);

    /// <summary>Creates a proposition.</summary>
    /// <returns>A specification for the model.</returns>
    internal SpecBase<IEnumerable<TModel>, string> Create(Expression statement) =>
        new HigherOrderFromBooleanResultExpressionTreeProposition<TModel, TPredicateResult>(
            expression,
            higherOrderOperation.HigherOrderPredicate,
            new ExpressionAsStatementDescription(statement),
            higherOrderOperation.CauseSelector);
}
