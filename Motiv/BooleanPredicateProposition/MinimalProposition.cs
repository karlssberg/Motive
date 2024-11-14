﻿namespace Motiv.BooleanPredicateProposition;

internal sealed class MinimalProposition<TModel>(
    Func<TModel, bool> predicate,
    ISpecDescription specDescription)
    : PolicyBase<TModel, string>
{
    private readonly string _trueBecause = GetTrueAssertion(specDescription.Statement);
    private readonly string _falseBecause = GetFalseAssertion(specDescription.Statement);


    public override IEnumerable<SpecBase> Underlying => [];


    public override ISpecDescription Description => specDescription;

    protected override PolicyResultBase<string> IsPolicySatisfiedBy(TModel model)
    {
        var isSatisfied = predicate(model);
        var assertion = isSatisfied
            ? _trueBecause
            : _falseBecause;

        return new MinimalPropositionPolicyResult(
            isSatisfied,
            assertion,
            new PropositionResultDescription(assertion, Description.Statement));
    }

    private static string GetTrueAssertion(string proposition) =>
        proposition.ContainsReservedCharacters()
            ? $"({proposition})"
            : proposition;

    private static string GetFalseAssertion(string proposition) =>
        proposition.ContainsReservedCharacters()
            ? $"¬({proposition})"
            : $"¬{proposition}";
}
