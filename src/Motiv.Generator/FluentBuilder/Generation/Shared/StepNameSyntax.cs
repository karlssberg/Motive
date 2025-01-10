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

        var name = step.ExistingStepConstructor?.ContainingType.ToDynamicDisplayString(rootTypeContainingNamespace);
        return name is not null
            ? ParseName(name)
            : CreateNameSyntax(step, rootTypeContainingNamespace);
    }

    private static NameSyntax CreateNameSyntax(FluentStep step, INamespaceSymbol rootNamespace)
    {
        var distinctGenericParameters = step.GenericConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeArguments())
            .DistinctBy(symbol => symbol.ToDynamicDisplayString(rootNamespace))
            .ToArray();

        return distinctGenericParameters.Length > 0
            ? GenericName(Identifier(step.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        distinctGenericParameters
                            .Select(arg => IdentifierName(arg.ToDynamicDisplayString(rootNamespace)))))
                )
            : IdentifierName(step.Name);
    }
}
