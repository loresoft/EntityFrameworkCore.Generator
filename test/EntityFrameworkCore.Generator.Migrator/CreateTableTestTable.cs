using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010112)]
public class CreateTableTestTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateTableTestTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("Table1 $ Test")
            .InSchema(DefaultSchema)

            .WithColumn("Test$")
                .AsFixedLengthAnsiString(10)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Blah #")
                .AsFixedLengthAnsiString(10)
                .Nullable()

            .WithColumn("Table Example ID")
                .AsInt32()
                .Nullable()

            .WithColumn("TableExampleObject")
                .AsInt32()
                .Nullable()

            .WithColumn("1stNumber")
                .AsString(50)
                .Nullable()

            .WithColumn("123Street")
                .AsString(50)
                .Nullable()

            .WithColumn("123 Test 123")
                .AsString(50)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("Table1 $ Test").InSchema(DefaultSchema);
    }
}
