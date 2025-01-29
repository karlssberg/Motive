using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel.Steps;

public interface IFluentReturn
{
    string IdentifierDisplayString(INamespaceSymbol currentNamespace);

    INamespaceSymbol Namespace { get; }

    /// <summary>
    /// The known constructor parameters up until this step.
    /// Potentially more parameters are required to satisfy a constructor signature.
    /// </summary>
    ParameterSequence KnownConstructorParameters { get; }
}
