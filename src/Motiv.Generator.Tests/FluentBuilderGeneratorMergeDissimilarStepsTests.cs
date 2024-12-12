using Motiv.Generator.FluentBuilder;
using VerifyCS =
    Motiv.Generator.Tests.CSharpSourceGeneratorVerifier<Motiv.Generator.FluentBuilder.FluentFactoryGenerator>;

namespace Motiv.Generator.Tests;

public class FluentBuilderGeneratorMergeDissimilarStepsTests
{
    [Fact]
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_a_single_and_dual_parameters()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            public class MyBuildTarget<T>
            {
                [GenerateFluentFactory("Test.Factory", Options = FluentFactoryOptions.NoCreateMethod)]
                public MyBuildTarget(T value1)
                {
                    Value1 = value1;
                }

                public T Value1 { get; set; }
            }

            public class MyBuildTarget
            {
                [GenerateFluentFactory("Test.Factory", Options = FluentFactoryOptions.NoCreateMethod)]
                public MyBuildTarget(string value1, string value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                public String Value1 { get; set; }

                public String Value2 { get; set; }
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
                    public static MyBuildTarget<T> Value1<T>(in T value1)
                    {
                        return new MyBuildTarget<T>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0 Value1(in string value1)
                    {
                        return new Step_0(value1);
                    }
                }

                public struct Step_0
                {
                    private readonly string _value1__parameter;
                    public Step_0(in string value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget Value2(in string value2)
                    {
                        return new MyBuildTarget(_value1__parameter, value2);
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
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_two_and_three_parameters()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T1, T2>
            {
                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    T2 value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                public T1 Value1 { get; set; }

                public T2 Value2 { get; set; }
            }

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory")]
                public MyBuildTarget(
                    String string1,
                    String string2,
                    String string3)
                {
                    String1 = string1;
                    String2 = string2;
                    String3 = string3;
                }

                public String String1 { get; set; }

                public String String2 { get; set; }

                public String String3 { get; set; }
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
                    public static Step_0<T1> Value1<T1>(in T1 value1)
                    {
                        return new Step_0<T1>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_1 String1(in string string1)
                    {
                        return new Step_1(string1);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_2<T1, T2> Value2<T2>(in T2 value2)
                    {
                        return new Step_2<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1
                {
                    private readonly string _string1__parameter;
                    public Step_1(in string string1)
                    {
                        _string1__parameter = string1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_3 String2(in string string2)
                    {
                        return new Step_3(_string1__parameter, string2);
                    }
                }

                public struct Step_2<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value2__parameter;
                    public Step_2(in T1 value1, in T2 value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2> Create()
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, _value2__parameter);
                    }
                }

                public struct Step_3
                {
                    private readonly string _string1__parameter;
                    private readonly string _string2__parameter;
                    public Step_3(in string string1, in string string2)
                    {
                        _string1__parameter = string1;
                        _string2__parameter = string2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_4 String3(in string string3)
                    {
                        return new Step_4(_string1__parameter, _string2__parameter, string3);
                    }
                }

                public struct Step_4
                {
                    private readonly string _string1__parameter;
                    private readonly string _string2__parameter;
                    private readonly string _string3__parameter;
                    public Step_4(in string string1, in string string2, in string string3)
                    {
                        _string1__parameter = string1;
                        _string2__parameter = string2;
                        _string3__parameter = string3;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget Create()
                    {
                        return new MyBuildTarget(_string1__parameter, _string2__parameter, _string3__parameter);
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
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_two_and_three_parameters_with_divergence_on_the_second_step()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T1, T2>
            {
                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    String value2,
                    T2 value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    T2 value3)
                {
                    Value1 = value1;
                    Value3 = value3;
                }

                public T1 Value1 { get; set; }

                public String Value2 { get; set; } = "";

                public T2 Value3 { get; set; }
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
                    public static Step_0<T1> Value1<T1>(in T1 value1)
                    {
                        return new Step_0<T1>(value1);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1<T1> Value2(in string value2)
                    {
                        return new Step_1<T1>(_value1__parameter, value2);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_2<T1, T2> Value3<T2>(in T2 value3)
                    {
                        return new Step_2<T1, T2>(_value1__parameter, value3);
                    }
                }

                public struct Step_1<T1>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_1(in T1 value1, in string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_3<T1, T2> Value3<T2>(in T2 value3)
                    {
                        return new Step_3<T1, T2>(_value1__parameter, _value2__parameter, value3);
                    }
                }

                public struct Step_2<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value3__parameter;
                    public Step_2(in T1 value1, in T2 value3)
                    {
                        _value1__parameter = value1;
                        _value3__parameter = value3;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2> Create()
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, _value3__parameter);
                    }
                }

                public struct Step_3<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    private readonly T2 _value3__parameter;
                    public Step_3(in T1 value1, in string value2, in T2 value3)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                        _value3__parameter = value3;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2> Create()
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, _value2__parameter, _value3__parameter);
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
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_three_parameters()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            public class MyBuildTarget<T1, T2>
            {
                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    T2 value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory")]
                public MyBuildTarget(
                    T2 value2,
                    T1 value1)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                public T1 Value1 { get; set; }

                public T2 Value2 { get; set; }

            }
            """;

        const string expected =
            """
            namespace Test
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0<T1> Value1<T1>(in T1 value1)
                    {
                        return new Step_0<T1>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_1<T2> Value2<T2>(in T2 value2)
                    {
                        return new Step_1<T2>(value2);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_2<T1, T2> Value2<T2>(in T2 value2)
                    {
                        return new Step_2<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1<T2>
                {
                    private readonly T2 _value2__parameter;
                    public Step_1(in T2 value2)
                    {
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_3<T2, T1> Value1<T1>(in T1 value1)
                    {
                        return new Step_3<T2, T1>(_value2__parameter, value1);
                    }
                }

                public struct Step_2<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value2__parameter;
                    public Step_2(in T1 value1, in T2 value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2> Create()
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, _value2__parameter);
                    }
                }

                public struct Step_3<T2, T1>
                {
                    private readonly T2 _value2__parameter;
                    private readonly T1 _value1__parameter;
                    public Step_3(in T2 value2, in T1 value1)
                    {
                        _value2__parameter = value2;
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2> Create()
                    {
                        return new MyBuildTarget<T1, T2>(_value2__parameter, _value1__parameter);
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
}
