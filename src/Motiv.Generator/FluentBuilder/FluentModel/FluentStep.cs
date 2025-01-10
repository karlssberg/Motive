using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel;

[DebuggerDisplay("{ToString()}")]
public class FluentStep(INamedTypeSymbol rootType)
{
#if DEBUG
    public int InstanceId => RuntimeHelpers.GetHashCode(this);
#endif
    public string Name { get; set; } = "Step";

    public bool IsExistingPartialType { get; set; }

    /// <summary>
    /// The known constructor parameters up until this step.
    /// Potentially more parameters are required to satisfy a constructor signature.
    /// </summary>
    public ParameterSequence KnownConstructorParameters { get; set; } = [];

    public IList<FluentMethod> FluentMethods { get; set; } = [];

    public ImmutableArray<IParameterSymbol> GenericConstructorParameters => [
        ..KnownConstructorParameters
            .Where(parameter => parameter.IsOpenGenericType())
    ];

    public Accessibility Accessibility { get; set; } = Accessibility.Internal;

    public override string ToString()
    {
        return string.Join(", ", KnownConstructorParameters.Select(p => p.ToDisplayString()));
    }

    public string Identifier => ExistingStepConstructor?.ContainingType.Name ?? Name;

    public bool IsEndStep { get; set; }
    public TypeKind TypeKind { get; set; }
    public bool IsRecord { get; set; }

    public IMethodSymbol? ExistingStepConstructor { get; set; }

    public IReadOnlyDictionary<IParameterSymbol, IPropertySymbol?> ParameterStoreMembers { get; set; } =
        new Dictionary<IParameterSymbol, IPropertySymbol?>(FluentParameterComparer.Default);

    public INamedTypeSymbol RootType { get; } = rootType;
}
