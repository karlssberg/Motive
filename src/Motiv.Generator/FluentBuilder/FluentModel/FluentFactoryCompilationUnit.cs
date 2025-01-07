using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentFactoryCompilationUnit(
    INamedTypeSymbol RootType,
    ImmutableArray<FluentMethod> FluentMethods,
    ImmutableArray<FluentStep> FluentSteps,
    ImmutableArray<INamespaceSymbol> Usings)
{
    public string Name { get; } = RootType.Name;

    public string Namespace { get; } = RootType.ContainingNamespace.ToDisplayString();

    public ImmutableArray<FluentMethod> FluentMethods { get; set; } = FluentMethods;

    public ImmutableArray<FluentStep> FluentSteps { get; set; } = FluentSteps;

    public INamedTypeSymbol RootType { get; } = RootType;

    public ImmutableArray<INamespaceSymbol> Usings { get; set; } = Usings;

    public TypeKind TypeKind { get; set; }

    public Accessibility Accessibility { get; set; }

    public bool IsStatic { get; set; } = true;

    public bool IsRecord { get; set; }

    private static string GetName(string fullName)
    {
        var lastIndexOf = fullName.LastIndexOf('.');

        return lastIndexOf == -1
            ? fullName
            : fullName.Substring(lastIndexOf + 1);
    }

    private static string GetNamespace(string fullName)
    {
        var lastIndexOf = fullName.LastIndexOf('.');

        return lastIndexOf == -1
            ? string.Empty
            : fullName.Substring(0, lastIndexOf);
    }
}
