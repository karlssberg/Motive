﻿using System.Linq.Expressions;
using Motiv.Shared;

namespace Motiv.ExpressionTreeProposition;

/// <summary>
/// An proposition representing a lambda expression tree.
/// </summary>
internal sealed class ExpressionTreeWithSingleTrueAssertionProposition<TModel, TPredicateResult>(
    Expression<Func<TModel, TPredicateResult>> expression,
    string trueBecause,
    Func<TModel, BooleanResultBase<string>, string> whenFalse,
    ISpecDescription description)
    : PolicyBase<TModel, string>
{
    public override IEnumerable<SpecBase> Underlying => [];

    private readonly ExpressionPredicate<TModel, TPredicateResult> _predicate = new(expression);

    public override ISpecDescription Description => description;

    protected override PolicyResultBase<string> IsPolicySatisfiedBy(TModel model)
    {
        var result = _predicate.Execute(model);
        BooleanResultBase<string>[] resultAsCollection = [result];

        var assertion = new Lazy<string>(() =>
            result.Satisfied switch
            {
                true => trueBecause,
                false => whenFalse(model, result)
            });

        var explanation = new Lazy<Explanation>(() => new Explanation(
            assertion.Value,
            resultAsCollection,
            resultAsCollection));

        var metadataTier = new Lazy<MetadataNode<string>>(() =>
            new MetadataNode<string>(assertion.Value,
                resultAsCollection));

        var resultDescription = new Lazy<ResultDescriptionBase>(() => new ExpressionTreeBooleanResultDescription(
            result,
            assertion.Value,
            expression,
            Description.Statement));

        return new ExpressionTreePolicyResult<string>(
            result.Satisfied,
            assertion,
            metadataTier,
            explanation,
            resultDescription);
    }
}
