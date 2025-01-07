using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Shared;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Methods;

public static class ExistingPartialTypeMethodDeclaration
{
    public static MethodDeclarationSyntax Create(
        FluentMethod method,
        FluentStep step)
    {
        var stepActivationArgs = CreateStepConstructorArguments(method, step);

        var returnObjectExpression = method.TargetConstructor is not null
            ? TargetTypeObjectCreationExpression.Create(method, method.TargetConstructor.ContainingType, stepActivationArgs)
            : FluentStepCreationExpression.Create(method, CreateStepConstructorArguments(method, step));

        var methodDeclaration = MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithAttributeLists(
                SingletonList(
                    AttributeList(
                        SingletonSeparatedList(AggressiveInliningAttributeSyntax.Create()))))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithBody(Block(ReturnStatement(returnObjectExpression)));

        if (method.FluentParameters.Length > 0)
        {
            methodDeclaration = methodDeclaration
                .WithParameterList(
                    ParameterList(SeparatedList(
                        method.FluentParameters
                            .Select(parameter =>
                                Parameter(
                                        Identifier(parameter.ParameterSymbol.Name.ToCamelCase()))
                                    .WithModifiers(TokenList(Token(SyntaxKind.InKeyword)))
                                    .WithType(
                                        IdentifierName(parameter.ParameterSymbol.Type.ToDynamicDisplayString(method.RootNamespace)))))));
        }

        if (!method.SourceParameterSymbol?.Type.ContainsGenericTypeParameter() ?? method.ParameterConverter?.TypeArguments.Length == 0)
            return methodDeclaration;

        var typeParameterSyntaxes = method.TypeParameters
            .Except(step.KnownConstructorParameters.SelectMany(parameter => parameter.Type.GetGenericTypeParameters()))
            .Select(symbol => symbol.ToTypeParameterSyntax())
            .ToImmutableArray();

        return typeParameterSyntaxes.Length == 0
            ? methodDeclaration
            : methodDeclaration.WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }

    private static IEnumerable<ArgumentSyntax> CreateStepConstructorArguments(
        FluentMethod method,
        FluentStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
            {
                var foundMember = step.ParameterStoreMembers.TryGetValue(parameter, out var member)
                    ? member
                    : null;

                ExpressionSyntax node = foundMember is null
                    ? DefaultExpression(ParseTypeName(parameter.Type.ToDynamicDisplayString(method.RootNamespace)))
                    : MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, ThisExpression() , IdentifierName(foundMember.Name));

                return Argument(node);
            })
            .Concat(method.FluentParameters
                .Select(p => p.ParameterSymbol.Name.ToCamelCase())
                .Select(IdentifierName)
                .Select(Argument));
    }
}
