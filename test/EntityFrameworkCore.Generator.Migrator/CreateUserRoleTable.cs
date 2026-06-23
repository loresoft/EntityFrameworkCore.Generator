using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010110)]
public class CreateUserRoleTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateUserRoleTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;
    public bool SupportForeignKeys => _providerDefault.SupportForeignKeys;

    public override void Up()
    {
        Create.Table("UserRole")
            .InSchema(DefaultSchema)

            .WithColumn("UserId")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("RoleId")
                .AsInt32()
                .NotNullable()
                .PrimaryKey();

        if (!SupportForeignKeys)
            return;

        Create.ForeignKey("FK_UserRole_Role_RoleId")
            .FromTable("UserRole").InSchema(DefaultSchema).ForeignColumn("RoleId")
            .ToTable("Role").InSchema(DefaultSchema).PrimaryColumn("Id");

        Create.ForeignKey("FK_UserRole_User_UserId")
            .FromTable("UserRole").InSchema(DefaultSchema).ForeignColumn("UserId")
            .ToTable("User").InSchema(DefaultSchema).PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("UserRole").InSchema(DefaultSchema);
    }
}
