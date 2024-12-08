using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentBuilderMethod(string MethodName, IParameterSymbol SourceParameterSymbol, FluentBuilderStep? ReturnStep)
{
    public string MethodName { get; } = MethodName;

    public IParameterSymbol SourceParameterSymbol { get; set; } = SourceParameterSymbol;

    public FluentBuilderStep? ReturnStep { get; set; } = ReturnStep;

    public IMethodSymbol? Constructor { get; set; }

    public ImmutableArray<IParameterSymbol> ConstructorParameters { get; set; } = ImmutableArray<IParameterSymbol>.Empty;

    private sealed class ConstructorParametersEqualityComparer : IEqualityComparer<FluentBuilderMethod>
    {
        public bool Equals(FluentBuilderMethod? x, FluentBuilderMethod? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.ConstructorParameters.SequenceEqual(y.ConstructorParameters);
        }

        public int GetHashCode(FluentBuilderMethod obj)
        {
            return obj.ConstructorParameters.GetHashCode();
        }
    }

    public static IEqualityComparer<FluentBuilderMethod> ConstructorParametersComparer { get; } = new ConstructorParametersEqualityComparer();

}
