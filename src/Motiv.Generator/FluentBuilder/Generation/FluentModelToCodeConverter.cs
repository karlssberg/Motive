using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Root;
using Motiv.Generator.FluentBuilder.Generation.StepDeclarations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class FluentModelToCodeConverter
{
    public static SyntaxNode CreateCompilationUnit(
        FluentBuilderFile file)
    {
        var rootType = RootTypeDeclaration.Create(file);
        var fluentSteps = file.FluentSteps.Select(FluentStepDeclaration.Create);

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
}
