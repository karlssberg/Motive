using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class FluentStepCreationExpression
{
    public static ObjectCreationExpressionSyntax Create(
        FluentMethod method,
        ArgumentListSyntax argumentList)
    {
        var name = StepNameSyntax.Create(method.ReturnStep!);

        if (method.OverloadParameter is not null && method.ParameterConverter is not null)
        {
            return CreateMethodOverloadExpression(method, argumentList, name);
        }
        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(SyntaxKind.NewKeyword))
            .WithArgumentList(argumentList);
    }

    private static ObjectCreationExpressionSyntax CreateMethodOverloadExpression(
        FluentMethod method,
        ArgumentListSyntax argumentList,
        TypeSyntax name)
    {
        var parameterConverterMethod = method.ParameterConverter!;
        var converterMethod =
            InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        ParseTypeName(parameterConverterMethod.ContainingType.ToDisplayString()),
                        IdentifierName(parameterConverterMethod.Name)))
                .WithArgumentList(argumentList);

        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList([Argument(converterMethod)])));
    }
}
