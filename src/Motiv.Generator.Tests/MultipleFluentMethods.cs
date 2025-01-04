using Motiv.Generator.FluentBuilder;

namespace Motiv.Generator.Tests;
using VerifyCS = CSharpSourceGeneratorVerifier<FluentBuilder.FluentFactoryGenerator>;

public class MultipleFluentMethodTests
{
    [Fact]
    public async Task Should_build_multiple_root_constructor_methods_for_single_parameter()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test.Namespace
            {
                [FluentFactory]
                public static partial class Factory;

                public class MyBuildTarget<T>
                {
                    [FluentConstructor(typeof(Factory))]
                    public MyBuildTarget(
                        [MultipleFluentMethods(typeof(MethodVariants))]T data)
                    {
                        Data = data;
                    }

                    public T Data { get; set; }
                }

                public class MethodVariants
                {
                    [FluentMethodTemplate]
                    public static T WithValue<T>(in T value)
                    {
                        return value;
                    }

                    [FluentMethodTemplate]
                    public static T WithFunction<T>(in Func<T> function)
                    {
                        return function();
                    }
                }
            }
            """;

        const string expected =
            """
            namespace Test.Namespace
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory<T> WithValue<T>(in T value)
                    {
                        return new Step_0__Test_Namespace_Factory<T>(Test.Namespace.MethodVariants.WithValue<T>(value));
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory<T> WithFunction<T>(in System.Func<T> function)
                    {
                        return new Step_0__Test_Namespace_Factory<T>(Test.Namespace.MethodVariants.WithFunction<T>(function));
                    }
                }

                public struct Step_0__Test_Namespace_Factory<T>
                {
                    private readonly T _data__parameter;
                    public Step_0__Test_Namespace_Factory(in T data)
                    {
                        _data__parameter = data;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T> Create()
                    {
                        return new MyBuildTarget<T>(_data__parameter);
                    }
                }
            }
            """;

        await new VerifyCS.Test
        {
            TestState =
            {
                Sources = { code },
                GeneratedSources =
                {
                    (typeof(FluentFactoryGenerator), "Test.Namespace.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_ensure_no_duplicate_signatures_are_generated_when_using_multiple_constructors()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test.Namespace
            {
                [FluentFactory]
                public static partial class Factory;

                public class MyBuildTargetA<T>
                {
                    [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
            }
            """;

        const string expected =
            """
            using System;

            namespace Test.Namespace
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory<T> WithDefaultValue<T>()
                    {
                        return new Step_0__Test_Namespace_Factory<T>(Test.Namespace.MethodVariants.WithDefaultValue<T>());
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory<T> WithFunction<T>(in System.Func<T> function)
                    {
                        return new Step_0__Test_Namespace_Factory<T>(Test.Namespace.MethodVariants.WithFunction<T>(function));
                    }
                }

                public struct Step_0__Test_Namespace_Factory<T>
                {
                    private readonly T _data__parameter;
                    public Step_0__Test_Namespace_Factory(in T data)
                    {
                        _data__parameter = data;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetA<T> WithValue(in int value)
                    {
                        return new MyBuildTargetA<T>(_data__parameter, value);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetB<T> WithValue(in string value)
                    {
                        return new MyBuildTargetB<T>(_data__parameter, value);
                    }
                }
            }
            """;

        await new VerifyCS.Test
        {
            TestState =
            {
                Sources = { code },
                GeneratedSources =
                {
                    (typeof(FluentFactoryGenerator), "Test.Namespace.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_ensure_no_duplicate_signatures_are_generated_when_using_multiple_constructors_on_second_parameter()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test.Namespace
            {
                [FluentFactory]
                public static partial class Factory;

                public class MyBuildTargetA<T>
                {
                    [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                    public MyBuildTargetA(
                        int number,
                        [MultipleFluentMethods(typeof(MethodVariants))]T data,
                        int value)
                    {
                        Number = number;
                        Data = data;
                        Value = value;
                    }

                    public int Number { get; set; }
                    public T Data { get; set; }
                    public int Value { get; set; }
                }

                public class MyBuildTargetB<T>
                {
                    [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                    public MyBuildTargetB(
                        int number,
                        [MultipleFluentMethods(typeof(MethodVariants))]T data,
                        string value)
                    {
                        Data = data;
                        Value = value;
                    }

                    public int Number { get; set; }
                    public T Data { get; set; }
                    public string Value { get; set; }
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
            }
            """;

        const string expected =
            """
            using System;

            namespace Test.Namespace
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory WithNumber(in int number)
                    {
                        return new Step_0__Test_Namespace_Factory(number);
                    }
                }

                public struct Step_0__Test_Namespace_Factory
                {
                    private readonly int _number__parameter;
                    public Step_0__Test_Namespace_Factory(in int number)
                    {
                        _number__parameter = number;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Namespace_Factory<T> WithDefaultValue<T>()
                    {
                        return new Step_1__Test_Namespace_Factory<T>(_number__parameter, Test.Namespace.MethodVariants.WithDefaultValue<T>());
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Namespace_Factory<T> WithFunction<T>(in System.Func<T> function)
                    {
                        return new Step_1__Test_Namespace_Factory<T>(_number__parameter, Test.Namespace.MethodVariants.WithFunction<T>(function));
                    }
                }

                public struct Step_1__Test_Namespace_Factory<T>
                {
                    private readonly int _number__parameter;
                    private readonly T _data__parameter;
                    public Step_1__Test_Namespace_Factory(in int number, in T data)
                    {
                        _number__parameter = number;
                        _data__parameter = data;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetA<T> WithValue(in int value)
                    {
                        return new MyBuildTargetA<T>(_number__parameter, _data__parameter, value);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetB<T> WithValue(in string value)
                    {
                        return new MyBuildTargetB<T>(_number__parameter, _data__parameter, value);
                    }
                }
            }
            """;

        await new VerifyCS.Test
        {
            TestState =
            {
                Sources = { code },
                GeneratedSources =
                {
                    (typeof(FluentFactoryGenerator), "Test.Namespace.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_ensure_no_duplicate_signatures_are_generated_when_using_multiple_constructors_on_second_parameter_against_a_regular_method()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test.Namespace
            {
                [FluentFactory]
                public static partial class Factory;

                public class MyBuildTargetA<T>
                {
                    [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                    public MyBuildTargetA(
                        int number,
                        [MultipleFluentMethods(typeof(MethodVariants))]T data,
                        int value)
                    {
                        Number = number;
                        Data = data;
                        Value = value;
                    }

                    public int Number { get; set; }
                    public T Data { get; set; }
                    public int Value { get; set; }
                }

                public class MyBuildTargetB<T>
                {
                    [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                    public MyBuildTargetB(
                        int number,
                        [FluentMethod("WithFunction")]Func<T> nativeFunction,
                        string value)
                    {
                        NativeFunction = nativeFunction;
                        Value = value;
                    }

                    public int Number { get; set; }
                    public Func<T> NativeFunction { get; set; }
                    public string Value { get; set; }
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
            }
            """;

        const string expected =
            """
            using System;

            namespace Test.Namespace
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory WithNumber(in int number)
                    {
                        return new Step_0__Test_Namespace_Factory(number);
                    }
                }

                public struct Step_0__Test_Namespace_Factory
                {
                    private readonly int _number__parameter;
                    public Step_0__Test_Namespace_Factory(in int number)
                    {
                        _number__parameter = number;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Namespace_Factory<T> WithDefaultValue<T>()
                    {
                        return new Step_1__Test_Namespace_Factory<T>(_number__parameter, Test.Namespace.MethodVariants.WithDefaultValue<T>());
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_2__Test_Namespace_Factory<T> WithFunction<T>(in System.Func<T> nativeFunction)
                    {
                        return new Step_2__Test_Namespace_Factory<T>(_number__parameter, nativeFunction);
                    }
                }

                public struct Step_1__Test_Namespace_Factory<T>
                {
                    private readonly int _number__parameter;
                    private readonly T _data__parameter;
                    public Step_1__Test_Namespace_Factory(in int number, in T data)
                    {
                        _number__parameter = number;
                        _data__parameter = data;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetA<T> WithValue(in int value)
                    {
                        return new MyBuildTargetA<T>(_number__parameter, _data__parameter, value);
                    }
                }

                public struct Step_2__Test_Namespace_Factory<T>
                {
                    private readonly int _number__parameter;
                    private readonly System.Func<T> _nativeFunction__parameter;
                    public Step_2__Test_Namespace_Factory(in int number, in System.Func<T> nativeFunction)
                    {
                        _number__parameter = number;
                        _nativeFunction__parameter = nativeFunction;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetB<T> WithValue(in string value)
                    {
                        return new MyBuildTargetB<T>(_number__parameter, _nativeFunction__parameter, value);
                    }
                }
            }
            """;

        await new VerifyCS.Test
        {
            TestState =
            {
                Sources = { code },
                GeneratedSources =
                {
                    (typeof(FluentFactoryGenerator), "Test.Namespace.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_support_multiple_methods_containing_overload_methods()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test.Namespace
            {
                [FluentFactory]
                public static partial class Factory;

                public class MyBuildTargetA<T>
                {
                    [FluentConstructor(typeof(Factory))]
                    public MyBuildTargetA(
                        [MultipleFluentMethods(typeof(NumberMethods))]int number,
                        [MultipleFluentMethods(typeof(AsMethods))]T data)
                    {
                        Number = number;
                        Data = data;
                    }

                    public int Number { get; set; }
                    public T Data { get; set; }
                }

                public class NumberMethods
                {
                    [FluentMethodTemplate]
                    public static int Number()
                    {
                        return default(int);
                    }

                    [FluentMethodTemplate]
                    public static int Number(in Func<int> function)
                    {
                        return function();
                    }

                    [FluentMethodTemplate]
                    public static int Number(in Func<string, int> function, in string value)
                    {
                        return function(value);
                    }
                }

                public class AsMethods
                {
                    [FluentMethodTemplate]
                    public static T As<T>()
                    {
                        return default(T);
                    }

                    [FluentMethodTemplate]
                    public static T As<T>(in Func<T> function)
                    {
                        return function();
                    }

                    [FluentMethodTemplate]
                    public static T As<T>(in Func<string, T> function, in string value)
                    {
                        return function(value);
                    }
                }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test.Namespace
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory Number()
                    {
                        return new Step_0__Test_Namespace_Factory(Test.Namespace.NumberMethods.Number());
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory Number(in System.Func<int> function)
                    {
                        return new Step_0__Test_Namespace_Factory(Test.Namespace.NumberMethods.Number(function));
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory Number(in System.Func<string, int> function, in string value)
                    {
                        return new Step_0__Test_Namespace_Factory(Test.Namespace.NumberMethods.Number(function, value));
                    }
                }

                public struct Step_0__Test_Namespace_Factory
                {
                    private readonly int _number__parameter;
                    public Step_0__Test_Namespace_Factory(in int number)
                    {
                        _number__parameter = number;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Namespace_Factory<T> As<T>()
                    {
                        return new Step_1__Test_Namespace_Factory<T>(_number__parameter, Test.Namespace.AsMethods.As<T>());
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Namespace_Factory<T> As<T>(in System.Func<T> function)
                    {
                        return new Step_1__Test_Namespace_Factory<T>(_number__parameter, Test.Namespace.AsMethods.As<T>(function));
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Namespace_Factory<T> As<T>(in System.Func<string, T> function, in string value)
                    {
                        return new Step_1__Test_Namespace_Factory<T>(_number__parameter, Test.Namespace.AsMethods.As<T>(function, value));
                    }
                }

                public struct Step_1__Test_Namespace_Factory<T>
                {
                    private readonly int _number__parameter;
                    private readonly T _data__parameter;
                    public Step_1__Test_Namespace_Factory(in int number, in T data)
                    {
                        _number__parameter = number;
                        _data__parameter = data;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetA<T> Create()
                    {
                        return new MyBuildTargetA<T>(_number__parameter, _data__parameter);
                    }
                }
            }
            """;

        await new VerifyCS.Test
        {
            TestState =
            {
                Sources = { code },
                GeneratedSources =
                {
                    (typeof(FluentFactoryGenerator), "Test.Namespace.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }
}
