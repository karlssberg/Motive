using Motiv.Generator.FluentBuilder;
using VerifyCS =
    Motiv.Generator.Tests.CSharpSourceGeneratorVerifier<Motiv.Generator.FluentBuilder.FluentBuilderGenerator>;

namespace Motiv.Generator.Tests;

public class FluentBuilderGeneratorNestedGenericTests
{
    [Fact]
    public async Task Should_generate_when_applied_to_a_class_constructor_with_a_single_parameter()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(Func<T, bool> value)
                {
                    Value = value;
                }

                public Func<T, bool>  Value { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static MyBuildTarget<T> Value<T>(in System.Func<T, bool> value)
                    {
                        return new MyBuildTarget<T>(value);
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
    public async Task Should_generate_when_applied_to_a_class_constructor_with_two_parameters()
    {
        const string code =
            """
            using System;
            using System.Collections.Generic;

            public class MyBuildTarget<T1, T2>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    Func<T1, bool> value1,
                    IEnumerable<T2> value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                public Func<T1, bool> Value1 { get; set; }

                public IEnumerable<T2> Value2 { get; set; }
            }
            """;

        const string expected =
            """
            using System;
            using System.Collections.Generic;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0<T1> Value1<T1>(in System.Func<T1, bool> value1)
                    {
                        return new Step_0<T1>(value1);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly System.Func<T1, bool> _value1__parameter;
                    public Step_0(in System.Func<T1, bool> value1)
                    {
                        _value1__parameter = value1;
                    }

                    public MyBuildTarget<T1, T2> Value2<T2>(in System.Collections.Generic.IEnumerable<T2> value2)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value2);
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
    public async Task Should_generate_when_applied_to_a_class_constructor_with_three_parameters()
    {
        const string code =
            """
            using System;
            using System.Collections.Generic;

            public class MyBuildTarget<T1, T2, T3>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    Func<T1, bool> value1,
                    IEnumerable<T2> value2,
                    Func<Func<T1, bool>, T3> value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                public Func<T1, bool> Value1 { get; set; }

                public IEnumerable<T2> Value2 { get; set; }

                public Func<Func<T1, bool>, T3> Value3 { get; set; }
            }
            """;

        const string expected =
            """
            using System;
            using System.Collections.Generic;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0<T1> Value1<T1>(in System.Func<T1, bool> value1)
                    {
                        return new Step_0<T1>(value1);
                    }
                }

                public struct Step_0<T1>
                {
                    private readonly System.Func<T1, bool> _value1__parameter;
                    public Step_0(in System.Func<T1, bool> value1)
                    {
                        _value1__parameter = value1;
                    }

                    public Step_1<T1, T2> Value2<T2>(in System.Collections.Generic.IEnumerable<T2> value2)
                    {
                        return new Step_1<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1<T1, T2>
                {
                    private readonly System.Func<T1, bool> _value1__parameter;
                    private readonly System.Collections.Generic.IEnumerable<T2> _value2__parameter;
                    public Step_1(in System.Func<T1, bool> value1, in System.Collections.Generic.IEnumerable<T2> value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    public MyBuildTarget<T1, T2, T3> Value3<T3>(in System.Func<System.Func<T1, bool>, T3> value3)
                    {
                        return new MyBuildTarget<T1, T2, T3>(_value1__parameter, _value2__parameter, value3);
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
    public async Task Should_generate_when_multiple_type_parameters_appear_on_a_constructor_parameter()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget<T1, T2, T3, T4, T5, T6>
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    Func<T1, T2> value1,
                    Func<T3, T4> value2,
                    Func<T5, T6> value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                public Func<T1, T2> Value1 { get; set; }

                public Func<T3, T4> Value2 { get; set; }

                public Func<T5, T6> Value3 { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0<T1, T2> Value1<T1, T2>(in System.Func<T1, T2> value1)
                    {
                        return new Step_0<T1, T2>(value1);
                    }
                }

                public struct Step_0<T1, T2>
                {
                    private readonly System.Func<T1, T2> _value1__parameter;
                    public Step_0(in System.Func<T1, T2> value1)
                    {
                        _value1__parameter = value1;
                    }

                    public Step_1<T1, T2, T3, T4> Value2<T3, T4>(in System.Func<T3, T4> value2)
                    {
                        return new Step_1<T1, T2, T3, T4>(_value1__parameter, value2);
                    }
                }

                public struct Step_1<T1, T2, T3, T4>
                {
                    private readonly System.Func<T1, T2> _value1__parameter;
                    private readonly System.Func<T3, T4> _value2__parameter;
                    public Step_1(in System.Func<T1, T2> value1, in System.Func<T3, T4> value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    public MyBuildTarget<T1, T2, T3, T4, T5, T6> Value3<T5, T6>(in System.Func<T5, T6> value3)
                    {
                        return new MyBuildTarget<T1, T2, T3, T4, T5, T6>(_value1__parameter, _value2__parameter, value3);
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
