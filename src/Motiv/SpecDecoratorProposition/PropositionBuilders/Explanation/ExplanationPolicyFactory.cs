using Motiv.Generator.Attributes;
using Motiv.Shared;

namespace Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation;

/// <summary>
/// A factory for creating propositions based on the supplied proposition and explanation factories.
/// This is particularly useful for handling edge-case scenarios where it would be impossible or impractical to create a proposition that covers every possibility, so instead it is done on a case-by-case basis.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TUnderlyingMetadata">The type of the underlying metadata associated with the proposition.</typeparam>
[FluentConstructor(typeof(Spec), Options = FluentOptions.NoCreateMethod)]
public readonly ref struct ExplanationPolicyFactory<TModel, TUnderlyingMetadata>(
    [FluentMethod("Build")]PolicyBase<TModel, TUnderlyingMetadata> spec,
    [FluentMethod("WhenTrue", Overloads = typeof(ExplanationConverter))]Func<TModel, PolicyResultBase<TUnderlyingMetadata>, string> trueBecause,
    [FluentMethod("WhenFalse", Overloads = typeof(ExplanationConverter))]Func<TModel, PolicyResultBase<TUnderlyingMetadata>, string> falseBecause)
{

    /// <summary>
    /// Creates a proposition and names it with the propositional statement provided.
    /// </summary>
    /// <param name="statement">The proposition statement of what the proposition represents.</param>
    /// <remarks>It is best to use short phases in natural-language, as if you were naming a boolean variable.</remarks>
    /// <returns>A proposition for the model.</returns>
    public PolicyBase<TModel, string> Create(string statement) =>
        new PolicyDecoratorExplanationProposition<TModel, TUnderlyingMetadata>(
            spec,
            trueBecause,
            falseBecause,
            new SpecDescription(statement.ThrowIfNullOrWhitespace(nameof(statement)), spec.Description));

}


public static class MetadataConverter
{
    [FluentParameterConverter]
    public static Func<T1, T2, TResult> Convert<T1, T2, TResult>(TResult value) =>
        (_, _) => value;

    [FluentParameterConverter]
    public static Func<T1, T2, TResult> Convert<T1, T2, TResult>(Func<T1, TResult> value) =>
        (arg1, _) => value(arg1);
}

public static class ExplanationConverter
{
    [FluentParameterConverter]
    public static Func<T1, T2, string> Convert<T1, T2>(string value) =>
        (_, _) => value;

    [FluentParameterConverter]
    public static Func<T1, T2, string> Convert<T1, T2>(Func<T1, string> value) =>
        (arg1, _) => value(arg1);
}
