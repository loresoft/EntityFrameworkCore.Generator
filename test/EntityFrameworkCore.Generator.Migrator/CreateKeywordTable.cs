using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010212)]
public class CreateKeywordTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateKeywordTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("Keyword")
            .InSchema(DefaultSchema)

            .WithColumn("Private")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Public")
                .AsInt32()
                .Nullable()

            .WithColumn("Keyword")
                .AsString(50)
                .Nullable()

            .WithColumn("namespace")
                .AsString(50)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("Keyword").InSchema(DefaultSchema);
    }
}
