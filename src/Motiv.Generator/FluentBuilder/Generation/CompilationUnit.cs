using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Root;
using Motiv.Generator.FluentBuilder.Generation.StepDeclarations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class CompilationUnit
{
    public static SyntaxNode CreateCompilationUnit(
        FluentFactoryCompilationUnit file)
    {
        var rootType = RootTypeDeclaration.Create(file);
        var fluentSteps = file.FluentSteps.Select(FluentStepDeclaration.Create);

        MemberDeclarationSyntax[] memberDeclarationSyntaxes = [rootType, ..fluentSteps];

        var usingDirectiveSyntaxes = file.Usings
            .Where(namespaceSymbol => !namespaceSymbol.IsGlobalNamespace)
            .Where(namespaceSymbol => namespaceSymbol.OriginalDefinition.ToDisplayString() != file.Namespace)
            .Select(dep =>
                UsingDirective(
                    ParseName(dep.ToDisplayString())));

        IEnumerable<MemberDeclarationSyntax> members = string.IsNullOrEmpty(file.Namespace)
            ? memberDeclarationSyntaxes
            : [NamespaceDeclaration(ParseName(file.Namespace)).WithMembers(List(memberDeclarationSyntaxes))];

        return CompilationUnit()
            .WithUsings(List(usingDirectiveSyntaxes))
            .WithMembers(List(members));
    }
}
