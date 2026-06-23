using EntityFrameworkCore.Generator.Migrator.Extensions;
using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010103)]
public class CreateRoleTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateRoleTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;
    public string DateTimeOffsetType => _providerDefault.DateTimeOffsetType;
    public bool SupportIdentity => _providerDefault.SupportIdentity;

    public override void Up()
    {
        Create.Table("Role")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsInt32()
                .IdentityIf(SupportIdentity)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name")
                .AsString(256)
                .NotNullable()

            .WithColumn("Description")
                .AsString(int.MaxValue)
                .Nullable()

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

        Create.Index("UX_Role_Name")
            .OnTable("Role")
            .InSchema(DefaultSchema)
            .OnColumn("Name").Ascending()
            .WithOptions().Unique();
    }

    public override void Down()
    {
        Delete.Table("Role").InSchema(DefaultSchema);
    }
}
