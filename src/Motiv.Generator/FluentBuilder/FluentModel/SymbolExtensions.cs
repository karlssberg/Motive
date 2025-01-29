﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.Generation;
using static Motiv.Generator.FluentBuilder.TypeName;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public static class SymbolExtensions
{
    public static bool IsPartial(this ITypeSymbol typeSymbol)
    {
        return typeSymbol.DeclaringSyntaxReferences
            .Select(r => r.GetSyntax())
            .OfType<TypeDeclarationSyntax>()
            .Any(c => c.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)));
    }

    public static string GetFluentMethodName(this IParameterSymbol parameterSymbol)
    {
        var regularMethodAttribute = parameterSymbol.GetAttribute(FluentMethodAttribute);
        var multipleMethodAttribute = parameterSymbol.GetAttribute(MultipleFluentMethodsAttribute);

        var name = (regularMethodAttribute, mutlipleMethodAttribute: multipleMethodAttribute) switch
        {
            ({ ConstructorArguments: { Length: 1 } args }, _) when args.First().Value is not null =>
                args.First().Value!.ToString(),
            (_, { ConstructorArguments: { Length: 1 } args }) when args.First().Value is INamedTypeSymbol =>
                args.First().Value!.ToString(),
            _ => $"With{parameterSymbol.Name.Capitalize()}"
        };

        return name;
    }

    public static IEnumerable<IMethodSymbol> GetMultipleFluentMethodSymbols(
        this Compilation compilation,
        IParameterSymbol parameterSymbol)
    {
        var attribute = parameterSymbol.GetAttribute(MultipleFluentMethodsAttribute);

        var methodTemplateClass = attribute?.ConstructorArguments.FirstOrDefault();
        if (methodTemplateClass?.Value is not ITypeSymbol methodTemplateClassSymbol)
            return [];

        return methodTemplateClassSymbol
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Where(method => method.IsStatic)
            .Where(method => compilation.IsAssignable(method.ReturnType.OriginalDefinition, parameterSymbol.Type))
            .Where(method => method.GetAttributes().Any(a =>
                a.AttributeClass?.ToDisplayString() == FluentMethodTemplateAttribute));
    }

    public  static AttributeData? GetAttribute(
        this IParameterSymbol parameterSymbol,
        string fluentMethodName)
    {
        var format = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.None
        );

        return parameterSymbol
            .GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString(format) == fluentMethodName);
    }
}
