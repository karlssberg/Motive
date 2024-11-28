namespace Motiv.Generator.FluentBuilder;

public record FluentBuilderContext
{
    public required string NameSpace { get; set; }
    public required ConstructorContext Constructor { get; set; }
    public required string RootTypeFullName { get; set; }
}
