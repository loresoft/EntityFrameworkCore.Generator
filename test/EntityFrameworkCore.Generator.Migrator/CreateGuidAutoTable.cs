using EntityFrameworkCore.Generator.Migrator.Extensions;
using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010213)]
public class CreateGuidAutoTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateGuidAutoTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;

    public bool SupportNonPrimaryKeyIdentity => _providerDefault.SupportNonPrimaryKeyIdentity;

    public override void Up()
    {
        Create.Table("GuidAuto")
            .InSchema(DefaultSchema)

            .WithColumn("GuidID")
                .AsGuid()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("AutoID")
                .AsInt32()
                .IdentityIf(SupportNonPrimaryKeyIdentity)
                .NotNullable()

            .WithColumn("Name")
                .AsAnsiString(50)
                .NotNullable()

            .WithColumn("Flag")
                .AsCustom(RowVersionType)
                .NotNullable();

        Create.Index("UX_GuidAuto_AutoID")
            .OnTable("GuidAuto")
            .InSchema(DefaultSchema)
            .OnColumn("AutoID").Ascending()
            .WithOptions().Unique();
    }

    public override void Down()
    {
        Delete.Table("GuidAuto").InSchema(DefaultSchema);
    }
}
