using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.FluentModel;

namespace Motiv.Generator.FluentBuilder.Analysis;

public record FluentConstructorContext
{
    public FluentConstructorContext(
        string @namespace,
        IMethodSymbol constructor,
        ISymbol? symbol,
        FluentFactoryMetadata metadata)
    {
        NameSpace = @namespace;
        Constructor = constructor;
        Options = metadata.Options;
        RootTypeFullName = metadata.RootTypeFullName;
        FluentMethodContexts =
        [
            ..Constructor?.Parameters.ToFluentMethodContexts() ?? []
        ];

        IsStatic = symbol switch
        {
            IMethodSymbol method => method.IsStatic,
            INamedTypeSymbol type => type.IsStatic,
            _ => true
        };

        IsRecord = symbol switch
        {
            IMethodSymbol method => method.ContainingType.IsRecord,
            INamedTypeSymbol type => type.IsRecord,
            _ => false
        };

        TypeKind =  symbol switch
        {
            IMethodSymbol method => method.ContainingType.TypeKind,
            INamedTypeSymbol type => type.TypeKind,
            _ => TypeKind.Class
        };

        Accessibility = symbol?.DeclaredAccessibility ?? Accessibility.Public;
    }

    public FluentFactoryGeneratorOptions Options { get; set; }

    public bool IsRecord { get; set; }

    public Accessibility Accessibility { get; set; }

    public bool IsStatic { get; set; }
    public TypeKind TypeKind { get; set; }
    public string NameSpace { get; set; }
    public IMethodSymbol Constructor { get; set; }
    public string RootTypeFullName { get; set; }
    public ImmutableArray<FluentMethodContext> FluentMethodContexts { get; set; }

}
