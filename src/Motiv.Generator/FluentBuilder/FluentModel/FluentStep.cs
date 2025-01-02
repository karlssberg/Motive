using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel;

[DebuggerDisplay("{ToString()}")]
public class FluentStep
{
#if DEBUG
    public int InstanceId => RuntimeHelpers.GetHashCode(this);
#endif
    public string Name { get; set; } = "Step";

    public FluentStep? Parent { get; set; }

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

    public override string ToString()
    {
        return string.Join(", ", KnownConstructorParameters.Select(p => p.ToDisplayString()));
    }

    public FluentStep Clone()
    {
        return new FluentStep
        {
            Name = Name,
            KnownConstructorParameters = KnownConstructorParameters,
            FluentMethods = [..FluentMethods.Select(method => method with { ReturnStep = method.ReturnStep?.Clone()})],
            Parent = Parent
        };
    }
}
