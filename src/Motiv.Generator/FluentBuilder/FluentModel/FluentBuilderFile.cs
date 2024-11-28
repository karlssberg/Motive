using System.Collections.Immutable;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentBuilderRoot(string Name)
{
    public string Name { get; } = Name;
    public required string NameSpace { get; set; }
    public required ImmutableArray<FluentBuilderMethod> FluentMethods { get; set; }
}
