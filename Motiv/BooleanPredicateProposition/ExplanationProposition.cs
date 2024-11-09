using Motiv.Shared;

namespace Motiv.BooleanPredicateProposition;

internal sealed class ExplanationProposition<TModel>(
    Func<TModel, bool> predicate,
    Func<TModel, string> trueBecause,
    Func<TModel, string> falseBecause,
    ISpecDescription specDescription)
    : PolicyBase<TModel, string>
{
    internal ExplanationProposition(Func<TModel, bool> predicate, ISpecDescription specDescription)
        : this(
            predicate,
            _ => ReasonFromPropositionStatement(true, specDescription.Statement),
            _ => ReasonFromPropositionStatement(false, specDescription.Statement),
            specDescription)
    {
    }

    public override IEnumerable<SpecBase> Underlying => [];


    public override ISpecDescription Description => specDescription;

    protected override PolicyResultBase<string> IsPolicySatisfiedBy(TModel model)
    {
        var isSatisfied = WrapException.CatchFuncExceptionOnBehalfOfSpecType(
            this,
            () => predicate(model),
            nameof(predicate));

        var assertion = new Lazy<string>(() =>
            isSatisfied switch
            {
                true => InvokeTrueBecauseFunction(model),
                false => InvokeFalseBecauseFunction(model)
            });

        return new PropositionPolicyResult<string>(
            isSatisfied,
            assertion,
            new Lazy<MetadataNode<string>>(() => new MetadataNode<string>(assertion.Value, [])),
            new Lazy<Explanation>(() => new Explanation(assertion.Value, [], [])),
            new Lazy<ResultDescriptionBase>(() =>
                new PropositionResultDescription(assertion.Value, Description.Statement)));
    }

    private string InvokeTrueBecauseFunction(TModel model) =>
        WrapException.CatchFuncExceptionOnBehalfOfSpecType(
            this,
            () => trueBecause(model),
            nameof(trueBecause));

    private string InvokeFalseBecauseFunction(TModel model) =>
        WrapException.CatchFuncExceptionOnBehalfOfSpecType(
            this,
            () => falseBecause(model),
            nameof(falseBecause));

    private static string ReasonFromPropositionStatement(bool isSatisfied, string proposition) =>
        (isSatisfied, proposition.ContainsReservedCharacters()) switch
        {
            (true, true) => $"({proposition})",
            (true, _) => proposition,
            (false, true) => $"¬({proposition})",
            (false, _) => $"¬{proposition}"
        };
}
