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
            a.AttributeClass?.ToDisplayString(format) == "Motiv.Generator.Attributes.FluentMethodAttribute");
        if (attribute is not null)
        {
            return attribute.ConstructorArguments[0].Value?.ToString() ?? string.Empty;
        }

        return $"With{parameterSymbol.Name.Capitalize()}";
    }
}
