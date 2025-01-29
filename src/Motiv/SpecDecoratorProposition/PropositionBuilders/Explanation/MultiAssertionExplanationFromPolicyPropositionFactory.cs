using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation;

/// <summary>
/// A factory for creating propositions based on the supplied proposition and explanation factories.
/// This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create
/// a proposition that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TMetadata">The type of the underlying metadata associated with the proposition.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly partial struct MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(
    [MultipleFluentMethods(typeof(PolicyBuildOverloads))]PolicyBase<TModel, TMetadata> policy,
    [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, PolicyResultBase<TMetadata>, IEnumerable<string>> trueBecause,
    [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, PolicyResultBase<TMetadata>, IEnumerable<string>> falseBecause)
{
    /// <summary>
    /// A factory for creating propositions based on the supplied proposition and explanation factories.
    /// </summary>
    /// <param name="policy">The policy to evaluate.</param>
    /// <param name="trueBecause">The explanations for when the policy is true.</param>
    /// <param name="falseBecause">The explanation for when the policy is false.</param>
    [FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiAssertionExplanationFromPolicyPropositionFactory(
        [MultipleFluentMethods(typeof(PolicyBuildOverloads))]PolicyBase<TModel, TMetadata> policy,
        [FluentMethod("WhenTrueYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, PolicyResultBase<TMetadata>, IEnumerable<string>> trueBecause,
        [FluentMethod("WhenFalse", Overloads = typeof(WhenOverloads))]Func<TModel, PolicyResultBase<TMetadata>, string> falseBecause)
        : this(policy, trueBecause, falseBecause.ToEnumerableReturn())
    {
    }

    /// <summary>
    /// A factory for creating propositions based on the supplied proposition and explanation factories.
    /// </summary>
    /// <param name="policy">The policy to evaluate.</param>
    /// <param name="trueBecause">The explanations for when the policy is true.</param>
    /// <param name="falseBecause">The explanation for when the policy is false.</param>
    [FluentConstructor(typeof(Motiv.Spec), Options = FluentOptions.NoCreateMethod)]
    public MultiAssertionExplanationFromPolicyPropositionFactory(
        [MultipleFluentMethods(typeof(PolicyBuildOverloads))]PolicyBase<TModel, TMetadata> policy,
        [FluentMethod("WhenTrue", Overloads = typeof(WhenOverloads))]Func<TModel, PolicyResultBase<TMetadata>, string> trueBecause,
        [FluentMethod("WhenFalseYield", Overloads = typeof(WhenYieldOverloads))]Func<TModel, PolicyResultBase<TMetadata>, IEnumerable<string>> falseBecause)
        : this(policy, trueBecause.ToEnumerableReturn(), falseBecause)
    {
    }

    /// <summary>
    /// Creates a proposition and names it with the propositional statement provided.
    /// </summary>
    /// <param name="statement">The proposition statement of what the proposition represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A proposition for the model.</returns>
    public SpecBase<TModel, string> Create(string statement) =>
        new PolicyDecoratorMultiMetadataProposition<TModel, string, TMetadata>(
            policy,
            trueBecause,
            falseBecause,
            new SpecDescription(statement.ThrowIfNullOrWhitespace(nameof(statement)), policy.Description));
}
