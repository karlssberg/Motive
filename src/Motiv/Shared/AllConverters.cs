using Motiv.Generator.Attributes;

namespace Motiv.Shared;

public static class AllConverters
{
    [FluentParameterOverload]
    public static Func<T1, T2, TValue> Convert<T1, T2, TValue>(TValue value)
    {
        return (_, _) => value;
    }

    [FluentParameterOverload]
    public static Func<T1, T2, TValue> Convert<T1, T2, TValue>(Func<T1, TValue> function)
    {
        return (parameter, _) => function(parameter);
    }
}
