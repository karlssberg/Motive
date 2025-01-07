using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.SyntaxElements.RootStep;
using Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.SyntaxElements;

public static class CompilationUnit
{
    public static SyntaxNode CreateCompilationUnit(
        FluentFactoryCompilationUnit file)
    {

        var usingDirectiveSyntaxes = file.Usings
            .Where(namespaceSymbol => !namespaceSymbol.IsGlobalNamespace)
            .Where(namespaceSymbol => namespaceSymbol.OriginalDefinition.ToDisplayString() != file.Namespace)
            .Select(dep =>
                UsingDirective(
                    ParseName(dep.ToDisplayString())));

        var members = GetMembers(file);

        return CompilationUnit()
            .WithUsings(List(usingDirectiveSyntaxes))
            .WithMembers(List(members));
    }

    private static IEnumerable<MemberDeclarationSyntax> GetMembers(FluentFactoryCompilationUnit file)
    {
        var rootType = RootTypeDeclaration.Create(file);
        var namespacesGroups = file.FluentSteps
            .GroupBy(step => step.ExistingStepConstructor?.ContainingNamespace ?? step.RootType.ContainingNamespace,
                SymbolEqualityComparer.Default)
            .Select(stepsInNamespace =>
            {
                var declarations = CreateTypeDeclarations(stepsInNamespace);
                return
                (
                    namespaces: stepsInNamespace.Key as INamespaceSymbol,
                    declarations: SymbolEqualityComparer.Default.Equals(file.RootType.ContainingNamespace,
                        stepsInNamespace.Key)
                        ? [rootType, ..declarations]
                        : declarations
                );
            });

        var memberDeclarations = namespacesGroups
            .SelectMany(tuple => MaybeEncapsulateInNamespace(tuple.namespaces, tuple.declarations))
            .ToArray();

        return memberDeclarations.Length > 0
            ? memberDeclarations
            : MaybeEncapsulateInNamespace(file.RootType.ContainingNamespace, [rootType]);

        IEnumerable<TypeDeclarationSyntax> CreateTypeDeclarations(
            IEnumerable<FluentStep> fluentSteps)
        {
            foreach (var step in fluentSteps)
            {
                yield return step.IsExistingPartialType
                    ? ExistingPartialTypeStepDeclaration.Create(step)
                    : FluentStepDeclaration.Create(step);
            }
        }

        IEnumerable<MemberDeclarationSyntax> MaybeEncapsulateInNamespace(INamespaceSymbol? namespaces, IEnumerable<TypeDeclarationSyntax> declarations)
        {
            if (namespaces is null || namespaces.IsGlobalNamespace)
                return declarations;

            return
                [
                    NamespaceDeclaration(ParseName(namespaces.ToDisplayString()))
                        .WithMembers(List([..declarations.OfType<MemberDeclarationSyntax>()]))
                ];
        }
    }
}
