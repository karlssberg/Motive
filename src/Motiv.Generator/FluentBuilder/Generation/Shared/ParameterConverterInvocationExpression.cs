using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class ParameterConverterInvocationExpression
{
    public static InvocationExpressionSyntax Create(IMethodSymbol parameterConverterMethod, IEnumerable<ArgumentSyntax> arguments)
    {
        SimpleNameSyntax identifierName = parameterConverterMethod.IsGenericMethod
            ? GenericName(parameterConverterMethod.Name)
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        parameterConverterMethod.TypeArguments
                            .Select(t => IdentifierName(t.ToDisplayString()))))
                )
            : IdentifierName(parameterConverterMethod.Name);

        var invocationExpression = InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    ParseTypeName(parameterConverterMethod.ContainingType.ToDisplayString()),
                    identifierName))
            .WithArgumentList(ArgumentList(SeparatedList(arguments)));

        return invocationExpression;
    }
}
