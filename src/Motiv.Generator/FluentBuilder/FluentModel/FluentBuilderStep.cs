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

    public ImmutableArray<IParameterSymbol> ConstructorParameters { get; set; } = [];

    public static IEqualityComparer<FluentBuilderStep> ConstructorParametersComparer { get; } =
        new ConstructorParametersEqualityComparer();

    public ImmutableArray<FluentBuilderMethod> FluentMethods { get; set; } = [];

    public ImmutableArray<IParameterSymbol> GenericConstructorParameters => [
        ..ConstructorParameters
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
            return x.ConstructorParameters.SequenceEqual(y.ConstructorParameters);
        }

        public int GetHashCode(FluentBuilderStep obj)
        {
            return obj.ConstructorParameters.GetHashCode();
        }
    }
}
