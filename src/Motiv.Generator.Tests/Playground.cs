            using System;
            using Motiv.Generator.Attributes;

            [FluentFactory]
            public static partial class Factory;

            public class MyClassA<T1, T2>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyClassA(
                    [MultipleFluentMethods(typeof(FirstStep))]Func<T1, T2> factory1,
                    [MultipleFluentMethods(typeof(SecondStep))]Func<T1, T2, string> factory2)
                {
                    Factory1 = factory1;
                    Factory2 = factory2;
                }

                public Func<T1, T2> Factory1 { get; set; }
                public Func<T1, T2, string> Factory2 { get; set; }
            }

            public class MyClassB<T1, T2>
            {
                [FluentConstructor(typeof(Factory), Options = FluentOptions.NoCreateMethod)]
                public MyClassB(
                    [FluentMethod("Value1")]Func<T1, T2> factory1,
                    [FluentMethod("Create")]Func<T1, T2, int> factory2)
                {
                    Factory1 = factory1;
                    Factory2 = factory2;
                }

                public Func<T1, T2> Factory1 { get; set; }
                public Func<T1, T2, int> Factory2 { get; set; }
            }

            public static class FirstStep
            {
                [FluentMethodTemplate]
                public static Func<T1, T2> Value1<T1, T2>(Func<T1, T2> value)
                {
                    return value;
                }

                [FluentMethodTemplate]
                public static Func<T1, T2> Value1<T1, T2>(T2 value)
                {
                    return _ => value;
                }
            }

            public static class SecondStep
            {
                [FluentMethodTemplate]
                public static Func<T1, T2, string> Create<T1, T2>(Func<T1, T2, string> value)
                {
                    return value;
                }

                [FluentMethodTemplate]
                public static Func<T1, T2, T3> Create<T1, T2, T3>(T3 value)
                {
                    return (_, _) => value;
                }
            }
