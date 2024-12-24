using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class FluentStepCreationExpression
{
    public static ObjectCreationExpressionSyntax Create(
        FluentMethod method,
        IEnumerable<ArgumentSyntax> arguments)
    {
        var name = StepNameSyntax.Create(method.ReturnStep!);

        if (method.OverloadParameter is not null && method.ParameterConverter is not null)
        {
            return CreateMethodOverloadExpression(method, arguments, name);
        }
        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList(arguments)));
    }

    private static ObjectCreationExpressionSyntax CreateMethodOverloadExpression(
        FluentMethod method,
        IEnumerable<ArgumentSyntax> arguments,
        TypeSyntax name)
    {
        var argumentList = arguments.ToList();
        var parameterConverterMethod = method.ParameterConverter!;
        IEnumerable<ArgumentSyntax> argNodes =
        [
            ..argumentList.Take(argumentList.Count - 1),
            Argument(ParameterConverterInvocationExpression.Create(method, parameterConverterMethod, argumentList.Last()))
        ];

        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList(argNodes)));
    }
}
