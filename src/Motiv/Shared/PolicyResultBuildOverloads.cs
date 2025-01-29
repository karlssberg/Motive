using Motiv.Generator.Attributes;

namespace Motiv.Shared;

public static class PolicyResultBuildOverloads
{

    [FluentMethodTemplate]
    public static Func<TModel, PolicyResultBase<TMetadata>> Build<TModel, TMetadata>(Func<TModel, PolicyResultBase<TMetadata>> resultFactory) => resultFactory;


    [FluentMethodTemplate]
    public static Func<TModel, PolicyResultBase<string>> Build<TModel>(Func<TModel, PolicyResultBase<string>> resultFactory) => resultFactory;
}
