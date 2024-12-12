using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.StepDeclarations;

public static class FluentStepFieldDeclarations
{
    public static IEnumerable<FieldDeclarationSyntax> Create(FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                FieldDeclaration(
                        VariableDeclaration(ParseTypeName(parameter.Type.ToString()))
                            .AddVariables(VariableDeclarator(
                                Identifier(parameter.Name.ToParameterFieldName()))))
                    .WithModifiers(TokenList(
                        Token(SyntaxKind.PrivateKeyword),
                        Token(SyntaxKind.ReadOnlyKeyword))));
    }
}
