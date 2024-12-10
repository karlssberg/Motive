using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class ParameterSymbolExtensions
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

}
