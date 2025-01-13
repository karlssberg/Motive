
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;

namespace Motiv.Generator.FluentBuilder.Analysis;

public class ConstructorAnalyzer(SemanticModel semanticModel)
{
    public IReadOnlyDictionary<IParameterSymbol, FluentParameterResolution> FindStoringMembers(IMethodSymbol constructor)
    {
        var results =
            constructor.Parameters.ToDictionary(
                p => p,
                _ => new FluentParameterResolution(),
                FluentParameterComparer.Default);

        var containingType = constructor.ContainingType;

        // For records, primary constructor parameters automatically become properties
        if (containingType.IsRecord)
        {
            foreach (var parameter in constructor.Parameters)
            {
                results[parameter] = new FluentParameterResolution(containingType.GetMembers()
                    .OfType<IPropertySymbol>()
                    .FirstOrDefault(p => p.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase)));
            }
            return results;
        }

        // For classes with primary constructors, look for property declarations
        var syntaxNode = constructor.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax();
        if (syntaxNode is TypeDeclarationSyntax { ParameterList: not null})
        {
            PopulatedWithPrimaryConstructorParameterReferences(constructor, results);
            return results;
        }

        // For explicit constructors, analyze the constructor body
        if (syntaxNode is not ConstructorDeclarationSyntax ctorSyntax) return results;

        // Analyze constructor body for assignments
        if (ctorSyntax.Body is not null)
        {
            PopulateWithRelevantFieldAndPropertyReferences(ctorSyntax, results);
        }

        // Look for property/field initializations in constructor initializer (i.e. : base() and : this())
        var initializer = ctorSyntax.Initializer;
        if (initializer == null) return results;
        var initializerMethod = (IMethodSymbol?)semanticModel.GetSymbolInfo(initializer).Symbol;
        if (initializerMethod == null) return results;
        var baseResults = FindStoringMembers(initializerMethod);

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

    private void PopulateWithRelevantFieldAndPropertyReferences(
        ConstructorDeclarationSyntax ctorSyntax,
        Dictionary<IParameterSymbol,
        FluentParameterResolution> results)
    {
        var assignments = ctorSyntax.Body?
            .DescendantNodes()
            .OfType<AssignmentExpressionSyntax>();

        foreach (var assignment in assignments ?? [])
        {
            var rightSymbol = semanticModel.GetSymbolInfo(assignment.Right).Symbol;
            if (rightSymbol is not IParameterSymbol paramSymbol ||
                !results.ContainsKey(paramSymbol)) continue;

            var memberSymbol = semanticModel.GetSymbolInfo(assignment.Left).Symbol;
            if (memberSymbol is IPropertySymbol or IFieldSymbol)
            {
                results[paramSymbol] = new FluentParameterResolution(
                    memberSymbol as IPropertySymbol,
                    memberSymbol as IFieldSymbol);
            }
        }
    }

    private void  PopulatedWithPrimaryConstructorParameterReferences(
        IMethodSymbol constructor,
        Dictionary<IParameterSymbol, FluentParameterResolution> results)
    {
        PopulateWithMembersInitializedFromPrimaryConstructors(constructor, results);

        foreach (var parameter in constructor.Parameters)
        {
            if (results.TryGetValue(parameter, out var resolution)
                && resolution is { FieldSymbol: not null } or { PropertySymbol: not null })
            {
                continue;
            }

            results[parameter] = new FluentParameterResolution
            {
                ShouldInitializeFromPrimaryConstructor = true
            };
        }
    }

    private void PopulateWithMembersInitializedFromPrimaryConstructors(
        IMethodSymbol primaryConstructor,
        Dictionary<IParameterSymbol,
        FluentParameterResolution> result)
    {
        foreach (var member in primaryConstructor.ContainingType.GetMembers())
        {
            switch (member)
            {
                case IFieldSymbol fieldSymbol:
                {
                    var initializer = GetInitializerSyntax(fieldSymbol);
                    if (initializer != null)
                    {
                        foreach (var parameter in primaryConstructor.Parameters)
                        {
                            var isInitialized = IsInitializedFromParameter(initializer, parameter);
                            if (isInitialized)
                            {
                                result[parameter] = new FluentParameterResolution(FieldSymbol: fieldSymbol);
                            }
                        }
                    }

                    break;
                }
                case IPropertySymbol propertySymbol:
                {
                    var initializer = GetInitializerSyntax(propertySymbol);
                    if (initializer != null)
                    {
                        foreach (var parameter in primaryConstructor.Parameters)
                        {
                            var isInitialized = IsInitializedFromParameter(initializer, parameter);
                            if (isInitialized)
                            {
                                result[parameter] = new FluentParameterResolution(PropertySymbol: propertySymbol);
                            }
                        }
                    }

                    break;
                }
            }
        }

        var parametersRequiringStorageFields = primaryConstructor.Parameters
            .Where(parameter => result.TryGetValue(parameter, out var resolution)
                                && resolution is { FieldSymbol: null, PropertySymbol: null });

        foreach (var parameter in parametersRequiringStorageFields)
        {
            result[parameter] = new FluentParameterResolution
            {
                ShouldInitializeFromPrimaryConstructor = true
            };
        }
    }

    private bool IsInitializedFromParameter(ExpressionSyntax initializer, IParameterSymbol parameter)
    {
        // Check if initializer is a simple reference to the parameter
        if (initializer is not IdentifierNameSyntax identifier)
            return false;

        var symbolInfo = semanticModel.GetSymbolInfo(identifier);
        return SymbolEqualityComparer.Default.Equals(symbolInfo.Symbol, parameter);
    }

    private static ExpressionSyntax? GetInitializerSyntax(ISymbol symbol)
    {
        var declaringSyntax = symbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax();

        return symbol switch
        {
            IFieldSymbol when declaringSyntax is VariableDeclaratorSyntax fieldDeclarator =>
                fieldDeclarator.Initializer?.Value,
            IPropertySymbol when declaringSyntax is PropertyDeclarationSyntax propertyDeclaration =>
                propertyDeclaration.Initializer?.Value,
            _ => null
        };
    }
}
