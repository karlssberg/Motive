using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class TargetTypeObjectCreationExpression
{
    public static ObjectCreationExpressionSyntax Create(
        INamedTypeSymbol constructorType,
        IEnumerable<IdentifierNameSyntax> constructorArguments)
    {
        var arguments = constructorArguments
            .Select(Argument)
            .InterleaveWith(Token(SyntaxKind.CommaToken));

        TypeSyntax name = constructorType.TypeArguments.Length == 0
            ? IdentifierName(constructorType.Name)
            : GenericName(Identifier(constructorType.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        constructorType.TypeArguments
                            .Select(t => IdentifierName(t.Name)))));

        return ObjectCreationExpression(name)
            .WithNewKeyword(Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(arguments)));
    }
}
