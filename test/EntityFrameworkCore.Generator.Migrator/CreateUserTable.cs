using EntityFrameworkCore.Generator.Migrator.Extensions;
using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010105)]
public class CreateUserTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateUserTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;
    public string DateTimeOffsetType => _providerDefault.DateTimeOffsetType;
    public bool SupportIdentity => _providerDefault.SupportIdentity;

    public override void Up()
    {
        Create.Table("User")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsInt32()
                .IdentityIf(SupportIdentity)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("UserName")
                .AsAnsiString(50)
                .NotNullable()

            .WithColumn("EmailAddress")
                .AsString(256)
                .NotNullable()

            .WithColumn("IsEmailAddressConfirmed")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false)

            .WithColumn("DisplayName")
                .AsString(256)
                .NotNullable()

            .WithColumn("FirstName")
                .AsString(256)
                .Nullable()

            .WithColumn("LastName")
                .AsString(256)
                .Nullable()

            .WithColumn("PasswordHash")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("ResetHash")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("InviteHash")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("AccessFailedCount")
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0)

            .WithColumn("LockoutEnabled")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false)

            .WithColumn("LockoutEnd")
                .AsCustom(DateTimeOffsetType)
                .Nullable()

            .WithColumn("LastLogin")
                .AsCustom(DateTimeOffsetType)
                .Nullable()

            .WithColumn("IsDeleted")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false)

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

        Create.Index("UX_User_EmailAddress")
            .OnTable("User")
            .InSchema(DefaultSchema)
            .OnColumn("EmailAddress").Ascending()
            .WithOptions().Unique();

        IfDatabase("SqlServer").Execute
            .Sql($"ALTER TABLE [{DefaultSchema}].[User] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);");

    }

    public override void Down()
    {
        Delete.Table("User").InSchema(DefaultSchema);
    }
}
