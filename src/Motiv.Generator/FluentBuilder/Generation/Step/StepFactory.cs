using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.RootStep;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Step;

public static class StepFactory
{
    public static NameSyntax GetStepTypeName(FluentBuilderStep step)
    {
        return step.GenericConstructorParameters.Length > 0
            ? GenericName(Identifier(step.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        step.GenericConstructorParameters
                            .SelectMany(t => t.Type.GetGenericTypeArguments())
                            .Select(arg => IdentifierName(arg.Name))))
                )
            : IdentifierName(step.Name);
    }


    public static ObjectCreationExpressionSyntax CreateTargetTypeActivationExpression(
        INamedTypeSymbol constructorType,
        IEnumerable<IdentifierNameSyntax> constructorArguments)
    {
        var arguments = constructorArguments
            .Select(Argument)
            .InterleaveWith(Token(SyntaxKind.CommaToken));

        TypeSyntax name = constructorType.TypeArguments.Length == 0
            ? IdentifierName(constructorType.Name)
            : GenericName(Identifier(constructorType.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        constructorType.TypeArguments
                            .Select(t => IdentifierName(t.Name)))));

        return ObjectCreationExpression(name)
            .WithNewKeyword(Token(SyntaxKind.NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(arguments)));
    }

    public static StructDeclarationSyntax CreateFluentStepDeclaration(
        FluentBuilderStep step)
    {
        var methodDeclarationSyntaxes = step.FluentMethods
            .Select<FluentBuilderMethod, MethodDeclarationSyntax>(method => method.Constructor is not null && method.ReturnStep is null
                ? CreateFluentFactoryMethod(method, step, method.Constructor)
                : CreateStepMethod(method, step));

        var propertyDeclaration = CreateStepFieldDeclarations(step);

        var constructorParameters = CreateFluentStepConstructorParameters(step);

        var constructor = ConstructorDeclaration(Identifier(step.Name))
            .WithModifiers(TokenList(
                Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(ParameterList(SeparatedList<ParameterSyntax>(constructorParameters)))
            .WithBody(Block(
                step.KnownConstructorParameters
                    .Select(p =>
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                IdentifierName(p.Name.ToParameterFieldName()),
                                IdentifierName(p.Name.ToCamelCase()))))
                    .ToArray<StatementSyntax>()  // Convert IEnumerable to array of statements
            ));

        var name = GetStepTypeName(step);

        var identifier = name is GenericNameSyntax genericName
            ? genericName.Identifier
            : ((SimpleNameSyntax)name).Identifier;

        var structDeclaration = StructDeclaration(identifier)
            .WithModifiers(
                TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithMembers(List<MemberDeclarationSyntax>([
                ..propertyDeclaration,
                constructor,
                ..methodDeclarationSyntaxes,
            ]));

        if (step.GenericConstructorParameters.Length > 0)
        {
            structDeclaration = structDeclaration
                .WithTypeParameterList(
                    TypeParameterList(
                        SeparatedList(
                            step.GenericConstructorParameters
                                .SelectMany(t => t.Type.GetGenericTypeParameters()))));
        }

        return structDeclaration;
    }

    private static IEnumerable<SyntaxNodeOrToken> CreateFluentStepConstructorParameters(FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                Parameter(Identifier(parameter.Name.ToCamelCase()))
                    .WithType(IdentifierName(parameter.Type.ToString())))
            .InterleaveWith(Token(SyntaxKind.CommaToken));
    }

    private static IEnumerable<FieldDeclarationSyntax> CreateStepFieldDeclarations(FluentBuilderStep step)
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

    private static MethodDeclarationSyntax CreateStepMethod(
        FluentBuilderMethod method,
        FluentBuilderStep step)
    {
        var stepActivationArgs = ArgumentList(SeparatedList(CreateStepConstructorArguments(method, step)));

        var returnObjectExpression = RootStepFactory.CreateFluentStepActivationExpression(method, stepActivationArgs);

        var methodDeclaration = MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                ParameterList(SingletonSeparatedList(
                    Parameter(
                            Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithType(
                            IdentifierName(method.SourceParameterSymbol.Type.ToString())))))
            .WithBody(Block(ReturnStatement(returnObjectExpression)));

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
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }

    private static IEnumerable<ArgumentSyntax> CreateStepConstructorArguments(FluentBuilderMethod method,
        FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                Argument(IdentifierName(parameter.Name.ToParameterFieldName())))
            .Append(
                Argument(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())));
    }

    private static MethodDeclarationSyntax CreateFluentFactoryMethod(
        FluentBuilderMethod method,
        FluentBuilderStep step,
        IMethodSymbol constructor)
    {
        var returnObjectExpression = StepFactory.CreateTargetTypeActivationExpression(
            constructor.ContainingType,
            method.ConstructorParameters
                .Select(p => IdentifierName(p.Name.ToParameterFieldName()))
                .Append(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())));

        var methodDeclaration = MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                ParameterList(SingletonSeparatedList(
                    Parameter(
                            Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithType(
                            IdentifierName(method.SourceParameterSymbol.Type.ToString())))))
            .WithBody(Block(ReturnStatement(returnObjectExpression)));


        if (!method.SourceParameterSymbol.Type.ContainsGenericTypeParameter())
            return methodDeclaration;

        var existingTypeParameters = step.KnownConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeParameters())
            .Select(p => p.Identifier.Text)
            .ToImmutableHashSet();

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type
            .GetGenericTypeParameters()
            .Where(p => !existingTypeParameters.Contains(p.ToString()));

        return methodDeclaration
            .WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }
}
