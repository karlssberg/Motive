﻿namespace Karlssberg.Motive;

public static class BooleanResultBaseExtensions
{
    public static IEnumerable<TMetadata> GetInsights<TMetadata>(this BooleanResultBase<TMetadata> booleanResultBase) =>
        new InsightsVisitor().Visit(booleanResultBase).Distinct();
}