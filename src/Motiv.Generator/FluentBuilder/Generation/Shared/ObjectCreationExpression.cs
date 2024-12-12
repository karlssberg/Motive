using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class ObjectCreationExpression
{
    public static ObjectCreationExpressionSyntax CreateTargetTypeActivationExpression(
        INamedTypeSymbol constructorType,
        IEnumerable<IdentifierNameSyntax> constructorArguments)
    {
        var arguments = constructorArguments
            .Select(SyntaxFactory.Argument)
            .InterleaveWith(SyntaxFactory.Token(SyntaxKind.CommaToken));

        TypeSyntax name = constructorType.TypeArguments.Length == 0
            ? SyntaxFactory.IdentifierName(constructorType.Name)
            : SyntaxFactory.GenericName(SyntaxFactory.Identifier(constructorType.Name))
                .WithTypeArgumentList(
                    SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(
                        constructorType.TypeArguments
                            .Select(t => SyntaxFactory.IdentifierName(t.Name)))));

        return SyntaxFactory.ObjectCreationExpression(name)
            .WithNewKeyword(SyntaxFactory.Token(SyntaxKind.NewKeyword))
            .WithArgumentList(SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList<ArgumentSyntax>(arguments)));
    }
}
