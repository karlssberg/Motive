namespace Motiv.Generator.Attributes;

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateFluentBuilderAttribute(string rootTypeFullName) : Attribute
{
    public string RootTypeFullName { get; } = rootTypeFullName;
}
