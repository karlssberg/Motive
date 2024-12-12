using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;

namespace Motiv.Generator.FluentBuilder.Generation.StepDeclarations;

public static class FluentStepConstructorDeclaration
{
    public static ConstructorDeclarationSyntax Create(FluentBuilderStep step)
    {
        var constructorParameters = CreateFluentStepConstructorParameters(step);

        var constructor = SyntaxFactory.ConstructorDeclaration(SyntaxFactory.Identifier(step.Name))
            .WithModifiers(SyntaxFactory.TokenList(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList<ParameterSyntax>(constructorParameters)))
            .WithBody(SyntaxFactory.Block(
                step.KnownConstructorParameters
                    .Select(p =>
                        SyntaxFactory.ExpressionStatement(
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName(p.Name.ToParameterFieldName()),
                                SyntaxFactory.IdentifierName(p.Name.ToCamelCase()))))
                    .ToArray<StatementSyntax>()  // Convert IEnumerable to array of statements
            ));
        return constructor;
    }

    private static IEnumerable<SyntaxNodeOrToken> CreateFluentStepConstructorParameters(FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name.ToCamelCase()))
                    .WithType(SyntaxFactory.IdentifierName(parameter.Type.ToString()))
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.InKeyword))))
            .InterleaveWith(SyntaxFactory.Token(SyntaxKind.CommaToken));
    }
}
