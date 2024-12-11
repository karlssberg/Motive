using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder;
using VerifyCS =
    Motiv.Generator.Tests.CSharpSourceGeneratorVerifier<Motiv.Generator.FluentBuilder.FluentBuilderGenerator>;

namespace Motiv.Generator.Tests;

public class FluentBuilderGeneratorPrimaryConstructorTests
{
    [Fact]
    public async Task Should_generate_when_applied_to_a_class_primary_constructor_with_a_single_parameter()
    {
        const string code =
            """
            using System;

            [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
            public class MyBuildTarget(int value)
            {
                public int Value { get; set; } = value;
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static MyBuildTarget Value(int value)
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

#if NET6_0_OR_GREATER

    [Fact]
    public async Task Should_generate_when_applied_to_a_positional_record_primary_constructor_with_two_parameters()
    {
        const string code =
            """
            using System;

            [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
            public record MyBuildTarget(int Number, string text);
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0 Number(int number)
                    {
                        return new Step_0(number);
                    }
                }

                public struct Step_0
                {
                    private readonly int _number__parameter;
                    public Step_0(int number)
                    {
                        _number__parameter = number;
                    }

                    public MyBuildTarget Text(string text)
                    {
                        return new MyBuildTarget(_number__parameter, text);
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
#endif

    [Fact]
    public async Task Should_generate_when_applied_to_a_record_primary_constructor_with_two_parameters()
    {
        const string code =
            """
            using System;

            [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
            public record MyBuildTarget(
                int Number,
                string text)
            {
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0 Number(int number)
                    {
                        return new Step_0(number);
                    }
                }

                public struct Step_0
                {
                    private readonly int _number__parameter;
                    public Step_0(int number)
                    {
                        _number__parameter = number;
                    }

                    public MyBuildTarget Text(string text)
                    {
                        return new MyBuildTarget(_number__parameter, text);
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
    public async Task Should_generate_when_applied_to_a_struct_primary_constructor_with_three_parameters()
    {
        const string code =
            """
            using System;

            [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
            public struct MyBuildTarget(
                int number,
                string text,
                Guid id)
            {
                public int Number { get; set; } = number;

                public string Text { get; set; } = text;

                public Guid Id { get; set; } = id;
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0 Number(int number)
                    {
                        return new Step_0(number);
                    }
                }

                public struct Step_0
                {
                    private readonly int _number__parameter;
                    public Step_0(int number)
                    {
                        _number__parameter = number;
                    }

                    public Step_1 Text(string text)
                    {
                        return new Step_1(_number__parameter, text);
                    }
                }

                public struct Step_1
                {
                    private readonly int _number__parameter;
                    private readonly string _text__parameter;
                    public Step_1(int number, string text)
                    {
                        _number__parameter = number;
                        _text__parameter = text;
                    }

                    public MyBuildTarget Id(System.Guid id)
                    {
                        return new MyBuildTarget(_number__parameter, _text__parameter, id);
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
    public async Task Should_generate_when_applied_to_a_ref_struct_primary_constructor_with_four_parameters()
    {
        const string code =
            """
            using System;
            using System.Text.RegularExpressions;

            [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
            public ref struct MyBuildTarget(
                int number,
                string text,
                Guid id,
                Regex regex)
            {
                public int Number { get; set; }

                public string Text { get; set; }

                public Guid Id { get; set; }

                public Regex Regex { get; set; }
            }
            """;

        const string expected =
            """
            using System;
            using System.Text.RegularExpressions;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0 Number(int number)
                    {
                        return new Step_0(number);
                    }
                }

                public struct Step_0
                {
                    private readonly int _number__parameter;
                    public Step_0(int number)
                    {
                        _number__parameter = number;
                    }

                    public Step_1 Text(string text)
                    {
                        return new Step_1(_number__parameter, text);
                    }
                }

                public struct Step_1
                {
                    private readonly int _number__parameter;
                    private readonly string _text__parameter;
                    public Step_1(int number, string text)
                    {
                        _number__parameter = number;
                        _text__parameter = text;
                    }

                    public Step_2 Id(System.Guid id)
                    {
                        return new Step_2(_number__parameter, _text__parameter, id);
                    }
                }

                public struct Step_2
                {
                    private readonly int _number__parameter;
                    private readonly string _text__parameter;
                    private readonly System.Guid _id__parameter;
                    public Step_2(int number, string text, System.Guid id)
                    {
                        _number__parameter = number;
                        _text__parameter = text;
                        _id__parameter = id;
                    }

                    public MyBuildTarget Regex(System.Text.RegularExpressions.Regex regex)
                    {
                        return new MyBuildTarget(_number__parameter, _text__parameter, _id__parameter, regex);
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
