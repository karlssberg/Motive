using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Motiv.Generator.FluentBuilder.Analysis;
using Motiv.Generator.FluentBuilder.Generation;
using Motiv.Generator.FluentBuilder.Generation.Shared;

namespace Motiv.Generator.FluentBuilder.FluentModel;


public class FluentModelFactory
{
    private OrderedDictionary<FluentStep, FluentStep> _steps = new(FluentStep.ConstructorParametersComparer);

    public FluentFactoryCompilationUnit CreateFluentFactoryCompilationUnit(string fullname, ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        _steps = new OrderedDictionary<FluentStep, FluentStep>(FluentStep.ConstructorParametersComparer);

        ImmutableArray<INamespaceSymbol> usings =
            [
                ..fluentConstructorContexts
                    .SelectMany(ctx => ctx.Constructor.Parameters)
                    .Select(parameter => parameter.Type)
                    .Select(type => type.ContainingNamespace)
                    .Concat(fluentConstructorContexts.Select(ctx => ctx.Constructor.ContainingType.ContainingNamespace))
                    .DistinctBy(ns => ns.ToDisplayString())
                    .OrderBy(ns => ns.ToDisplayString())
            ];

        var startingFluentSteps = GetStartingFluentSteps(fluentConstructorContexts);

        var mergedFluentBuilderSteps = MergeFluentSteps(startingFluentSteps).ToImmutableArray();

        var fluentBuilderSteps = _steps.Values
            .Except(startingFluentSteps)
            .Reverse()
            .Select((step, index) =>
            {
                step.Name = $"Step_{index}__{fullname.Replace(".", "_")}";
                return step;
            })
            .ToImmutableArray();

        var fluentBuilderMethods = mergedFluentBuilderSteps
            .SelectMany(step => step.FluentMethods)
            .ToImmutableArray();

        var sampleConstructorContext = fluentConstructorContexts.First();
        return new FluentFactoryCompilationUnit(
            fullname,
            [..fluentBuilderMethods],
            fluentBuilderSteps,
            usings)
        {
            IsStatic = sampleConstructorContext.IsStatic,
            TypeKind = sampleConstructorContext.TypeKind,
            Accessibility = sampleConstructorContext.Accessibility,
            IsRecord = sampleConstructorContext.IsRecord
        };
    }

    private static ImmutableArray<FluentStep> GetStartingFluentSteps(ImmutableArray<FluentConstructorContext> fluentConstructorContexts)
    {
        return
        [
            ..fluentConstructorContexts
                .Select(constructorContext => constructorContext.FluentMethodContexts
                    .Reverse()
                    .Aggregate(default(FluentStep?), (previousStep, method) =>
                        CreateFluentBuilderStep(method, previousStep, constructorContext)))
                .Where(step => step is not null)
        ];
    }

    private static FluentStep? CreateFluentBuilderStep(
        FluentMethodContext method,
        FluentStep? nextStep,
        FluentConstructorContext constructorContext)
    {
        var fluentMethodName = method.SourceParameter.GetFluentMethodName();

        var constructorParameters =
            method.PriorMethodContexts
                .Select(methodContext => methodContext.SourceParameter)
                .ToImmutableArray();

        var fluentBuilderMethod =
            new FluentMethod(
                fluentMethodName,
                nextStep ?? MaybeCreateCreationMethodStep(nextStep, constructorContext),
                constructorContext.Constructor)
            {
                SourceParameterSymbol = method.SourceParameter,
                KnownConstructorParameters = constructorParameters,
                ParameterConverters = method.ParameterConverters
            };

        var fluentBuilderStep = new FluentStep
        {
            KnownConstructorParameters = [..constructorParameters],
            FluentMethods = [
                fluentBuilderMethod,
                ..fluentBuilderMethod.ParameterConverters
                    .Select<IMethodSymbol, FluentMethod>(converter =>
                    {
                        var normalizedConverter = NormalizedConverterMethod(converter, fluentBuilderMethod);
                        return fluentBuilderMethod with
                        {
                            SourceParameterSymbol = normalizedConverter.Parameters.First(),
                            ParameterConverter = normalizedConverter
                        };
                    })
            ]
        };

        if (fluentBuilderMethod.ReturnStep is not null)
        {
            fluentBuilderMethod.ReturnStep.Parent = fluentBuilderStep;
        }

        return fluentBuilderStep;
    }

    private static FluentStep? MaybeCreateCreationMethodStep(FluentStep? nextStep, FluentConstructorContext constructorContext)
    {
        if (constructorContext.Options.HasFlag(FluentFactoryGeneratorOptions.NoCreateMethod))
            return null;

        return new FluentStep
        {
            KnownConstructorParameters = [..constructorContext.Constructor.Parameters],
            FluentMethods =
            [
                new FluentMethod("Create", nextStep, constructorContext.Constructor)
                {
                    KnownConstructorParameters = constructorContext.Constructor.Parameters,
                }
            ]
        };
    }

    private IEnumerable<FluentStep> MergeFluentSteps(IEnumerable<FluentStep> fluentBuilderSteps)
    {
        var groupedSteps = fluentBuilderSteps
            .GroupBy(step => step, FluentStep.ConstructorParametersComparer);

        foreach (var group in groupedSteps)
        {
            var mergedStep = group
                .Aggregate((prevStep, nextStep) =>
                {
                    prevStep.FluentMethods = prevStep.FluentMethods.AddRange(nextStep.FluentMethods);
                    return prevStep;
                });
            mergedStep.FluentMethods = [..MergeFluentMethods(mergedStep.FluentMethods)];
            foreach (var method in mergedStep.FluentMethods.Where(method => method.ReturnStep is not null))
            {
                method.ReturnStep!.Parent = mergedStep;
            }

            if (_steps.TryGetValue(mergedStep, out var step))
            {
                yield return step;
                yield break;
            }

            _steps.Add(mergedStep, mergedStep);
            yield return mergedStep;
        }
    }

    private static IMethodSymbol NormalizedConverterMethod(IMethodSymbol converter, FluentMethod method)
    {
        var returnType = converter.ReturnType;
        var sourceParameter = method.SourceParameterSymbol;

        var mapping = TypeMapper.MapGenericArguments(returnType, sourceParameter!.Type);

        var normalizedMethod = converter.NormalizeMethodTypeParameters(mapping);

        return normalizedMethod;
    }

    private IEnumerable<FluentMethod> MergeFluentMethods(IEnumerable<FluentMethod> fluentMethods)
    {
        var groups = fluentMethods
            .GroupBy(method => method, FluentMethod.FluentBuilderMethodComparer);

        foreach (var group in groups)
        {
            var method = group
                .Aggregate((prevMethod, nextMethod) =>
                {
                    if (prevMethod.ReturnStep is not null && nextMethod.ReturnStep is not null)
                    {
                        prevMethod.ReturnStep!.FluentMethods =
                            prevMethod.ReturnStep.FluentMethods.AddRange(nextMethod.ReturnStep!.FluentMethods);
                    }

                    prevMethod.ParameterConverters =
                    [
                        ..prevMethod.ParameterConverters
                            .Union(nextMethod.ParameterConverters, SymbolEqualityComparer.Default)
                            .OfType<IMethodSymbol>()
                    ];

                    return prevMethod;
                });
            var returnSteps = group
                .Select(m => m.ReturnStep)
                .OfType<FluentStep>();

            var mergedReturnSteps = MergeFluentSteps(returnSteps).ToArray();

            Debug.Assert(mergedReturnSteps.Length <= 1);
            method.ReturnStep = mergedReturnSteps.FirstOrDefault();

            yield return method;
        }
    }

}
public class OrderedDictionary<TKey, TValue>(IEqualityComparer<TKey> comparer)
{
    private readonly Dictionary<TKey, TValue> _dictionary = new(comparer);
    private readonly List<TKey> _keys = [];

    public void Add(TKey key, TValue value)
    {
        _dictionary.Add(key, value);
        _keys.Add(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    public bool Remove(TKey key)
    {
        if (_dictionary.Remove(key))
        {
            _keys.Remove(key);
            return true;
        }
        return false;
    }

    public TValue this[TKey key]
    {
        get => _dictionary[key];
        set
        {
            if (!_dictionary.ContainsKey(key))
                _keys.Add(key);
            _dictionary[key] = value;
        }
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> GetOrderedItems()
    {
        foreach (var key in _keys)
        {
            yield return new KeyValuePair<TKey, TValue>(key, _dictionary[key]);
        }
    }

    public int Count => _dictionary.Count;
    public IEnumerable<TValue> Values => _keys.Select(key => _dictionary[key]);

    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
    public void Clear()
    {
        _dictionary.Clear();
        _keys.Clear();
    }
}
