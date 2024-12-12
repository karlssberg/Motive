namespace Motiv.Generator.Attributes;

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateFluentFactoryAttribute(string rootTypeFullName) : Attribute
{
    public string RootTypeFullName { get; } = rootTypeFullName;

    public FluentFactoryOptions Options { get; set; }
}
