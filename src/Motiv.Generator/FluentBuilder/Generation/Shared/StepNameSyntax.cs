using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class StepNameSyntax
{
    public static NameSyntax Create(FluentBuilderStep step)
    {
        var distinctGemericParameters = step.GenericConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeArguments())
            .DistinctBy(symbol => symbol.ToDisplayString())
            .ToImmutableArray();

        return distinctGemericParameters.Length > 0
            ? GenericName(Identifier(step.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        distinctGemericParameters
                            .Select(arg => IdentifierName(arg.Name))))
                )
            : IdentifierName(step.Name);
    }
}
