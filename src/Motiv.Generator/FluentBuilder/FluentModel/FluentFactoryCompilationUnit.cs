using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.FluentModel.Methods;
using Motiv.Generator.FluentBuilder.FluentModel.Steps;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentFactoryCompilationUnit(
    INamedTypeSymbol RootType,
    ImmutableArray<IFluentMethod> FluentMethods,
    ImmutableArray<IFluentStep> FluentSteps,
    ImmutableArray<INamespaceSymbol> Usings)
{
    public string Name { get; } = RootType.Name;

    public string Namespace { get; } = RootType.ContainingNamespace.ToDisplayString();

    public ImmutableArray<IFluentMethod> FluentMethods { get; set; } = FluentMethods;

    public ImmutableArray<IFluentStep> FluentSteps { get; set; } = FluentSteps;

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
