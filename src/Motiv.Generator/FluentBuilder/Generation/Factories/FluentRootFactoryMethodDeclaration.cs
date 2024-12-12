using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using ObjectCreationExpression = Motiv.Generator.FluentBuilder.Generation.Shared.ObjectCreationExpression;

namespace Motiv.Generator.FluentBuilder.Generation.Factories;

public static class FluentRootFactoryMethodDeclaration
{
    public static MethodDeclarationSyntax Create(
        FluentBuilderMethod method,
        IMethodSymbol constructor)
    {
        var returnObjectExpression = ObjectCreationExpression.CreateTargetTypeActivationExpression(
            constructor.ContainingType,
            method.ConstructorParameters.Select(p => IdentifierName(p.Name.ToParameterFieldName()))
                .Append(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())));

        var methodDeclaration = MethodDeclaration(
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
                            IdentifierName(method.SourceParameterSymbol.Type.ToString())))))
            .WithBody(Block(ReturnStatement(returnObjectExpression)));

        if (!method.SourceParameterSymbol.Type.ContainsGenericTypeParameter())
            return methodDeclaration;

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type.GetGenericTypeParameters();

        return methodDeclaration
            .WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }
}
