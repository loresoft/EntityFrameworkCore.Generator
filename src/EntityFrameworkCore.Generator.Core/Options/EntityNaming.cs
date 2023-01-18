namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Entity class naming strategy
/// </summary>
public enum EntityNaming
{
    /// <summary>
    /// Use table name as entity name
    /// </summary>
    Preserve = 0,

    /// <summary>
    /// Use table name in plural form
    /// </summary>
    Plural = 1,

    /// <summary>
    /// Use table name in singular form
    /// </summary>
    Singular = 2
}