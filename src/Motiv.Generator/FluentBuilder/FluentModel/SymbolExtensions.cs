using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public static class SymbolExtensions
{
    public static string GetFluentMethodName(this IParameterSymbol parameterSymbol)
    {
        var attribute = parameterSymbol.GetAttributes().FirstOrDefault(a => a.AttributeClass?.Name == "FluentMethodAttribute");
        if (attribute is not null)
        {
            return attribute.ConstructorArguments[0].Value?.ToString() ?? string.Empty;
        }

        return parameterSymbol.Name.Capitalize();
    }
}
