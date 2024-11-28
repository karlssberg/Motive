namespace Motiv.Generator.Attributes;

[AttributeUsage(AttributeTargets.Struct)]
public class GenerateFluentBuilderAttribute(string root) : Attribute
{
    public string Root { get;  } = root;
}
