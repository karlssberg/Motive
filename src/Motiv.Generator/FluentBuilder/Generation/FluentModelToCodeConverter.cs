using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class FluentModelToCodeConverter
{
    public static SyntaxNode CreateCompilationUnit(
        FluentBuilderFile file)
    {
        var rootType = CreateRootTypeDeclaration(file);
        var fluentSteps = file.FluentSteps.Select(CreateFluentStepDeclaration);

        IEnumerable<MemberDeclarationSyntax> memberDeclarationSyntaxes = [rootType, ..fluentSteps];

        var usingDirectiveSyntaxes = file.Usings
            .Where(namespaceSymbol => !namespaceSymbol.IsGlobalNamespace)
            .Select(dep =>
                UsingDirective(
                    ParseName(dep.ToDisplayString())));

        return CompilationUnit()
            .WithUsings(List(usingDirectiveSyntaxes))
            .WithMembers(List(Enumerable.Empty<MemberDeclarationSyntax>()
                .Append(NamespaceDeclaration(ParseName(file.NameSpace))
                    .WithMembers(List(memberDeclarationSyntaxes)))));
    }

    private static TypeDeclarationSyntax CreateRootTypeDeclaration(FluentBuilderFile file)
    {
        var rootMethodDeclarations = GetRootMethodDeclarations(file);

       TypeDeclarationSyntax typeDeclaration = file.TypeKind switch
        {
            TypeKind.Class when file.IsRecord =>
                RecordDeclaration(SyntaxKind.RecordDeclaration, Token(RecordKeyword),file.Name)
                    .WithOpenBraceToken(Token(OpenBraceToken))
                    .WithCloseBraceToken(Token(CloseBraceToken))
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(file)))
                    .WithMembers(
                        List(rootMethodDeclarations.OfType<MemberDeclarationSyntax>())),

            TypeKind.Class =>
                ClassDeclaration(file.Name)
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(file)))
                    .WithMembers(
                        List(rootMethodDeclarations.OfType<MemberDeclarationSyntax>())),

            TypeKind.Struct when file.IsRecord  =>
                RecordDeclaration(SyntaxKind.RecordStructDeclaration, Token(StructKeyword), Identifier(file.Name))
                    .WithOpenBraceToken(Token(OpenBraceToken))
                    .WithCloseBraceToken(Token(CloseBraceToken))
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(file).Append(Token(RecordKeyword))))
                    .WithMembers(
                        List(rootMethodDeclarations.OfType<MemberDeclarationSyntax>())),

            TypeKind.Struct =>
                StructDeclaration(file.Name)
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(file)))
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
            Accessibility.Public => PublicKeyword,
            Accessibility.Protected => ProtectedKeyword,
            Accessibility.Internal => InternalKeyword,
            Accessibility.Private => PrivateKeyword,
            _ => PublicKeyword
        });
        if (file.IsStatic)
        {
            yield return Token(StaticKeyword);
        }
        yield return Token(PartialKeyword);
    }

    private static IEnumerable<MethodDeclarationSyntax> GetRootMethodDeclarations(FluentBuilderFile file)
    {
        return file.FluentMethods
            .Select(method => method.Constructor is not null && method.ReturnStep is null
                ? CreateRootFactoryMethod(method, method.Constructor)
                : CreateRootStepMethod(method))

            .Select(method =>
            {
                return method
                    .WithModifiers(
                        TokenList(GetSyntaxTokens()));

                IEnumerable<SyntaxToken> GetSyntaxTokens()
                {
                    yield return Token(PublicKeyword);
                    if (file.IsStatic)
                        yield return Token(StaticKeyword);
                }
            });
    }

    private static MethodDeclarationSyntax CreateRootFactoryMethod(
        FluentBuilderMethod method,
        IMethodSymbol constructor)
    {
        var returnObjectExpression = CreateTargetTypeActivationExpression(
            constructor.ContainingType,
            method.ConstructorParameters
                .Select(p => IdentifierName(ToParameterFieldName(p.Name)))
                .Append(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())));

        var methodDeclaration = MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithModifiers(
                TokenList(
                    Token(PublicKeyword)))
            .WithParameterList(
                ParameterList(SingletonSeparatedList(
                    Parameter(
                            Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithType(
                            IdentifierName(method.SourceParameterSymbol.Type.Name)))))
            .WithBody(Block(ReturnStatement(returnObjectExpression)));

        if (method.SourceParameterSymbol.Type is ITypeParameterSymbol)
        {
            methodDeclaration = methodDeclaration
                .WithTypeParameterList(
                    TypeParameterList(
                        SingletonSeparatedList(
                            TypeParameter(Identifier(method.SourceParameterSymbol.Type.Name)))));
        }

        return methodDeclaration;
    }

    private static MethodDeclarationSyntax CreateRootStepMethod(
        FluentBuilderMethod method)
    {
        var returnObjectExpression = CreateFluentStepActivationExpression(
            method,
            ArgumentList(
                SeparatedList<ArgumentSyntax>(
                    [
                        Argument(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase()))
                    ])));

        var methodDeclaration =  MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithModifiers(
                TokenList(
                    Token(PublicKeyword)))
            .WithParameterList(
                ParameterList(SingletonSeparatedList(
                    Parameter(
                            Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithType(
                            IdentifierName(method.SourceParameterSymbol.Type.Name))))
                )
            .WithBody(Block(ReturnStatement(returnObjectExpression)));

        if (method.SourceParameterSymbol.Type is ITypeParameterSymbol)
        {
            methodDeclaration = methodDeclaration
                .WithTypeParameterList(
                    TypeParameterList(
                        SingletonSeparatedList(
                            TypeParameter(Identifier(method.SourceParameterSymbol.Type.Name)))));
        }

        return methodDeclaration;
    }

    private static ObjectCreationExpressionSyntax CreateFluentStepActivationExpression(
        FluentBuilderMethod method,
        ArgumentListSyntax argumentList)
    {
        var name = GetStepTypeName(method.ReturnStep!);
        return ObjectCreationExpression(name)
            .WithNewKeyword(
                Token(NewKeyword))
            .WithArgumentList(argumentList);
    }

    private static ObjectCreationExpressionSyntax CreateTargetTypeActivationExpression(
        INamedTypeSymbol constructorType,
        IEnumerable<IdentifierNameSyntax> constructorArguments)
    {
        var arguments = constructorArguments
            .Select(Argument)
            .InterleaveWith(Token(CommaToken));

        TypeSyntax name = constructorType.TypeArguments.Length == 0
            ? IdentifierName(constructorType.Name)
            : GenericName(Identifier(constructorType.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SeparatedList<TypeSyntax>(
                            constructorType.TypeArguments
                                .Select(t => IdentifierName(t.Name)))));

        return ObjectCreationExpression(name)
            .WithNewKeyword(Token(NewKeyword))
            .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(arguments)));
    }

    private static StructDeclarationSyntax CreateFluentStepDeclaration(
        FluentBuilderStep step)
     {
         var methodDeclarationSyntaxes = step.FluentMethods
             .Select(method => method.Constructor is not null && method.ReturnStep is null
                ? CreateFluentFactoryMethod(method, method.Constructor)
                : CreateStepMethod(method, step));

         var propertyDeclaration = CreateStepFieldDeclarations(step);

         var constructorParameters = CreateFluentStepConstructorParameters(step);

         var constructor = ConstructorDeclaration(Identifier(step.Name))
             .WithModifiers(TokenList(
                 Token(PublicKeyword)))
             .WithParameterList(ParameterList(SeparatedList<ParameterSyntax>(constructorParameters)))
             .WithBody(Block(
                 step.KnownConstructorParameters
                     .Select(p =>
                          ExpressionStatement(
                             AssignmentExpression(
                                 SimpleAssignmentExpression,
                                 IdentifierName(ToParameterFieldName(p.Name)),
                                 IdentifierName(p.Name))))
                     .ToArray<StatementSyntax>()  // Convert IEnumerable to array of statements
             ));

         var name = GetStepTypeName(step);

         var identifier = name is GenericNameSyntax genericName
             ? genericName.Identifier
             : ((SimpleNameSyntax)name).Identifier;

         var structDeclaration = StructDeclaration(identifier)
             .WithModifiers(
                 TokenList(Token(PublicKeyword)))
             .WithMembers(
                 List<MemberDeclarationSyntax>([
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
                                 .Select(t => TypeParameter(t.Type.Name)))));
         }

         return structDeclaration;
     }

    private static IEnumerable<SyntaxNodeOrToken> CreateFluentStepConstructorParameters(FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                Parameter(Identifier(parameter.Name))
                    .WithType(IdentifierName(parameter.Type.Name)))
            .InterleaveWith(Token(CommaToken));
    }

    private static IEnumerable<FieldDeclarationSyntax> CreateStepFieldDeclarations(FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                FieldDeclaration(
                        VariableDeclaration(ParseTypeName(parameter.Type.Name))
                            .AddVariables(VariableDeclarator(
                                Identifier(ToParameterFieldName(parameter.Name)))))
                    .WithModifiers(TokenList(
                        Token(PrivateKeyword),
                        Token(ReadOnlyKeyword))));
    }

    private static MethodDeclarationSyntax CreateStepMethod(
        FluentBuilderMethod method,
        FluentBuilderStep step)
    {
        var stepActivationArgs = ArgumentList(SeparatedList(CreateStepConstructorArguments(method, step)));

        var returnObjectExpression = CreateFluentStepActivationExpression(method, stepActivationArgs);

        var methodDeclaration = MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithModifiers(
                TokenList(
                    Token(PublicKeyword)))
            .WithParameterList(
                ParameterList(SingletonSeparatedList(
                    Parameter(
                            Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithType(
                            IdentifierName(method.SourceParameterSymbol.Type.Name)))))
            .WithBody(Block(ReturnStatement(returnObjectExpression)));



        if (method.SourceParameterSymbol.Type is ITypeParameterSymbol)
        {
            methodDeclaration = methodDeclaration
                .WithTypeParameterList(
                    TypeParameterList(
                        SingletonSeparatedList(
                            TypeParameter(Identifier(method.SourceParameterSymbol.Type.Name)))));
        }

        return methodDeclaration;
    }

    private static IEnumerable<ArgumentSyntax> CreateStepConstructorArguments(FluentBuilderMethod method,
        FluentBuilderStep step)
    {
        return step.KnownConstructorParameters
            .Select(parameter =>
                Argument(IdentifierName(ToParameterFieldName(parameter.Name))))
            .Append(
                Argument(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())));
    }

    private static MethodDeclarationSyntax CreateFluentFactoryMethod(
        FluentBuilderMethod method,
        IMethodSymbol constructor)
    {
        var returnObjectExpression = CreateTargetTypeActivationExpression(
            constructor.ContainingType,
                method.ConstructorParameters
                    .Select(p => IdentifierName(ToParameterFieldName(p.Name)))
                    .Append(IdentifierName(method.SourceParameterSymbol.Name.ToCamelCase())));

        var methodDeclaration = MethodDeclaration(
                returnObjectExpression.Type,
                Identifier(method.MethodName))
            .WithModifiers(
                TokenList(
                    Token(PublicKeyword)))
            .WithParameterList(
                ParameterList(SingletonSeparatedList(
                    Parameter(
                            Identifier(method.SourceParameterSymbol.Name.ToCamelCase()))
                        .WithType(
                            IdentifierName(method.SourceParameterSymbol.Type.Name)))))
            .WithBody(Block(ReturnStatement(returnObjectExpression)));



        if (method.SourceParameterSymbol.Type is ITypeParameterSymbol)
        {
            methodDeclaration = methodDeclaration
                .WithTypeParameterList(
                    TypeParameterList(
                        SingletonSeparatedList(
                            TypeParameter(Identifier(method.SourceParameterSymbol.Type.Name)))));
        }

        return methodDeclaration;
    }

    private static NameSyntax GetStepTypeName(FluentBuilderStep step)
    {
        return step.GenericConstructorParameters.Length > 0
            ? GenericName(Identifier(step.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SeparatedList<TypeSyntax>(
                            step.GenericConstructorParameters
                                .Select(t => IdentifierName(t.Type.Name))))
                )
            : IdentifierName(step.Name);
    }

    private static string ToParameterFieldName(string name)
    {
        return $"_{name.ToCamelCase()}__parameter";
    }
}
