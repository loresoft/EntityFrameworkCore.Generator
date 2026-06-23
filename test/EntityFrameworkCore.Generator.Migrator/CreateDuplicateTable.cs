using EntityFrameworkCore.Generator.Migrator.Extensions;
using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010216)]
public class CreateDuplicateTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateDuplicateTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;
    public bool SupportIdentity => _providerDefault.SupportIdentity;

    public override void Up()
    {
        Create.Table("Duplicate")
            .InSchema(DefaultSchema)

            .WithColumn("DuplicateID")
                .AsInt32()
                .IdentityIf(SupportIdentity)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("DuplicateName")
                .AsAnsiString(50)
                .Nullable()

            .WithColumn("Duplicate_Name")
                .AsAnsiString(50)
                .Nullable()

            .WithColumn("Duplicate")
                .AsAnsiString(50)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("Duplicate").InSchema(DefaultSchema);
    }
}
