using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public static class SymbolExtensions
{
    public static string GetFluentMethodName(this IParameterSymbol parameterSymbol)
    {
        var format = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.None
        );

        var attribute = parameterSymbol.GetAttributes().FirstOrDefault(a =>
            a.AttributeClass?.ToDisplayString(format) == TypeName.FluentMethodAttribute);
        if (attribute is { ConstructorArguments.Length: 1 })
        {
            return attribute.ConstructorArguments.First().Value?.ToString() ?? string.Empty;
        }

        return $"With{parameterSymbol.Name.Capitalize()}";
    }
}
