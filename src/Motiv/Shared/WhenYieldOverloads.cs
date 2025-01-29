using Motiv.Generator.Attributes;

namespace Motiv.Shared;

public static class WhenYieldOverloads
{
    [FluentMethodTemplate]
    public static Func<T1, T2, TValue> Convert<T1, T2, TValue>(TValue value)
    {
        return (_, _) => value;
    }

    [FluentMethodTemplate]
    public static Func<T1, T2, TValue> Convert<T1, T2, TValue>(Func<T1, TValue> function)
    {
        return (parameter, _) => function(parameter);
    }

    [FluentMethodTemplate]
    public static Func<T1, TValue> Convert<T1, TValue>(TValue value)
    {
        return _ => value;
    }
}
