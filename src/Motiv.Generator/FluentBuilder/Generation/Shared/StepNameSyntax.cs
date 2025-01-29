using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel.Steps;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class StepNameSyntax
{
    public static NameSyntax Create(
        INamespaceSymbol currentNamespace,
        IFluentReturn fluentReturn)
    {

        return fluentReturn switch
        {
            ExistingTypeFluentStep existingTypeFluentStep =>
                ParseName(fluentReturn.IdentifierDisplayString(currentNamespace)),
            _ => CreateNameSyntax(currentNamespace, fluentReturn)
        };
    }

    private static NameSyntax CreateNameSyntax(INamespaceSymbol currentNamespace, IFluentReturn fluentReturn)
    {
        return IdentifierName(fluentReturn.IdentifierDisplayString(currentNamespace));
    }
}
