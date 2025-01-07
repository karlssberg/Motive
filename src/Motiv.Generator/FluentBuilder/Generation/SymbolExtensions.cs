﻿using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.Attributes;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class SymbolExtensions
{
    private static readonly SymbolDisplayFormat FullyQualifiedWithGlobalFormat = new (
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
    );

    private static readonly SymbolDisplayFormat FullyQualifiedFormat = new (
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
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
           .Union(second, SymbolEqualityComparer.IncludeNullability)
           .OfType<ITypeParameterSymbol>()
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

   public static ImmutableArray<IMethodSymbol> GetFluentMethodOverloads(
       this Compilation compilation,
       IParameterSymbol sourceParameter)
   {
       var typeConstant = sourceParameter
           .GetAttributes()
           .Where(attr => attr?.AttributeClass?.ToDisplayString() == TypeName.FluentMethodAttribute)
           .Select(attr => attr.NamedArguments
               .FirstOrDefault(namedArg => namedArg.Key == nameof(FluentMethodAttribute.Overloads))
               .Value)
           .FirstOrDefault();

       // has overloads?
       if (typeConstant.IsNull || typeConstant.Value is not INamedTypeSymbol typeSymbol)
           return ImmutableArray<IMethodSymbol>.Empty;

       return
       [
           ..typeSymbol
               .GetMembers()
               .OfType<IMethodSymbol>()
               .Where(method => method.IsStatic)
               .Where(method => compilation.IsAssignable(method.ReturnType.OriginalDefinition, sourceParameter.Type))
               .Where(method => method.GetAttributes().Any(a =>
                   a.AttributeClass?.ToDisplayString() == TypeName.FluentParameterOverloadAttribute))
       ];
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

   // public static string ToDynamicDisplayString(this ITypeSymbol typeSymbol, INamespaceSymbol namespaceSymbol)
   // {
   //     if (typeSymbol.SpecialType != SpecialType.None)
   //         return typeSymbol.ToDisplayString(NameFormat);
   //     if (typeSymbol is ITypeParameterSymbol typeParameter)
   //         return typeParameter.Name;
   //
   //     var typeSymbolNamespaceParts = typeSymbol.ContainingNamespace.ToDisplayParts();
   //     var namespaceSymbolParts = namespaceSymbol.ToDisplayParts();
   //
   //     var sb = new StringBuilder();
   //     var isCommonPrefixMode = true;
   //     for (var i = 0; i < typeSymbolNamespaceParts.Length; i++)
   //     {
   //         var typePart = i < typeSymbolNamespaceParts.Length ? typeSymbolNamespaceParts[i].ToString() : null;
   //         var namespacePart = i < namespaceSymbolParts.Length ? namespaceSymbolParts[i].ToString() : null;
   //
   //         if (isCommonPrefixMode && typePart == namespacePart)
   //             continue;
   //
   //         isCommonPrefixMode = false;
   //         sb.Append(typePart);
   //
   //     }
   //
   //     var serializedMinimalNamespace = sb.ToString();
   //
   //     return serializedMinimalNamespace == string.Empty
   //         ? typeSymbol.ToDisplayString(NameFormat)
   //         : $"{serializedMinimalNamespace}.{typeSymbol.ToDisplayString(NameFormat)}";
   // }

   private static readonly SymbolDisplayFormat NameFormat = new(
         typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
         genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
         miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
    );

   public static string ToFullyQualifiedDisplayString(this ITypeSymbol typeSymbol)
   {
       return typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
   }

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
