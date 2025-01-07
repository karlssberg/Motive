﻿using Motiv.Generator.FluentBuilder;

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
    public async Task Should_not_use_namespace_when_parameter_type_declared_within_current_namespace()
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
                public MyBuildTarget(in MyParameterValue value)
                {
                    Value = value;
                }

                public MyParameterValue Value { get; set; }
            }

            public record MyParameterValue(int Value)
            {
                public int Value { get; set; } = Value;
            }
            """;

        const string expected =
            """
            namespace Test
            {
                public partial record struct Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static MyBuildTarget WithValue(in MyParameterValue value)
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
    public async Task Should_not_use_namespace_when_parameter_type_declared_within_current_namespace_using_three_constructors()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public partial record struct Factory;

            public partial class MyBuildTarget
            {
                [Motiv.Generator.Attributes.FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTarget(in MyParameterValue value1, in MyParameterValue value2, MyParameterValue value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                public MyParameterValue Value1 { get; set; }
                public MyParameterValue Value2 { get; set; }
                public MyParameterValue Value3 { get; set; }
            }

            public record MyParameterValue();
            """;

        const string expected =
            """
            namespace Test
            {
                public partial record struct Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Factory WithValue1(in MyParameterValue value1)
                    {
                        return new Step_0__Test_Factory(value1);
                    }
                }

                public struct Step_0__Test_Factory
                {
                    private readonly Test.MyParameterValue _value1__parameter;
                    public Step_0__Test_Factory(in Test.MyParameterValue value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Factory WithValue2(in MyParameterValue value2)
                    {
                        return new Step_1__Test_Factory(_value1__parameter, value2);
                    }
                }

                public struct Step_1__Test_Factory
                {
                    private readonly Test.MyParameterValue _value1__parameter;
                    private readonly Test.MyParameterValue _value2__parameter;
                    public Step_1__Test_Factory(in Test.MyParameterValue value1, in Test.MyParameterValue value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget WithValue3(in MyParameterValue value3)
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }



    [Fact]
    public async Task Should_not_use_namespace_when_parameter_as_generic_type_declared_within_current_namespace_using_three_constructor_parameters()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public partial record struct Factory;

            public partial class MyBuildTarget<T1, T2>
            {
                [Motiv.Generator.Attributes.FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTarget(in MyParameterValue<T1, T2> value1, in MyParameterValue<T1, T2> value2, MyParameterValue<T1, T2> value3)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                }

                public MyParameterValue<T1, T2> Value1 { get; set; }
                public MyParameterValue<T1, T2> Value2 { get; set; }
                public MyParameterValue<T1, T2> Value3 { get; set; }
            }

            public record MyParameterValue<T1, T2>();
            """;

        const string expected =
            """
            namespace Test
            {
                public partial record struct Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Factory<T1, T2> WithValue1<T1, T2>(in MyParameterValue<T1, T2> value1)
                    {
                        return new Step_0__Test_Factory<T1, T2>(value1);
                    }
                }

                public struct Step_0__Test_Factory<T1, T2>
                {
                    private readonly Test.MyParameterValue<T1, T2> _value1__parameter;
                    public Step_0__Test_Factory(in Test.MyParameterValue<T1, T2> value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_Factory<T1, T2> WithValue2(in MyParameterValue<T1, T2> value2)
                    {
                        return new Step_1__Test_Factory<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1__Test_Factory<T1, T2>
                {
                    private readonly Test.MyParameterValue<T1, T2> _value1__parameter;
                    private readonly Test.MyParameterValue<T1, T2> _value2__parameter;
                    public Step_1__Test_Factory(in Test.MyParameterValue<T1, T2> value1, in Test.MyParameterValue<T1, T2> value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2> WithValue3(in MyParameterValue<T1, T2> value3)
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_not_use_namespace_when_parameter_type_declared_within_current_namespace_using_two_constructors()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public partial record struct Factory;

            public partial class MyBuildTargetA
            {
                [Motiv.Generator.Attributes.FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTargetA(in MyParameterValue value1, in MyParameterValue value2, MyParameterValue value3, MyParameterValue value4)
                {
                    Value1 = value1;
                    Value2 = value2;
                    Value3 = value3;
                    Value4 = value4;
                }

                public MyParameterValue Value1 { get; set; }
                public MyParameterValue Value2 { get; set; }
                public MyParameterValue Value3 { get; set; }
                public MyParameterValue Value4 { get; set; }
            }

            public partial class MyBuildTargetB
            {
                [Motiv.Generator.Attributes.FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyBuildTargetB(in MyParameterValue value1, in MyParameterValue value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                public MyParameterValue Value1 { get; set; }
                public MyParameterValue Value2 { get; set; }
            }

            public record MyParameterValue();
            """;

        const string expected =
            """
            namespace Test
            {
                public partial record struct Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Factory WithValue1(in MyParameterValue value1)
                    {
                        return new Step_0__Test_Factory(value1);
                    }
                }

                public struct Step_0__Test_Factory
                {
                    private readonly Test.MyParameterValue _value1__parameter;
                    public Step_0__Test_Factory(in Test.MyParameterValue value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetB WithValue2(in MyParameterValue value2)
                    {
                        return new MyBuildTargetB(_value1__parameter, value2);
                    }
                }

                public partial class MyBuildTargetB
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_2__Test_Factory WithValue3(in MyParameterValue value3)
                    {
                        return new Step_2__Test_Factory(this.Value1, this.Value2, value3);
                    }
                }

                public struct Step_2__Test_Factory
                {
                    private readonly Test.MyParameterValue _value1__parameter;
                    private readonly Test.MyParameterValue _value2__parameter;
                    private readonly Test.MyParameterValue _value3__parameter;
                    public Step_2__Test_Factory(in Test.MyParameterValue value1, in Test.MyParameterValue value2, in Test.MyParameterValue value3)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                        _value3__parameter = value3;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTargetA WithValue4(in MyParameterValue value4)
                    {
                        return new MyBuildTargetA(_value1__parameter, _value2__parameter, _value3__parameter, value4);
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
