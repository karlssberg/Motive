using Motiv.Generator.FluentBuilder;
using VerifyCS =
    Motiv.Generator.Tests.CSharpSourceGeneratorVerifier<Motiv.Generator.FluentBuilder.FluentBuilderGenerator>;

namespace Motiv.Generator.Tests;

public class FluentBuilderGeneratorMergeTests
{
    [Fact]
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_a_single_parameter()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(T value)
                {
                    Value = value;
                }

                public T Value { get; set; }
            }

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(string value)
                {
                    Value = value;
                }

                public String Value { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static MyBuildTarget<T> Value<T>(in T value)
                    {
                        return new MyBuildTarget<T>(value);
                    }

                    public static MyBuildTarget Value(in string value)
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
                    (typeof(FluentBuilderGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_two_parameters()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T1, T2>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
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
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    String value1,
                    String value2)
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
                    public static Step_0<T1> Value1<T1>(in T1 value1)
                    {
                        return new Step_0<T1>(value1);
                    }

                    public static Step_1 Value1(in string value1)
                    {
                        return new Step_1(value1);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    public MyBuildTarget<T1, T2> Value2<T2>(in T2 value2)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1
                {
                    private readonly string _value1__parameter;
                    public Step_1(in string value1)
                    {
                        _value1__parameter = value1;
                    }

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
                    (typeof(FluentBuilderGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_two_parameters_with_divergence_on_the_second_step()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T1, T2>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
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

            public class MyBuildTarget<T1>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    String value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                public T1 Value1 { get; set; }

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

                    public MyBuildTarget<T1, T2> Value2<T2>(in T2 value2)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value2);
                    }

                    public MyBuildTarget<T1> Value2(in string value2)
                    {
                        return new MyBuildTarget<T1>(_value1__parameter, value2);
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
                    (typeof(FluentBuilderGenerator), "Test.Factory.g.cs", expected)
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

            public class MyBuildTarget<T1, T2, T3>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    T2 value2,
                    T3 value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                public T1 Value1 { get; set; }

                public T2 Value2 { get; set; }

                public T3 Value3 { get; set; }
            }

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    String value1,
                    String value2,
                    String value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                public String Value1 { get; set; }

                public String Value2 { get; set; }

                public String Value3 { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0<T1> Value1<T1>(in T1 value1)
                    {
                        return new Step_0<T1>(value1);
                    }

                    public static Step_1 Value1(in string value1)
                    {
                        return new Step_1(value1);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    public Step_2<T1, T2> Value2<T2>(in T2 value2)
                    {
                        return new Step_2<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1
                {
                    private readonly string _value1__parameter;
                    public Step_1(in string value1)
                    {
                        _value1__parameter = value1;
                    }

                    public Step_3 Value2(in string value2)
                    {
                        return new Step_3(_value1__parameter, value2);
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

                    public MyBuildTarget<T1, T2, T3> Value3<T3>(in T3 value3)
                    {
                        return new MyBuildTarget<T1, T2, T3>(_value1__parameter, _value2__parameter, value3);
                    }
                }

                public struct Step_3
                {
                    private readonly string _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_3(in string value1, in string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    public MyBuildTarget Value3(in string value3)
                    {
                        return new MyBuildTarget(_value1__parameter, _value2__parameter, value3);
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
                    (typeof(FluentBuilderGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_four_parameters_where_divergence_occurs_on_the_second_step()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T1, T2, T3, T4>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    T2 value2,
                    T3 value3,
                    T4 value4)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                    Value4 = value4;
                }

                public T1 Value1 { get; set; }

                public T2 Value2 { get; set; }

                public T3 Value3 { get; set; }

                public T4 Value4 { get; set; }
            }

            public class MyBuildTarget<T1>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    String value2,
                    String value3,
                    String value4)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                    Value4 = value4;
                }

                public T1 Value1 { get; set; }

                public String Value2 { get; set; }

                public String Value3 { get; set; }

                public String Value4 { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
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

                    public Step_1<T1, T2> Value2<T2>(in T2 value2)
                    {
                        return new Step_1<T1, T2>(_value1__parameter, value2);
                    }

                    public Step_2<T1> Value2(in string value2)
                    {
                        return new Step_2<T1>(_value1__parameter, value2);
                    }
                }

                public struct Step_1<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value2__parameter;
                    public Step_1(in T1 value1, in T2 value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    public Step_3<T1, T2, T3> Value3<T3>(in T3 value3)
                    {
                        return new Step_3<T1, T2, T3>(_value1__parameter, _value2__parameter, value3);
                    }
                }

                public struct Step_2<T1>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_2(in T1 value1, in string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    public Step_4<T1> Value3(in string value3)
                    {
                        return new Step_4<T1>(_value1__parameter, _value2__parameter, value3);
                    }
                }

                public struct Step_3<T1, T2, T3>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value2__parameter;
                    private readonly T3 _value3__parameter;
                    public Step_3(in T1 value1, in T2 value2, in T3 value3)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                        _value3__parameter = value3;
                    }

                    public MyBuildTarget<T1, T2, T3, T4> Value4<T4>(in T4 value4)
                    {
                        return new MyBuildTarget<T1, T2, T3, T4>(_value1__parameter, _value2__parameter, _value3__parameter, value4);
                    }
                }

                public struct Step_4<T1>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    private readonly string _value3__parameter;
                    public Step_4(in T1 value1, in string value2, in string value3)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                        _value3__parameter = value3;
                    }

                    public MyBuildTarget<T1> Value4(in string value4)
                    {
                        return new MyBuildTarget<T1>(_value1__parameter, _value2__parameter, _value3__parameter, value4);
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
                    (typeof(FluentBuilderGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }
}
