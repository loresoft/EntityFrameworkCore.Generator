using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010102)]
public class CreatePriorityTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreatePriorityTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;
    public string DateTimeOffsetType => _providerDefault.DateTimeOffsetType;

    public override void Up()
    {
        Create.Table("Priority")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name")
                .AsString(100)
                .NotNullable()

            .WithColumn("Description")
                .AsString(255)
                .Nullable()

            .WithColumn("DisplayOrder")
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0)

            .WithColumn("IsActive")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(true)

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
        Delete.Table("Priority").InSchema(DefaultSchema);
    }
}
