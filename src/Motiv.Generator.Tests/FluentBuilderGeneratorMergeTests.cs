﻿using Motiv.Generator.FluentBuilder;
using VerifyCS =
    Motiv.Generator.Tests.CSharpSourceGeneratorVerifier<Motiv.Generator.FluentBuilder.FluentFactoryGenerator>;

namespace Motiv.Generator.Tests;

public class FluentBuilderGeneratorMergeTests
{
    [Fact]
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_a_single_parameter()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            public class MyClass<T>
            {
                [FluentConstructor(typeof(MyClass), Options = FluentOptions.NoCreateMethod)]
                public MyClass([FluentMethod("Create")]T value)
                {
                    Value = value;
                }

                public T Value { get; set; }
            }

            [FluentFactory]
            public partial class MyClass
            {
                [FluentConstructor(typeof(MyClass), Options = FluentOptions.NoCreateMethod)]
                public MyClass([FluentMethod("Create")]string value)
                {
                    Value = value;
                }

                public String Value { get; set; }
            }
            """;

        const string expected =
            """
            using System;

            public partial class MyClass
            {
                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                public static MyClass<T> Create<T>(in T value)
                {
                    return new MyClass<T>(value);
                }

                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                public static MyClass Create(in string value)
                {
                    return new MyClass(value);
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
                    (typeof(FluentFactoryGenerator), "MyClass.g.cs", expected)
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
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            public class MyBuildTarget<T1, T2>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Factory<T1> WithValue1<T1>(in T1 value1)
                    {
                        return new Step_0__Test_Factory<T1>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_1__Test_Factory WithValue1(in string value1)
                    {
                        return new Step_1__Test_Factory(value1);
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
                    public MyBuildTarget<T1, T2> WithValue2<T2>(in T2 value2)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1__Test_Factory
                {
                    private readonly string _value1__parameter;
                    public Step_1__Test_Factory(in string value1)
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
    public async Task Should_merge_generated_from_different_namespaces_when_applied_to_class_constructors_with_two_parameters()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace MyFactory
            {
                [FluentFactory]
                public static partial class Factory;
            }

            namespace TestA
            {
                public class MyBuildTarget<T1, T2>
                {
                    [FluentConstructor(typeof(MyFactory.Factory), Options = FluentOptions.NoCreateMethod)]
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
            }

            namespace TestB
            {
                public class MyBuildTarget
                {
                    [FluentConstructor(typeof(MyFactory.Factory), Options = FluentOptions.NoCreateMethod)]
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
            }
            """;

        const string expected =
            """
            using System;
            using TestA;
            using TestB;

            namespace MyFactory
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__MyFactory_Factory<T1> WithValue1<T1>(in T1 value1)
                    {
                        return new Step_0__MyFactory_Factory<T1>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_1__MyFactory_Factory WithValue1(in string value1)
                    {
                        return new Step_1__MyFactory_Factory(value1);
                    }
                }

                public struct Step_0__MyFactory_Factory<T1>
                {
                    private readonly T1 _value1__parameter;
                    public Step_0__MyFactory_Factory(in T1 value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2> WithValue2<T2>(in T2 value2)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1__MyFactory_Factory
                {
                    private readonly string _value1__parameter;
                    public Step_1__MyFactory_Factory(in string value1)
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
                    (typeof(FluentFactoryGenerator), "MyFactory.Factory.g.cs", expected)
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
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            public class MyBuildTarget<T1, T2>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                    public MyBuildTarget<T1, T2> WithValue2<T2>(in T2 value2)
                    {
                        return new MyBuildTarget<T1, T2>(_value1__parameter, value2);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1> WithValue2(in string value2)
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

            public class MyBuildTarget<T1, T2, T3>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Factory<T1> WithValue1<T1>(in T1 value1)
                    {
                        return new Step_0__Test_Factory<T1>(value1);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_1__Test_Factory WithValue1(in string value1)
                    {
                        return new Step_1__Test_Factory(value1);
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
                    public Step_2__Test_Factory<T1, T2> WithValue2<T2>(in T2 value2)
                    {
                        return new Step_2__Test_Factory<T1, T2>(_value1__parameter, value2);
                    }
                }

                public struct Step_1__Test_Factory
                {
                    private readonly string _value1__parameter;
                    public Step_1__Test_Factory(in string value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_3__Test_Factory WithValue2(in string value2)
                    {
                        return new Step_3__Test_Factory(_value1__parameter, value2);
                    }
                }

                public struct Step_2__Test_Factory<T1, T2>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value2__parameter;
                    public Step_2__Test_Factory(in T1 value1, in T2 value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2, T3> WithValue3<T3>(in T3 value3)
                    {
                        return new MyBuildTarget<T1, T2, T3>(_value1__parameter, _value2__parameter, value3);
                    }
                }

                public struct Step_3__Test_Factory
                {
                    private readonly string _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_3__Test_Factory(in string value1, in string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget WithValue3(in string value3)
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
    public async Task Should_merge_generated_when_applied_to_class_constructors_with_four_parameters_where_divergence_occurs_on_the_second_step()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            public class MyBuildTarget<T1, T2, T3, T4>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
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
                    public Step_1__Test_Factory<T1, T2> WithValue2<T2>(in T2 value2)
                    {
                        return new Step_1__Test_Factory<T1, T2>(_value1__parameter, value2);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_2__Test_Factory<T1> WithValue2(in string value2)
                    {
                        return new Step_2__Test_Factory<T1>(_value1__parameter, value2);
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
                    public Step_3__Test_Factory<T1, T2, T3> WithValue3<T3>(in T3 value3)
                    {
                        return new Step_3__Test_Factory<T1, T2, T3>(_value1__parameter, _value2__parameter, value3);
                    }
                }

                public struct Step_2__Test_Factory<T1>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_2__Test_Factory(in T1 value1, in string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_4__Test_Factory<T1> WithValue3(in string value3)
                    {
                        return new Step_4__Test_Factory<T1>(_value1__parameter, _value2__parameter, value3);
                    }
                }

                public struct Step_3__Test_Factory<T1, T2, T3>
                {
                    private readonly T1 _value1__parameter;
                    private readonly T2 _value2__parameter;
                    private readonly T3 _value3__parameter;
                    public Step_3__Test_Factory(in T1 value1, in T2 value2, in T3 value3)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                        _value3__parameter = value3;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1, T2, T3, T4> WithValue4<T4>(in T4 value4)
                    {
                        return new MyBuildTarget<T1, T2, T3, T4>(_value1__parameter, _value2__parameter, _value3__parameter, value4);
                    }
                }

                public struct Step_4__Test_Factory<T1>
                {
                    private readonly T1 _value1__parameter;
                    private readonly string _value2__parameter;
                    private readonly string _value3__parameter;
                    public Step_4__Test_Factory(in T1 value1, in string value2, in string value3)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                        _value3__parameter = value3;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T1> WithValue4(in string value4)
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }

    [Fact]
    public async Task Should_not_merge_generated_when_applied_to_different_root_types()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test;

            [FluentFactory]
            public static partial class Factory;

            [FluentFactory]
            public partial class MyBuildTarget
            {
                [FluentConstructor(typeof(Factory))]
                [FluentConstructor(typeof(MyBuildTarget))]
                public MyBuildTarget(
                    string value1,
                    string value2)
                {
                    Value1 = value1;
                    Value2 = value2;
                }

                public string Value1 { get; set; }

                public string Value2 { get; set; }
            }
            """;

        const string expectedFactory =
            """
            using System;

            namespace Test
            {
                public static partial class Factory
                {
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
                    public Step_1__Test_Factory WithValue2(in string value2)
                    {
                        return new Step_1__Test_Factory(_value1__parameter, value2);
                    }
                }

                public struct Step_1__Test_Factory
                {
                    private readonly string _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_1__Test_Factory(in string value1, in string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget Create()
                    {
                        return new MyBuildTarget(_value1__parameter, _value2__parameter);
                    }
                }
            }
            """;

        const string expectedMyBuildTarget =
            """
            using System;

            namespace Test
            {
                public partial class MyBuildTarget
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_MyBuildTarget WithValue1(in string value1)
                    {
                        return new Step_0__Test_MyBuildTarget(value1);
                    }
                }

                public struct Step_0__Test_MyBuildTarget
                {
                    private readonly string _value1__parameter;
                    public Step_0__Test_MyBuildTarget(in string value1)
                    {
                        _value1__parameter = value1;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Step_1__Test_MyBuildTarget WithValue2(in string value2)
                    {
                        return new Step_1__Test_MyBuildTarget(_value1__parameter, value2);
                    }
                }

                public struct Step_1__Test_MyBuildTarget
                {
                    private readonly string _value1__parameter;
                    private readonly string _value2__parameter;
                    public Step_1__Test_MyBuildTarget(in string value1, in string value2)
                    {
                        _value1__parameter = value1;
                        _value2__parameter = value2;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget Create()
                    {
                        return new MyBuildTarget(_value1__parameter, _value2__parameter);
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
                    (typeof(FluentFactoryGenerator), "Test.Factory.g.cs", expectedFactory),
                    (typeof(FluentFactoryGenerator), "Test.MyBuildTarget.g.cs", expectedMyBuildTarget)
                }
            }
        }.RunAsync();
    }
}
