using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Shared;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Root;

public static class FluentRootStepMethodDeclaration
{
    public static MethodDeclarationSyntax Create(
        FluentBuilderMethod method)
    {
        var returnObjectExpression = FluentStepCreationExpression.Create(
            method,
            ArgumentList(SeparatedList<ArgumentSyntax>(
            [
                Argument(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase()))
            ])));

        var methodDeclaration =  MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                ParameterList(SingletonSeparatedList(
                    Parameter(
                            Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithModifiers(TokenList(Token(SyntaxKind.InKeyword)))
                        .WithType(
                            IdentifierName(method.SourceParameterSymbol.Type.ToString()))))
            )
            .WithBody(Block(ReturnStatement(returnObjectExpression)));

        if (!method.SourceParameterSymbol.Type.ContainsGenericTypeParameter())
            return methodDeclaration;

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type.GetGenericTypeParameters();

        return methodDeclaration
            .WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }
}
