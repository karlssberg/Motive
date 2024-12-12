using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class FluentStepCreationExpression
{
    public static ObjectCreationExpressionSyntax Create(
        FluentBuilderMethod method,
        ArgumentListSyntax argumentList)
    {
        var name = StepNameSyntax.Create(method.ReturnStep!);
        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(SyntaxKind.NewKeyword))
            .WithArgumentList(argumentList);
    }
}
