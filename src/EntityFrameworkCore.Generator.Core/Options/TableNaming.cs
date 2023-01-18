namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Table naming hint for how database tables are named.
/// </summary>
public enum TableNaming
{
    /// <summary>
    /// Mix of Plural and Singular
    /// </summary>
    Mixed = 0,

    /// <summary>
    /// Tables are named in plural form
    /// </summary>
    Plural = 1,

    /// <summary>
    /// Tables are named in singular form
    /// </summary>
    Singular = 2
}