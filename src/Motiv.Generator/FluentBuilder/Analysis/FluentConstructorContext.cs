using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.Analysis;

public record FluentConstructorContext
{
    private readonly Lazy<ImmutableArray<FluentMethodContext>> _lazyFluentMethods;

    public FluentConstructorContext(string @namespace, IMethodSymbol constructor, string rootTypeFullName)
    {
        NameSpace = @namespace;
        Constructor = constructor;
        RootTypeFullName = rootTypeFullName;
        _lazyFluentMethods =  new Lazy<ImmutableArray<FluentMethodContext>>(() =>
        [
            ..Constructor?.Parameters
                .Aggregate(ImmutableArray<FluentMethodContext>.Empty,
                    (parameterArray, parameter) =>
                        parameterArray.Add(new FluentMethodContext(parameterArray, parameter)))
            ?? []
        ]);
    }

    public string NameSpace { get; set; }
    public IMethodSymbol Constructor { get; set; }
    public string RootTypeFullName { get; set; }
    public ImmutableArray<FluentMethodContext> FluentMethodContexts => _lazyFluentMethods.Value;

}
