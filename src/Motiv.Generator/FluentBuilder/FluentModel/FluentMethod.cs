using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentMethod
{
#if DEBUG
    public int InstanceId => RuntimeHelpers.GetHashCode(this);

    public override string ToString()
    {
        return $"InstanceId: {InstanceId}, MethodName: {MethodName}, SourceParameter: {SourceParameterSymbol?.ToDisplayString()}";
    }

#endif

    public string MethodName { get; }

    public IParameterSymbol? SourceParameterSymbol => FluentParameters
        .Select(p => p.ParameterSymbol)
        .FirstOrDefault();

    public ImmutableArray<FluentParameter> FluentParameters { get; set; } = [];

    public FluentStep? ReturnStep { get; set; }

    public IMethodSymbol? TargetConstructor { get; set; }

    public ParameterSequence KnownConstructorParameters { get; set; } = new();

    public ImmutableArray<IMethodSymbol> ParameterConverters { get; set; } = ImmutableArray<IMethodSymbol>.Empty;

    public IMethodSymbol? ParameterConverter { get; }

    public IParameterSymbol? OverloadParameter => ParameterConverter?.Parameters.FirstOrDefault();

    private readonly Lazy<ImmutableArray<ITypeParameterSymbol>> _typeParameters;

    public FluentMethod(
        string MethodName,
        FluentStep? ReturnStep,
        INamespaceSymbol RootNamespace,
        IMethodSymbol? TargetConstructor = null,
        IMethodSymbol? ParameterConverter = null)
    {
        this.MethodName = MethodName;
        this.RootNamespace = RootNamespace;
        this.ReturnStep = ReturnStep;
        this.TargetConstructor = TargetConstructor;
        this.ParameterConverter = ParameterConverter;
        _typeParameters = new Lazy<ImmutableArray<ITypeParameterSymbol>>(GetTypeParameters);
    }

    public FluentMethod(
        string MethodName,
        INamespaceSymbol rootNamespace,
        IMethodSymbol? constructor,
        IMethodSymbol? ParameterConverter = null)
        : this(MethodName, null, rootNamespace, constructor, ParameterConverter)
    {
    }

    public ImmutableArray<ITypeParameterSymbol> TypeParameters => _typeParameters.Value;
    public INamespaceSymbol RootNamespace { get; set; }

    private ImmutableArray<ITypeParameterSymbol> GetTypeParameters()
    {
        var parameterConverterTypeArguments = ParameterConverter?.TypeArguments.OfType<ITypeParameterSymbol>() ?? [];
        var sourceParameterGenericParameters = SourceParameterSymbol?.Type.GetGenericTypeParameters() ?? [];

        return
        [
            ..sourceParameterGenericParameters.Union(parameterConverterTypeArguments)
        ];
    }

    private sealed class FluentMethodSignatureEqualityComparer : IEqualityComparer<FluentMethod>
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

    public static IEqualityComparer<FluentMethod> FluentMethodSignatureComparer { get; } = new FluentMethodSignatureEqualityComparer();

    public void Deconstruct(out string MethodName, out FluentStep? ReturnStep, out IMethodSymbol? Constructor, out IMethodSymbol? ParameterConverter)
    {
        MethodName = this.MethodName;
        ReturnStep = this.ReturnStep;
        Constructor = this.TargetConstructor;
        ParameterConverter = this.ParameterConverter;
    }
}
