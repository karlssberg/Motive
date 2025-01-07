using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class StepNameSyntax
{
    public static NameSyntax Create(FluentStep step)
    {
        var rootTypeContainingNamespace = step.RootType.ContainingNamespace;
        var distinctGenericParameters = step.GenericConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeArguments())
            .DistinctBy(symbol => symbol.ToDynamicDisplayString(rootTypeContainingNamespace))
            .ToArray();

        var name = step.ExistingStepConstructor?.ContainingType.ToDynamicDisplayString(rootTypeContainingNamespace);
        return CreateNameSyntax(name ?? step.Name, distinctGenericParameters, rootTypeContainingNamespace);
    }

    private static NameSyntax CreateNameSyntax(string name, ICollection<ITypeSymbol> distinctGenericParameters, INamespaceSymbol rootNamespace)
    {
        return distinctGenericParameters.Count > 0
            ? GenericName(Identifier(name))
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        distinctGenericParameters
                            .Select(arg => IdentifierName(arg.ToDynamicDisplayString(rootNamespace)))))
                )
            : IdentifierName(name);
    }
}
