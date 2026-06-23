namespace EntityFrameworkCore.Generator.Migrator.Providers;

public class SqlServerDefault : IProviderDefault
{
    public string DefaultSchema { get; set; } = "dbo";
    public string RowVersionType { get; set; } = "ROWVERSION";
    public string DateTimeOffsetType { get; set; } = "DATETIMEOFFSET";

    public bool SupportTemporalTable { get; set; } = true;
    public bool SupportChangeTracking { get; set; } = true;
    public bool SupportSchema { get; set; } = true;
    public bool SupportSequences { get; set; } = true;
    public bool SupportForeignKeys { get; set; } = true;
    public bool SupportNonPrimaryKeyIdentity { get; set; } = true;
    public bool SupportIdentity { get; set; } = true;
}
