using Motiv.Generator.Attributes;

namespace Motiv.Shared;

public static class SpecBuildOverloads
{

    [FluentMethodTemplate]
    public static SpecBase<TModel, TMetadata> Build<TModel, TMetadata>(SpecBase<TModel, TMetadata> spec) => spec;

    [FluentMethodTemplate]
    public static SpecBase<TModel, TMetadata> Build<TModel, TMetadata>(Func<SpecBase<TModel, TMetadata>> specFactory) => specFactory();

    [FluentMethodTemplate]
    public static SpecBase<TModel, string> Build<TModel>(SpecBase<TModel, string> spec) => spec;

    [FluentMethodTemplate]
    public static SpecBase<TModel, string> Build<TModel>(Func<SpecBase<TModel, string>> specFactory) => specFactory();
}
