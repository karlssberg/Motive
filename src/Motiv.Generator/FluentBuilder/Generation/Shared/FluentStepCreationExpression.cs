﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel.Methods;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class FluentStepCreationExpression
{
    public static ObjectCreationExpressionSyntax Create(
        INamespaceSymbol currentNamespace,
        MultiMethod method,
        IEnumerable<ArgumentSyntax> arguments)
    {
        var name = StepNameSyntax.Create(currentNamespace, method.Return);
        return CreateMethodOverloadExpression(method, arguments, name);
    }

    public static ObjectCreationExpressionSyntax Create(
        INamespaceSymbol currentNamespace,
        IFluentMethod method,
        IEnumerable<ArgumentSyntax> arguments)
    {
        var name = StepNameSyntax.Create(currentNamespace, method.Return);
        return CreateObjectCreationExpression(arguments, name);
    }

    private static ObjectCreationExpressionSyntax CreateObjectCreationExpression(IEnumerable<ArgumentSyntax> arguments, NameSyntax name)
    {
        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList(arguments)));
    }

    private static ObjectCreationExpressionSyntax CreateMethodOverloadExpression(
        MultiMethod method,
        IEnumerable<ArgumentSyntax> arguments,
        TypeSyntax name)
    {
        var argumentList = arguments.ToList();
        var parameterConverterMethod = method.ParameterConverter!;

        var fieldArgumentsIndex = argumentList.Count - method.MethodParameters.Length;
        var fieldSourcedArguments = argumentList.Take(fieldArgumentsIndex);
        var methodParameterSourcedArguments = argumentList.Skip(fieldArgumentsIndex);

        IEnumerable<ArgumentSyntax> argNodes =
        [
            ..fieldSourcedArguments,
            Argument(ParameterConverterInvocationExpression.Create(parameterConverterMethod, methodParameterSourcedArguments, method.RootNamespace))
        ];

        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList(argNodes)));
    }
}
