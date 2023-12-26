﻿using static Karlssberg.Motive.SpecificationException;

namespace Karlssberg.Motive.AtMost;

internal sealed class AtMostSpecification<TModel, TMetadata>(
    int maximum,
    SpecificationBase<TModel, TMetadata> underlyingSpecification,
    Func<bool, IEnumerable<BooleanResultWithModel<TModel, TMetadata>>, IEnumerable<TMetadata>> metadataFactory)
    : SpecificationBase<IEnumerable<TModel>, TMetadata>
{
    internal AtMostSpecification(int maximum, SpecificationBase<TModel, TMetadata> specification)
        : this(maximum, specification, SelectCauses)
    {
    }

    internal AtMostSpecification(
        int maximum,
        SpecificationBase<TModel, TMetadata> underlyingSpecification,
        Func<IEnumerable<TModel>, TMetadata> whenTrue)
        : this(maximum, underlyingSpecification, CreateMetadataFactory(whenTrue))
    {
    }

    internal AtMostSpecification(
        int maximum,
        SpecificationBase<TModel, TMetadata> underlyingSpecification,
        Func<IEnumerable<TModel>, TMetadata> whenTrue,
        Func<BooleanResultWithModel<TModel, TMetadata>, TMetadata> whenMaximumExceeded)
        : this(maximum, underlyingSpecification, CreateMetadataFactory(whenTrue, whenMaximumExceeded))
    {
    }

    public SpecificationBase<TModel, TMetadata> UnderlyingSpecification { get; } = Throw.IfNull(underlyingSpecification, nameof(underlyingSpecification));

    public override string Description => $"AT_MOST[{maximum}]({underlyingSpecification})";

    public override BooleanResultBase<TMetadata> Evaluate(IEnumerable<TModel> models)
    {
        var results = models
            .Select(
                model =>
                    WrapThrownExceptions(
                        this,
                        UnderlyingSpecification,
                        () =>
                        {
                            var underlyingResult = UnderlyingSpecification.Evaluate(model);
                            return new BooleanResultWithModel<TModel, TMetadata>(model, underlyingResult);
                        }))
            .ToList();

        return new AtMostBooleanResult<TMetadata>(maximum, ResolveMetadata, results.Select(result => result.UnderlyingResult));

        IEnumerable<TMetadata> ResolveMetadata(bool isSatisfied)
        {
            return metadataFactory(isSatisfied, results);
        }
    }

    private static Func<bool, IEnumerable<BooleanResultWithModel<TModel, TMetadata>>, IEnumerable<TMetadata>> CreateMetadataFactory(
        Func<IEnumerable<TModel>, TMetadata> whenTrue,
        Func<BooleanResultWithModel<TModel, TMetadata>, TMetadata> whenAnyFalse)
    {
        return (isSatisfied, results) => isSatisfied
            ? [whenTrue(results.Select(result => result.Model))]
            : results.Select(whenAnyFalse);
    }

    private static Func<bool, IEnumerable<BooleanResultWithModel<TModel, TMetadata>>, IEnumerable<TMetadata>> CreateMetadataFactory(
        Func<IEnumerable<TModel>, TMetadata> whenTrue)
    {
        return (isSatisfied, results) => isSatisfied
            ? [whenTrue(results.Select(result => result.Model))]
            : results
                .Where(result => !result.IsSatisfied)
                .SelectMany(result => result.GetInsights());
    }

    private static IEnumerable<TMetadata> SelectCauses(bool isSatisfied, IEnumerable<BooleanResultWithModel<TModel, TMetadata>> results) =>
        results
            .Where(result => result.IsSatisfied == isSatisfied)
            .SelectMany(result => result.GetInsights());
}