using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Shared;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.StepDeclarations;

public static class FluentStepMethodDeclaration
{
    public static MethodDeclarationSyntax Create(
        FluentMethod method,
        FluentBuilderStep step)
    {
        var stepActivationArgs = ArgumentList(SeparatedList(CreateStepConstructorArguments(method, step)));

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

        if (!(method.SourceParameterSymbol?.Type.ContainsGenericTypeParameter() ?? false))
            return methodDeclaration;

        var existingTypeParameters = step.GenericConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeParameters())
            .Select(p => p.Identifier.Text)
            .ToImmutableHashSet();

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type
            .GetGenericTypeParameters()
            .Where(p => !existingTypeParameters.Contains(p.ToString()))
            .ToImmutableArray();

        return typeParameterSyntaxes.Length == 0
            ? methodDeclaration
            : methodDeclaration.WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }

    private static IEnumerable<ArgumentSyntax> CreateStepConstructorArguments(FluentMethod method,
        FluentBuilderStep step)
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
