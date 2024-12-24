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

    public static IEnumerable<TypeParameterSyntax> GetGenericTypeParameters(this ITypeSymbol type)
    {
        IEnumerable<ITypeSymbol> types = [type];

        return types.GetGenericTypeParameters()
            .Select(ToTypeParameterSyntax);
    }

    public static IEnumerable<ITypeParameterSymbol> GetGenericTypeParameters(this IEnumerable<ITypeSymbol> type)
    {
        return type
            .SelectMany(symbol => symbol.GetGenericTypeParametersInternal())
            .DistinctBy(symbol => symbol.ToDisplayString());
    }

    public static TypeParameterSyntax ToTypeParameterSyntax(this ITypeParameterSymbol typeParameter)
    {
        return SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(typeParameter.Name));
    }

    private static IEnumerable<ITypeParameterSymbol> GetGenericTypeParametersInternal(this ITypeSymbol type)
    {
        return type switch
        {
            ITypeParameterSymbol typeParameter => [typeParameter],
            INamedTypeSymbol namedType => namedType.TypeArguments
                .SelectMany(t => t.GetGenericTypeParametersInternal()),
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
                    foreach (var constraint in targetParam.ConstraintTypes)
                    {
                        var paramConversion = compilation.ClassifyCommonConversion(sourceParam, constraint);
                        if (!paramConversion.Exists)
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
                foreach (var constraint in sourceParam.ConstraintTypes)
                {
                    var typeConversion = compilation.ClassifyCommonConversion(constraint, argumentType);
                    if (typeConversion.Exists && typeConversion.IsImplicit)
                        return true;
                }

                // Check special constraints against target
                if (sourceParam.HasValueTypeConstraint && !argumentType.IsValueType)
                    return false;

                return !sourceParam.HasReferenceTypeConstraint || argumentType.IsReferenceType;
            }
            default:
                // Fall back to checking conversion
                var conversion = compilation.ClassifyCommonConversion(parameterType, argumentType);
                return conversion is { Exists: true, IsImplicit: true };
        }
    }
}
