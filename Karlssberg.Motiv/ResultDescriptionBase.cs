﻿namespace Karlssberg.Motiv;

/// <summary>
/// Represents the base class for a description of a <see cref="BooleanResultBase"/>.
/// </summary>
public abstract class ResultDescriptionBase
{
    /// <summary>
    /// Gets the count of causal operands.
    /// </summary>
    internal abstract int CausalOperandCount { get; }

    /// <summary>
    /// Gets the reason for the result.
    /// </summary>
    public abstract string Reason { get; }

    /// <summary>
    /// Gets the rationale for the result, represented as a string.
    /// </summary>
    public virtual string Rationale => string.Join(Environment.NewLine, GetDetailsAsLines());

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Reason;

    /// <summary>
    /// Retrieves the details of the result as a collection of lines.
    /// </summary>
    /// <returns>An enumerable collection of strings, each representing a line of detail.</returns>
    public abstract IEnumerable<string> GetDetailsAsLines();
}