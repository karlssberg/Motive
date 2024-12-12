using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Shared;

namespace Motiv.Generator.FluentBuilder.Generation.Factories;

public static class FluentFactoryMethodDeclaration
{
    public static MethodDeclarationSyntax Create(
        FluentBuilderMethod method,
        FluentBuilderStep step,
        IMethodSymbol constructor)
    {
        var returnObjectExpression = ObjectCreationExpression.CreateTargetTypeActivationExpression(
            constructor.ContainingType,
            method.ConstructorParameters
                .Select(p => SyntaxFactory.IdentifierName(p.Name.ToParameterFieldName()))
                .Append(SyntaxFactory.IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())));

        var methodDeclaration = SyntaxFactory.MethodDeclaration(
                returnObjectExpression.Type,
                SyntaxFactory.Identifier(method.MethodName))
            .WithModifiers(
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                SyntaxFactory.ParameterList(SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Parameter(
                            SyntaxFactory.Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.InKeyword)))
                        .WithType(
                            SyntaxFactory.IdentifierName(method.SourceParameterSymbol.Type.ToString())))))
            .WithBody(SyntaxFactory.Block(SyntaxFactory.ReturnStatement(returnObjectExpression)));


        if (!method.SourceParameterSymbol.Type.ContainsGenericTypeParameter())
            return methodDeclaration;

        var existingTypeParameters = step.KnownConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeParameters())
            .Select(p => p.Identifier.Text)
            .ToImmutableHashSet();

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type
            .GetGenericTypeParameters()
            .Where(p => !existingTypeParameters.Contains(p.ToString()));

        return methodDeclaration
            .WithTypeParameterList(
                SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList([..typeParameterSyntaxes])));
    }
}
