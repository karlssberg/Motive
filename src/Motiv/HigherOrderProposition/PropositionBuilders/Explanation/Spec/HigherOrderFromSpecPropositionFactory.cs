﻿using System.Linq.Expressions;
using Motiv.Generator.Attributes;
using Motiv.HigherOrderProposition.BooleanResultPredicate;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Spec;

/// <summary>
/// A factory for creating propositions based on a predicate and metadata factories.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the proposition.</typeparam>
[FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct HigherOrderFromSpecPropositionFactory<TModel, TMetadata>(
    [FluentMethod("Build")]SpecBase<TModel, TMetadata> spec,
    [MultipleFluentMethods(typeof(HigherOrderPredicateSpecMethods))]HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation)
{
    /// <summary>Creates a proposition and names it with the propositional statement provided.</summary>
    /// <param name="statement">The proposition statement of what the specification represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A specification for the model.</returns>
    public SpecBase<IEnumerable<TModel>, TMetadata> Create(string statement) =>
        new HigherOrderFromBooleanResultProposition<TModel, TMetadata>(
            spec.IsSatisfiedBy,
            higherOrderOperation.HigherOrderPredicate,
            new SpecDescription(
                statement.ThrowIfNullOrWhitespace(nameof(statement)),
                spec.Description),
            higherOrderOperation.CauseSelector);

    internal SpecBase<IEnumerable<TModel>, TMetadata> Create(Expression statement) =>
        new HigherOrderFromBooleanResultProposition<TModel, TMetadata>(
            spec.IsSatisfiedBy,
            higherOrderOperation.HigherOrderPredicate,
            new ExpressionDescription(
                statement,
                spec.Description),
            higherOrderOperation.CauseSelector);
}
