using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010209)]
public class CreateOrderDocumentTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateOrderDocumentTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("OrderDocument")
            .InSchema(DefaultSchema)

            .WithColumn("OrderID")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("DocumentID")
                .AsInt32()
                .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("OrderDocument").InSchema(DefaultSchema);
    }
}
