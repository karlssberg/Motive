using System.Collections;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public class KnownConstructorParameters : IEquatable<KnownConstructorParameters>, IEnumerable<IParameterSymbol>
{
    private ImmutableArray<IParameterSymbol> ParameterSymbols { get; }
    private (string Type, string Name)[] ParameterSymbolsPrecomputed { get; }

    private readonly int _hashCode;

    public KnownConstructorParameters()
    {
        ParameterSymbols = [];
        ParameterSymbolsPrecomputed = [];
        _hashCode = 101;
    }

    public KnownConstructorParameters(IEnumerable<IParameterSymbol> parameterSymbols)
    {
        ImmutableArray<IParameterSymbol> parameterSymbolsArray = [..parameterSymbols];
        ParameterSymbols = parameterSymbolsArray;
        ParameterSymbolsPrecomputed = parameterSymbolsArray.Select(p => (Type: p.Type.ToDisplayString(), Name: p.GetFluentMethodName())).ToArray();
        _hashCode = parameterSymbolsArray
            .Select(p => (p.Type, Name: p.GetFluentMethodName()))
            .Aggregate(
                101, (left, right) =>
                    left * 397 ^ right.Type.ToDisplayString().GetHashCode() * 397 ^ right.Name.GetHashCode());
    }

    public KnownConstructorParameters(IEnumerable<FluentParameter> fluentParameters) : this(fluentParameters.Select(fp => fp.ParameterSymbol))
    {
    }

    public IEnumerator<IParameterSymbol> GetEnumerator()
    {
        return ParameterSymbols.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)ParameterSymbols).GetEnumerator();
    }

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals(obj as KnownConstructorParameters);
    }

    public bool Equals(KnownConstructorParameters? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ParameterSymbolsPrecomputed.SequenceEqual(other.ParameterSymbolsPrecomputed);
    }

    public static implicit operator ImmutableArray<IParameterSymbol> (KnownConstructorParameters knownConstructorParameters)
    {
        return knownConstructorParameters.ParameterSymbols;
    }

    public static bool operator ==(KnownConstructorParameters left, KnownConstructorParameters right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(KnownConstructorParameters left, KnownConstructorParameters right)
    {
        return !(left == right);
    }
}
