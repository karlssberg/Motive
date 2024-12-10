using Motiv.Generator.Attributes;

namespace Motiv.Generator.Tests.Examples;

[GenerateFluentBuilder("Motiv.Generator.Tests.Proposition")]
public class PropositionClass<TModel, TMetadata>(
    [FluentMethod("Build")] Func<TModel, bool> predicate,
    TMetadata whenTrue,
    TMetadata whenFalse,
    [FluentMethod("Create")] string name)
{
    public Func<TModel, bool> Predicate { get; } = predicate;
    public TMetadata WhenTrue { get; } = whenTrue;
    public TMetadata WhenFalse { get; } = whenFalse;
    public string Name { get; } = name;
}
