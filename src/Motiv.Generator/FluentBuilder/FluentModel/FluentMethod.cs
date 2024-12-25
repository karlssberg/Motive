using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentMethod(string MethodName, FluentStep? ReturnStep, IMethodSymbol Constructor)
{
#if DEBUG
    public int InstanceId => RuntimeHelpers.GetHashCode(this);
#endif
    public string MethodName { get; } = MethodName;

    public IParameterSymbol? SourceParameterSymbol { get; set; }

    public FluentStep? ReturnStep { get; set; } = ReturnStep;

    public IMethodSymbol Constructor { get; set; } = Constructor;

    public ImmutableArray<IParameterSymbol> KnownConstructorParameters { get; set; } = ImmutableArray<IParameterSymbol>.Empty;
    public ImmutableArray<IMethodSymbol> ParameterConverters { get; set; } = ImmutableArray<IMethodSymbol>.Empty;
    public IMethodSymbol? ParameterConverter { get; set; }
    public IParameterSymbol? OverloadParameter => ParameterConverter?.Parameters.FirstOrDefault();

    private sealed class FluentBuilderMethodEqualityComparer : IEqualityComparer<FluentMethod>
    {
        public bool Equals(FluentMethod? x, FluentMethod? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            if (x.MethodName != y.MethodName) return false;
            if (!x.KnownConstructorParameters
                    .Select(p => (p.Type.ToDisplayString(), p.GetFluentMethodName()))
                    .SequenceEqual(y.KnownConstructorParameters.Select(p => (p.Type.ToDisplayString(), p.GetFluentMethodName()))))
                return false;

            if (x.ParameterConverter is null && y.ParameterConverter is null) return true;
            if (x.ParameterConverter is null || y.ParameterConverter is null) return false;
            if (x.ParameterConverter.ToDisplayString() != y.ParameterConverter.ToDisplayString()) return false;

            if (x.SourceParameterSymbol is null && y.SourceParameterSymbol is null) return true;
            if (x.SourceParameterSymbol is null || y.SourceParameterSymbol is null) return false;

            return x.SourceParameterSymbol.Type.ToDisplayString() == y.SourceParameterSymbol.Type.ToDisplayString();
        }

        public int GetHashCode(FluentMethod obj)
        {
            var hash = obj.KnownConstructorParameters
                .Select(p => (p.Type, Name: p.GetFluentMethodName()))
                .Aggregate(
                    101, (left, right) =>
                        left * 397 ^ right.Type.ToDisplayString().GetHashCode() * 397 ^ right.Name.GetHashCode());

            hash = hash * 397 ^ obj.MethodName.GetHashCode();
            hash = hash * 397 ^ obj.ParameterConverter?.Name.GetHashCode() ?? 0;

            return obj.SourceParameterSymbol is null
                ? hash
                : obj.SourceParameterSymbol.Type.ToDisplayString().GetHashCode();
        }
    }

    public static IEqualityComparer<FluentMethod> FluentBuilderMethodComparer { get; } = new FluentBuilderMethodEqualityComparer();

}
