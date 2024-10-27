using System.Linq.Expressions;

namespace Motiv.ExpressionTreeProposition;

internal static class ExpressionExtensions
{
    internal static Expression ToAssertionExpression(this Expression expression, bool satisfied)
    {
        return (expression, satisfied) switch
        {
            (ConstantExpression constantExpression, _) when constantExpression.Type == typeof(bool) =>
                constantExpression,
            (TypeBinaryExpression { NodeType: ExpressionType.TypeIs } typeIsExpression, true) =>
                typeIsExpression,
            (TypeBinaryExpression { NodeType: ExpressionType.TypeIs } typeIsExpression, false) =>
                Expression.Not(typeIsExpression),
            _ =>
                Expression.Equal(expression, Expression.Constant(satisfied)),
        };
    }
}
