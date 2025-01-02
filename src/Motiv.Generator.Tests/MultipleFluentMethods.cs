using Motiv.Generator.FluentBuilder;

namespace Motiv.Generator.Tests;
using VerifyCS = CSharpSourceGeneratorVerifier<FluentBuilder.FluentFactoryGenerator>;

public class MultipleFluentMethodTests
{
    [Fact]
    public async Task Should_build_multiple_root_constructor_methods_for_single_parameter()
    {
        const string code =
            """
            using Motiv.Generator.Attributes;

            namespace Test.Namespace
            {
                [FluentFactory]
                public static partial class Factory;

                public class MyBuildTarget<T>
                {
                    [FluentConstructor(typeof(Factory))]
                    public MyBuildTarget(
                        [MultipleFluentMethods(typeof(MethodVariants))]T data)
                    {
                        Data = data;
                    }

                    public T Data { get; set; }
                }

                public class MethodVariants
                {
                    [FluentMethod]
                    public static T WithValue<T>(in T value)
                    {
                        return value;
                    }

                    [FluentMethod("WithFunction")]
                    public static T WithValue<T>(in Func<T> function)
                    {
                        return function();
                    }
                }
            }
            """;

        const string expected =
            """
            namespace Test.Namespace
            {
                public static partial class Factory
                {
                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory<T> WithValue<T>(in T value)
                    {
                        return new Step_0__Test_Namespace_Factory<T>(value);
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public static Step_0__Test_Namespace_Factory<T> WithFunction<T>(in Func<T> function)
                    {
                        return new Step_0__Test_Namespace_Factory<T>(MethodVariants.WithValue<T>(value));
                    }
                }

                public struct Step_0__Test_Namespace_Factory<T>
                {
                    private readonly T _value__parameter;
                    public Step_0__Test_Namespace_Factory(in T value)
                    {
                        _value__parameter = value;
                    }

                    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    public MyBuildTarget<T> Create()
                    {
                        return new MyBuildTarget<T>(_value__parameter);
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
                    (typeof(FluentFactoryGenerator), "Test.Namespace.Factory.g.cs", expected)
                }
            }
        }.RunAsync();
    }
}
