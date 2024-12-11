using Motiv.Generator.FluentBuilder;
using VerifyCS =
    Motiv.Generator.Tests.CSharpSourceGeneratorVerifier<Motiv.Generator.FluentBuilder.FluentBuilderGenerator>;

namespace Motiv.Generator.Tests;

public class FluentBuilderGeneratorMergeDissimilarStepsTests
{
    [Fact]
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_a_single_and_dual_parameters()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(T value1)
                {
                    Value1 = value1;
                }

                public T Value1 { get; set; }
            }

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
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
                    public static MyBuildTarget<T> Value1<T>(T value1)
                    {
                        return new MyBuildTarget<T>(value1);
                    }

                    public static Step_0 Value1(string value1)
                    {
                        return new Step_0(value1);
                    }
                }

                public struct Step_0
                {
                    private readonly string _value1__parameter;
                    public Step_0(string value1)
                    {
                        _value1__parameter = value1;
                    }

                    public MyBuildTarget Value2(string value2)
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
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_two_and_three_parameters()
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
                    public static Step_0<T1> Value1<T1>(T1 value1)
                    {
                        return new Step_0<T1>(value1);
                    }

                    public static Step_1 String1(string string1)
                    {
                        return new Step_1(string1);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0(T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    public MyBuildTarget<T1, T2> Value2<T2>(T2 value2)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1
                {
                    private readonly string _string1__parameter;
                    public Step_1(string string1)
                    {
                        _string1__parameter = string1;
                    }

                    public Step_2 String2(string string2)
                    {
                        return new Step_2(_string1__parameter, string2);
                    }
                }

                public struct Step_2
                {
                    private readonly string _string1__parameter;
                    private readonly string _string2__parameter;
                    public Step_2(string string1, string string2)
                    {
                        _string1__parameter = string1;
                        _string2__parameter = string2;
                    }

                    public MyBuildTarget String3(string string3)
                    {
                        return new MyBuildTarget(_string1__parameter, _string2__parameter, string3);
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
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_two_and_three_parameters_with_divergence_on_the_second_step()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T1, T2>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    T1 value1,
                    String value2,
                    T2 value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
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
                    public static Step_0<T1> Value1<T1>(T1 value1)
                    {
                        return new Step_0<T1>(value1);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0(T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    public Step_1<T1> Value2(string value2)
                    {
                        return new Step_1<T1>(_value1__parameter, value2);
                    }

                    public MyBuildTarget<T1, T2> Value3<T2>(T2 value3)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value3);
                    }
                }

                public struct Step_1<T1>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_1(T1 value1, string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    public MyBuildTarget<T1, T2> Value3<T2>(T2 value3)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, _value2__parameter, value3);
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

                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
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
                    public static Step_0<T1> Value1<T1>(T1 value1)
                    {
                        return new Step_0<T1>(value1);
                    }

                    public static Step_1<T2> Value2<T2>(T2 value2)
                    {
                        return new Step_1<T2>(value2);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0(T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    public MyBuildTarget<T1, T2> Value2<T2>(T2 value2)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1<T2>
                {
                    private readonly T2 _value2__parameter;
                    public Step_1(T2 value2)
                    {
                        _value2__parameter = value2;
                    }

                    public MyBuildTarget<T1, T2> Value1<T1>(T1 value1)
                    {
                        return new MyBuildTarget<T1, T2>(_value2__parameter, value1);
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
