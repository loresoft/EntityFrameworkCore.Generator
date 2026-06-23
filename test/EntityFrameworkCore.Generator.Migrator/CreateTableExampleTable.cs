using EntityFrameworkCore.Generator.Migrator.Extensions;
using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010204)]
public class CreateTableExampleTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateTableExampleTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;
    public bool SupportIdentity => _providerDefault.SupportIdentity;

    public override void Up()
    {
        Create.Table("Table Example")
            .InSchema(DefaultSchema)

            .WithColumn("Table Example ID")
                .AsInt32()
                .IdentityIf(SupportIdentity)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name Example")
                .AsAnsiString(50)
                .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Table Example").InSchema(DefaultSchema);
    }
}
