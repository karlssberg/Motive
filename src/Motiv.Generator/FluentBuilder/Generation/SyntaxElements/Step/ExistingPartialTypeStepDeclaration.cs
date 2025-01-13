﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Fields;
using Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step.Methods;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Motiv.Generator.FluentBuilder.Generation.SyntaxElements.Step;

public static class ExistingPartialTypeStepDeclaration
{
    public static TypeDeclarationSyntax Create(
        FluentStep step)
    {
        var methodDeclarationSyntaxes = step.FluentMethods
            .Select<FluentMethod, MethodDeclarationSyntax>(method => ExistingPartialTypeMethodDeclaration.Create(method, step));

        var parameterFieldDeclaration = step.KnownConstructorParameters
            .Where(parameter =>
                step.ParameterStoreMembers.TryGetValue(parameter, out var parameterResolution)
                && parameterResolution.ShouldInitializeFromPrimaryConstructor)
            .Select(parameter =>
                ParameterFieldDeclarations.CreateWithInitialization(
                    parameter,
                    IdentifierName(parameter.Name),
                    step.ExistingStepConstructor!.ContainingNamespace));

        var typeDeclaration = CreateTypeDeclarationSyntax(step, IdentifierName(step.Identifier).Identifier)
            .WithMembers(List<MemberDeclarationSyntax>([
                ..parameterFieldDeclaration,
                ..methodDeclarationSyntaxes,
            ]));

        if (step.GenericConstructorParameters.Length <= 0)
            return typeDeclaration;

        var typeParameterSyntaxes = step.GenericConstructorParameters
            .Select<IParameterSymbol, ITypeSymbol>(t => t.Type)
            .GetGenericTypeParameters()
            .Distinct(SymbolEqualityComparer.Default)
            .OfType<ITypeParameterSymbol>()
            .Select(symbol => symbol.ToTypeParameterSyntax())
            .ToImmutableArray();

        if (typeParameterSyntaxes.Length == 0)
            return typeDeclaration;

        return typeDeclaration
            .WithTypeParameterList(
                TypeParameterList(
                    SeparatedList(typeParameterSyntaxes)));
    }

    private static TypeDeclarationSyntax CreateTypeDeclarationSyntax(FluentStep step, SyntaxToken identifier)
    {
        return step.TypeKind switch
        {
            TypeKind.Class when step.IsRecord =>
                RecordDeclaration(
                        SyntaxKind.RecordDeclaration,
                        Token(SyntaxKind.RecordKeyword),
                        identifier)
                    .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
                    .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken))
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(step))),

            TypeKind.Class =>
                ClassDeclaration(identifier)
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(step))),

            TypeKind.Struct  when step.IsRecord=>
                StructDeclaration(identifier)
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(step).Append(Token(SyntaxKind.RecordKeyword)))),

            _ =>
                StructDeclaration(identifier)
                    .WithModifiers(
                        TokenList(GetRootTypeModifiers(step))),
        };
    }

    private static IEnumerable<SyntaxToken> GetRootTypeModifiers(FluentStep step)
    {
        return step.Accessibility
            .AccessibilityToSyntaxKind()
            .Select(Token)
            .Append(Token(SyntaxKind.PartialKeyword));
    }
}
