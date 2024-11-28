using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentStep(string Name)
{
    public string Name { get; } = Name;
    public required IParameterSymbol ParameterType { get; set; }
    public required ImmutableArray<INamedTypeSymbol> Attributes { get; set; } = [];
    public ImmutableArray<FluentMethod> FluentMethods { get; set; } = [];
}
