using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.FluentModel;

namespace Motiv.Generator.FluentBuilder;

public class FluentModelGenerator
{

    public ImmutableArray<FluentBuilderRoot> GetFluentBuilders(
        ImmutableDictionary<string, ImmutableArray<FluentBuilderContext>> fluentBuilderContexts)
    {
        return
        [
            ..fluentBuilderContexts.Select(VisitFluentConstructor)
        ];
    }

    private FluentBuilderRoot VisitFluentConstructor(KeyValuePair<string, ImmutableArray<FluentBuilderContext>> kvp)
    {
        var constructorParameterGroups = kvp.Value
            .SelectMany(fbc => fbc.Constructor.Parameters)
            .GroupBy(p => p, SymbolEqualityComparer.Default);

        foreach (var constructorParameterGroup in constructorParameterGroups)
        {
            var fluentStep = new FluentBuilderStep(CreateMethodDeclaration(constructorParameterGroup))
            {
                ParameterType = constructorParameterGroup.,
                Attributes = constructorParameterGroup.First().GetAttributes(),
                FluentMethods =
                [
                    ..constructorParameterGroup
                        .Select(VisitFluentConstructorParameter)
                ]
            };
        }

        return new FluentBuilderRoot(kvp.Key)
        {
            NameSpace = kvp.Value.First().NameSpace,
            FluentMethods = constructorParameter
                .Aggregate((prevParameter, parameter) =>
                    new FluentBuilderMethod(parameter.Name)
                    {
                        ParameterType = ,
                        Return = VisitFluentBuilderStep
                    });
        };
    }

    private FluentBuilderMethod VisitFluentConstructorParameter(IParameterSymbol parameter)
    {
        return ;
    }

    private string CreateMethodDeclaration(string Name, IParameterSymbol parameterSymbol)
    {
        var typeSymbol = parameterSymbol.Type;

        // If it's a type parameter (like T), we need a generic method
        if (typeSymbol is ITypeParameterSymbol)
        {
            var format = new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters);

            // Will output something like: "void Method<T>(T param)"
            return $"public void {Name}<{typeSymbol.ToDisplayString(format)}>({typeSymbol.ToDisplayString(format)} {parameterSymbol.Name})";
        }
        else
        {
            var format = new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

            // Will output something like: "void Method(string param)"
            return $"public void {Name}({typeSymbol.ToDisplayString(format)} {parameterSymbol.Name})";
        }
    }

    private string CreateTypeDeclaration(INamedTypeSymbol typeSymbol)
    {
        var originalType = typeSymbol.OriginalDefinition;

        return originalType.ToDisplayString(new SymbolDisplayFormat(
            kindOptions: SymbolDisplayKindOptions.IncludeTypeKeyword,
            memberOptions: SymbolDisplayMemberOptions.IncludeModifiers,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters |
                             SymbolDisplayGenericsOptions.IncludeTypeConstraints));
    }

}
