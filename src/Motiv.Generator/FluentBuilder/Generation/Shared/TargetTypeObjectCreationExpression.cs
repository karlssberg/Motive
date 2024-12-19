using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class TargetTypeObjectCreationExpression
{
    public static ObjectCreationExpressionSyntax Create(
        FluentMethod method,
        INamedTypeSymbol constructorType,
        IEnumerable<IdentifierNameSyntax> constructorArguments)
    {
        var arguments = constructorArguments
            .Select(Argument);

        var distinctTypeArguments = constructorType.TypeArguments
            .DistinctBy(type => type.ToDisplayString())
            .ToImmutableArray();

        TypeSyntax name = distinctTypeArguments.Length == 0
            ? IdentifierName(constructorType.Name)
            : GenericName(Identifier(constructorType.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        distinctTypeArguments
                            .Select(t => IdentifierName(t.Name)))));

        if (method.OverloadParameter is not null && method.ParameterConverter is not null)
        {
            return CreateMethodOverloadExpression(method, arguments, name);
        }

        return ObjectCreationExpression(name)
            .WithNewKeyword(Token(SyntaxKind.NewKeyword))
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
            Argument(InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        ParseTypeName(parameterConverterMethod.ContainingType.ToDisplayString()),
                        IdentifierName(parameterConverterMethod.Name)))
                .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(
                    argumentList.Skip(argumentList.Count - 1)
                ))))
        ];

        return ObjectCreationExpression(name)
            .WithNewKeyword(Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList(argNodes)));
    }
}
