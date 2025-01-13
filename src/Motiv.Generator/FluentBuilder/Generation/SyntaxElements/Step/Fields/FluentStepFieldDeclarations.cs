using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Fields;

public static class ParameterFieldDeclarations
{
    public static FieldDeclarationSyntax Create(IParameterSymbol parameter, INamespaceSymbol namespaceSymbol)
    {
        return FieldDeclaration(
                    VariableDeclaration(ParseTypeName(parameter.Type.ToDynamicDisplayString(namespaceSymbol)))
                        .AddVariables(VariableDeclarator(
                            Identifier(parameter.Name.ToParameterFieldName()))))
                    .WithModifiers(TokenList(
                        Token(SyntaxKind.PrivateKeyword),
                        Token(SyntaxKind.ReadOnlyKeyword)));
    }

    public static FieldDeclarationSyntax CreateWithInitialization(IParameterSymbol parameter, ExpressionSyntax expression, INamespaceSymbol namespaceSymbol)
    {
        return FieldDeclaration(
                    VariableDeclaration(ParseTypeName(parameter.Type.ToDynamicDisplayString(namespaceSymbol)))
                        .WithVariables(SingletonSeparatedList(
                            VariableDeclarator(Identifier(parameter.Name.ToParameterFieldName()))
                                .WithInitializer(EqualsValueClause(expression)))))
                    .WithModifiers(TokenList(
                        Token(SyntaxKind.PrivateKeyword),
                        Token(SyntaxKind.ReadOnlyKeyword)));
    }
}
