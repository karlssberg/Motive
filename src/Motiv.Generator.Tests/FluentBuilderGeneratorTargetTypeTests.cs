using Motiv.Generator.FluentBuilder;

namespace Motiv.Generator.Tests;
using VerifyCS = CSharpSourceGeneratorVerifier<FluentFactoryGenerator>;

public class FluentBuilderGeneratorTargetTypeTests
{
    [Fact]
    public async Task Should_generate_for_a_class_target_type()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            public class MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory", Options = FluentFactoryOptions.NoCreateMethod)]
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
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }


    [Fact]
    public async Task Should_generate_for_a_class_target_type_with_primary_constructor()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory", Options = FluentFactoryOptions.NoCreateMethod)]
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
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_generate_for_a_struct_target_type()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            public struct MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory", Options = FluentFactoryOptions.NoCreateMethod)]
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_generate_for_a_struct_target_type_with_primary_constructor()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory", Options = FluentFactoryOptions.NoCreateMethod)]
            public struct MyBuildTarget(int value)
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
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_generate_for_a_record_target_type()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            public record MyBuildTarget
            {
                [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory", Options = FluentFactoryOptions.NoCreateMethod)]
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_generate_for_a_record_target_type_with_primary_constructor()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            [Motiv.Generator.Attributes.GenerateFluentFactory("Test.Factory", Options = FluentFactoryOptions.NoCreateMethod)]
            public record MyBuildTarget(int Value)
            {
                public int Value { get; set; } = Value;
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }
}
