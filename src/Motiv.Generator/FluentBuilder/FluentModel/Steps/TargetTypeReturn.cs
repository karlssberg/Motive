using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Generation;

namespace Motiv.Generator.FluentBuilder.FluentModel.Steps;

public class TargetTypeReturn(
    IMethodSymbol targetTypeConstructor,
    ParameterSequence knownConstructorParameters) : IFluentReturn
{
    public ImmutableArray<IParameterSymbol> GenericConstructorParameters { get; } =
    [
        ..knownConstructorParameters
            .Where(parameter => parameter.IsOpenGenericType())
    ];

    public string IdentifierDisplayString(INamespaceSymbol currentNamespace)
    {
        return targetTypeConstructor.ContainingType.ToDynamicDisplayString(currentNamespace);
    }

    public INamespaceSymbol Namespace => targetTypeConstructor.ContainingNamespace;
    public ParameterSequence KnownConstructorParameters { get; } = knownConstructorParameters;
}
