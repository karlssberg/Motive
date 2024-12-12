using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Motiv.Generator.FluentBuilder.Generation.Shared;

public static class AggressiveInliningAttributeSyntax
{
    private static AttributeSyntax Default { get; } =
        SyntaxFactory.Attribute(
            SyntaxFactory.ParseName("System.Runtime.CompilerServices.MethodImpl"),
            SyntaxFactory.AttributeArgumentList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.AttributeArgument(
                        SyntaxFactory.ParseExpression("System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining")
                    )
                )
            )
        );
}
