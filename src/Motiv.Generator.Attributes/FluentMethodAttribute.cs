namespace Motiv.Generator.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class FluentMethodAttribute(string methodName) : Attribute;
