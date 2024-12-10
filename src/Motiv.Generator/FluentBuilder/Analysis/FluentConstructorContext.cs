using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.Analysis;

public record FluentConstructorContext
{
    private readonly Lazy<ImmutableArray<FluentMethodContext>> _lazyFluentMethods;

    public FluentConstructorContext(string @namespace, IMethodSymbol constructor, string rootTypeFullName,
        ISymbol? symbol)
    {
        NameSpace = @namespace;
        Constructor = constructor;
        RootTypeFullName = rootTypeFullName;
        _lazyFluentMethods =  new Lazy<ImmutableArray<FluentMethodContext>>(() =>
        [
            ..Constructor?.Parameters
                .Aggregate(ImmutableArray<FluentMethodContext>.Empty,
                    (parameterArray, parameter) =>
                        parameterArray.Add(new FluentMethodContext(parameterArray, parameter)))
            ?? []
        ]);

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

    public bool IsRecord { get; set; }

    public Accessibility Accessibility { get; set; }

    public bool IsStatic { get; set; }
    public TypeKind TypeKind { get; set; }
    public string NameSpace { get; set; }
    public IMethodSymbol Constructor { get; set; }
    public string RootTypeFullName { get; set; }
    public ImmutableArray<FluentMethodContext> FluentMethodContexts => _lazyFluentMethods.Value;

}
