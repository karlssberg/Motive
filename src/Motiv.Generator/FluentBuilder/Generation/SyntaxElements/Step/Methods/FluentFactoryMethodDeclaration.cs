using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Shared;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Methods;

public static class FluentFactoryMethodDeclaration
{
    public static MethodDeclarationSyntax Create(
        FluentMethod method,
        FluentStep step,
        IMethodSymbol constructor)
    {
        var identifierNameSyntaxes = method.KnownConstructorParameters
            .Select(p => IdentifierName(p.Name.ToParameterFieldName()))
            .AppendIfNotNull(
                method.SourceParameterSymbol is not null
                    ? IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())
                    : null);

        var returnObjectExpression = TargetTypeObjectCreationExpression.Create(
            method,
            constructor.ContainingType,
            identifierNameSyntaxes);

        var methodDeclaration = MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithAttributeLists(
                SingletonList(
                    AttributeList(
                        SingletonSeparatedList(AggressiveInliningAttributeSyntax.Create()))))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithBody(Block(ReturnStatement(returnObjectExpression)));

        if (method.SourceParameterSymbol is not null)
        {
            methodDeclaration = methodDeclaration.WithParameterList(
                ParameterList(SingletonSeparatedList(
                    Parameter(
                            Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithModifiers(TokenList(Token(SyntaxKind.InKeyword)))
                        .WithType(
                            IdentifierName(method.SourceParameterSymbol.Type.ToString())))));
        }

        if (!(method.SourceParameterSymbol?.Type.ContainsGenericTypeParameter() ?? false))
            return methodDeclaration;

        var existingTypeParameters = step.KnownConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeParameters())
            .Select(p => p.Identifier.Text)
            .ToImmutableHashSet();

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type
            .GetGenericTypeParameters()
            .Where(p => !existingTypeParameters.Contains(p.ToString()))
            .ToImmutableArray();

        if (typeParameterSyntaxes.Length == 0)
            return methodDeclaration;

        return methodDeclaration
            .WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }
}
