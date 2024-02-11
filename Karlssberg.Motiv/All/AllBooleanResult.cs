﻿namespace Karlssberg.Motiv.All;

/// <summary>Represents a boolean result that indicates whether all operand results are satisfied.</summary>
/// <typeparam name="TMetadata">The type of metadata associated with the boolean result.</typeparam>
/// <typeparam name="TModel">The model used to evaluate each underlying operand.</typeparam>
/// <typeparam name="TMetadata">The type of metadata associated with each underlying operand.</typeparam>
internal sealed class AllBooleanResult<TMetadata>(
    bool isSatisfied,
    IReadOnlyCollection<BooleanResultBase<TMetadata>> operandResults)
    : BooleanResultBase<TMetadata>, ILogicalOperatorResult<TMetadata>, IHigherOrderLogicalOperatorResult<TMetadata>
{
    /// <summary>Gets the collection of all the operand results.</summary>
    public override IEnumerable<BooleanResultBase<TMetadata>> UnderlyingResults { get; } =
        operandResults.ThrowIfNull(nameof(operandResults));

    /// <summary>Gets the determinative operand results that have the same satisfaction as the overall result.</summary>
    public override IEnumerable<BooleanResultBase<TMetadata>> DeterminativeOperands => UnderlyingResults
        .Where(result => result.Satisfied == Satisfied);

    /// <inheritdoc cref="BooleanResultBase{TMetadata}.Satisfied" />
    public override bool Satisfied => isSatisfied;

    /// <inheritdoc cref="BooleanResultBase{TMetadata}.Description" />
    public override string Description => GetDescription();

    private string GetDescription()
    {
        var satisfiedCount = UnderlyingResults.Count(result => result.Satisfied);
        var higherOrderStatement =
            $"ALL{{{satisfiedCount}/{UnderlyingResults.Count()}}}:{IsSatisfiedDisplayText}";

        return DeterminativeOperands.Any()
            ? $"{higherOrderStatement}({ReasonHierarchy.SummarizeReasons()})"
            : higherOrderStatement;
    }

    /// <inheritdoc />
    public override IEnumerable<Reason> ReasonHierarchy => DeterminativeOperands
        .SelectMany(result => result.ReasonHierarchy);
}