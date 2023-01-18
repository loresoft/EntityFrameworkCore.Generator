namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// The database to generate code for
/// </summary>
public enum DatabaseProviders
{
    /// <summary>
    /// The SQL server provider
    /// </summary>
    SqlServer,

    /// <summary>
    /// The PostgreSQL provider
    /// </summary>
    PostgreSQL,

    /// <summary>
    /// The MySQL provider
    /// </summary>
    MySQL,

    /// <summary>
    /// The sqlite provider
    /// </summary>
    Sqlite,

    /// <summary>
    /// The Oracle provider
    /// </summary>
    Oracle
}