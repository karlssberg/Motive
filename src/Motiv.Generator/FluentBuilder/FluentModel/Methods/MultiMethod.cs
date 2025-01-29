using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.FluentModel.Steps;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel.Methods;

public class MultiMethod : IFluentMethod
{
    public string MethodName { get; }
    public ImmutableArray<FluentMethodParameter> MethodParameters { get; }

    public IParameterSymbol? SourceParameterSymbol { get; }

    public ImmutableArray<FluentMethodParameter> AvailableParameterFields { get; }

    public IFluentReturn Return { get; }

    public IMethodSymbol ParameterConverter { get; }

    private readonly Lazy<ImmutableArray<FluentTypeParameter>> _typeParameters;

    public MultiMethod(
        string methodName,
        IParameterSymbol sourceParameterSymbol,
        IFluentReturn fluentReturn,
        INamespaceSymbol rootNamespace,
        IMethodSymbol parameterConverter,
        ImmutableArray<FluentMethodParameter> availableParameterFields)
    {
        MethodName = methodName;
        MethodParameters = [..parameterConverter.Parameters.Select(p => new FluentMethodParameter(p, methodName))];
        RootNamespace = rootNamespace;
        Return = fluentReturn;
        ParameterConverter = parameterConverter;
        AvailableParameterFields = availableParameterFields;
        _typeParameters = new Lazy<ImmutableArray<FluentTypeParameter>>(GetTypeParameters);
    }

    public ImmutableArray<FluentTypeParameter> TypeParameters => _typeParameters.Value;
    public INamespaceSymbol RootNamespace { get; }

    private ImmutableArray<FluentTypeParameter> GetTypeParameters()
    {
        var parameterConverterTypeArguments = ParameterConverter?.TypeArguments
            .OfType<ITypeParameterSymbol>()
            .Select(typeParameter => new FluentTypeParameter(typeParameter)) ?? [];

        var sourceParameterGenericParameters = SourceParameterSymbol?.Type
            .GetGenericTypeParameters()
            .Select(typeParameter => new FluentTypeParameter(typeParameter)) ?? [];

        return
        [
            ..sourceParameterGenericParameters.Union(parameterConverterTypeArguments)
        ];
    }
}
