using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace EntityFrameworkCore.Generator.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex("([A-Z][a-z]*)|([0-9]+)", RegexOptions.ExplicitCapture, matchTimeoutMilliseconds: 1000)]
    private static partial Regex WordsExpression();

    private static readonly Regex _splitNameRegex = new Regex(@"[\W_]+");

    /// <summary>
    /// Truncates the specified text.
    /// </summary>
    /// <param name="text">The text to truncate.</param>
    /// <param name="keep">The number of characters to keep.</param>
    /// <param name="ellipsis">The ellipsis string to use when truncating. (Default ...)</param>
    /// <returns>
    /// A truncate string.
    /// </returns>
    [return: NotNullIfNotNull(nameof(text))]
    public static string? Truncate(this string? text, int keep, string ellipsis = "...")
    {
        if (string.IsNullOrEmpty(text))
            return text;

        if (text!.Length <= keep)
            return text;

        ellipsis ??= string.Empty;

        if (text.Length <= keep + ellipsis.Length || keep < ellipsis.Length)
            return text[..keep];

        int prefix = keep - ellipsis.Length;
        return string.Concat(text[..prefix], ellipsis);
    }

    /// <summary>
    /// Indicates whether the specified String object is null or an empty string
    /// </summary>
    /// <param name="item">A String reference</param>
    /// <returns>
    ///     <see langword="true"/> if is null or empty; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? item)
    {
        return string.IsNullOrEmpty(item);
    }

    /// <summary>
    /// Indicates whether a specified string is null, empty, or consists only of white-space characters
    /// </summary>
    /// <param name="item">A String reference</param>
    /// <returns>
    ///      <see langword="true"/> if is null or empty; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? item)
    {
        if (item == null)
            return true;

        for (int i = 0; i < item.Length; i++)
        {
            if (!char.IsWhiteSpace(item[i]))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Determines whether the specified string is not <see cref="IsNullOrEmpty"/>.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <paramref name="value"/> is not <see cref="IsNullOrEmpty"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool HasValue([NotNullWhen(true)] this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Does string contain both uppercase and lowercase characters?
    /// </summary>
    /// <param name="s">The value.</param>
    /// <returns>True if contain mixed case.</returns>
    public static bool IsMixedCase(this string s)
    {
        if (s.IsNullOrEmpty())
            return false;

        var containsUpper = s.Any(Char.IsUpper);
        var containsLower = s.Any(Char.IsLower);

        return containsLower && containsUpper;
    }

    /// <summary>
    /// Converts a string to use camelCase.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The to camel case. </returns>
    public static string ToCamelCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        string output = ToPascalCase(value);
        if (output.Length > 2)
            return char.ToLower(output[0]) + output[1..];

        return output.ToLower();
    }

    /// <summary>
    /// Converts a string to use PascalCase.
    /// </summary>
    /// <param name="value">Text to convert</param>
    /// <returns>The string</returns>
    public static string ToPascalCase(this string value)
    {
        return value.ToPascalCase(_splitNameRegex);
    }

    /// <summary>
    /// Converts a string to use PascalCase.
    /// </summary>
    /// <param name="value">Text to convert</param>
    /// <param name="splitRegex">Regular Expression to split words on.</param>
    /// <returns>The string</returns>
    public static string ToPascalCase(this string value, Regex splitRegex)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        var mixedCase = value.IsMixedCase();
        var names = splitRegex.Split(value);
        var output = new StringBuilder();

        if (names.Length > 1)
        {
            foreach (string name in names)
            {
                if (name.Length > 1)
                {
                    output.Append(char.ToUpper(name[0]));
                    output.Append(mixedCase ? name[1..] : name[1..].ToLower());
                }
                else
                {
                    output.Append(name.ToUpper());
                }
            }
        }
        else if (value.Length > 1)
        {
            output.Append(char.ToUpper(value[0]));
            output.Append(mixedCase ? value[1..] : value[1..].ToLower());
        }
        else
        {
            output.Append(value.ToUpper());
        }

        return output.ToString();
    }

    /// <summary>
    /// Combines two strings with the specified separator.
    /// </summary>
    /// <param name="first">The first string.</param>
    /// <param name="second">The second string.</param>
    /// <param name="separator">The separator string.</param>
    /// <returns>A string combining the <paramref name="first"/> and <paramref name="second"/> parameters with the <paramref name="separator"/> between them</returns>
    [return: NotNullIfNotNull(nameof(first))]
    [return: NotNullIfNotNull(nameof(second))]
    public static string? Combine(this string? first, string? second, char separator = '/')
    {
        if (string.IsNullOrEmpty(first))
            return second;

        if (string.IsNullOrEmpty(second))
            return first;

        var firstEndsWith = first[^1] == separator;
        var secondStartsWith = second[0] == separator;

        if (firstEndsWith && !secondStartsWith)
            return string.Concat(first, second);

        if (!firstEndsWith && secondStartsWith)
            return string.Concat(first, second);

        if (firstEndsWith && secondStartsWith)
            return string.Concat(first, second[1..]);

        return $"{first}{separator}{second}";
    }

    /// <summary>
    /// Converts a NameIdentifier and spaces it out into words "Name Identifier".
    /// </summary>
    /// <param name="text">The text value to convert.</param>
    /// <returns>The text converted</returns>
    [return: NotNullIfNotNull(nameof(text))]
    public static string? ToTitle(this string? text)
    {
        if (text.IsNullOrEmpty() || text.Length < 2)
            return text;

        var words = WordsExpression().Matches(text);

        var wrote = false;
        var builder = new DefaultInterpolatedStringHandler(literalLength: text.Length + 5, formattedCount: 1);
        foreach (Match word in words)
        {
            if (wrote)
                builder.AppendLiteral(" ");

            builder.AppendLiteral(word.Value);
            wrote = true;
        }

        return builder.ToStringAndClear();
    }
}
