using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentMethod(string MethodName, FluentStep? ReturnStep, IMethodSymbol? Constructor = null)
{
    public FluentMethod(string MethodName, IMethodSymbol? Constructor) : this(MethodName, null, Constructor)
    {
    }

#if DEBUG
    public int InstanceId => RuntimeHelpers.GetHashCode(this);

    public override string ToString()
    {
        return $"InstanceId: {InstanceId}, MethodName: {MethodName}, SourceParameter: {SourceParameterSymbol?.ToDisplayString()}";
    }
#endif

    public string MethodName { get; } = MethodName;

    public IParameterSymbol? SourceParameterSymbol => FluentParameters
        .Select(p => p.ParameterSymbol)
        .FirstOrDefault();

    public ImmutableArray<FluentParameter> FluentParameters { get; set; } = [];

    public FluentStep? ReturnStep { get; set; } = ReturnStep;

    public IMethodSymbol? Constructor { get; set; } = Constructor;

    public ParameterSequence KnownConstructorParameters { get; set; } = new();

    public ImmutableArray<IMethodSymbol> ParameterConverters { get; set; } = ImmutableArray<IMethodSymbol>.Empty;

    public IMethodSymbol? ParameterConverter { get; set; }

    public IParameterSymbol? OverloadParameter => ParameterConverter?.Parameters.FirstOrDefault();

    private sealed class FluentMethodEqualityComparer : IEqualityComparer<FluentMethod>
    {
        public bool Equals(FluentMethod? x, FluentMethod? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            if (x.MethodName != y.MethodName) return false;
            if (x.KnownConstructorParameters != y.KnownConstructorParameters) return false;

            if (x.SourceParameterSymbol is null && y.SourceParameterSymbol is null) return true;
            if (x.SourceParameterSymbol is null || y.SourceParameterSymbol is null) return false;

            return x.SourceParameterSymbol.Type.ToDisplayString() == y.SourceParameterSymbol.Type.ToDisplayString();
        }

        public int GetHashCode(FluentMethod obj)
        {
            var hash = obj.KnownConstructorParameters.GetHashCode();

            hash = hash * 397 ^ obj.MethodName.GetHashCode();

            return obj.SourceParameterSymbol is null
                ? hash
                : obj.SourceParameterSymbol.Type.ToDisplayString().GetHashCode();
        }
    }

    public static IEqualityComparer<FluentMethod> FluentMethodComparer { get; } = new FluentMethodEqualityComparer();
}
