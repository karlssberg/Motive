using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentMethod(string Name)
{
    public string Name { get; } = Name;
    public required IParameterSymbol? ParameterType { get; set; }
    public required FluentBuilderStep Return { get; set; }
}
