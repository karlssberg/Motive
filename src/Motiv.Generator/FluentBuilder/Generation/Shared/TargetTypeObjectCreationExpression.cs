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
        IEnumerable<ArgumentSyntax> arguments)
    {
        var name = IdentifierName(constructorType.ToDynamicDisplayString(method.RootNamespace));

        if (method.ParameterConverter is not null)
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

        var fieldArgumentsIndex = argumentList.Count - method.FluentParameters.Length;
        var fieldSourcedArguments = argumentList.Take(fieldArgumentsIndex);
        var methodParameterSourcedArguments = argumentList.Skip(fieldArgumentsIndex);

        IEnumerable<ArgumentSyntax> argNodes =
        [
            ..fieldSourcedArguments,
            Argument(ParameterConverterInvocationExpression.Create(parameterConverterMethod, methodParameterSourcedArguments, method.RootNamespace))
        ];

        return ObjectCreationExpression(name)
            .WithNewKeyword(Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList(argNodes)));
    }
}
