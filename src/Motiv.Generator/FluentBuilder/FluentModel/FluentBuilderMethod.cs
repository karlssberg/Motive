using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentBuilderMethod(string MethodName, FluentBuilderStep? ReturnStep)
{
    public string MethodName { get; } = MethodName;

    public IParameterSymbol? SourceParameterSymbol { get; set; }

    public FluentBuilderStep? ReturnStep { get; set; } = ReturnStep;

    public IMethodSymbol? Constructor { get; set; }

    public ImmutableArray<IParameterSymbol> KnownConstructorParameters { get; set; } = ImmutableArray<IParameterSymbol>.Empty;

    private sealed class FluentBuilderMethodEqualityComparer : IEqualityComparer<FluentBuilderMethod>
    {
        public bool Equals(FluentBuilderMethod? x, FluentBuilderMethod? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            if (x.MethodName != y.MethodName) return false;
            if (!x.KnownConstructorParameters
                    .Select(p => (p.Type, p.Name))
                    .SequenceEqual(y.KnownConstructorParameters.Select(p => (p.Type, p.Name))))
                return false;

            if (x.SourceParameterSymbol is null && y.SourceParameterSymbol is null) return true;
            if (x.SourceParameterSymbol is null || y.SourceParameterSymbol is null) return false;

            if (x.SourceParameterSymbol.Type.IsOpenGenericType() || y.SourceParameterSymbol.Type.IsOpenGenericType())
                return x.SourceParameterSymbol.Type.ToString() == y.SourceParameterSymbol.Type.ToString();
            return SymbolEqualityComparer.Default.Equals(x.SourceParameterSymbol?.Type, y.SourceParameterSymbol?.Type);
        }

        public int GetHashCode(FluentBuilderMethod obj)
        {
            var hash = obj.KnownConstructorParameters
                .Select(p => (p.Type, p.Name))
                .Aggregate(
                    101,
                    (left, right) =>
                        left * 397 ^ SymbolEqualityComparer.Default.GetHashCode(right.Type) * 397 ^ right.Name.GetHashCode());

            hash = hash * 397 ^ obj.MethodName.GetHashCode();

            if (obj.SourceParameterSymbol is null)
                return hash;

            return obj.SourceParameterSymbol.Type.IsOpenGenericType()
                ? hash * 397 ^ obj.SourceParameterSymbol.Type.ToString().GetHashCode()
                : hash * 397 ^ SymbolEqualityComparer.Default.GetHashCode(obj.SourceParameterSymbol.Type);
        }
    }

    public static IEqualityComparer<FluentBuilderMethod> FluentBuilderMethodComparer { get; } = new FluentBuilderMethodEqualityComparer();

}
