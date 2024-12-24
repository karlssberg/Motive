using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class ParameterConverterInvocationExpression
{
    public static InvocationExpressionSyntax Create(FluentMethod method, IMethodSymbol parameterConverterMethod, ArgumentSyntax argument)
    {

        SimpleNameSyntax identifierName = parameterConverterMethod.IsGenericMethod
            ? GenericName(parameterConverterMethod.Name)
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        parameterConverterMethod.TypeArguments
                            .Select(t => IdentifierName(t.ToDisplayString()))))
                )
            : IdentifierName(parameterConverterMethod.ToDisplayString());

        var invocationExpression = InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    ParseTypeName(parameterConverterMethod.ContainingType.ToDisplayString()),
                    identifierName))
            .WithArgumentList(ArgumentList(SingletonSeparatedList(argument)));

        return invocationExpression;
    }
}
