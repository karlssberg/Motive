﻿namespace Karlssberg.Motiv.Exactly;

/// <summary>Represents a boolean result that is satisfied if exactly <c>n</c> underlying results are satisfied.</summary>
/// <typeparam name="TMetadata">The type of metadata associated with the boolean result.</typeparam>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TUnderlyingMetadata"></typeparam>
internal sealed class ExactlyBooleanResult<TModel, TMetadata>(
    int n,
    bool isSatisfied,
    IReadOnlyCollection<BooleanResultBase<TMetadata>> operandResults)
    : BooleanResultBase<TMetadata>, ILogicalOperatorResult<TMetadata>
{
    /// <summary>Gets the collection of operand results.</summary>
    public override IEnumerable<BooleanResultBase<TMetadata>> UnderlyingResults => operandResults;

    public override IEnumerable<BooleanResultBase<TMetadata>> DeterminativeOperands => Satisfied switch
    {
        true => UnderlyingResults.Where(result => result.Satisfied),
        false => UnderlyingResults
    };

    /// <summary>Gets a value indicating whether the boolean result is satisfied.</summary>
    public override bool Satisfied => isSatisfied;

    /// <summary>Gets the description of the boolean result.</summary>
    public override string Description => GetDescription();

    private string GetDescription()
    {
        var satisfiedCount = UnderlyingResults.Count(result => result.Satisfied);
        var higherOrderStatement =
            $"{n}_SATISFIED{{{satisfiedCount}/{UnderlyingResults.Count()}}}:{IsSatisfiedDisplayText}";

        return DeterminativeOperands.Any()
            ? $"{higherOrderStatement}({ReasonHierarchy.SummarizeReasons()})"
            : higherOrderStatement;
    }

    /// <summary>Gets the reasons associated with the boolean result.</summary>
    public override IEnumerable<Reason> ReasonHierarchy => DeterminativeOperands
        .SelectMany(r => r.ReasonHierarchy);
}