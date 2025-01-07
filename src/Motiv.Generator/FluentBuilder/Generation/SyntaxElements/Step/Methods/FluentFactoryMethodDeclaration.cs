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
                    : null)
            .Select(Argument);

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
                            IdentifierName(method.SourceParameterSymbol.Type.ToDynamicDisplayString(method.RootNamespace))))));
        }

        if (!method.SourceParameterSymbol?.Type.ContainsGenericTypeParameter() ?? method.ParameterConverter?.TypeArguments.Length == 0)
            return methodDeclaration;

        var typeParameterSyntaxes = method.TypeParameters
            .Except(step.KnownConstructorParameters.SelectMany(parameter => parameter.Type.GetGenericTypeParameters()))
            .Select(symbol => symbol.ToTypeParameterSyntax())
            .ToImmutableArray();

        if (typeParameterSyntaxes.Length == 0)
            return methodDeclaration;

        return methodDeclaration
            .WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }
}
