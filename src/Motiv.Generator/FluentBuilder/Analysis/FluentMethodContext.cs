using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.Attributes;

namespace Motiv.Generator.FluentBuilder.Analysis;

public class FluentMethodContext(
    ImmutableArray<FluentMethodContext> priorMethodContexts,
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
            return PriorMethodContexts.Aggregate(
                SymbolEqualityComparer.Default.GetHashCode(SourceParameter) * 397,
                (prev, current) => prev ^ current.GetHashCode() * 397);
        }
    }

    public IParameterSymbol SourceParameter { get; } = sourceParameter;

    private readonly Lazy<ImmutableArray<IMethodSymbol>> _lazyParameterOverloads = new(() =>
    {
        var typeConstant = sourceParameter
            .GetAttributes()
            .Where(attr => attr?.AttributeClass?.ToDisplayString() == TypeName.FluentMethodAttribute)
            .Select(attr => attr.NamedArguments
                .FirstOrDefault(namedArg => namedArg.Key == nameof(FluentMethodAttribute.Overloads))
                .Value)
            .FirstOrDefault();
        if (typeConstant.IsNull || typeConstant.Value is not INamedTypeSymbol typeSymbol)
            return ImmutableArray<IMethodSymbol>.Empty;

        return [
            ..typeSymbol
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(method => method.Parameters.Length == 1)
                .Where(method => method.ReturnType.Name == sourceParameter.Type.Name)
                .Where(method => method.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == TypeName.FluentParameterConverterAttribute))
        ];
    });

    public ImmutableArray<IMethodSymbol> ParameterConverters  => _lazyParameterOverloads.Value;

    public bool Equals(FluentMethodContext? fluentMethodContext)
    {
        if (fluentMethodContext is null) return false;
        if (ReferenceEquals(this, fluentMethodContext)) return true;
        return SymbolEqualityComparer.Default.Equals(SourceParameter, fluentMethodContext.SourceParameter) &&
               PriorMethodContexts.SequenceEqual(fluentMethodContext.PriorMethodContexts);
    }

    public ImmutableArray<FluentMethodContext> PriorMethodContexts { get;} = priorMethodContexts;
}


