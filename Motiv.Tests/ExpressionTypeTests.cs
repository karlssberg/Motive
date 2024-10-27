﻿using System.Linq.Expressions;
using System.Net.Security;

namespace Motiv.Tests;

public class ExpressionTypeTests
{
    [Fact]
    public void Should_serialize_add_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n + 1 > n)
                .Create("n + 1");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n + 1 > n");
    }

    [Fact]
    public void Should_serialize_add_check_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => checked(n + 1) > n)
                .Create("checked(n + 1)");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("checked(n + 1) > n");
    }

    [Fact]
    public void Should_serialize_bitwise_expression()
    {
        // Assemble
        var sut =
            Spec.From((byte n) => (n | 0) == n)
                .Create("n & 0");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("(n | 0) == n");
    }

    [Fact]
    public void Should_serialize_logical_and_expression()
    {
        // Assemble
        var sut =
            Spec.From((bool b) => b && b)
                .Create("b && b");

        // Act
        var act = sut.IsSatisfiedBy(true);

        // Assert
        act.Assertions.Should().BeEquivalentTo("b == true");
    }

    [Fact]
    public void Should_serialize_array_length_expression()
    {
        // Assemble
        var sut =
            Spec.From((int[] a) => a.Length > 0)
                .Create("a.Length");

        // Act
        var act = sut.IsSatisfiedBy([1]);

        // Assert
        act.Assertions.Should().BeEquivalentTo("a.Length > 0");
    }

    [Fact]
    public void Should_serialize_array_index_expression()
    {
        // Assemble
        var sut =
            Spec.From((int[] a) => a[0] > 0)
                .Create("a[0]");

        // Act
        var act = sut.IsSatisfiedBy([1]);

        // Assert
        act.Assertions.Should().BeEquivalentTo("a[0] > 0");
    }

    [Fact]
    public void Should_serialize_method_call_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => Math.Abs(n) > 0)
                .Create("Math.Abs(n)");

        // Act
        var act = sut.IsSatisfiedBy(-1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("Math.Abs(n) > 0");
    }

    [Fact]
    public void Should_serialize_coalesce_expression()
    {
        // Assemble
        var sut =
            Spec.From((int? n) => (n ?? 0) > 0)
                .Create("n ?? 0");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("(n ?? 0) > 0");
    }

    [Fact]
    public void Should_serialize_conditional_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n > 0 ? n > 10 : n < -10)
                .Create("n > 0 ? n : 0");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n <= 10");
    }

    [Fact]
    public void Should_serialize_constant_expression()
    {
        // Assemble
        const int zero = 0;
        var sut =
            Spec.From((int n) => n > zero)
                .Create("n > 0");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n > 0");
    }

    [Fact]
    public void Should_serialize_convert_expression()
    {
        // Assemble
        var sut =
            Spec.From((double n) => (int)n > 0)
                .Create("(int)n");

        // Act
        var act = sut.IsSatisfiedBy(1.5);

        // Assert
        act.Assertions.Should().BeEquivalentTo("(int)n > 0");
    }

    [Fact]
    public void Should_serialize_convert_checked_expression()
    {
        // Assemble
        var sut =
            Spec.From((double n) => checked((int)n) > 0)
                .Create("checked((int)n)");

        // Act
        var act = sut.IsSatisfiedBy(1.5);

        // Assert
        act.Assertions.Should().BeEquivalentTo("checked((int)n) > 0");
    }

    [Fact]
    public void Should_serialize_division_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n / 2 > 0)
                .Create("n / 2");

        // Act
        var act = sut.IsSatisfiedBy(2);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n / 2 > 0");
    }

    [Fact]
    public void Should_serialize_equal_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n == 0)
                .Create("n == 0");

        // Act
        var act = sut.IsSatisfiedBy(0);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n == 0");
    }

    [Fact]
    public void Should_serialize_exclusive_or_expression()
    {
        // Assemble
        var a = false;
        var sut =
            Spec.From((bool b) => a ^ b)
                .Create("a ^ b");

        // Act
        var act = sut.IsSatisfiedBy(true);

        // Assert
        act.Assertions.Should().BeEquivalentTo("a == false", "b == true");
    }

    [Fact]
    public void Should_serialize_greater_than_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n > 0)
                .Create("n > 0");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n > 0");
    }

    [Fact]
    public void Should_serialize_greater_than_or_equal_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n >= 0)
                .Create("n >= 0");

        // Act
        var act = sut.IsSatisfiedBy(0);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n >= 0");
    }

    [Fact]
    public void Should_serialize_invoke_expression()
    {
        // Assemble
        var sut =
            Spec.From((Func<int, bool> f) => f(1))
                .Create("f(1)");

        // Act
        var act = sut.IsSatisfiedBy(n => n > 0);

        // Assert
        act.Assertions.Should().BeEquivalentTo("f(1) == true");
    }

    [Fact]
    public void Should_serialize_lambda_expression()
    {
        // Assemble
        var sut =
            Spec.From((Func<int, bool> f) => f(1))
                .Create("f(1)");

        // Act
        var act = sut.IsSatisfiedBy(n => n > 0);

        // Assert
        act.Assertions.Should().BeEquivalentTo("f(1) == true");
    }

    [Fact]
    public void Should_serialize_left_shift_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n << 1 > 0)
                .Create("n << 1");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n << 1 > 0");
    }

    [Fact]
    public void Should_serialize_less_than_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n < 0)
                .Create("n < 0");

        // Act
        var act = sut.IsSatisfiedBy(-1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n < 0");
    }

    [Fact]
    public void Should_serialize_less_than_or_equal_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n <= 0)
                .Create("n <= 0");

        // Act
        var act = sut.IsSatisfiedBy(0);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n <= 0");
    }

    [Fact]
    public void Should_serialize_list_init_expression()
    {
        // Assemble
        var sut =
            Spec.From((List<int> l) => l[0] > 0)
                .Create("l[0] > 0");

        // Act
        var act = sut.IsSatisfiedBy(new List<int> { 1 });

        // Assert
        act.Assertions.Should().BeEquivalentTo("l[0] > 0");
    }

    [Fact]
    public void Should_serialize_member_access_expression()
    {
        // Assemble
        var sut =
            Spec.From((DateTime d) => d.Year > 0)
                .Create("d.Year");

        // Act
        var act = sut.IsSatisfiedBy(DateTime.Now);

        // Assert
        act.Assertions.Should().BeEquivalentTo("d.Year > 0");
    }

    [Fact]
    public void Should_serialize_member_init_expression()
    {
        // Assemble
        var sut =
            Spec.From((DateTime d) => d.Year > 0)
                .Create("d.Year");

        // Act
        var act = sut.IsSatisfiedBy(new DateTime(2021, 1, 1));

        // Assert
        act.Assertions.Should().BeEquivalentTo("d.Year > 0");
    }

    [Fact]
    public void Should_serialize_modulo_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n % 2 > 0)
                .Create("n % 2");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n % 2 > 0");
    }

    [Fact]
    public void Should_serialize_multiply_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n * 2 > 0)
                .Create("n * 2");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n * 2 > 0");
    }

    [Fact]
    public void Should_serialize_multiply_checked_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => checked(n * 2) > 0)
                .Create("checked(n * 2)");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("checked(n * 2) > 0");
    }

    [Fact]
    public void Should_serialize_negate_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => -n > 0)
                .Create("-n");

        // Act
        var act = sut.IsSatisfiedBy(-1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("-n > 0");
    }

    [Fact]
    public void Should_serialize_unary_plus_expression()
    {
        // Assemble
        var parameter = Expression.Parameter(typeof(int), "n");
        var zero = Expression.Constant(0);
        var equals = Expression.GreaterThan(
            Expression.MakeUnary(ExpressionType.UnaryPlus, parameter, typeof(int)),
            zero);
        var lambda = Expression.Lambda<Func<int, bool>>(equals, parameter);

        var sut =
            Spec.From(lambda)
                .Create("+n");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("+n > 0");
    }

    [Fact]
    public void Should_serialize_negate_checked_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => checked(-n) > 0)
                .Create("checked(-n)");

        // Act
        var act = sut.IsSatisfiedBy(-1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("checked(-n) > 0");
    }

    [Fact]
    public void Should_serialize_new_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => new List<int> { n }[0] > 0)
                .Create("new List<int> { n }[0]");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("new List<int> { n }[0] > 0");
    }

    [Fact]
    public void Should_serialize_new_array_init_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => new[] { n }[0] > 0)
                .Create("new int[] { n }[0]");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("new int[] { n }[0] > 0");
    }

    [Fact]
    public void Should_serialize_uninitialized_array_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => new int[n].Length == n)
                .Create("new int[n].Length == n");

        // Act
        var act = sut.IsSatisfiedBy(2);

        // Assert
        act.Assertions.Should().BeEquivalentTo("new int[n].Length == n");
    }

    [Fact]
    public void Should_serialize_multi_dimensional_array_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => new int[n, n, n].Length == n * n * n)
                .Create("new int[n, n, n].Length == n * n * n");

        // Act
        var act = sut.IsSatisfiedBy(2);

        // Assert
        act.Assertions.Should().BeEquivalentTo("new int[n, n, n].Length == n * n * n");
    }

    [Fact]
    public void Should_serialize_not_expression()
    {
        // Assemble
        var sut =
            Spec.From((bool b) => !b)
                .Create("!b");

        // Act
        var act = sut.IsSatisfiedBy(false);

        // Assert
        act.Assertions.Should().BeEquivalentTo("b == false");
    }

    [Fact]
    public void Should_serialize_not_equal_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n != 0)
                .Create("n != 0");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n != 0");
    }

    [Fact]
    public void Should_serialize_or_expression()
    {
        // Assemble
        var sut =
            Spec.From((bool b) => b || b)
                .Create("b || b");

        // Act
        var act = sut.IsSatisfiedBy(true);

        // Assert
        act.Assertions.Should().BeEquivalentTo("b == true");
    }

    [Fact]
    public void Should_serialize_or_else_expression()
    {
        // Assemble
        var sut =
            Spec.From((bool b) => b | b)
                .Create("b | b");

        // Act
        var act = sut.IsSatisfiedBy(true);

        // Assert
        act.Assertions.Should().BeEquivalentTo("b == true");
    }

    [Fact]
    public void Should_serialize_parameter_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n > 0)
                .Create("n > 0");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n > 0");
    }

    [Fact]
    public void Should_serialize_right_shift_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n >> 1 > 0)
                .Create("n >> 1");

        // Act
        var act = sut.IsSatisfiedBy(2);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n >> 1 > 0");
    }

    [Fact]
    public void Should_serialize_subtract_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => n - 1 > 0)
                .Create("n - 1");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("n - 1 <= 0");
    }

    [Fact]
    public void Should_serialize_subtract_checked_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => checked(n - 1) > 0)
                .Create("checked(n - 1)");

        // Act
        var act = sut.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("checked(n - 1) <= 0");
    }

    [Fact]
    public void Should_serialize_type_as_expression()
    {
        // Assemble
        var sut =
            Spec.From((object o) => o as string != null)
                .Create("o as string");

        // Act
        var act = sut.IsSatisfiedBy("string");

        // Assert
        act.Assertions.Should().BeEquivalentTo("o as string != null");
    }

    [Fact]
    public void Should_serialize_type_is_expression()
    {
        // Assemble
        var sut =
            Spec.From((object o) => o is string)
                .Create("o is string");

        // Act
        var act = sut.IsSatisfiedBy("string");

        // Assert
        act.Assertions.Should().BeEquivalentTo("o is string");
    }

    [Fact]
    public void Should_serialize_index_expression()
    {
        // Assemble
        var sut =
            Spec.From((Dictionary<int, string> d) => d[0] == "0")
                .Create("d[0]");

        // Act
        var act = sut.IsSatisfiedBy(new Dictionary<int, string> { [0] = "0" });

        // Assert
        act.Assertions.Should().BeEquivalentTo("d[0] == \"0\"");
    }

    [Fact]
    public void Should_serialize_type_equal_expression()
    {
        // Assemble
        var sut =
            Spec.From((object obj) => obj is string)
                .Create("obj is string");

        // Act
        var act = sut.IsSatisfiedBy("string");

        // Assert
        act.Assertions.Should().BeEquivalentTo("obj is string");
    }

    [Fact]
    public void Should_serialize_negated_type_equal_expression()
    {
        // Assemble
        var sut =
            Spec.From((object obj) => obj is string)
                .Create("obj is not string");

        // Act
        var act = sut.IsSatisfiedBy(3);

        // Assert
        act.Assertions.Should().BeEquivalentTo("obj is not string");
    }

    [Fact]
    public void Should_serialize_ones_complement_expression()
    {
        // Assemble
        var sut =
            Spec.From((int n) => ~n > 0)
                .Create("~n > 0");

        // Act
        var act = sut.IsSatisfiedBy(-1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("~n <= 0");
    }

    [Fact]
    public void Should_serialize_is_true_expression()
    {
        // Assemble
        var sut =
            Spec.From((bool b) => true)
                .Create("true");

        // Act
        var act = sut.IsSatisfiedBy(true);

        // Assert
        act.Assertions.Should().BeEquivalentTo("true");
    }

    [Fact]
    public void Should_serialize_is_false_expression()
    {
        // Assemble
        var sut =
            Spec.From((bool b) => false)
                .Create("false");

        // Act
        var act = sut.IsSatisfiedBy(false);

        // Assert
        act.Assertions.Should().BeEquivalentTo("false");
    }
}
