namespace Motiv.Generator.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class FluentMethodNameAttribute(string methodName) : Attribute
{
    public string MethodName { get; } = methodName;
}
