﻿using Motiv.Generator.FluentBuilder;

namespace Motiv.Generator.Tests;
using VerifyCS = CSharpSourceGeneratorVerifier<FluentBuilder.FluentFactoryGenerator>;

public class NamespaceTests
{
    [Fact]
    public async Task Should_that_namespace_references_are_properly_made()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test
            {
                [FluentFactory]
                public static partial class Factory;
            }

            namespace Test.NamespaceA
            {

                public class MyBuildTargetA<T>
                {
                    [FluentConstructor(typeof(Test.Factory), Options = FluentOptions.NoCreateMethod)]
                    public MyBuildTargetA(
                        [MultipleFluentMethods(typeof(MethodVariants))]T data,
                        int value)
                    {
                        Data = data;
                        Value = value;
                    }

                    public T Data { get; set; }
                    public int Value { get; set; }
                }
            }

            namespace Test.NamespaceB
            {
                public class MyBuildTargetB<T>
                {
                    [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                    public MyBuildTargetB(
                        [MultipleFluentMethods(typeof(MethodVariants))]T data,
                        string value)
                    {
                        Data = data;
                        Value = value;
                    }

                    public T Data { get; set; }
                    public string Value { get; set; }
                }
            }

            public class MethodVariants
            {
                [FluentMethodTemplate]
                public static T WithDefaultValue<T>()
                {
                    return default(T);
                }

                [FluentMethodTemplate]
                public static T WithFunction<T>(in Func<T> function)
                {
                    return function();
                }
            }
            """;

        const string expected =
            """
            using System;
            using Test.NamespaceA;
            using Test.NamespaceB;

            namespace Test
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Factory<T> WithDefaultValue<T>()
                    {
                        return new Step_0__Test_Factory<T>(MethodVariants.WithDefaultValue<T>());
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Factory<T> WithFunction<T>(in System.Func<T> function)
                    {
                        return new Step_0__Test_Factory<T>(MethodVariants.WithFunction<T>(function));
                    }
                }

                public struct Step_0__Test_Factory<T>
                {
                    private readonly T _data__parameter;
                    public Step_0__Test_Factory(in T data)
                    {
                        this._data__parameter = data;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public NamespaceA.MyBuildTargetA<T> WithValue(in int value)
                    {
                        return new NamespaceA.MyBuildTargetA<T>(this._data__parameter, value);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public NamespaceB.MyBuildTargetB<T> WithValue(in string value)
                    {
                        return new NamespaceB.MyBuildTargetB<T>(this._data__parameter, value);
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
    public async Task Should_that_namespace_references_are_properly_made_when_referencing_existing_step_types()
    {
        const string code =
            """
            using System;
            using Motiv.Generator.Attributes;

            namespace Test
            {
                [FluentFactory]
                public static partial class Factory;
            }

            namespace Test.NamespaceA
            {
                public class MyBuildTargetA<T>
                {
                    [FluentConstructor(typeof(Test.Factory), Options = FluentOptions.NoCreateMethod)]
                    public MyBuildTargetA(
                        [MultipleFluentMethods(typeof(MethodVariants))]T data,
                        int value)
                    {
                        Data = data;
                        Value = value;
                    }

                    public T Data { get; set; }
                    public int Value { get; set; }
                }
            }

            namespace Test.NamespaceB
            {
                public partial class MyBuildTargetB<T>
                {
                    [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                    public MyBuildTargetB(
                        [MultipleFluentMethods(typeof(MethodVariants))]T data)
                    {
                        Data = data;
                    }

                    public T Data { get; set; }
                }
            }

            public class MethodVariants
            {
                [FluentMethodTemplate]
                public static T WithDefaultValue<T>()
                {
                    return default(T);
                }

                [FluentMethodTemplate]
                public static T WithValue<T>(in T data)
                {
                    return data;
                }
            }
            """;

        const string expected =
            """
            using System;
            using Test.NamespaceA;
            using Test.NamespaceB;

            namespace Test
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static NamespaceB.MyBuildTargetB<T> WithDefaultValue<T>()
                    {
                        return new NamespaceB.MyBuildTargetB<T>(MethodVariants.WithDefaultValue<T>());
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static NamespaceB.MyBuildTargetB<T> WithValue<T>(in T data)
                    {
                        return new NamespaceB.MyBuildTargetB<T>(MethodVariants.WithValue<T>(data));
                    }
                }
            }

            namespace Test.NamespaceB
            {
                public partial class MyBuildTargetB<T>
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public Test.NamespaceA.MyBuildTargetA<T> WithValue(in int value)
                    {
                        return new Test.NamespaceA.MyBuildTargetA<T>(this.Data, value);
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
