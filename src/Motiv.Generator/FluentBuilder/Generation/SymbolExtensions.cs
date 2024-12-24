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
            INamedTypeSymbol namedType => namedType.TypeArguments.Any(t => t switch
                                          {
                                              ITypeParameterSymbol => true,
                                              INamedTypeSymbol { IsGenericType: true } typeSymbol => typeSymbol.IsOpenGenericType(),
                                              _ => false
                                          }) ||
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
            INamedTypeSymbol namedType => namedType.TypeArguments.Any(t => t switch
                                          {
                                              ITypeParameterSymbol => true,
                                              INamedTypeSymbol { IsGenericType: true } typeSymbol => typeSymbol.IsOpenGenericType(),
                                              _ => false
                                          }) ||
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

    public static IEnumerable<TypeParameterSyntax> GetGenericTypeParameterSyntaxList(this IEnumerable<ITypeSymbol> types)
    {
        return types.GetGenericTypeParameters()
            .Select(ToTypeParameterSyntax);
    }

    public static IEnumerable<TypeParameterSyntax> GetGenericTypeParameterSyntaxList(this ITypeSymbol type)
    {
        return new[] { type }.GetGenericTypeParameterSyntaxList();
    }

    public static IEnumerable<ITypeParameterSymbol> GetGenericTypeParameters(this IEnumerable<ITypeSymbol> type)
    {
        return type
            .SelectMany(symbol => symbol.GetGenericTypeParameters())
            .DistinctBy(symbol => symbol.ToDisplayString());
    }

    public static TypeParameterSyntax ToTypeParameterSyntax(this ITypeParameterSymbol typeParameter)
    {
        return SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(typeParameter.Name));
    }

    public static IEnumerable<ITypeParameterSymbol> GetGenericTypeParameters(this ITypeSymbol type)
    {
        return type switch
        {
            ITypeParameterSymbol typeParameter => [typeParameter],
            INamedTypeSymbol namedType => namedType.TypeArguments
                .SelectMany(t => t.GetGenericTypeParameters()),
            _ => []
        };
    }

    public static IEnumerable<ITypeSymbol> GetGenericTypeArguments(this ITypeSymbol type)
    {
        return GenericTypeArgumentsInternal(type).DistinctBy(type => type.Name);
    }

    private static IEnumerable<ITypeSymbol> GenericTypeArgumentsInternal(ITypeSymbol type)
    {
        return type switch
        {
            ITypeParameterSymbol typeParameter => [typeParameter],
            INamedTypeSymbol namedType => namedType.TypeArguments
                .SelectMany(t => t.GetGenericTypeArguments()),
            _ => []
        };
    }

   public static bool IsAssignable(this Compilation compilation, ITypeSymbol parameterType, ITypeSymbol argumentType)
    {
        // If they're exactly the same type, return true
        if (SymbolEqualityComparer.Default.Equals(parameterType, argumentType))
            return true;

        switch (parameterType)
        {
            // Handle when either is a type parameter
            case ITypeParameterSymbol sourceParam:
            {
                // If target is also a type parameter, check if source satisfies target's constraints
                if (argumentType is ITypeParameterSymbol targetParam)
                {
                    // Check if source type parameter satisfies all target's constraints
                    if (targetParam.ConstraintTypes
                        .Select(constraint => compilation.ClassifyCommonConversion(sourceParam, constraint))
                        .Any(paramConversion => paramConversion is { Exists: false }))
                    {
                        return false;
                    }

                    if (targetParam.HasValueTypeConstraint && !sourceParam.HasValueTypeConstraint)
                        return false;

                    if (targetParam.HasReferenceTypeConstraint && !sourceParam.HasReferenceTypeConstraint)
                        return false;

                    if (targetParam.HasConstructorConstraint && !sourceParam.HasConstructorConstraint)
                        return false;

                    return true;
                }

                // If target is a regular type, check if source's constraints are compatible
                if (sourceParam.ConstraintTypes
                    .Select(constraint => compilation.ClassifyCommonConversion(constraint, argumentType))
                    .Any(typeConversion => typeConversion is { Exists: true, IsImplicit: true }))
                {
                    return true;
                }

                // Check special constraints against target
                if (sourceParam.HasValueTypeConstraint && !argumentType.IsValueType)
                    return false;

                return !sourceParam.HasReferenceTypeConstraint || argumentType.IsReferenceType;
            }
            case INamedTypeSymbol { TypeKind: TypeKind.Delegate } paramNamedType:
            {
                // Handle delegate variance
                if (argumentType is not INamedTypeSymbol { TypeKind: TypeKind.Delegate } argNamedType)
                    return false;

                // Check if they have the same number of type arguments
                if (paramNamedType.TypeArguments.Length != argNamedType.TypeArguments.Length)
                    return false;

                // For Func<>, the return type is covariant (last type parameter)
                // All other parameters are contravariant
                for (int i = 0; i < paramNamedType.TypeArguments.Length; i++)
                {
                    var paramTypeArg = paramNamedType.TypeArguments[i];
                    var argTypeArg = argNamedType.TypeArguments[i];

                    // For the return type (last parameter)
                    if (i == paramNamedType.TypeArguments.Length - 1)
                    {
                        // Covariant - argType must be assignable to paramType
                        if (!compilation.IsAssignable(paramTypeArg, argTypeArg))
                            return false;
                    }
                    else
                    {
                        // Contravariant - paramType must be assignable to argType
                        if (!compilation.IsAssignable(argTypeArg, paramTypeArg))
                            return false;
                    }
                }
                return true;
            }
            default:
                // Fall back to checking conversion
                var conversion = compilation.ClassifyCommonConversion(parameterType, argumentType);
                return conversion is { Exists: true, IsImplicit: true };
        }
    }

   public static IEnumerable<ITypeParameterSymbol> Union(this IEnumerable<ITypeParameterSymbol> first, IEnumerable<ITypeParameterSymbol> second) =>
       first
           .Union(second, SymbolEqualityComparer.IncludeNullability)
           .OfType<ITypeParameterSymbol>()
           .OrderBy(symbol => symbol.Name);
}
