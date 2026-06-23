using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010208)]
public class CreateProviderTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateProviderTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("Provider")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name")
                .AsString(50)
                .NotNullable()

            .WithColumn("Type")
                .AsString(50)
                .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Provider").InSchema(DefaultSchema);
    }
}
