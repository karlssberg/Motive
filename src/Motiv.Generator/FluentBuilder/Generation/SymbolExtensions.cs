using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class SymbolExtensions
{
    public static bool IsOpenGenericType(this IParameterSymbol parameter)
    {
        return parameter.Type switch
        {
            ITypeParameterSymbol => true,
            INamedTypeSymbol namedType => namedType.TypeArguments.Any(t => t is ITypeParameterSymbol) ||
                                          // Also check if this is a generic type definition itself
                                          namedType.IsUnboundGenericType,
            _ => false
        };
    }

    public static bool IsOpenGenericType(this ITypeSymbol type)
    {
        return type switch
        {
            ITypeParameterSymbol => true,
            INamedTypeSymbol namedType => namedType.TypeArguments.Any(t => t is ITypeParameterSymbol) ||
                                          // Also check if this is a generic type definition itself
                                          namedType.IsUnboundGenericType,
            _ => false
        };
    }

    public static bool ContainsGenericTypeParameter(this ITypeSymbol type)
    {
        return type switch
        {
            ITypeParameterSymbol => true,
            INamedTypeSymbol namedType => namedType.TypeArguments.Any(t => t.ContainsGenericTypeParameter()),
            _ => false
        };
    }

    public static IEnumerable<TypeParameterSyntax> GetGenericTypeParameters(this ITypeSymbol type)
    {
        return type switch
        {
            ITypeParameterSymbol typeParameter => [CreateTypeParameterSyntax(typeParameter)],
            INamedTypeSymbol namedType => namedType.TypeArguments.SelectMany(t => t.GetGenericTypeParameters()),
            _ => []
        };

        TypeParameterSyntax CreateTypeParameterSyntax(ITypeParameterSymbol typeParameter)
        {
            return SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(typeParameter.Name));
        }
    }

    public static IEnumerable<ITypeSymbol> GetGenericTypeArguments(this ITypeSymbol type)
    {
        return type switch
        {
            ITypeParameterSymbol typeParameter => [typeParameter],
            INamedTypeSymbol namedType => namedType.TypeArguments.SelectMany(t => t.GetGenericTypeArguments()),
            _ => []
        };
    }
}
