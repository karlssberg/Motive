using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.RootStep;
using Motiv.Generator.FluentBuilder.Generation.Step;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class FluentModelToCodeConverter
{
    public static SyntaxNode CreateCompilationUnit(
        FluentBuilderFile file)
    {
        var rootType = RootStepFactory.CreateRootTypeDeclaration(file);
        var fluentSteps = file.FluentSteps.Select(StepFactory.CreateFluentStepDeclaration);

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
