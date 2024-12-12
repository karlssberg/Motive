using Motiv.Generator.FluentBuilder;

namespace Motiv.Generator.Tests;

using VerifyCS = CSharpSourceGeneratorVerifier<FluentBuilderGenerator>;

public class FluentBuilderRootTypeTests
{
    [Fact]
    public async Task Should_generate_for_an_existing_static_partial_class_target_type()
    {
        const string code =
            """
            using System;

            namespace Test;

            public static partial class Factory
            {
            }

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
                    public static MyBuildTarget Value(in int value)
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
    public async Task Should_generate_for_an_existing_partial_record_target_type()
    {
        const string code =
            """
            using System;

            namespace Test;

            public partial record Factory
            {
            }

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
                public partial record Factory
                {
                    public MyBuildTarget Value(in int value)
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
    public async Task Should_generate_for_an_existing_partial_struct_target_type()
    {
        const string code =
            """
            using System;

            namespace Test;

            public partial struct Factory
            {
            }

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
                public partial struct Factory
                {
                    public MyBuildTarget Value(in int value)
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
    public async Task Should_generate_for_an_existing_partial_record_struct_target_type()
    {
        const string code =
            """
            using System;

            namespace Test;

            public partial record struct Factory
            {
            }

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
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
                    public MyBuildTarget Value(in int value)
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
}
