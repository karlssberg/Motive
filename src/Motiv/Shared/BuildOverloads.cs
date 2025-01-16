using Motiv.Generator.Attributes;

namespace Motiv.Shared;

public static class BuildOverloads
{
    [FluentParameterOverload]
    public static T Convert<T>(Func<T> function)
    {
        return function();
    }
}
