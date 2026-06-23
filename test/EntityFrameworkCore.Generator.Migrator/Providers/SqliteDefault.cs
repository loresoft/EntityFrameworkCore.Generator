namespace EntityFrameworkCore.Generator.Migrator.Providers;

public class SqliteDefault : IProviderDefault
{
    public string DefaultSchema { get; set; } = string.Empty;
    public string RowVersionType { get; set; } = "INTEGER";
    public string DateTimeOffsetType { get; set; } = "DATETIME";

    public bool SupportTemporalTable { get; set; } = false;
    public bool SupportChangeTracking { get; set; } = false;
    public bool SupportSchema { get; set; } = false;
    public bool SupportSequences { get; set; } = false;
    public bool SupportForeignKeys { get; set; } = false;
    public bool SupportNonPrimaryKeyIdentity { get; set; } = true;
    public bool SupportIdentity { get; set; } = true;
}
