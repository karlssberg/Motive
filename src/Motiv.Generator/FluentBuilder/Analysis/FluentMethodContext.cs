using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder.FluentModel;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.Analysis;

public class FluentMethodContext(
    ImmutableArray<FluentMethodContext> priorMethodContexts,
    IParameterSymbol sourceParameter,
    Compilation compilation)
    : IEquatable<FluentMethodContext>
{
    public IParameterSymbol SourceParameter { get; } = sourceParameter;

    public ImmutableArray<FluentMethodContext> PriorMethodContexts { get; } = priorMethodContexts;

    public bool Equals(FluentMethodContext? fluentMethodContext)
    {
        if (fluentMethodContext is null) return false;
        if (ReferenceEquals(this, fluentMethodContext)) return true;
        return SymbolEqualityComparer.Default.Equals(SourceParameter, fluentMethodContext.SourceParameter) &&
               PriorMethodContexts.SequenceEqual(fluentMethodContext.PriorMethodContexts);
    }

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
            return PriorMethodContexts.Aggregate(
                SymbolEqualityComparer.Default.GetHashCode(SourceParameter) * 397,
                (prev, current) => prev ^ (current.GetHashCode() * 397));
        }
    }
}
