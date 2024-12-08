using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder;

[Generator]
public class FluentBuilderGenerator : IIncrementalGenerator
{
    private static readonly string GenerateFluentBuilderAttributeFullName = typeof(GenerateFluentBuilderAttribute).FullName;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //AttachDebugger();

        // Step 1: Find all FluentConstructors
        var constructorDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (node, _) => node switch
                {
                    TypeDeclarationSyntax { ParameterList: not null} type => type.AttributeLists.Count > 0,
                    ConstructorDeclarationSyntax ctor => ctor.AttributeLists.Count > 0,
                    _ => false
                },
                transform: CreateBuilderModel);

        // Step 2: Collect and consolidate all FluentConstructors
        var consolidated = constructorDeclarations
            .Collect()
            .SelectMany((builderContextsCollection, _) =>
                builderContextsCollection
                    .SelectMany(builderContexts => builderContexts
                    .GroupBy(builderContext => builderContext.RootTypeFullName)
                    .Select(group =>
                        FluentModelFactory.CreateFluentBuilderFile(group.Key, [..group]))));

        // Step 3: Generate based on consolidated view
        context.RegisterSourceOutput(consolidated, GenerateDispatcher);
    }

    private static ImmutableArray<FluentConstructorContext> CreateBuilderModel(
        GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) return ImmutableArray<FluentConstructorContext>.Empty;

        var syntax = context.Node;
        var semanticModel = context.SemanticModel;
        var symbol = semanticModel.GetDeclaredSymbol(syntax);

        var attribute = symbol?.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToString() == GenerateFluentBuilderAttributeFullName);

        if (attribute == null) return ImmutableArray<FluentConstructorContext>.Empty;
        if (attribute.ConstructorArguments.Length == 0) return ImmutableArray<FluentConstructorContext>.Empty;

        var rootTypeFullName = attribute.ConstructorArguments[0].Value?.ToString();
        if (rootTypeFullName == null) return ImmutableArray<FluentConstructorContext>.Empty;

        var nameSpace = rootTypeFullName.Substring(0, rootTypeFullName.LastIndexOf('.'));

        return symbol switch
        {
            IMethodSymbol method => [new FluentConstructorContext(nameSpace, method, rootTypeFullName)],
            INamedTypeSymbol type => CreateFluentConstructorContexts(type),
            _ => ImmutableArray<FluentConstructorContext>.Empty
        };

        ImmutableArray<FluentConstructorContext> CreateFluentConstructorContexts(INamedTypeSymbol type)
        {
            var primaryCtor = type.Constructors.FirstOrDefault(c => c.Parameters.Length > 0);
            if (primaryCtor != null)
                return [new FluentConstructorContext(nameSpace, primaryCtor, rootTypeFullName)];

            return
            [
                ..type.Constructors
                    .Where(c => c.Parameters.Length > 0)
                    .Select(ctor =>
                        new FluentConstructorContext(nameSpace, ctor, rootTypeFullName))
            ];
        }
    }

    private static void GenerateDispatcher(
        SourceProductionContext context,
        FluentBuilderFile builder)
    {
        var source = FluentModelToCodeConverter.CreateCompilationUnit(builder).NormalizeWhitespace().ToString();

        context.AddSource($"{builder.NameSpace}.{builder.Name}.g.cs", source);
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
