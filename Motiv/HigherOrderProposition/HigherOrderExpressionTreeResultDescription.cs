using System.Linq.Expressions;
using Motiv.ExpressionTreeProposition;
using Motiv.Shared;

namespace Motiv.HigherOrderProposition;

internal sealed class HigherOrderExpressionTreeResultDescription<TUnderlyingMetadata>(
    bool satisfied,
    string reason,
    IEnumerable<string> additionalAssertions,
    LambdaExpression expression,
    IEnumerable<BooleanResultBase<TUnderlyingMetadata>> causes,
    string propositionStatement)
    : ResultDescriptionBase
{
    private readonly ICollection<BooleanResultBase<TUnderlyingMetadata>> _causes = causes.ToArray();

    internal override int CausalOperandCount => _causes.Count;

    internal override string Statement => propositionStatement;

    public override string Reason => reason;

    public override IEnumerable<string> GetJustificationAsLines()
    {
        yield return Reason;
        yield return expression.ToAssertion(satisfied).Indent();
        var distinctWithOrderPreserved = additionalAssertions.DistinctWithOrderPreserved().ToArray();
        foreach (var line in distinctWithOrderPreserved)
            yield return line.Indent(2);

        foreach (var line in GetUnderlyingJustificationsAsLines())
        {
            var levelOfIndentation = distinctWithOrderPreserved.Length > 0 ? 2 : 1;
            yield return line.Indent(levelOfIndentation);
        }
    }

    private IEnumerable<string> GetUnderlyingJustificationsAsLines()
    {
        foreach (var line in UnderlyingDetailsAsLines())
            yield return line.Indent();

        yield break;

        IEnumerable<string> UnderlyingDetailsAsLines()
        {
            return _causes
                .DistinctWithOrderPreserved(result => result.Justification)
                .SelectMany(cause => cause.Description.GetJustificationAsLines());
        }
    }
}

