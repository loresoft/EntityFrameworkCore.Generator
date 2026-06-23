using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010219)]
public class CreateContactImportTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateContactImportTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("ContactImport")
            .InSchema(DefaultSchema)

            .WithColumn("FirstName")
                .AsAnsiString(50)
                .Nullable()

            .WithColumn("LastName")
                .AsAnsiString(50)
                .Nullable()

            .WithColumn("Address1")
                .AsAnsiString(150)
                .Nullable()

            .WithColumn("Address2")
                .AsAnsiString(150)
                .Nullable()

            .WithColumn("City")
                .AsAnsiString(50)
                .Nullable()

            .WithColumn("State")
                .AsAnsiString(2)
                .Nullable()

            .WithColumn("Zip")
                .AsAnsiString(10)
                .Nullable()

            .WithColumn("Email")
                .AsAnsiString(150)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("ContactImport").InSchema(DefaultSchema);
    }
}
