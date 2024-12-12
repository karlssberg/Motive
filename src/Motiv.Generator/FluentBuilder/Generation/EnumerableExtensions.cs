using Microsoft.CodeAnalysis;

namespace Motiv.Generator.FluentBuilder.Generation;

public static class EnumerableExtensions
{
    public static IEnumerable<SyntaxNodeOrToken> InterleaveWith(this IEnumerable<SyntaxNode> source, SyntaxNodeOrToken value)
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        yield return enumerator.Current;
        while (enumerator.MoveNext())
        {
            yield return value;
            yield return enumerator.Current;
        }
    }

    public static IEnumerable<T> AppendIfNotNull<T>(this IEnumerable<T> source, T? value)
        where T : class
    {
        return value is null
            ? source
            : source.Append(value);
    }
}
