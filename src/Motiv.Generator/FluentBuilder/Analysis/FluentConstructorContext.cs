using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.FluentModel;

namespace Motiv.Generator.FluentBuilder.Analysis;

public record FluentConstructorContext
{
    public FluentConstructorContext(
        IMethodSymbol constructor,
        INamedTypeSymbol rootSymbol,
        FluentFactoryMetadata metadata,
        SemanticModel semanticModel)
    {
        Constructor = constructor;
        Options = metadata.Options;
        RootTypeFullName = metadata.RootTypeFullName;
        IsStatic = rootSymbol.IsStatic;
        IsRecord = rootSymbol.IsRecord;
        TypeKind = rootSymbol.TypeKind;
        Accessibility = rootSymbol.DeclaredAccessibility;
        ParameterStores = new ConstructorAnalyzer(semanticModel).FindStoringMembers(constructor);
        RootType = rootSymbol;
    }

    public INamedTypeSymbol RootType { get; set; }

    public IReadOnlyDictionary<IParameterSymbol,FluentParameterResolution> ParameterStores { get; set; } =
        new Dictionary<IParameterSymbol, FluentParameterResolution>(FluentParameterComparer.Default);

    public FluentFactoryGeneratorOptions Options { get; }

    public bool IsRecord { get; }

    public Accessibility Accessibility { get; }

    public bool IsStatic { get; }
    public TypeKind TypeKind { get; }
    public IMethodSymbol Constructor { get; }
    public string RootTypeFullName { get; }
}
