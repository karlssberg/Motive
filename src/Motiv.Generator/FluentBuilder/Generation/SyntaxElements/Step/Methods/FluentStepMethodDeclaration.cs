using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Shared;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Methods;

public static class FluentStepMethodDeclaration
{
    public static MethodDeclarationSyntax Create(
        FluentMethod method,
        FluentStep step)
    {
        var stepActivationArgs = CreateStepConstructorArguments(method, step);

        var returnObjectExpression = FluentStepCreationExpression.Create(method, stepActivationArgs);

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
            methodDeclaration = methodDeclaration
                .WithParameterList(
                    ParameterList(SingletonSeparatedList(
                        Parameter(
                                Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                            .WithModifiers(TokenList(Token(SyntaxKind.InKeyword)))
                            .WithType(
                                IdentifierName(method.SourceParameterSymbol.Type.ToString())))));
        }

        if (!method.SourceParameterSymbol?.Type.ContainsGenericTypeParameter() ?? method.ParameterConverter?.TypeArguments.Length == 0)
            return methodDeclaration;

        var typeParameterSyntaxes = method.TypeParameters
            .Except(step.KnownConstructorParameters.SelectMany(parameter => parameter.Type.GetGenericTypeParameters()))
            .Select(symbol => symbol.ToTypeParameterSyntax())
            .ToImmutableArray();

        return typeParameterSyntaxes.Length == 0
            ? methodDeclaration
            : methodDeclaration.WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }

    private static IEnumerable<ArgumentSyntax> CreateStepConstructorArguments(
        FluentMethod method,
        FluentStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                Argument(IdentifierName(parameter.Name.ToParameterFieldName())))
            .AppendIfNotNull(
                method.SourceParameterSymbol is not null
                    ? Argument(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase()))
                    : null);
    }
}
