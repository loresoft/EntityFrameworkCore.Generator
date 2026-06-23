using EntityFrameworkCore.Generator.Migrator.Extensions;
using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010101)]
public class CreateAuditTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateAuditTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;
    public string DateTimeOffsetType => _providerDefault.DateTimeOffsetType;
    public bool SupportIdentity => _providerDefault.SupportIdentity;

    public override void Up()
    {
        Create.Table("Audit")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsInt32()
                .IdentityIf(SupportIdentity)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Date")
                .AsDateTime()
                .NotNullable()

            .WithColumn("UserId")
                .AsInt32()
                .Nullable()

            .WithColumn("TaskId")
                .AsInt32()
                .Nullable()

            .WithColumn("Content")
                .AsString(int.MaxValue)
                .NotNullable()
            .WithColumn("Username")
                .AsString(50)
                .NotNullable()

            .WithColumn("Created")
                .AsCustom(DateTimeOffsetType)
                .NotNullable()
                .WithDefault(SystemMethods.CurrentUTCDateTime)

            .WithColumn("CreatedBy")
                .AsString(100)
                .Nullable()

            .WithColumn("Updated")
                .AsCustom(DateTimeOffsetType)
                .NotNullable()
                .WithDefault(SystemMethods.CurrentUTCDateTime)

            .WithColumn("UpdatedBy")
                .AsString(100)
                .Nullable()

            .WithColumn("RowVersion")
                .AsCustom(RowVersionType)
                .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Audit").InSchema(DefaultSchema);
    }
}
