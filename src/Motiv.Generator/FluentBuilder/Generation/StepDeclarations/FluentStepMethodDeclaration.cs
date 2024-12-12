using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Shared;

namespace Motiv.Generator.FluentBuilder.Generation.StepDeclarations;

public static class FluentStepMethodDeclaration
{
    public static MethodDeclarationSyntax CreateStepMethod(
        FluentBuilderMethod method,
        FluentBuilderStep step)
    {
        var stepActivationArgs = SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(CreateStepConstructorArguments(method, step)));

        var returnObjectExpression = FluentStepCreationExpression.Create(method, stepActivationArgs);

        var methodDeclaration = SyntaxFactory.MethodDeclaration(
                returnObjectExpression.Type,
                SyntaxFactory.Identifier(method.MethodName))
            .WithModifiers(
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                SyntaxFactory.ParameterList(SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Parameter(
                            SyntaxFactory.Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.InKeyword)))
                        .WithType(
                            SyntaxFactory.IdentifierName(method.SourceParameterSymbol.Type.ToString())))))
            .WithBody(SyntaxFactory.Block(SyntaxFactory.ReturnStatement(returnObjectExpression)));

        if (!method.SourceParameterSymbol.Type.ContainsGenericTypeParameter())
            return methodDeclaration;

        var existingTypeParameters = step.GenericConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeParameters())
            .Select(p => p.Identifier.Text)
            .ToImmutableHashSet();

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type
            .GetGenericTypeParameters()
            .Where(p => !existingTypeParameters.Contains(p.ToString()));

        return methodDeclaration
            .WithTypeParameterList(
                SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList([..typeParameterSyntaxes])));
    }

    private static IEnumerable<ArgumentSyntax> CreateStepConstructorArguments(FluentBuilderMethod method,
        FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                SyntaxFactory.Argument(SyntaxFactory.IdentifierName(parameter.Name.ToParameterFieldName())))
            .Append(
                SyntaxFactory.Argument(SyntaxFactory.IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())));
    }
}
