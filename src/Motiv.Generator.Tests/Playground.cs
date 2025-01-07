using System;
using Motiv.Generator.Attributes;

namespace Test
{
    [FluentFactory]
    public static partial class Factory;
}

namespace Test.NamespaceA
{
    public class MyBuildTargetA<T>
    {
        [FluentConstructor(typeof(Test.Factory), Options = FluentOptions.NoCreateMethod)]
        public MyBuildTargetA(
            [MultipleFluentMethods(typeof(MethodVariants))]T data,
            int value)
        {
            Data = data;
            Value = value;
        }

        public T Data { get; set; }
        public int Value { get; set; }
    }
}

namespace Test.NamespaceB
{
    public class MyBuildTargetB<T>
    {
        [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
        public MyBuildTargetB(
            [MultipleFluentMethods(typeof(MethodVariants))]T data,
            string value)
        {
            Data = data;
            Value = value;
        }

        public T Data { get; set; }
        public string Value { get; set; }
    }
}

public class MethodVariants
{
    [FluentMethodTemplate]
    public static T WithDefaultValue<T>()
    {
        return default(T);
    }

    [FluentMethodTemplate]
    public static T WithFunction<T>(in Func<T> function)
    {
        return function();
    }
}
