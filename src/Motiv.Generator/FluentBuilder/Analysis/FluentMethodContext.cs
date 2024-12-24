using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.Analysis;

public class FluentMethodContext(
    ImmutableArray<FluentMethodContext> priorMethodContexts,
    IParameterSymbol sourceParameter,
    Compilation compilation)
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

    public ImmutableArray<IMethodSymbol> ParameterConverters { get; } = GetMethodOverloads(sourceParameter, compilation);

    public bool Equals(FluentMethodContext? fluentMethodContext)
    {
        if (fluentMethodContext is null) return false;
        if (ReferenceEquals(this, fluentMethodContext)) return true;
        return SymbolEqualityComparer.Default.Equals(SourceParameter, fluentMethodContext.SourceParameter) &&
               PriorMethodContexts.SequenceEqual(fluentMethodContext.PriorMethodContexts);
    }

    public ImmutableArray<FluentMethodContext> PriorMethodContexts { get;} = priorMethodContexts;

    private static ImmutableArray<IMethodSymbol> GetMethodOverloads(IParameterSymbol sourceParameter, Compilation compilation)
    {
        var typeConstant = sourceParameter
            .GetAttributes()
            .Where(attr => attr?.AttributeClass?.ToDisplayString() == TypeName.FluentMethodAttribute)
            .Select(attr => attr.NamedArguments
                .FirstOrDefault(namedArg => namedArg.Key == nameof(FluentMethodAttribute.Overloads))
                .Value)
            .FirstOrDefault();

        // has overloads?
        if (typeConstant.IsNull || typeConstant.Value is not INamedTypeSymbol typeSymbol)
            return ImmutableArray<IMethodSymbol>.Empty;

        return [
            ..typeSymbol
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(method => method.Parameters.Length == 1)
                .Where(method => compilation.IsAssignable(method.ReturnType,sourceParameter.Type))
                .Where(method => method.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == TypeName.FluentParameterConverterAttribute))
        ];
    }
}


