using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;

namespace Motiv.Generator.FluentBuilder.Generation.StepDeclarations;

public static class FluentStepFieldDeclarations
{
    public static IEnumerable<FieldDeclarationSyntax> Create(FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                SyntaxFactory.FieldDeclaration(
                        SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(parameter.Type.ToString()))
                            .AddVariables(SyntaxFactory.VariableDeclarator(
                                SyntaxFactory.Identifier(parameter.Name.ToParameterFieldName()))))
                    .WithModifiers(SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PrivateKeyword),
                        SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword))));
    }
}
