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

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            public class MyBuildTarget<T>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTarget(T value1)
                {
                    Value1 = value1;
                }

                public T Value1 { get; set; }
            }

            public class MyBuildTarget
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                    public static MyBuildTarget<T> WithValue1<T>(in T value1)
                    {
                        return new MyBuildTarget<T>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Factory WithValue1(in string value1)
                    {
                        return new Step_0__Test_Factory(value1);
                    }
                }

                public struct Step_0__Test_Factory
                {
                    private readonly string _value1__parameter;
                    public Step_0__Test_Factory(in string value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget WithValue2(in string value2)
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
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            public class MyBuildTarget<T1, T2>
            {
                [FluentConstructor(typeof(Factory))]
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
                [FluentConstructor(typeof(Factory))]
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
                    public static Step_0__Test_Factory<T1> WithValue1<T1>(in T1 value1)
                    {
                        return new Step_0__Test_Factory<T1>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_2__Test_Factory WithString1(in string string1)
                    {
                        return new Step_2__Test_Factory(string1);
                    }
                }

                public struct Step_0__Test_Factory<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0__Test_Factory(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Factory<T1, T2> WithValue2<T2>(in T2 value2)
                    {
                        return new Step_1__Test_Factory<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1__Test_Factory<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value2__parameter;
                    public Step_1__Test_Factory(in T1 value1, in T2 value2)
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

                public struct Step_2__Test_Factory
                {
                    private readonly string _string1__parameter;
                    public Step_2__Test_Factory(in string string1)
                    {
                        _string1__parameter = string1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_3__Test_Factory WithString2(in string string2)
                    {
                        return new Step_3__Test_Factory(_string1__parameter, string2);
                    }
                }

                public struct Step_3__Test_Factory
                {
                    private readonly string _string1__parameter;
                    private readonly string _string2__parameter;
                    public Step_3__Test_Factory(in string string1, in string string2)
                    {
                        _string1__parameter = string1;
                        _string2__parameter = string2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_4__Test_Factory WithString3(in string string3)
                    {
                        return new Step_4__Test_Factory(_string1__parameter, _string2__parameter, string3);
                    }
                }

                public struct Step_4__Test_Factory
                {
                    private readonly string _string1__parameter;
                    private readonly string _string2__parameter;
                    private readonly string _string3__parameter;
                    public Step_4__Test_Factory(in string string1, in string string2, in string string3)
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
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            public class MyBuildTarget<T1, T2>
            {
                [FluentConstructor(typeof(Factory))]
                public MyBuildTarget(
                    T1 value1,
                    String value2,
                    T2 value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                [FluentConstructor(typeof(Factory))]
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
                    public static Step_0__Test_Factory<T1> WithValue1<T1>(in T1 value1)
                    {
                        return new Step_0__Test_Factory<T1>(value1);
                    }
                }

                public struct Step_0__Test_Factory<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0__Test_Factory(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Factory<T1> WithValue2(in string value2)
                    {
                        return new Step_1__Test_Factory<T1>(_value1__parameter, value2);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_3__Test_Factory<T1, T2> WithValue3<T2>(in T2 value3)
                    {
                        return new Step_3__Test_Factory<T1, T2>(_value1__parameter, value3);
                    }
                }

                public struct Step_1__Test_Factory<T1>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_1__Test_Factory(in T1 value1, in string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_2__Test_Factory<T1, T2> WithValue3<T2>(in T2 value3)
                    {
                        return new Step_2__Test_Factory<T1, T2>(_value1__parameter, _value2__parameter, value3);
                    }
                }

                public struct Step_2__Test_Factory<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    private readonly T2 _value3__parameter;
                    public Step_2__Test_Factory(in T1 value1, in string value2, in T2 value3)
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

                public struct Step_3__Test_Factory<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value3__parameter;
                    public Step_3__Test_Factory(in T1 value1, in T2 value3)
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

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            public class MyBuildTarget<T1, T2>
            {
                [FluentConstructor(typeof(Factory))]
                public MyBuildTarget(
                    T1 value1,
                    T2 value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                [FluentConstructor(typeof(Factory))]
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
                    public static Step_0__Test_Factory<T1> WithValue1<T1>(in T1 value1)
                    {
                        return new Step_0__Test_Factory<T1>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_2__Test_Factory<T2> WithValue2<T2>(in T2 value2)
                    {
                        return new Step_2__Test_Factory<T2>(value2);
                    }
                }

                public struct Step_0__Test_Factory<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0__Test_Factory(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Factory<T1, T2> WithValue2<T2>(in T2 value2)
                    {
                        return new Step_1__Test_Factory<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1__Test_Factory<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value2__parameter;
                    public Step_1__Test_Factory(in T1 value1, in T2 value2)
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

                public struct Step_2__Test_Factory<T2>
                {
                    private readonly T2 _value2__parameter;
                    public Step_2__Test_Factory(in T2 value2)
                    {
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_3__Test_Factory<T2, T1> WithValue1<T1>(in T1 value1)
                    {
                        return new Step_3__Test_Factory<T2, T1>(_value2__parameter, value1);
                    }
                }

                public struct Step_3__Test_Factory<T2, T1>
                {
                    private readonly T2 _value2__parameter;
                    private readonly T1 _value1__parameter;
                    public Step_3__Test_Factory(in T2 value2, in T1 value1)
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

    [Fact]
    public async Task Should_merge_generated_when_applied_to_record_constructors_with_divergence_from_the_second_step_when_no_create_methods_are_requested()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public static partial class Shape;

            [FluentConstructor(typeof(Shape), Options = FluentOptions.NoCreateMethod)]
            public partial record Square(int Width)
            {
                public int Width { get; set; } = Width;
            }

            [FluentConstructor(typeof(Shape), Options = FluentOptions.NoCreateMethod)]
            public partial class Rectangle(int width, int height)
            {
                public int Width { get; set; } = width;

                public int Height { get; set; } = height;
            }

            [FluentConstructor(typeof(Shape), Options = FluentOptions.NoCreateMethod)]
            public partial record Cuboid(int Width, int Height, int Depth)
            {
                public int Width { get; set; } = Width;
                public int Height { get; set; } = Height;
                public int Depth { get; set; } = Depth;
            }
            """;

        const string expected =
            """
            using System;

            namespace Test
            {
                public static partial class Shape
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Square WithWidth(in int width)
                    {
                        return new Square(width);
                    }
                }

                public partial record Square
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Rectangle WithHeight(in int height)
                    {
                        return new Rectangle(this.Width, height);
                    }
                }

                public partial class Rectangle
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Cuboid WithDepth(in int depth)
                    {
                        return new Cuboid(this.Width, this.Height, depth);
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
                    (typeof(FluentFactoryGenerator), "Test.Shape.g.cs", expected)
                }
            }
        }.RunAsync();
    }
}
