using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class StepNameSyntax
{
    public static NameSyntax Create(FluentBuilderStep step)
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
}
