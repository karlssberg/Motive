using Motiv.Generator.Attributes;

namespace Motiv.Shared;

public static class WhenOverloads
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

    [FluentParameterOverload]
    public static Func<T1, TValue> Convert<T1, TValue>(TValue value)
    {
        return _ => value;
    }
}
