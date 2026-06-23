using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010218)]
public class CreateTwoKeyTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateTwoKeyTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("TwoKey")
            .InSchema(DefaultSchema)

            .WithColumn("FirstName")
                .AsString(50)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("LastName")
                .AsString(50)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("EmailAddress")
                .AsString(50)
                .Nullable()

            .WithColumn("Phone")
                .AsString(50)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("TwoKey").InSchema(DefaultSchema);
    }
}
