using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Factories;

namespace Motiv.Generator.FluentBuilder.Generation.Root;

public static class FluentRootMethodDeclarations
{
    public static IEnumerable<MethodDeclarationSyntax> Create(FluentBuilderFile file)
    {
        return file.FluentMethods
            .Select<FluentBuilderMethod, MethodDeclarationSyntax>(method => method.Constructor is not null && method.ReturnStep is null
                ? FluentRootFactoryMethodDeclaration.Create(method, method.Constructor)
                : FluentRootStepMethodDeclaration.Create(method))

            .Select(method =>
            {
                return method
                    .WithModifiers(
                        SyntaxFactory.TokenList(GetSyntaxTokens()));

                IEnumerable<SyntaxToken> GetSyntaxTokens()
                {
                    yield return SyntaxFactory.Token(SyntaxKind.PublicKeyword);
                    if (file.IsStatic)
                        yield return SyntaxFactory.Token(SyntaxKind.StaticKeyword);
                }
            });
    }
}
