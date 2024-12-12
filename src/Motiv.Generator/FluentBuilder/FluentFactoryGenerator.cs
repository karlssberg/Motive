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
public class FluentFactoryGenerator : IIncrementalGenerator
{
    private const string GenerateFluentBuilderAttributeFullName = "Motiv.Generator.Attributes.GenerateFluentFactoryAttribute";

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

        // Step 2: Collect and consolidate all FluentConstructors and transform to FluentBuilderFiles
        var consolidated = constructorDeclarations
            .Collect()
            .SelectMany((builderContextsCollection, _) =>
                builderContextsCollection
                    .SelectMany(builderContexts => builderContexts)
                    .GroupBy(builderContext => builderContext.RootTypeFullName)
                    .Select(group =>
                        FluentModelFactory.CreateFluentBuilderFile(group.Key, [..group])));

        // Step 3: Generate based on consolidated view
        context.RegisterSourceOutput(consolidated, GenerateDispatcher);
    }

    private static ImmutableArray<FluentConstructorContext> CreateBuilderModel(
        GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return ImmutableArray<FluentConstructorContext>.Empty;

        var syntax = context.Node;
        var semanticModel = context.SemanticModel;
        var symbol = semanticModel.GetDeclaredSymbol(syntax);
        if (symbol == null)
            return ImmutableArray<FluentConstructorContext>.Empty;

        var metadata = GetFluentFactoryMetadata(symbol);
        var attributePresent = metadata.AttributePresent;
        var rootTypeFullName = metadata.RootTypeFullName;
        if (!attributePresent || string.IsNullOrWhiteSpace(rootTypeFullName))
            return ImmutableArray<FluentConstructorContext>.Empty;

        var lastIndexOf = rootTypeFullName.LastIndexOf('.');
        var nameSpace = lastIndexOf == -1
            ? rootTypeFullName
            : rootTypeFullName.Substring(0, lastIndexOf);

        var alreadyDeclaredRootType = semanticModel.Compilation.GetTypeByMetadataName(rootTypeFullName);

        return symbol switch
        {
            IMethodSymbol method => [new FluentConstructorContext(nameSpace, method, alreadyDeclaredRootType, metadata)],
            INamedTypeSymbol type => CreateFluentConstructorContexts(type),
            _ => ImmutableArray<FluentConstructorContext>.Empty
        };

        ImmutableArray<FluentConstructorContext> CreateFluentConstructorContexts(INamedTypeSymbol type)
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

    private static FluentFactoryMetadata GetFluentFactoryMetadata(ISymbol symbol)
    {
        var attribute = symbol.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToString() == GenerateFluentBuilderAttributeFullName);

        if (attribute == null || attribute.ConstructorArguments.Length == 0)
            return FluentFactoryMetadata.Invalid;

        var rootTypeFullName = attribute.ConstructorArguments[0].Value?.ToString();
        if (string.IsNullOrWhiteSpace(rootTypeFullName))
            return FluentFactoryMetadata.Invalid;

        var namedAttributeArgument = attribute.NamedArguments
            .FirstOrDefault(namedArg => namedArg.Key == nameof(GenerateFluentFactoryAttribute.Options))
            .Value;
        var options = ConvertToFluentFactoryGeneratorOptions(namedAttributeArgument);

        return new FluentFactoryMetadata
        {
            Options = options,
            RootTypeFullName = rootTypeFullName ?? string.Empty
        };
    }

    private static FluentFactoryGeneratorOptions ConvertToFluentFactoryGeneratorOptions(TypedConstant namedAttributeArgument)
    {
        if (namedAttributeArgument.Kind != TypedConstantKind.Enum)
            return FluentFactoryGeneratorOptions.None;

        // Get the underlying int value
        var value = (int?)namedAttributeArgument.Value ?? (int)FluentFactoryOptions.None;

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
                var memberValue = (int?)member.ConstantValue ?? (int)FluentFactoryOptions.None;
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
