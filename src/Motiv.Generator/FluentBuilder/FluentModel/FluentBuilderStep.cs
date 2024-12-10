using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel;


public record FluentBuilderStep
{
    public string Name { get; set; } = "Step";

    public IMethodSymbol? Constructor { get; set; }

    public FluentBuilderStep? Parent { get; set; }

    public bool IsRoot => Parent is null;

    /// <summary>
    /// The known constructor parameters up until this step.
    /// Potentially more parameters are required to satisfy a constructor signature.
    /// </summary>
    public ImmutableArray<IParameterSymbol> KnownConstructorParameters { get; set; } = [];

    public static IEqualityComparer<FluentBuilderStep> ConstructorParametersComparer { get; } =
        new ConstructorParametersEqualityComparer();

    public ImmutableArray<FluentBuilderMethod> FluentMethods { get; set; } = [];

    public ImmutableArray<IParameterSymbol> GenericConstructorParameters => [
        ..KnownConstructorParameters
            .Where(parameter => parameter.IsOpenGenericType())
    ];

    private sealed class ConstructorParametersEqualityComparer : IEqualityComparer<FluentBuilderStep>
    {
        public bool Equals(FluentBuilderStep? x, FluentBuilderStep? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.KnownConstructorParameters.SequenceEqual(y.KnownConstructorParameters);
        }

        public int GetHashCode(FluentBuilderStep obj)
        {
            return obj.KnownConstructorParameters.GetHashCode();
        }
    }
}
