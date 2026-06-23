using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010109)]
public class CreateUserLoginTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateUserLoginTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;
    public string DateTimeOffsetType => _providerDefault.DateTimeOffsetType;
    public bool SupportForeignKeys => _providerDefault.SupportForeignKeys;

    public override void Up()
    {
        Create.Table("UserLogin")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsGuid()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("EmailAddress")
                .AsString(256)
                .NotNullable()

            .WithColumn("UserId")
                .AsInt32()
                .Nullable()

            .WithColumn("UserAgent")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("Browser")
                .AsString(256)
                .Nullable()

            .WithColumn("OperatingSystem")
                .AsString(256)
                .Nullable()

            .WithColumn("DeviceFamily")
                .AsString(256)
                .Nullable()

            .WithColumn("DeviceBrand")
                .AsString(256)
                .Nullable()

            .WithColumn("DeviceModel")
                .AsString(256)
                .Nullable()

            .WithColumn("IpAddress")
                .AsString(50)
                .Nullable()

            .WithColumn("IsSuccessful")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false)

            .WithColumn("FailureMessage")
                .AsString(256)
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

        if (SupportForeignKeys)
        {
            Create.ForeignKey("FK_UserLogin_User_UserId")
                .FromTable("UserLogin").InSchema(DefaultSchema).ForeignColumn("UserId")
                .ToTable("User").InSchema(DefaultSchema).PrimaryColumn("Id");
        }

        Create.Index("IX_UserLogin_EmailAddress")
            .OnTable("UserLogin")
            .InSchema(DefaultSchema)
            .OnColumn("EmailAddress").Ascending();

        Create.Index("IX_UserLogin_UserId")
            .OnTable("UserLogin")
            .InSchema(DefaultSchema)
            .OnColumn("UserId").Ascending();
    }

    public override void Down()
    {
        Delete.Table("UserLogin").InSchema(DefaultSchema);
    }
}
