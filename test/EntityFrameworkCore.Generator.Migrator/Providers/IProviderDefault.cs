namespace EntityFrameworkCore.Generator.Migrator.Providers;

public interface IProviderDefault
{
    string DefaultSchema { get; set; }

    string RowVersionType { get; set; }

    string DateTimeOffsetType { get; set; }

    bool SupportSchema { get; set; }

    bool SupportTemporalTable { get; set; }

    bool SupportChangeTracking { get; set; }

    bool SupportSequences { get; set; }

    bool SupportForeignKeys { get; set; }

    bool SupportIdentity { get; set; }

    bool SupportNonPrimaryKeyIdentity { get; set; }
}
