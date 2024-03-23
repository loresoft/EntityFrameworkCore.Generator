using System.Text;

namespace EntityFrameworkCore.Generator.Extensions;

public static class EnumerableExtensions
{
    public static string ToDelimitedString<T>(this IEnumerable<T> values)
    {
        return values.ToDelimitedString(",");
    }

    public static string ToDelimitedString<T>(this IEnumerable<T> values, string delimiter)
    {
        if (values is null)
            return null;

        var sb = new StringBuilder();
        foreach (var i in values)
        {
            if (sb.Length > 0)
                sb.Append(delimiter ?? ",");
            sb.Append(i.ToString());
        }

        return sb.ToString();
    }

    public static string ToDelimitedString(this IEnumerable<string> values)
    {
        return values.ToDelimitedString(",");
    }

    public static string ToDelimitedString(this IEnumerable<string> values, string delimiter)
    {
        return values.ToDelimitedString(delimiter, null);
    }

    public static string ToDelimitedString(this IEnumerable<string> values, string delimiter, Func<string, string> escapeDelimiter)
    {
        if (values is null)
            return null;

        var sb = new StringBuilder();
        foreach (var value in values)
        {
            if (sb.Length > 0)
                sb.Append(delimiter);

            var v = escapeDelimiter != null
                ? escapeDelimiter(value ?? string.Empty)
                : value ?? string.Empty;

            sb.Append(v);
        }

        return sb.ToString();
    }
}
