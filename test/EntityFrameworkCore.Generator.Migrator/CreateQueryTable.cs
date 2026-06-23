using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010207)]
public class CreateQueryTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateQueryTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("Query")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name")
                .AsString(50)
                .NotNullable()

            .WithColumn("Query")
                .AsString(int.MaxValue)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("Query").InSchema(DefaultSchema);
    }
}
