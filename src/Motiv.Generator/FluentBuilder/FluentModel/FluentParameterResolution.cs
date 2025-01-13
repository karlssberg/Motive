using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentParameterResolution(IPropertySymbol? PropertySymbol = null, IFieldSymbol? FieldSymbol = null)
{
    public IPropertySymbol? PropertySymbol { get; } = PropertySymbol;

    public IFieldSymbol? FieldSymbol { get; } = FieldSymbol;

    public bool ShouldInitializeFromPrimaryConstructor { get; set; }
}
