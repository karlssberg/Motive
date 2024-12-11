namespace Motiv.Generator.FluentBuilder.Generation;

public static class StringExtensions
{
    public static string Capitalize(this string input) =>
        string.IsNullOrEmpty(input)
            ? input
            : $"{char.ToUpper(input[0])}{input.Substring(1)}";

    public static string ToCamelCase(this string input) =>
        string.IsNullOrEmpty(input)
            ? input
            : $"{char.ToLower(input[0])}{input.Substring(1)}";

    public static string ToParameterFieldName(this string name)
    {
        return $"_{name.ToCamelCase()}__parameter";
    }

}
