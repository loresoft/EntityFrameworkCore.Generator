using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010210)]
public class CreateObjectTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateObjectTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("Object")
            .InSchema(DefaultSchema)

            .WithColumn("ObjectId")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name")
                .AsAnsiString(50)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("Object").InSchema(DefaultSchema);
    }
}
