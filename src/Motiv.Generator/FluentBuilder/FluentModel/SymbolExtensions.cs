using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder.Analysis;
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
        if (attribute is not null)
        {
            return attribute.ConstructorArguments[0].Value?.ToString() ?? string.Empty;
        }

        return $"With{parameterSymbol.Name.Capitalize()}";
    }

    public static ImmutableArray<FluentMethodContext> ToFluentMethodContexts(
        this IMethodSymbol constructor,
        Compilation compilation)
    {
        return
        [
            ..constructor.Parameters.ToFluentMethodContexts(compilation)
        ];
    }

    public static ImmutableArray<FluentMethodContext> ToFluentMethodContexts(
        this IEnumerable<IParameterSymbol> source,
        Compilation compilation) =>
        source
            .Aggregate(ImmutableArray<FluentMethodContext>.Empty, (accumulator, parameter) =>
                accumulator.Add(new FluentMethodContext(accumulator, parameter, compilation)));
}
