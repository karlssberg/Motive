using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentBuilderFile(
    string FullName,
    ImmutableArray<FluentBuilderMethod> FluentMethods,
    ImmutableArray<FluentBuilderStep> FluentSteps,
    ImmutableArray<INamespaceSymbol> Usings)
{
    public string Name { get; } = FullName.Substring(FullName.LastIndexOf('.') + 1);
    public string NameSpace { get; } = FullName.Substring(0, FullName.LastIndexOf('.'));
    public ImmutableArray<FluentBuilderMethod> FluentMethods { get; set; } = FluentMethods;
    public ImmutableArray<FluentBuilderStep> FluentSteps { get; set; } = FluentSteps;

    public string FullName { get; } = FullName;

    public ImmutableArray<INamespaceSymbol> Usings { get; set; } = Usings;
}
