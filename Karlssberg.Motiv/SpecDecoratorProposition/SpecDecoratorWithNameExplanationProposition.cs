﻿namespace Karlssberg.Motiv.SpecDecoratorProposition;

internal sealed class SpecDecoratorWithNameExplanationProposition<TModel, TUnderlyingMetadata>(
    SpecBase<TModel, TUnderlyingMetadata> underlyingSpec,
    string trueBecause,
    Func<TModel, BooleanResultBase<TUnderlyingMetadata>, string> falseBecause,
    string propositionalAssertion)
    : SpecBase<TModel, string>
{
    public override ISpecDescription Description => new SpecDescription(propositionalAssertion, UnderlyingSpec.Description.Detailed);

    public SpecBase<TModel, TUnderlyingMetadata> UnderlyingSpec { get; } = underlyingSpec;

    public override BooleanResultBase<string> IsSatisfiedBy(TModel model)
    {
        var booleanResult = UnderlyingSpec.IsSatisfiedBy(model);

        var assertion = new Lazy<string>(() =>
            booleanResult.Satisfied switch
            {
                true => trueBecause,
                false => falseBecause(model, booleanResult)
            });

        var explanation = new Lazy<Explanation>(() =>
            new Explanation(assertion.Value, booleanResult.ToEnumerable()));
        
        var metadataTree = new Lazy<MetadataNode<string>>(() => 
            new MetadataNode<string>(assertion.Value, 
                booleanResult.MetadataTiers.ToEnumerable() as IEnumerable<MetadataNode<string>> ?? []));

        return new BooleanResultWithUnderlying<string, TUnderlyingMetadata>(
            booleanResult, 
            MetadataTree,
            Explanation,
            Reason);

        MetadataNode<string> MetadataTree() => metadataTree.Value;
        Explanation Explanation() => explanation.Value;
        string Reason() => Description.ToReason(booleanResult.Satisfied);
    }
}