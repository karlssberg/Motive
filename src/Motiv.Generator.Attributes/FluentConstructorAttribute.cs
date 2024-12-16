namespace Motiv.Generator.Attributes;

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class FluentConstructorAttribute(Type rootType) : Attribute
{
    public Type RootType { get; } = rootType;

    public FluentMethodOptions Options { get; set; }
}
