using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010224)]
public class CreateEmployeeDepartmentTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateEmployeeDepartmentTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;
    public bool SupportForeignKeys => _providerDefault.SupportForeignKeys;

    public override void Up()
    {
        Create.Table("EmployeeDepartment")
            .InSchema(DefaultSchema)

            .WithColumn("DepartmentId")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("EmployeeId")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("CreatedBy")
                .AsInt32()
                .NotNullable()

            .WithColumn("UpdatedBy")
                .AsInt32()
                .NotNullable();

        if (!SupportForeignKeys)
            return;

        Create.ForeignKey("FK_EmployeeDepartment_Department_DepartmentId")
            .FromTable("EmployeeDepartment").InSchema(DefaultSchema).ForeignColumn("DepartmentId")
            .ToTable("Department").InSchema(DefaultSchema).PrimaryColumn("DepartmentId");

        Create.ForeignKey("FK_EmployeeDepartment_Employees_EmployeeId")
            .FromTable("EmployeeDepartment").InSchema(DefaultSchema).ForeignColumn("EmployeeId")
            .ToTable("Employees").InSchema(DefaultSchema).PrimaryColumn("EmployeeId");

        Create.ForeignKey("FK_EmployeeDepartment_Employees_CreatedBy")
            .FromTable("EmployeeDepartment").InSchema(DefaultSchema).ForeignColumn("CreatedBy")
            .ToTable("Employees").InSchema(DefaultSchema).PrimaryColumn("EmployeeId");

        Create.ForeignKey("FK_EmployeeDepartment_Employees_UpdatedBy")
            .FromTable("EmployeeDepartment").InSchema(DefaultSchema).ForeignColumn("UpdatedBy")
            .ToTable("Employees").InSchema(DefaultSchema).PrimaryColumn("EmployeeId");
    }

    public override void Down()
    {
        Delete.Table("EmployeeDepartment").InSchema(DefaultSchema);
    }
}
