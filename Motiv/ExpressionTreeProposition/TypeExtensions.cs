﻿using System.Linq.Expressions;
using System.Reflection;

namespace Motiv.ExpressionTreeProposition;

internal static class ExpressionTreeExtensions
{
    internal static string ToCSharpName(this Type type)
    {
        var isNullableType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        var nullableSuffix = isNullableType ? "?" : string.Empty;
        type = isNullableType ? Nullable.GetUnderlyingType(type)! : type;

        var typeName = GetTypeKeyword(type);
        if (!type.IsGenericType) return $"{typeName}{nullableSuffix}";

        var genericTypeName = typeName.Substring(0, type.Name.IndexOf('`'));
        var genericArguments = type.GetGenericArguments().Select(ToCSharpName);
        return $"{genericTypeName}<{string.Join(", ", genericArguments)}>{nullableSuffix}";
    }

    internal static string ToCSharpName(this MethodCallExpression methodCallExpression)
    {
        return methodCallExpression.Method.Name;
    }

    internal static (object?, Type) GetConstantExpressionValue(
        this ConstantExpression constantExpression,
        string capturedVariableName)
    {
        var value = constantExpression.Value;
        var valueType = constantExpression.Type;

        if (value == null || !IsClosureObject(value.GetType())) return (value, valueType); // not a closure

        var capturedField = value.GetClosureObjectField(capturedVariableName);

        return capturedField != null
            ? (capturedField.GetValue(value), capturedField.FieldType)
            : (value, valueType); // If no suitable field found, return the closure object itself
    }

    internal static FieldInfo? GetClosureObjectField(this object value, string capturedVariableName) =>
        value.GetType().GetField(capturedVariableName);

    internal static bool IsClosureObject(this Type type) =>
        // Closure types are compiler-generated and usually have specific naming patterns
        type.Name.StartsWith("<>") || type.Name.Contains("DisplayClass");

    private static string GetTypeKeyword(Type type)
    {
        return type switch
        {
            // Integral types
            _ when type == typeof(sbyte) => "sbyte",
            _ when type == typeof(byte) => "byte",
            _ when type == typeof(short) => "short",
            _ when type == typeof(ushort) => "ushort",
            _ when type == typeof(int) => "int",
            _ when type == typeof(uint) => "uint",
            _ when type == typeof(long) => "long",
            _ when type == typeof(ulong) => "ulong",
            _ when type == typeof(char) => "char",

            // Floating-point types
            _ when type == typeof(float) => "float",
            _ when type == typeof(double) => "double",

            // Decimal type
            _ when type == typeof(decimal) => "decimal",

            // Boolean type
            _ when type == typeof(bool) => "bool",

            // String type
            _ when type == typeof(string) => "string",

            // Object type
            _ when type == typeof(object) => "object",

            // Native-sized integers
            _ when type == typeof(nint) => "nint",
            _ when type == typeof(nuint) => "nuint",

            // Special case for void
            _ when type == typeof(void) => "void",

            // For any other type, return its name
            _ => type.Name
        };
    }

    internal static bool InheritsFrom(this Type concreteType, Type superType)
    {
        var type = concreteType;
        while (type != null && type != typeof(object))
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == superType || type == superType)
                return true;

            type = type.BaseType;
        }
        return false;
    }

    internal static string Serialize(this Expression expression)
    {
        return new CSharpExpressionSerializer().Serialize(expression);
    }

    internal static string Serialize<T>(this Expression expression, T parameterValue, ParameterExpression parameter)
    {
        return new CSharpExpressionSerializer<T>(parameterValue, parameter).Serialize(expression);
    }

    internal static Expression ToExpressionAssertion(
        this Expression expression,
        bool satisfied)
    {
        return (satisfied, expression, expression.NodeType) switch
        {
            (false, BinaryExpression expr, ExpressionType.GreaterThan) =>
                Expression.LessThanOrEqual(expr.Left, expr.Right),
            (true, BinaryExpression, ExpressionType.GreaterThan) =>
                expression,
            (false, BinaryExpression expr, ExpressionType.GreaterThanOrEqual) =>
                Expression.LessThan(expr.Left, expr.Right),
            (true, BinaryExpression, ExpressionType.GreaterThanOrEqual) =>
                expression,
            (false, BinaryExpression expr, ExpressionType.LessThan) =>
                Expression.GreaterThanOrEqual(expr.Left, expr.Right),
            (true, BinaryExpression, ExpressionType.LessThan) =>
                expression,
            (false, BinaryExpression expr, ExpressionType.LessThanOrEqual) =>
                Expression.GreaterThan(expr.Left, expr.Right),
            (true, BinaryExpression, ExpressionType.LessThanOrEqual) =>
                expression,
            (false, BinaryExpression expr, ExpressionType.Equal) =>
                Expression.NotEqual(expr.Left, expr.Right),
            (true, BinaryExpression, ExpressionType.Equal) =>
                expression,
            (false, BinaryExpression expr, ExpressionType.NotEqual) =>
                Expression.Equal(expr.Left, expr.Right),
            (true, BinaryExpression, ExpressionType.NotEqual) =>
                expression,
            (_, _, _) =>
                    Expression.Equal(
                        Expression.Convert(expression, typeof(object)),
                        Expression.Convert(Expression.Constant(satisfied), typeof(object))),
        };
    }
}
