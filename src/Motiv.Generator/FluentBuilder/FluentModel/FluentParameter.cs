using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public class FluentParameter(IParameterSymbol parameterSymbol) : IEquatable<FluentParameter>
{
    private readonly (string, string) _key = (parameterSymbol.Type.ToDisplayString(), parameterSymbol.GetFluentMethodName());

    public IParameterSymbol ParameterSymbol { get; } = parameterSymbol;

    public string Name { get; } = parameterSymbol.GetFluentMethodName();

    public bool Equals(FluentParameter? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _key.Equals(other._key);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FluentParameter)obj);
    }

    public override int GetHashCode() => _key.GetHashCode();

    public override string ToString() => $"{_key.Item1} {_key.Item2}";
}
