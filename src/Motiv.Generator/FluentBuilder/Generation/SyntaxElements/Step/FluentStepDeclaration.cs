using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Shared;
using Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Constructors;
using Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Fields;
using Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Methods;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step;

public static class FluentStepDeclaration
{
    public static StructDeclarationSyntax Create(
        FluentStep step)
    {
        var methodDeclarationSyntaxes = step.FluentMethods
            .Select<FluentMethod, MethodDeclarationSyntax>(method => method.TargetConstructor is not null && method.ReturnStep is null
                ? FluentFactoryMethodDeclaration.Create(method, step, method.TargetConstructor)
                : FluentStepMethodDeclaration.Create(method, step));

        var propertyDeclaration = FluentStepFieldDeclarations.Create(step);

        var constructor = FluentStepConstructorDeclaration.Create(step);

        var name = StepNameSyntax.Create(step);

        var identifier = name is GenericNameSyntax genericName
            ? genericName.Identifier
            : ((SimpleNameSyntax)name).Identifier;

        var structDeclaration = StructDeclaration(identifier)
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithMembers(List<MemberDeclarationSyntax>([
                ..propertyDeclaration,
                constructor,
                ..methodDeclarationSyntaxes,
            ]));

        if (step.GenericConstructorParameters.Length <= 0)
            return structDeclaration;

        var typeParameterSyntaxes = step.GenericConstructorParameters
            .Select<IParameterSymbol, ITypeSymbol>(t => t.Type)
            .GetGenericTypeParameters()
            .Distinct(SymbolEqualityComparer.Default)
            .OfType<ITypeParameterSymbol>()
            .Select(symbol => symbol.ToTypeParameterSyntax())
            .ToImmutableArray();

        if (typeParameterSyntaxes.Length == 0)
            return structDeclaration;

        return structDeclaration
            .WithTypeParameterList(
                TypeParameterList(
                    SeparatedList(typeParameterSyntaxes)));
    }
}
