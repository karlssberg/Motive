

using Motiv.Generator.Attributes;

namespace Motiv.Generator.Tests;

public class FluentBuilderGeneratorTests
{
    [Fact]
    public void Should_generate_source_files()
    {
        // var proposition = Proposition
        //     .Build((int x) => x > 0)
        //     .WhenTrue("x is positive")
        //     .WhenFalse("x is not positive")
        //     .Create();


    }
}


[GenerateFluentBuilder("Proposition")]
public readonly ref struct PropositionFactory<TModel, TMetadata>(
    [FluentMethodName("Build")] Func<TModel, bool> predicate,
    [FluentMethodName("WhenTrue")] TMetadata whenTrue,
    [FluentMethodName("WhenFalse")] TMetadata whenFalse)
{
    public Proposition<TModel, TMetadata> Create() => new(predicate, whenTrue, whenFalse);
}

public class Proposition<TModel, TMetadata>(Func<TModel, bool> predicate, TMetadata trueBecause, TMetadata falseBecause)
{
    public Func<TModel, bool> Predicate { get; } = predicate;
    public TMetadata TrueBecause { get; } = trueBecause;
    public TMetadata FalseBecause { get; } = falseBecause;
}
