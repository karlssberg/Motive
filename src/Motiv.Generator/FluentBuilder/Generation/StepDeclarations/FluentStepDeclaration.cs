using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.Factories;
using Motiv.Generator.FluentBuilder.Generation.Shared;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.StepDeclarations;

public static class FluentStepDeclaration
{
    public static StructDeclarationSyntax Create(
        FluentBuilderStep step)
    {
        var methodDeclarationSyntaxes = step.FluentMethods
            .Select<FluentBuilderMethod, MethodDeclarationSyntax>(method => method.Constructor is not null && method.ReturnStep is null
                ? FluentFactoryMethodDeclaration.Create(method, step, method.Constructor)
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
}
