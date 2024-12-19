using Motiv.Generator.FluentBuilder;

namespace Motiv.Generator.Tests;

using VerifyCS = CSharpSourceGeneratorVerifier<FluentFactoryGenerator>;

public class FluentBuilderRootTypeTests
{
    [Fact]
    public async Task Should_generate_for_an_existing_static_partial_class_target_type()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTarget(int value)
                {
                    Value = value;
                }

                public int Value { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static MyBuildTarget WithValue(in int value)
                    {
                        return new MyBuildTarget(value);
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_generate_for_an_existing_partial_record_target_type()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public partial record Factory;

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTarget(int value)
                {
                    Value = value;
                }

                public int Value { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public partial record Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static MyBuildTarget WithValue(in int value)
                    {
                        return new MyBuildTarget(value);
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_generate_for_an_existing_partial_struct_target_type()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public partial struct Factory;

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTarget(int value)
                {
                    Value = value;
                }

                public int Value { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public partial struct Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static MyBuildTarget WithValue(in int value)
                    {
                        return new MyBuildTarget(value);
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }



    [Fact]
    public async Task Should_generate_for_an_existing_partial_record_struct_target_type()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public partial record struct Factory;

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTarget(in int value)
                {
                    Value = value;
                }

                public int Value { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public partial record struct Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static MyBuildTarget WithValue(in int value)
                    {
                        return new MyBuildTarget(value);
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_add_overloaded_methods()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            [FluentFactory]
            public static partial class Factory;

            public class MyClass<T>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyClass([FluentMethod("Create", Overloads = typeof(Overloads))]T value)
                {
                    Value = value;
                }

                public T Value { get; set; }
            }

            public static class Overloads
            {
                [FluentParameterConverter]
                public static T Convert<T>(Func<T> factory)
                {
                    return factory();
                }
            }

            """;

        const string expected =
            """
            public static partial class Factory
            {
                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                public static MyClass<T> Create<T>(in T value)
                {
                    return new MyClass<T>(value);
                }

                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                public static MyClass<T> Create<T>(in System.Func<T> factory)
                {
                    return new MyClass<T>(Overloads.Convert(factory));
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
                    (typeof(FluentFactoryGenerator), "Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_add_overloaded_methods_using_a_two_step_fluent_factory()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            [FluentFactory]
            public static partial class Factory;

            public class MyClass<T>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyClass(
                    [FluentMethod("Value1", Overloads = typeof(Overloads))]T value1,
                    [FluentMethod("Create", Overloads = typeof(Overloads))]T value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                public T Value1 { get; set; }
                public T Value2 { get; set; }
            }

            public static class Overloads
            {
                [FluentParameterConverter]
                public static T Convert<T>(Func<T> factory)
                {
                    return factory();
                }
            }

            """;

        const string expected =
            """
            public static partial class Factory
            {
                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                public static Step_0__Factory<T> Value1<T>(in T value1)
                {
                    return new Step_0__Factory<T>(value1);
                }

                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                public static Step_0__Factory<T> Value1<T>(in System.Func<T> factory)
                {
                    return new Step_0__Factory<T>(Overloads.Convert(factory));
                }
            }

            public struct Step_0__Factory<T>
            {
                private readonly T _value1__parameter;
                public Step_0__Factory(in T value1)
                {
                    _value1__parameter = value1;
                }

                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                public MyClass<T> Create(in T value2)
                {
                    return new MyClass<T>(_value1__parameter, value2);
                }

                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                public MyClass<T> Create(in System.Func<T> factory)
                {
                    return new MyClass<T>(_value1__parameter, Overloads.Convert(factory));
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
                    (typeof(FluentFactoryGenerator), "Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }
}
