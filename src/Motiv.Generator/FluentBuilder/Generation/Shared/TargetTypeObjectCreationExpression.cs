﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel.Methods;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class TargetTypeObjectCreationExpression
{
    public static ObjectCreationExpressionSyntax Create(
        INamespaceSymbol currentNamespace,
        IFluentMethod method,
        IEnumerable<ArgumentSyntax> fieldArguments,
        IEnumerable<ArgumentSyntax> methodArguments)
    {
        var name = IdentifierName(method.Return.IdentifierDisplayString(currentNamespace));

        if (method is MultiMethod multiMethod)
        {
            return CreateMethodOverloadExpression(multiMethod, fieldArguments, methodArguments, name);
        }

        return ObjectCreationExpression(name)
            .WithNewKeyword(Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList(methodArguments)));
    }

    private static ObjectCreationExpressionSyntax CreateMethodOverloadExpression(
        MultiMethod method,
        IEnumerable<ArgumentSyntax> fieldArguments,
        IEnumerable<ArgumentSyntax> methodArguments,
        TypeSyntax name)
    {
        IEnumerable<ArgumentSyntax> argNodes =
        [
            ..fieldArguments,
            Argument(ParameterConverterInvocationExpression.Create(method.ParameterConverter, methodArguments, method.RootNamespace))
        ];

        return ObjectCreationExpression(name)
            .WithNewKeyword(Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList(argNodes)));
    }
}
