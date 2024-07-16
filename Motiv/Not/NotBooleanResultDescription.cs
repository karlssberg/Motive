﻿namespace Motiv.Not;

internal sealed class NotBooleanResultDescription<TMetadata>(BooleanResultBase operand) : ResultDescriptionBase
{
    private readonly JustificationNegationMappings _negations = new();

    internal override int CausalOperandCount => 1;
    internal override string Statement => "NOT";

    public override string Reason => FormatReason(operand);

    public override IEnumerable<string> GetJustificationAsLines()
    {
        var lines = operand.Description
            .GetJustificationAsLines()
            .ReplaceFirstLine(firstLine =>
                _negations.TryGetValue(firstLine, out var negated)
                    ? negated
                    : firstLine);

        foreach (var line in lines)
            yield return line;
    }

    private static string FormatReason(BooleanResultBase result)
    {
        return result switch
        {
            NotPolicyResult<TMetadata> notResult => NegateNotOperator(notResult),
            NotBooleanOperationResult<TMetadata> notResult => NegateNotOperator(notResult),
            IBooleanOperationResult =>  $"!({result.Reason})",
            _ =>$"!{result.Reason}"
        };
    }

    private static string NegateNotOperator(IUnaryOperationResult<TMetadata> notOperationResult)
    {
        var count = 0;
        var current = notOperationResult;
        while (current.Operand is IUnaryOperationResult<TMetadata> nested)
        {
            count++;
            current = nested;
        }

        return (count % 2 == 0, current.Operand) switch
        {
            (true, _) => current.Operand.Reason,
            (false, IBooleanOperationResult) => $"!({current.Operand.Reason})",
            (false, _) => $"!{current.Operand.Reason}",
        };
    }
}
