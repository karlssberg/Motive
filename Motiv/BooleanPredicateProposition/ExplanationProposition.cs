using Motiv.Shared;

namespace Motiv.BooleanPredicateProposition;

internal sealed class ExplanationProposition<TModel>(
    Func<TModel, bool> predicate,
    Func<TModel, string> trueBecause,
    Func<TModel, string> falseBecause,
    ISpecDescription specDescription)
    : PolicyBase<TModel, string>
{
    public override IEnumerable<SpecBase> Underlying => [];

    public override ISpecDescription Description => specDescription;

    protected override PolicyResultBase<string> IsPolicySatisfiedBy(TModel model)
    {
        var isSatisfied = predicate(model);

        var assertion = new Lazy<string>(() =>
            isSatisfied switch
            {
                true => trueBecause(model),
                false => falseBecause(model)
            });

        return new PropositionPolicyResult<string>(
            isSatisfied,
            assertion,
            new Lazy<MetadataNode<string>>(() => new MetadataNode<string>(assertion.Value, [])),
            new Lazy<Explanation>(() => new Explanation(assertion.Value)),
            new Lazy<ResultDescriptionBase>(() =>
                new PropositionResultDescription(assertion.Value, Description.Statement)));
    }
}
