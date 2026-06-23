using System.Text.RegularExpressions;

using EntityFrameworkCore.Generator.Extensions;

using YamlDotNet.Serialization;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// String match options
/// </summary>
public class MatchOptions : OptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MatchOptions"/> class.
    /// </summary>
    /// <param name="variables">The shared variables dictionary.</param>
    /// <param name="prefix">The variable key prefix.</param>
    public MatchOptions(VariableDictionary variables, string? prefix)
        : base(variables, prefix)
    {
    }

    /// <summary>
    /// Gets or sets the exact string match option.
    /// </summary>
    /// <value>
    /// The exact string match option.
    /// </value>
    [YamlMember(Alias = "exact")]
    public string? Exact
    {
        get => GetProperty();
        set => SetProperty(value);
    }

    /// <summary>
    /// Gets or sets the regular expression pattern match option.
    /// </summary>
    /// <value>
    /// The regular expression pattern match option.
    /// </value>
    [YamlMember(Alias = "regex")]
    public string? Expression
    {
        get => GetProperty();
        set => SetProperty(value);
    }

    /// <summary>
    /// Determines whether the specified value is a match.
    /// </summary>
    /// <param name="value">The value to match.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is a match; otherwise, <c>false</c>.
    /// </returns>
    public bool IsMatch(string value)
    {
        if (Exact.HasValue())
            return string.Equals(value, Exact);

        if (Expression.HasValue())
            return Regex.IsMatch(value, Expression);

        return false;
    }
}
