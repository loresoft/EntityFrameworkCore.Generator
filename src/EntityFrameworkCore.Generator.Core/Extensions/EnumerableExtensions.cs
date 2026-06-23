namespace EntityFrameworkCore.Generator.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IEnumerable{T}"/> to assist with string formatting and conversion.
/// </summary>
public static partial class EnumerableExtensions
{
    /// <summary>
    /// Concatenates the members of a sequence, using the specified delimiter between each member, and returns the resulting string.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the sequence.
    /// </typeparam>
    /// <param name="values">
    /// The sequence of values to concatenate. Each value will be converted to a string using its <c>ToString()</c> method.
    /// </param>
    /// <param name="delimiter">
    /// The string to use as a delimiter. If <c>null</c>, a comma (",") is used by default.
    /// </param>
    /// <returns>
    /// A string that consists of the elements in <paramref name="values"/> delimited by the <paramref name="delimiter"/> string.
    /// If <paramref name="values"/> is empty, returns <see cref="string.Empty"/>.
    /// </returns>
    public static string ToDelimitedString<T>(this IEnumerable<T?> values, string? delimiter = ",")
        => string.Join(delimiter ?? ",", values);

    /// <summary>
    /// Concatenates the members of a sequence of strings, using the specified delimiter between each member, and returns the resulting string.
    /// </summary>
    /// <param name="values">
    /// The sequence of string values to concatenate. <c>null</c> values are treated as empty strings.
    /// </param>
    /// <param name="delimiter">
    /// The string to use as a delimiter. If <c>null</c>, a comma (",") is used by default.
    /// </param>
    /// <returns>
    /// A string that consists of the elements in <paramref name="values"/> delimited by the <paramref name="delimiter"/> string.
    /// If <paramref name="values"/> is empty, returns <see cref="string.Empty"/>.
    /// </returns>
    public static string ToDelimitedString(this IEnumerable<string?> values, string? delimiter = ",")
        => string.Join(delimiter ?? ",", values);
}
