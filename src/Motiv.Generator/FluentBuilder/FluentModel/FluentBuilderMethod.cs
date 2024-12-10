using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentBuilderMethod(string MethodName, IParameterSymbol SourceParameterSymbol, FluentBuilderStep? ReturnStep)
{
    public string MethodName { get; } = MethodName;

    public IParameterSymbol SourceParameterSymbol { get; set; } = SourceParameterSymbol;

    public FluentBuilderStep? ReturnStep { get; set; } = ReturnStep;

    public IMethodSymbol? Constructor { get; set; }

    public ImmutableArray<IParameterSymbol> ConstructorParameters { get; set; } = ImmutableArray<IParameterSymbol>.Empty;

    private sealed class FluentBuilderMethodEqualityComparer : IEqualityComparer<FluentBuilderMethod>
    {
        public bool Equals(FluentBuilderMethod? x, FluentBuilderMethod? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            if (x.MethodName != y.MethodName) return false;
            return x.ConstructorParameters.SequenceEqual(y.ConstructorParameters)
                   && Equals(x.SourceParameterSymbol.Type, y.SourceParameterSymbol.Type);
        }

        private static bool Equals(ITypeSymbol x, ITypeSymbol y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (SymbolEqualityComparer.Default.Equals(x, y)) return true;
            return x.IsOpenGenericType()
                   && y.IsOpenGenericType()
                   && x.Name == y.Name;
        }

        public int GetHashCode(FluentBuilderMethod obj)
        {
            var hash = obj.ConstructorParameters.GetHashCode() * 397
                                    ^ obj.MethodName.GetHashCode() * 397;

            return obj.SourceParameterSymbol.Type.IsOpenGenericType()
                ? hash ^ obj.SourceParameterSymbol.Type.Name.GetHashCode()
                : hash ^ SymbolEqualityComparer.Default.GetHashCode(obj.SourceParameterSymbol.Type);
        }
    }

    public static IEqualityComparer<FluentBuilderMethod> FluentBuilderMethodComparer { get; } = new FluentBuilderMethodEqualityComparer();

}
