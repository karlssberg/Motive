﻿using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation.SyntaxElements;

namespace Motiv.Generator.FluentBuilder;

[Generator(LanguageNames.CSharp)]
public class FluentFactoryGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //AttachDebugger();
        var compilationProvider = context.CompilationProvider;

        var typeOrConstructorDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(TypeName.FluentConstructorAttribute,
                (node, _) => node switch
                {
                    // Capture primary constructors
                    TypeDeclarationSyntax { ParameterList: not null, AttributeLists.Count: > 0 } => true,
                    // Capture explicit constructors
                    ConstructorDeclarationSyntax { AttributeLists.Count: > 0 } => true,
                    _ => false
                },
                (ctx, _) =>
                {
                    var syntax = ctx.TargetNode;
                    var filePath = syntax.SyntaxTree.FilePath;
                    return (syntax, filePath);
                }
            );

        // Step 2: Gather all discovered candidate constructors
        var constructorModels = typeOrConstructorDeclarations
            .Combine(compilationProvider)
            .SelectMany((data, ct) =>
            {
                var compilation = data.Right;
                var syntax = data.Left.syntax;
                return CreateConstructorModels(compilation, syntax, ct);
            })
            .WithTrackingName("ConstructorModelCreation");

        // Step 3: Convert to a model of the fluent steps
        var consolidated = constructorModels
            .Collect()
            .Combine(compilationProvider)
            .WithTrackingName("ConstructorModelsConsolidation")
            .SelectMany((tuple, _) =>
            {
                var (builderContextsCollection, compilation) = tuple;
                return builderContextsCollection
                    .SelectMany(builderContexts => builderContexts)
                    .GroupBy(builderContext => builderContext.RootTypeFullName)
                    .Select(group =>
                        new FluentModelFactory(compilation).CreateFluentFactoryCompilationUnit(group.Key, [..group]));
            })
            .WithTrackingName("ConstructorModelsToFluentBuilderFiles");

        // Step 4: Generate source based on model
        context.RegisterSourceOutput(consolidated, Execute);
    }

    private static void Execute(
        SourceProductionContext context,
        FluentFactoryCompilationUnit builder)
    {
        var source = CompilationUnit.CreateCompilationUnit(builder).NormalizeWhitespace().ToString();

        context.CancellationToken.ThrowIfCancellationRequested();

        context.AddSource($"{builder.FullName}.g.cs", source);
    }

    private static ImmutableArray<IEnumerable<FluentConstructorContext>> CreateConstructorModels(
        Compilation compilation,
        SyntaxNode syntaxTree,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return [];

        var semanticModel = compilation.GetSemanticModel(syntaxTree.SyntaxTree);

        var symbol = semanticModel.GetDeclaredSymbol(syntaxTree);
        if (symbol is null)
            return [];

        return
        [
            ..GetFluentFactoryMetadata(symbol, semanticModel)
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
                        IMethodSymbol constructor =>
                        [
                            new FluentConstructorContext(
                                nameSpace,
                                constructor,
                                alreadyDeclaredRootType,
                                metadata,
                                constructor.ToFluentMethodContexts(compilation))
                        ],
                        INamedTypeSymbol type => CreateFluentConstructorContexts(type, nameSpace,
                            alreadyDeclaredRootType!, metadata),
                        _ => []
                    };
                })
        ];

        ImmutableArray<FluentConstructorContext> CreateFluentConstructorContexts(
            INamedTypeSymbol type,
            string nameSpace,
            ISymbol alreadyDeclaredRootType,
            FluentFactoryMetadata metadata)
        {
            var primaryCtor = type.Constructors.FirstOrDefault(c => c.Parameters.Length > 0);
            if (primaryCtor != null)
                return
                [
                    new FluentConstructorContext(
                        nameSpace,
                        primaryCtor,
                        alreadyDeclaredRootType,
                        metadata,
                        primaryCtor.ToFluentMethodContexts(compilation))
                ];

            return
            [
                ..type.Constructors
                    .Select(ctor =>
                        new FluentConstructorContext(
                            nameSpace,
                            ctor,
                            alreadyDeclaredRootType,
                            metadata,
                            ctor.ToFluentMethodContexts(compilation)))
            ];
        }
    }

    private static bool IsRootTypeDecoratedWithAttribute(INamedTypeSymbol? alreadyDeclaredRootType)
    {
        return alreadyDeclaredRootType is not null
               && alreadyDeclaredRootType
                   .GetAttributes()
                   .Any(attr => attr.AttributeClass?.ToDisplayString() == TypeName.FluentFactoryAttribute);
    }

    private static IEnumerable<FluentFactoryMetadata> GetFluentFactoryMetadata(ISymbol symbol,
        SemanticModel semanticModel)
    {
        return symbol.GetAttributes()
            .Where(a => a.AttributeClass?.ToDisplayString() == TypeName.FluentConstructorAttribute)
            .Select(attribute =>
            {
                // ensure an attribute is present and has an argument
                if (attribute is null || attribute.ConstructorArguments.Length == 0)
                    return FluentFactoryMetadata.Invalid;

                var typeArg = attribute.ConstructorArguments.FirstOrDefault();
                if (typeArg.IsNull)
                    return FluentFactoryMetadata.Invalid;

                // Ensure the argument is a typeof() expression
                if (typeArg.Value is not ITypeSymbol typeSymbol)
                    return FluentFactoryMetadata.Invalid;

                // Grab the options flags symbol
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

    private static FluentFactoryGeneratorOptions ConvertToFluentFactoryGeneratorOptions(
        TypedConstant namedAttributeArgument)
    {
        if (namedAttributeArgument.Kind != TypedConstantKind.Enum)
            return FluentFactoryGeneratorOptions.None;

        // Get the underlying int value
        var value = (int?)namedAttributeArgument.Value ?? (int)FluentOptions.None;

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
            .Where(member =>
            {
                var memberValue = (int?)member.ConstantValue ?? (int)FluentOptions.None;
                return memberValue != 0 && (value & memberValue) == memberValue;
            })
            .ToList();

        return setFlags
            .Select(flag => Enum.TryParse<FluentFactoryGeneratorOptions>(flag.Name, true, out var option)
                ? option
                : FluentFactoryGeneratorOptions.None)
            .Aggregate((prev, next) => prev | next);
    }

    [Conditional("DEBUG")]
    private static void AttachDebugger()
    {
        if (!Debugger.IsAttached) Debugger.Launch();
    }
}
