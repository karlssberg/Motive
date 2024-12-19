using Motiv.Generator.Attributes;

namespace Motiv.SpecDecoratorProposition.PropositionBuilders;

[FluentConstructor(typeof(Spec))]
public partial struct TruePropositionBuilder<TModel, TUnderlyingMetadata>(
    [FluentMethod("Build")]TruePropositionBuilder<TModel, TUnderlyingMetadata> builder)
{

}
