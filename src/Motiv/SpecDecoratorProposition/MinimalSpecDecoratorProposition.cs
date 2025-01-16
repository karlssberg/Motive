using Motiv.Shared;

namespace Motiv.SpecDecoratorProposition;

internal sealed class MinimalSpecDecoratorProposition<TModel, TUnderlyingMetadata>(
    SpecBase<TModel, TUnderlyingMetadata> underlyingSpec,
    ISpecDescription description)
    : SpecBase<TModel, string>
{
    private readonly string _trueBecause = GetTrueAssertion(underlyingSpec.Statement);
    private readonly string _falseBecause = GetFalseAssertion(underlyingSpec.Statement);
    public override IEnumerable<SpecBase> Underlying => UnderlyingSpec.ToEnumerable();

    public override ISpecDescription Description => description;

    public SpecBase<TModel, TUnderlyingMetadata> UnderlyingSpec { get; } = underlyingSpec;

    protected override BooleanResultBase<string> IsSpecSatisfiedBy(TModel model)
    {
        var predicateResult = UnderlyingSpec.IsSatisfiedBy(model);
        var assertion = GetLazyAssertion(model, predicateResult);

        return CreateSpecResult(assertion, predicateResult);
    }

    private Lazy<string> GetLazyAssertion(TModel model, BooleanResultBase<TUnderlyingMetadata> predicateResult)
    {
        return new Lazy<string>(() =>
            predicateResult.Satisfied switch
            {
                true => _trueBecause,
                false => _falseBecause
            });
    }

    private BooleanResultBase<string> CreateSpecResult(Lazy<string> assertion, BooleanResultBase<TUnderlyingMetadata> booleanResult)
    {
        var explanation = new Lazy<Explanation>(() =>
            new Explanation(assertion.Value, booleanResult.ToEnumerable(), booleanResult.ToEnumerable()));

        var metadataTier = new Lazy<MetadataNode<string>>(() =>
            new MetadataNode<string>(assertion.Value.ToEnumerable(),
                booleanResult.ToEnumerable() as IEnumerable<BooleanResultBase<string>> ?? []));

        var resultDescription = new Lazy<ResultDescriptionBase>(() =>
            new BooleanResultDescriptionWithUnderlying(
                booleanResult,
                assertion.Value,
                Description.Statement));

        return new BooleanResultWithUnderlying<string, TUnderlyingMetadata>(
            booleanResult,
            Value,
            MetadataTier,
            ResultDescription);

        MetadataNode<string> Value() => metadataTier.Value;
        Explanation MetadataTier() => explanation.Value;
        ResultDescriptionBase ResultDescription() => resultDescription.Value;
    }


    private static string GetTrueAssertion(string proposition) =>
        proposition.ContainsReservedCharacters()
            ? $"({proposition})"
            : proposition;

    private static string GetFalseAssertion(string proposition) =>
        proposition.ContainsReservedCharacters()
            ? $"({proposition})".AsUnsatisfied()
            : proposition.AsUnsatisfied();
}
