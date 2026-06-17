using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010108)]
public class CreateTaskExtendedTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateTaskExtendedTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;
    public string DateTimeOffsetType => _providerDefault.DateTimeOffsetType;
    public bool SupportForeignKeys => _providerDefault.SupportForeignKeys;

    public override void Up()
    {
        Create.Table("TaskExtended")
            .InSchema(DefaultSchema)

            .WithColumn("TaskId")
                .AsGuid()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("UserAgent")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("Browser")
                .AsString(256)
                .Nullable()

            .WithColumn("OperatingSystem")
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

        if (!SupportForeignKeys)
            return;

        Create.ForeignKey("FK_TaskExtended_Task_TaskId")
            .FromTable("TaskExtended").InSchema(DefaultSchema).ForeignColumn("TaskId")
            .ToTable("Task").InSchema(DefaultSchema).PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("TaskExtended").InSchema(DefaultSchema);
    }
}
