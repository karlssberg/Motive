﻿namespace Karlssberg.Motive;

public static class EnumerableExtensions
{
    public static BooleanResultBase<TMetadata> Any<TModel, TMetadata>(
        this IEnumerable<TModel> source, 
        SpecificationBase<TModel, TMetadata> specification)
    {
        return specification.ToAnySpecification().IsSatisfiedBy(source);
    }
    
    public static BooleanResultBase<TMetadata> All<TModel, TMetadata>(
        this IEnumerable<TModel> source, 
        SpecificationBase<TModel, TMetadata> specification)
    {
        return specification.ToAnySpecification().IsSatisfiedBy(source);
    }
    
    public static IEnumerable<T> ElseIfEmpty<T>(this IEnumerable<T> source, IEnumerable<T> other)
    {
        var hasItems = false;
        foreach (var item in source)
        {
            yield return item;
            hasItems = true;
        }

        if (hasItems)
            yield break;
        
        foreach (var item in other)
            yield return item;
    }
}