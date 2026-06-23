using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010107)]
public class CreateTaskTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateTaskTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;
    public string DateTimeOffsetType => _providerDefault.DateTimeOffsetType;
    public bool SupportForeignKeys => _providerDefault.SupportForeignKeys;

    public override void Up()
    {
        Create.Table("Task")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsGuid()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("StatusId")
                .AsInt32()
                .NotNullable()

            .WithColumn("PriorityId")
                .AsInt32()
                .Nullable()

            .WithColumn("Title")
                .AsString(255)
                .NotNullable()

            .WithColumn("Description")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("StartDate")
                .AsCustom(DateTimeOffsetType)
                .Nullable()

            .WithColumn("DueDate")
                .AsCustom(DateTimeOffsetType)
                .Nullable()

            .WithColumn("CompleteDate")
                .AsCustom(DateTimeOffsetType)
                .Nullable()

            .WithColumn("AssignedId")
                .AsInt32()
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
            Create.ForeignKey("FK_Task_Priority_PriorityId")
                .FromTable("Task").InSchema(DefaultSchema).ForeignColumn("PriorityId")
                .ToTable("Priority").InSchema(DefaultSchema).PrimaryColumn("Id");

            Create.ForeignKey("FK_Task_Status_StatusId")
                .FromTable("Task").InSchema(DefaultSchema).ForeignColumn("StatusId")
                .ToTable("Status").InSchema(DefaultSchema).PrimaryColumn("Id");

            Create.ForeignKey("FK_Task_User_AssignedId")
                .FromTable("Task").InSchema(DefaultSchema).ForeignColumn("AssignedId")
                .ToTable("User").InSchema(DefaultSchema).PrimaryColumn("Id");
        }

        Create.Index("IX_Task_AssignedId")
            .OnTable("Task")
            .InSchema(DefaultSchema)
            .OnColumn("AssignedId").Ascending();

        Create.Index("IX_Task_PriorityId")
            .OnTable("Task")
            .InSchema(DefaultSchema)
            .OnColumn("PriorityId").Ascending();

        Create.Index("IX_Task_StatusId")
            .OnTable("Task")
            .InSchema(DefaultSchema)
            .OnColumn("StatusId").Ascending();
    }

    public override void Down()
    {
        Delete.Table("Task").InSchema(DefaultSchema);
    }
}
