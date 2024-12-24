using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class StepNameSyntax
{
    public static NameSyntax Create(FluentStep step)
    {
        var distinctGenericParameters = step.GenericConstructorParameters
            .SelectMany(t => t.Type.GetGenericTypeArguments())
            .DistinctBy(symbol => symbol.ToDisplayString())
            .ToImmutableArray();

        return distinctGenericParameters.Length > 0
            ? GenericName(Identifier(step.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(
                        distinctGenericParameters
                            .Select(arg => IdentifierName(arg.ToDisplayString()))))
                )
            : IdentifierName(step.Name);
    }
}
