using EntityFrameworkCore.Generator.Migrator.Extensions;
using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010223)]
public class CreateDepartmentTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateDepartmentTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;
    public bool SupportForeignKeys => _providerDefault.SupportForeignKeys;
    public bool SupportIdentity => _providerDefault.SupportIdentity;

    public override void Up()
    {
        Create.Table("Department")
            .InSchema(DefaultSchema)

            .WithColumn("DepartmentId")
                .AsInt32()
                .IdentityIf(SupportIdentity)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name")
                .AsBinary(50)
                .NotNullable()

            .WithColumn("Description")
                .AsBinary(50)
                .Nullable()

            .WithColumn("CreatedBy")
                .AsInt32()
                .NotNullable()

            .WithColumn("UpdatedBy")
                .AsInt32()
                .NotNullable();

        if (!SupportForeignKeys)
            return;

        Create.ForeignKey("FK_Department_Employees_CreatedBy")
            .FromTable("Department").InSchema(DefaultSchema).ForeignColumn("CreatedBy")
            .ToTable("Employees").InSchema(DefaultSchema).PrimaryColumn("EmployeeId");

        Create.ForeignKey("FK_Department_Employees_UpdatedBy")
            .FromTable("Department").InSchema(DefaultSchema).ForeignColumn("UpdatedBy")
            .ToTable("Employees").InSchema(DefaultSchema).PrimaryColumn("EmployeeId");
    }

    public override void Down()
    {
        Delete.Table("Department").InSchema(DefaultSchema);
    }
}
