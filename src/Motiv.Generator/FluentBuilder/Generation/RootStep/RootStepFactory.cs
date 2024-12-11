using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Step;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.RootStep;

public static class RootStepFactory
{
    public static TypeDeclarationSyntax CreateRootTypeDeclaration(FluentBuilderFile file)
    {
        var rootMethodDeclarations = GetRootMethodDeclarations(file);

        TypeDeclarationSyntax typeDeclaration = file.TypeKind switch
        {
            TypeKind.Class when file.IsRecord =>
                RecordDeclaration(SyntaxKind.RecordDeclaration, Token(SyntaxKind.RecordKeyword),file.Name)
                    .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
                    .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken))
                    .WithModifiers(
                        TokenList((IEnumerable<SyntaxToken>)GetRootTypeModifiers(file)))
                    .WithMembers(
                        List(rootMethodDeclarations.OfType<MemberDeclarationSyntax>())),

            TypeKind.Class =>
                ClassDeclaration(file.Name)
                    .WithModifiers(
                        TokenList((IEnumerable<SyntaxToken>)GetRootTypeModifiers(file)))
                    .WithMembers(
                        List(rootMethodDeclarations.OfType<MemberDeclarationSyntax>())),

            TypeKind.Struct when file.IsRecord  =>
                RecordDeclaration(SyntaxKind.RecordStructDeclaration, Token(SyntaxKind.StructKeyword), Identifier(file.Name))
                    .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
                    .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken))
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(file).Append(Token(SyntaxKind.RecordKeyword))))
                    .WithMembers(
                        List(rootMethodDeclarations.OfType<MemberDeclarationSyntax>())),

            TypeKind.Struct =>
                StructDeclaration(file.Name)
                    .WithModifiers(
                        TokenList((IEnumerable<SyntaxToken>)GetRootTypeModifiers(file)))
                    .WithMembers(
                        List(rootMethodDeclarations.OfType<MemberDeclarationSyntax>())),

            _ => ClassDeclaration(file.Name)
        };

        return typeDeclaration;
    }

    private static IEnumerable<SyntaxToken> GetRootTypeModifiers(FluentBuilderFile file)
    {
        yield return Token(file.Accessibility switch
        {
            Accessibility.Public => SyntaxKind.PublicKeyword,
            Accessibility.Protected => SyntaxKind.ProtectedKeyword,
            Accessibility.Internal => SyntaxKind.InternalKeyword,
            Accessibility.Private => SyntaxKind.PrivateKeyword,
            _ => SyntaxKind.PublicKeyword
        });
        if (file.IsStatic)
        {
            yield return Token(SyntaxKind.StaticKeyword);
        }
        yield return Token(SyntaxKind.PartialKeyword);
    }

    private static IEnumerable<MethodDeclarationSyntax> GetRootMethodDeclarations(FluentBuilderFile file)
    {
        return file.FluentMethods
            .Select<FluentBuilderMethod, MethodDeclarationSyntax>(method => method.Constructor is not null && method.ReturnStep is null
                ? CreateRootFactoryMethod(method, method.Constructor)
                : CreateRootStepMethod(method))

            .Select(method =>
            {
                return method
                    .WithModifiers(
                        TokenList(GetSyntaxTokens()));

                IEnumerable<SyntaxToken> GetSyntaxTokens()
                {
                    yield return Token(SyntaxKind.PublicKeyword);
                    if (file.IsStatic)
                        yield return Token(SyntaxKind.StaticKeyword);
                }
            });
    }

    private static MethodDeclarationSyntax CreateRootFactoryMethod(
        FluentBuilderMethod method,
        IMethodSymbol constructor)
    {
        var returnObjectExpression = StepFactory.CreateTargetTypeActivationExpression(
            constructor.ContainingType,
            method.ConstructorParameters.Select(p => IdentifierName(p.Name.ToParameterFieldName()))
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

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type.GetGenericTypeParameters();

        return methodDeclaration
            .WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }

    private static MethodDeclarationSyntax CreateRootStepMethod(
        FluentBuilderMethod method)
    {
        var returnObjectExpression = CreateFluentStepActivationExpression(
            method,
            ArgumentList(SeparatedList<ArgumentSyntax>(
                [
                    Argument(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase()))
                ])));

        var methodDeclaration =  MethodDeclaration(
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
                            IdentifierName(method.SourceParameterSymbol.Type.ToString()))))
            )
            .WithBody(Block(ReturnStatement(returnObjectExpression)));

        if (!method.SourceParameterSymbol.Type.ContainsGenericTypeParameter())
            return methodDeclaration;

        var typeParameterSyntaxes = method.SourceParameterSymbol.Type.GetGenericTypeParameters();

        return methodDeclaration
            .WithTypeParameterList(
                TypeParameterList(SeparatedList([..typeParameterSyntaxes])));
    }

    public static ObjectCreationExpressionSyntax CreateFluentStepActivationExpression(
        FluentBuilderMethod method,
        ArgumentListSyntax argumentList)
    {
        var name = StepFactory.GetStepTypeName(method.ReturnStep!);
        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(SyntaxKind.NewKeyword))
            .WithArgumentList(argumentList);
    }
}
