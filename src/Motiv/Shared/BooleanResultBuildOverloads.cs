using Motiv.Generator.Attributes;

namespace Motiv.Shared;

public static class BooleanResultBuildOverloads
{

    [FluentMethodTemplate]
    public static Func<TModel, BooleanResultBase<TMetadata>> Build<TModel, TMetadata>(Func<TModel, BooleanResultBase<TMetadata>> resultFactory) => resultFactory;


    [FluentMethodTemplate]
    public static Func<TModel, BooleanResultBase<string>> Build<TModel>(Func<TModel, BooleanResultBase<string>> resultFactory) => resultFactory;
}
