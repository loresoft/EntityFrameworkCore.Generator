using YamlDotNet.Serialization;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Match options
/// </summary>
public class MatchModel
{
    /// <summary>
    /// Gets or sets the exact string match option.
    /// </summary>
    /// <value>
    /// The exact string match option.
    /// </value>
    [YamlMember(Alias = "exact")]
    public string Exact { get; set; }

    /// <summary>
    /// Gets or sets the regular expression pattern match option.
    /// </summary>
    /// <value>
    /// The regular expression pattern match option.
    /// </value>
    [YamlMember(Alias = "regex")]
    public string Expression { get; set; }

    /// <summary>
    /// Performs an implicit conversion from <see cref="string"/> to <see cref="MatchModel"/>.
    /// </summary>
    /// <param name="value">The value to use for conversion.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator MatchModel(string value)
    {
        return new MatchModel
        {
            Expression = value
        };
    }
}
