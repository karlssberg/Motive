using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;

namespace Motiv.Generator.FluentBuilder.Analysis;

public static class ConstructorAnalyzer
{
    public static IReadOnlyDictionary<IParameterSymbol, IPropertySymbol?> FindStoringMembers(
        IMethodSymbol constructor,
        SemanticModel semanticModel)
    {
        var results = new Dictionary<IParameterSymbol, IPropertySymbol?>(FluentParameterComparer.Default);

        foreach (var parameter in constructor.Parameters)
        {
            results[parameter] = null;
        }

        var containingType = constructor.ContainingType;

        // For records, primary constructor parameters automatically become properties
        if (containingType.IsRecord)
        {
            foreach (var parameter in constructor.Parameters)
            {
                results[parameter] = containingType.GetMembers()
                    .OfType<IPropertySymbol>()
                    .FirstOrDefault(p => p.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase));
            }
            return results;
        }

        // For classes with primary constructors, look for property declarations
        var syntaxNode = constructor.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax();
        if (syntaxNode is TypeDeclarationSyntax { ParameterList: not null})
        {
            FindPropertiesInitializedFromParameters(containingType, results, semanticModel);
            return results;
        }

        // For explicit constructors, analyze the constructor body
        if (syntaxNode is not ConstructorDeclarationSyntax ctorSyntax) return results;

        // Analyze constructor body for assignments
        if (ctorSyntax.Body != null)
        {
            var assignments = ctorSyntax.Body
                .DescendantNodes()
                .OfType<AssignmentExpressionSyntax>();

            foreach (var assignment in assignments)
            {
                var rightSymbol = semanticModel.GetSymbolInfo(assignment.Right).Symbol;
                if (rightSymbol is not IParameterSymbol paramSymbol ||
                    !results.ContainsKey(paramSymbol)) continue;

                if (semanticModel.GetSymbolInfo(assignment.Left).Symbol is IPropertySymbol propertySymbol)
                {
                    results[paramSymbol] = propertySymbol;
                }
            }
        }

        // Look for property/field initializations in constructor initializer
        var initializer = ctorSyntax.Initializer;
        if (initializer == null) return results;
        var initializerMethod = (IMethodSymbol?)semanticModel.GetSymbolInfo(initializer).Symbol;
        if (initializerMethod == null) return results;
        var baseResults = FindStoringMembers(initializerMethod, semanticModel);

        for (var i = 0; i < initializer.ArgumentList.Arguments.Count; i++)
        {
            var argument = initializer.ArgumentList.Arguments[i];
            if (semanticModel.GetSymbolInfo(argument.Expression).Symbol is not IParameterSymbol parameterSymbol ||
                !results.ContainsKey(parameterSymbol)) continue;

            if (i >= initializerMethod.Parameters.Length) continue;

            var baseParam = initializerMethod.Parameters[i];
            if (baseResults.TryGetValue(baseParam, out var baseProperty))
            {
                results[parameterSymbol] = baseProperty;
            }
        }

        return results;
    }

    private static void  FindPropertiesInitializedFromParameters(
        INamedTypeSymbol type,
        Dictionary<IParameterSymbol, IPropertySymbol?> results,
        SemanticModel semanticModel)
    {
        foreach (var property in type.GetMembers().OfType<IPropertySymbol>())
        {
            if (property.DeclaringSyntaxReferences
                    .FirstOrDefault()?
                    .GetSyntax()
                is not PropertyDeclarationSyntax propSyntax) continue;

            if (propSyntax.Initializer?.Value is not IdentifierNameSyntax identifier) continue;

            var symbol = semanticModel.GetSymbolInfo(identifier).Symbol;
            if (symbol is IParameterSymbol paramSymbol &&
                results.ContainsKey(paramSymbol))
            {
                results[paramSymbol] = property;
            }
        }
    }
}
