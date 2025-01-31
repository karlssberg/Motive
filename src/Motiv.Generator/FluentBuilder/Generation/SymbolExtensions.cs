using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.Attributes;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class SymbolExtensions
{
    private static readonly SymbolDisplayFormat TypeNameOnlyFormat = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters
    );

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
                .SelectMany(typeArg => typeArg.GetGenericTypeParameters())
                .Distinct<ITypeParameterSymbol>(SymbolEqualityComparer.Default),
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

    public static bool IsAssignable(this Compilation compilation, ITypeSymbol? parameterType, ITypeSymbol? argumentType)
    {
        if (parameterType is null || argumentType is null)
            return false;

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
            case INamedTypeSymbol paramNamedType:
            {
                // Handle delegate variance
                if (argumentType is not INamedTypeSymbol argNamedType)
                    return false;

                // Check if they have the same number of type arguments
                if (paramNamedType.TypeArguments.Length != argNamedType.TypeArguments.Length)
                    return false;
                // All other parameters are contravariant
                for (var i = 0; i < paramNamedType.TypeArguments.Length; i++)
                {
                    var paramTypeArg = paramNamedType.TypeArguments[i];
                    var argTypeArg = argNamedType.TypeArguments[i];

                    if (!compilation.IsAssignable(paramTypeArg, argTypeArg))
                        return false;
                }
                return true;
            }
            default:
                // Fall back to checking conversion
                var conversion = compilation.ClassifyCommonConversion(parameterType, argumentType);
                return conversion is { Exists: true, IsImplicit: true };
        }
    }

    public static IEnumerable<ITypeParameterSymbol> Union(
       this IEnumerable<ITypeParameterSymbol> first,
       IEnumerable<ITypeParameterSymbol> second)
   {
       return first
           .Union<ITypeParameterSymbol>(second, SymbolEqualityComparer.IncludeNullability)
           .OrderBy(symbol => symbol.Name);
   }


    public static IEnumerable<ITypeParameterSymbol> Except(
       this IEnumerable<ITypeParameterSymbol> collection,
       IEnumerable<ITypeParameterSymbol> exclusions)
   {
       var exclusionSet = new HashSet<string>(exclusions.Select(parameter => parameter.ToDisplayString()));

       foreach (var item in collection)
       {
          if (!exclusionSet.Contains(item.ToDisplayString()))
            yield return item;
       }
   }

   public static IEnumerable<SyntaxKind> AccessibilityToSyntaxKind(this Accessibility accessibility) =>
       accessibility switch
       {
           Accessibility.Public => [SyntaxKind.PublicKeyword],
           Accessibility.Private => [SyntaxKind.PrivateKeyword],
           Accessibility.Protected => [SyntaxKind.ProtectedKeyword],
           Accessibility.Internal => [SyntaxKind.InternalKeyword],
           Accessibility.ProtectedOrInternal => [SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword], // Note: This will need both protected and internal
           Accessibility.ProtectedAndInternal => [SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword],
           _ => [SyntaxKind.None]
       };


   private static readonly SymbolDisplayFormat NameFormat = new(
         typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
         genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
         miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
    );

   public static string ToFullyQualifiedDisplayString(this ITypeSymbol typeSymbol)
   {
       return typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
   }

   public static string ToUnqualifiedDisplayStriong(this ITypeSymbol typeSymbol) =>
       typeSymbol.ToDisplayString(TypeNameOnlyFormat);


   public static string ToDynamicDisplayString(this ITypeSymbol typeSymbol, INamespaceSymbol currentNamespace)
   {
       switch (typeSymbol)
       {
           case null:
               return string.Empty;
           // Handle named type symbols (regular classes, structs, interfaces)
           case INamedTypeSymbol namedType:
           {
               // Get the base name without namespace
               var baseName = GetBaseTypeName(namedType, currentNamespace);

               // If it's not a generic type, return the base name
               if (!namedType.IsGenericType)
                   return baseName;

               // Handle generic type parameters
               var typeArguments = namedType.TypeArguments;
               if (!typeArguments.Any())
                   return baseName;

               // Process each type argument recursively
               var processedTypeArgs = typeArguments.Select(arg => arg.ToDynamicDisplayString(currentNamespace));

               // Combine the base name with processed type arguments
               return $"{baseName.Split('<')[0]}<{string.Join(", ", processedTypeArgs)}>";
           }
           // Handle array types
           case IArrayTypeSymbol arrayType:
               return $"{arrayType.ElementType.ToDynamicDisplayString(currentNamespace)}[]";
           default:
               // Handle other type symbols (like type parameters)
               return typeSymbol.Name;
       }
   }

   private static string GetBaseTypeName(INamedTypeSymbol typeSymbol, INamespaceSymbol currentNamespace)
   {
       var fullName = typeSymbol.ContainingType != null
           ? $"{typeSymbol.ContainingType.ToDynamicDisplayString(currentNamespace)}.{typeSymbol.Name}"
           : typeSymbol.ToDisplayString();

       var namespaceName = currentNamespace.ToDisplayString();

       // Remove namespace prefix if it matches current namespace
       return typeSymbol.ToDisplayString().StartsWith(namespaceName)
           ? fullName.Substring(namespaceName.Length).TrimStart('.')
           : fullName;
   }
}
