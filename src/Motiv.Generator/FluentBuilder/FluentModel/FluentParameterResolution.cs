using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public record FluentParameterResolution(IPropertySymbol? ExistingPropertySymbol = null, IFieldSymbol? ExistingFieldSymbol = null)
{
    public enum ValueLocationType
    {
        Member,
        PrimaryConstructorParameter
    }
    public IPropertySymbol? ExistingPropertySymbol { get; } = ExistingPropertySymbol;

    public IFieldSymbol? ExistingFieldSymbol { get; } = ExistingFieldSymbol;

    public ValueLocationType ResolutionType { get; set; } = ValueLocationType.Member;
}

