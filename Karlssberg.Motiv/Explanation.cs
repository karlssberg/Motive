﻿using System.Diagnostics;

namespace Karlssberg.Motiv;

[DebuggerDisplay("{Debug}")]
public sealed class Explanation
{
    public Explanation(string assertion, IEnumerable<BooleanResultBase> causes) 
        : this(assertion.ToEnumerable(), causes)
    {
    }
    
    public Explanation(IEnumerable<string> assertions, IEnumerable<BooleanResultBase> causes) 
    {
        var assertionsArray = assertions as string[] ?? assertions.ToArray();
        Causes = causes;
        Assertions = assertionsArray.Distinct();
    }

    public IEnumerable<BooleanResultBase> Causes { get; }

    private static IEnumerable<Explanation> ResolveUnderlying(
       IEnumerable<string> assertions,
        IEnumerable<BooleanResultBase> causes)
    {
        var underlying = causes
            .SelectMany(cause =>
                cause switch
                {
                    IBooleanOperationResult => cause.UnderlyingAssertionSources,
                    _ => cause.ToEnumerable()
                })
            .Select(cause => cause.Explanation)
            .ToArray();

        var underlyingAssertions = underlying
            .SelectMany(explanation => explanation.Assertions)
            .Distinct();

        var doesParentEqualChildAssertion = underlyingAssertions.SequenceEqual(assertions);

        return doesParentEqualChildAssertion
            ? underlying.SelectMany(result => result.Underlying)
            : underlying;
    }

    public IEnumerable<string> Assertions { get; }

    public IEnumerable<Explanation> Underlying => ResolveUnderlying(Assertions, Causes);
    
    public override string ToString() => Assertions.Serialize();

    private string Debug => GetDebuggerDisplay();
    
    private string GetDebuggerDisplay()
    {
        return HaveComprehensiveAssertions() || !Underlying.Any()
            ? ToString()
            : $$"""
                {{ToString()}} { {{Underlying.GetAssertions().Serialize()}} }
                """;
        
        bool HaveComprehensiveAssertions() => Assertions.HasAtLeast(2);
    }
}