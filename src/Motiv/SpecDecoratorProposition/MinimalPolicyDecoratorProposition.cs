using Motiv.Shared;

namespace Motiv.SpecDecoratorProposition;

internal sealed partial class MinimalPolicyDecoratorProposition<TModel, TUnderlyingMetadata>(
    PolicyBase<TModel, TUnderlyingMetadata> underlyingPolicy,
    ISpecDescription description)
    : PolicyBase<TModel, string>
{
    private readonly string _trueBecause = GetTrueAssertion(underlyingPolicy.Statement);
    private readonly string _falseBecause = GetFalseAssertion(underlyingPolicy.Statement);
    public override IEnumerable<SpecBase> Underlying => UnderlyingPolicy.ToEnumerable();

    public override ISpecDescription Description => description;

    public PolicyBase<TModel, TUnderlyingMetadata> UnderlyingPolicy { get; } = underlyingPolicy;

    protected override PolicyResultBase<string> IsPolicySatisfiedBy(TModel model)
    {
        var predicateResult = UnderlyingPolicy.IsSatisfiedBy(model);
        var assertion = GetLazyAssertion(model, predicateResult);

        return CreatePolicyResult(assertion, predicateResult);
    }

    private Lazy<string> GetLazyAssertion(TModel model, PolicyResultBase<TUnderlyingMetadata> predicateResult)
    {
        return new Lazy<string>(() =>
            predicateResult.Satisfied switch
            {
                true => _trueBecause,
                false => _falseBecause
            });
    }

    private PolicyResultBase<string> CreatePolicyResult(Lazy<string> assertion, PolicyResultBase<TUnderlyingMetadata> policyResult)
    {
        var explanation = new Lazy<Explanation>(() =>
            new Explanation(assertion.Value, policyResult.ToEnumerable(), policyResult.ToEnumerable()));

        var metadataTier = new Lazy<MetadataNode<string>>(() =>
            new MetadataNode<string>(assertion.Value.ToEnumerable(),
                policyResult.ToEnumerable() as IEnumerable<PolicyResultBase<string>> ?? []));

        var resultDescription = new Lazy<ResultDescriptionBase>(() =>
            new BooleanResultDescriptionWithUnderlying(
                policyResult,
                assertion.Value,
                Description.Statement));

        return new PolicyResultWithUnderlying<string, TUnderlyingMetadata>(
            policyResult,
            Value,
            MetadataTier,
            Explanation,
            ResultDescription);

        string Value() => assertion.Value;
        MetadataNode<string> MetadataTier() => metadataTier.Value;
        Explanation Explanation() => explanation.Value;
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
