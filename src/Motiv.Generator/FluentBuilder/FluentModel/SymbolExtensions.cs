using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;
using static Motiv.Generator.FluentBuilder.TypeName;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public static class SymbolExtensions
{
    public static string GetFluentMethodName(this IParameterSymbol parameterSymbol)
    {
        var attribute = parameterSymbol.GetAttribute(FluentMethodAttribute);
        if (attribute is { ConstructorArguments.Length: 1 })
        {
            return attribute.ConstructorArguments.First().Value?.ToString() ?? string.Empty;
        }

        return $"With{parameterSymbol.Name.Capitalize()}";
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

    private static AttributeData? GetAttribute(
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
