﻿using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder;

[Generator(LanguageNames.CSharp)]
public class FluentFactoryGenerator : IIncrementalGenerator
{
    private const string FluentFactoryConstructorAttributeFullName = "Motiv.Generator.Attributes.FluentConstructorAttribute";
    private const string FluentFactoryAttributeFullName = "Motiv.Generator.Attributes.FluentFactoryAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //AttachDebugger();
        var compilationProvider = context.CompilationProvider;

        // Step 1: Find all FluentConstructors
        var typeOrConstructorDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (node, _) => node switch
                {
                    TypeDeclarationSyntax { ParameterList: not null } type => type.AttributeLists.Count > 0,
                    ConstructorDeclarationSyntax ctor => ctor.AttributeLists.Count > 0,
                    _ => false
                },
                transform: (ctx, token) =>
                {
                    var syntax = ctx.Node;
                    var filePath = syntax.SyntaxTree.FilePath;
                    return (syntax, filePath);
                });

        // Step 2: Gather all discovered candidate constructors
        var constructorModels = typeOrConstructorDeclarations
            .Combine(compilationProvider)
            .SelectMany((data, ct) => CreateConstructorModels(data.Left.syntax, data.Left.filePath, data.Right, ct))
            .WithTrackingName("ConstructorModelCreation");

        // Step 3: Convert to a model of the fluent steps
        var consolidated = constructorModels
            .Collect()
            .WithTrackingName("ConstructorModelsConsolidation")
            .SelectMany((builderContextsCollection, _) =>
                builderContextsCollection
                    .SelectMany(builderContexts => builderContexts)
                    .GroupBy(builderContext => builderContext.RootTypeFullName)
                    .Select(group =>
                        FluentModelFactory.CreateFluentBuilderFile(group.Key, [..group])))
            .WithTrackingName("ConstructorModelsToFluentBuilderFiles");

        // Step 4: Generate source based on model
        context.RegisterSourceOutput(consolidated, GenerateDispatcher);
    }

    private static ImmutableArray<IEnumerable<FluentConstructorContext>> CreateConstructorModels(
        SyntaxNode syntax,
        string filePath,
        Compilation compilation,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return [];

        var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
        var symbol = semanticModel.GetDeclaredSymbol(syntax);
        if (symbol == null)
            return [];

        return [..
            GetFluentFactoryMetadata(symbol, semanticModel)
                .Select(metadata =>
                {
                    var attributePresent = metadata.AttributePresent;
                    var rootTypeFullName = metadata.RootTypeFullName;
                    if (!attributePresent || string.IsNullOrWhiteSpace(rootTypeFullName))
                        return [];

                    var lastIndexOf = rootTypeFullName.LastIndexOf('.');
                    var nameSpace = lastIndexOf == -1
                        ? rootTypeFullName
                        : rootTypeFullName.Substring(0, lastIndexOf);

                    var alreadyDeclaredRootType = semanticModel.Compilation.GetTypeByMetadataName(rootTypeFullName);
                    if (!IsRootTypeDecoratedWithAttribute(alreadyDeclaredRootType))
                        return [];

                    return symbol switch
                    {
                        IMethodSymbol method => [new FluentConstructorContext(nameSpace, method, alreadyDeclaredRootType, metadata)],
                        INamedTypeSymbol type => CreateFluentConstructorContexts(type, nameSpace, alreadyDeclaredRootType!, metadata),
                        _ => []
                    };
                })
        ];


        ImmutableArray<FluentConstructorContext> CreateFluentConstructorContexts(INamedTypeSymbol type, string nameSpace, ISymbol alreadyDeclaredRootType, FluentFactoryMetadata metadata)
        {
            var primaryCtor = type.Constructors.FirstOrDefault(c => c.Parameters.Length > 0);
            if (primaryCtor != null)
                return [new FluentConstructorContext(nameSpace, primaryCtor, alreadyDeclaredRootType, metadata)];

            return
            [
                ..type.Constructors
                    .Select(ctor =>
                        new FluentConstructorContext(nameSpace, ctor, alreadyDeclaredRootType, metadata))
            ];
        }
    }

    private static bool IsRootTypeDecoratedWithAttribute(INamedTypeSymbol? alreadyDeclaredRootType)
    {
        return alreadyDeclaredRootType is not null
               && alreadyDeclaredRootType
                   .GetAttributes()
                   .Any(attr => attr.AttributeClass?.ToDisplayString() == FluentFactoryAttributeFullName);
    }

    private static IEnumerable<FluentFactoryMetadata> GetFluentFactoryMetadata(ISymbol symbol, SemanticModel semanticModel)
    {
        return symbol.GetAttributes()
            .Where(a => a.AttributeClass?.ToDisplayString() == FluentFactoryConstructorAttributeFullName)
            .Select(attribute =>
            {
                if (attribute == null || attribute.ConstructorArguments.Length == 0)
                    return FluentFactoryMetadata.Invalid;

                var typeArg = attribute.ConstructorArguments.FirstOrDefault();
                if (typeArg.IsNull)
                    return FluentFactoryMetadata.Invalid;

                if (typeArg.Value is not ITypeSymbol typeSymbol)
                    return FluentFactoryMetadata.Invalid;

                var namedAttributeArgument = attribute.NamedArguments
                    .FirstOrDefault(namedArg => namedArg.Key == nameof(FluentConstructorAttribute.Options))
                    .Value;
                var options = ConvertToFluentFactoryGeneratorOptions(namedAttributeArgument);

                return new FluentFactoryMetadata
                {
                    Options = options,
                    RootTypeFullName = typeSymbol.ToDisplayString()
                };
            });
    }

    private static FluentFactoryGeneratorOptions ConvertToFluentFactoryGeneratorOptions(TypedConstant namedAttributeArgument)
    {
        if (namedAttributeArgument.Kind != TypedConstantKind.Enum)
            return FluentFactoryGeneratorOptions.None;

        // Get the underlying int value
        var value = (int?)namedAttributeArgument.Value ?? (int)FluentMethodOptions.None;

        // Get the type symbol for the enum
        if (namedAttributeArgument.Type is not INamedTypeSymbol enumType)
            return FluentFactoryGeneratorOptions.None;

        // Get all the declared members of the enum
        var flagMembers = enumType.GetMembers()
            .OfType<IFieldSymbol>()
            .Where(f => f.HasConstantValue && f.ConstantValue is int)
            .ToList();

        // Check which flags are set
        var setFlags = flagMembers
            .Where(member => {
                var memberValue = (int?)member.ConstantValue ?? (int)FluentMethodOptions.None;
                return memberValue != 0 && (value & memberValue) == memberValue;
            })
            .ToList();

        return setFlags
            .Select(flag => Enum.TryParse<FluentFactoryGeneratorOptions>(flag.Name, true, out var option)
                ? option
                : FluentFactoryGeneratorOptions.None)
            .Aggregate((prev, next) => prev | next);
    }

    private static void GenerateDispatcher(
        SourceProductionContext context,
        FluentBuilderFile builder)
    {
        var source = CompilationUnit.CreateCompilationUnit(builder).NormalizeWhitespace().ToString();

        context.CancellationToken.ThrowIfCancellationRequested();

        context.AddSource($"{builder.FullName}.g.cs", source);
    }

    [Conditional("DEBUG")]
    private static void AttachDebugger()
    {
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }
    }
}
