using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.FluentModel.Methods;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel.Steps;

[DebuggerDisplay("{ToString()}")]
public class ExistingTypeFluentStep(INamedTypeSymbol rootType, IMethodSymbol existingStepConstructor) : IFluentStep
{
#if DEBUG
    public int InstanceId => RuntimeHelpers.GetHashCode(this);
#endif
    public string Name { get; set; } = existingStepConstructor.ContainingType.Name;

    /// <summary>
    /// The known constructor parameters up until this step.
    /// Potentially more parameters are required to satisfy a constructor signature.
    /// </summary>
    public ParameterSequence KnownConstructorParameters { get; set; } = [];

    public IList<IFluentMethod> FluentMethods { get; set; } = [];

    public ImmutableArray<IParameterSymbol> GenericConstructorParameters => [
        ..KnownConstructorParameters
            .Where(parameter => parameter.IsOpenGenericType())
    ];

    public Accessibility Accessibility { get; set; } = Accessibility.Internal;

    public TypeKind TypeKind { get; set; }
    public bool IsRecord { get; set; }

    public IMethodSymbol ExistingStepConstructor { get; } = existingStepConstructor;

    public IReadOnlyDictionary<IParameterSymbol, FluentParameterResolution> ParameterStoreMembers { get; set; } =
        new Dictionary<IParameterSymbol, FluentParameterResolution>(FluentParameterComparer.Default);

    public INamedTypeSymbol RootType { get; } = rootType;


    public string IdentifierDisplayString(INamespaceSymbol currentNamespace)
    {
        return ExistingStepConstructor.ContainingType.ToDynamicDisplayString(currentNamespace);
    }

    public INamespaceSymbol Namespace => ExistingStepConstructor.ContainingNamespace;

    public IFluentStep MergeWith(IFluentStep other)
    {
        Debug.Assert(other is ExistingTypeFluentStep, "Cannot merge two existing ExistingTypeFluentStep instances");

        return new ExistingTypeFluentStep(RootType, ExistingStepConstructor)
        {
            Name = Name,
            KnownConstructorParameters = KnownConstructorParameters,
            FluentMethods = [..FluentMethods, ..other.FluentMethods],
            Accessibility = Accessibility,
            TypeKind = TypeKind,
            IsRecord = IsRecord,
            ParameterStoreMembers = ParameterStoreMembers
        };
    }
}
