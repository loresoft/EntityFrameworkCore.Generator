using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010215)]
public class CreateUglyDuplicateTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateUglyDuplicateTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public override void Up()
    {
        if (!_providerDefault.SupportSchema)
            return;

        Create
            .Schema("Ugly");

        Create
            .Table("Duplicate")
            .InSchema("Ugly")

            .WithColumn("DuplicateID")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name")
                .AsString(50)
                .NotNullable();
    }

    public override void Down()
    {
        if (!_providerDefault.SupportSchema)
            return;

        Delete.Table("Duplicate").InSchema("Ugly");
        Delete.Schema("Ugly");
    }
}
