using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.Analysis;

public class FluentMethodContext(
    ImmutableArray<FluentMethodContext> priorSourceParameters,
    IParameterSymbol sourceParameter)
    : IEquatable<FluentMethodContext>
{
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FluentMethodContext)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return PriorSourceParameters.Aggregate(
                SymbolEqualityComparer.Default.GetHashCode(SourceParameter) * 397,
                (prev, current) => prev ^ current.GetHashCode() * 397);
        }
    }

    public IParameterSymbol SourceParameter { get; } = sourceParameter;

    public bool Equals(FluentMethodContext? fluentMethodContext)
    {
        if (fluentMethodContext is null) return false;
        if (ReferenceEquals(this, fluentMethodContext)) return true;
        return SymbolEqualityComparer.Default.Equals(SourceParameter, fluentMethodContext.SourceParameter) &&
               PriorSourceParameters.SequenceEqual(fluentMethodContext.PriorSourceParameters);
    }

    public ImmutableArray<FluentMethodContext> PriorSourceParameters { get;} = priorSourceParameters;

    public static IEqualityComparer<FluentMethodContext> PriorSourceParametersComparer { get; } = new PriorSourceParametersEqualityComparer();

    private sealed class PriorSourceParametersEqualityComparer : IEqualityComparer<FluentMethodContext>
    {
        public bool Equals(FluentMethodContext? x, FluentMethodContext? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.PriorSourceParameters.SequenceEqual(y.PriorSourceParameters);
        }

        public int GetHashCode(FluentMethodContext obj)
        {
            return obj.PriorSourceParameters.GetHashCode();
        }
    }
}


