namespace EntityFrameworkCore.Generator.Migrator.Providers;

public class MySqlDefault : IProviderDefault
{
    public string DefaultSchema { get; set; } = string.Empty;
    public string RowVersionType { get; set; } = "TIMESTAMP";
    public string DateTimeOffsetType { get; set; } = "DATETIME";

    public bool SupportTemporalTable { get; set; } = false;
    public bool SupportChangeTracking { get; set; } = false;
    public bool SupportSchema { get; set; } = false;
    public bool SupportSequences { get; set; } = false;
    public bool SupportForeignKeys { get; set; } = true;
    public bool SupportNonPrimaryKeyIdentity { get; set; } = false;
    public bool SupportIdentity { get; set; } = true;
}
