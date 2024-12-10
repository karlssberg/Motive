using Motiv.Generator.Attributes;

namespace Motiv.Generator.Tests.Examples;

[GenerateFluentBuilder("Motiv.Generator.Tests.Proposition")]
public readonly struct PropositionStructFactory<TModel>(
    [FluentMethod("Build")] Func<TModel, bool> predicate,
    [FluentMethod("WhenTrue")] string trueBecause,
    [FluentMethod("WhenFalse")] string falseBecause)
{
    public PropositionClass<TModel, string> Create()
    {
        return new PropositionClass<TModel, string>(predicate, trueBecause, falseBecause, trueBecause);
    }
}
