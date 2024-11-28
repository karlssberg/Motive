namespace Motiv.Shared;

internal static class IndentStringExtensions
{
    private const string Value = "    ";

    internal static string Indent(this string line, int levelOfIndentation = 1) =>
        $"{string.Join("", Enumerable.Repeat(Value, levelOfIndentation))}{line}";
}
