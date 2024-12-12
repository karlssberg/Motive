﻿using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder;
using VerifyCS =
    Motiv.Generator.Tests.CSharpSourceGeneratorVerifier<Motiv.Generator.FluentBuilder.FluentBuilderGenerator>;

namespace Motiv.Generator.Tests;

public class FluentBuilderGeneratorNonGenericTests
{
    [Fact]
    public async Task Should_generate_when_applied_to_a_class_constructor_with_a_single_parameter()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
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
                    public static Step_0 Number(in int number)
                    {
                        return new Step_0(number);
                    }
                }

                public struct Step_0
                {
                    private readonly int _number__parameter;
                    public Step_0(in int number)
                    {
                        _number__parameter = number;
                    }

                    public MyBuildTarget Create()
                    {
                        return new MyBuildTarget(_number__parameter);
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

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    int number,
                    string text)
                {
                    Number = number;
                    Text = text;
                }

                public int Number { get; set; }

                public string Text { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0 Number(in int number)
                    {
                        return new Step_0(number);
                    }
                }

                public struct Step_0
                {
                    private readonly int _number__parameter;
                    public Step_0(in int number)
                    {
                        _number__parameter = number;
                    }

                    public MyBuildTarget Text(in string text)
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
    public async Task Should_generate_when_applied_to_a_class_constructor_with_three_parameters()
    {
        const string code =
            """
            using System;

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    int number,
                    string text,
                    Guid id)
                {
                    Number = number;
                    Text = text;
                    Id = id;
                }

                public int Number { get; set; }

                public string Text { get; set; }

                public Guid Id { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
                    public static Step_0 Number(in int number)
                    {
                        return new Step_0(number);
                    }
                }

                public struct Step_0
                {
                    private readonly int _number__parameter;
                    public Step_0(in int number)
                    {
                        _number__parameter = number;
                    }

                    public Step_1 Text(in string text)
                    {
                        return new Step_1(_number__parameter, text);
                    }
                }

                public struct Step_1
                {
                    private readonly int _number__parameter;
                    private readonly string _text__parameter;
                    public Step_1(in int number, in string text)
                    {
                        _number__parameter = number;
                        _text__parameter = text;
                    }

                    public MyBuildTarget Id(in System.Guid id)
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
    public async Task Should_generate_when_applied_to_a_class_constructor_with_four_parameters()
    {
        const string code =
            """
            using System;
            using System.Text.RegularExpressions;

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
                public MyBuildTarget(
                    int number,
                    string text,
                    Guid id,
                    Regex regex)
                {
                    Number = number;
                    Text = text;
                    Id = id;
                    Regex = regex;
                }

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
                    public static Step_0 Number(in int number)
                    {
                        return new Step_0(number);
                    }
                }

                public struct Step_0
                {
                    private readonly int _number__parameter;
                    public Step_0(in int number)
                    {
                        _number__parameter = number;
                    }

                    public Step_1 Text(in string text)
                    {
                        return new Step_1(_number__parameter, text);
                    }
                }

                public struct Step_1
                {
                    private readonly int _number__parameter;
                    private readonly string _text__parameter;
                    public Step_1(in int number, in string text)
                    {
                        _number__parameter = number;
                        _text__parameter = text;
                    }

                    public Step_2 Id(in System.Guid id)
                    {
                        return new Step_2(_number__parameter, _text__parameter, id);
                    }
                }

                public struct Step_2
                {
                    private readonly int _number__parameter;
                    private readonly string _text__parameter;
                    private readonly System.Guid _id__parameter;
                    public Step_2(in int number, in string text, in System.Guid id)
                    {
                        _number__parameter = number;
                        _text__parameter = text;
                        _id__parameter = id;
                    }

                    public MyBuildTarget Regex(in System.Text.RegularExpressions.Regex regex)
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
