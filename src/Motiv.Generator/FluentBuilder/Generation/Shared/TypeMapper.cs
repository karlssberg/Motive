using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class TypeMapper
{
    public static ImmutableDictionary<ITypeParameterSymbol, ITypeSymbol> MapGenericArguments(ITypeSymbol openType, ITypeSymbol closedType)
    {
        var typeMap = new Dictionary<ITypeParameterSymbol, ITypeSymbol>(SymbolEqualityComparer.Default);
        MapTypes(openType, closedType, typeMap);
        return typeMap.ToImmutableDictionary(SymbolEqualityComparer.Default);
    }

    private static void MapTypes(ITypeSymbol open, ITypeSymbol closed, Dictionary<ITypeParameterSymbol, ITypeSymbol> typeMap)
    {
        switch (open)
        {
            case ITypeParameterSymbol typeParam:
                typeMap[typeParam] = closed;
                break;
            case INamedTypeSymbol openNamed when closed is INamedTypeSymbol closedNamed:
            {
                for (var i = 0; i < openNamed.TypeArguments.Length; i++)
                {
                    MapTypes(openNamed.TypeArguments[i], closedNamed.TypeArguments[i], typeMap);
                }
                break;
            }
        }
    }

    public static IMethodSymbol NormalizeMethodTypeParameters(
        this IMethodSymbol method,
        ImmutableDictionary<ITypeParameterSymbol, ITypeSymbol> substitutions)
    {
        // If no type parameters, return as-is
        if (!method.IsGenericMethod)
            return method;

        // Substitute type parameters in parameter types
        var newParameters = method.Parameters.Select(p =>
            p.Type.ReplaceTypeParameters(substitutions));

        // Substitute in return type
        var newReturnType = method.ReturnType.ReplaceTypeParameters(substitutions);

        // Construct new method type
        return method.Construct(substitutions.Values.ToArray());
    }

    // Extension method to help with substitution
    public static ITypeSymbol ReplaceTypeParameters(
        this ITypeSymbol type,
        ImmutableDictionary<ITypeParameterSymbol, ITypeSymbol> substitutions)
    {
        if (type is ITypeParameterSymbol typeParam &&
            substitutions.TryGetValue(typeParam, out var replacement))
        {
            return replacement;
        }

        if (type is INamedTypeSymbol namedType && namedType.IsGenericType)
        {
            var newTypeArgs = namedType.TypeArguments
                .Select(t => t.ReplaceTypeParameters(substitutions))
                .ToArray();
            return namedType.Construct(newTypeArgs);
        }

        return type;
    }
}
