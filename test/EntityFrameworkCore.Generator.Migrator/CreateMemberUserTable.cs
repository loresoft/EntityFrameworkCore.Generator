using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010106)]
public class CreateMemberUserTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateMemberUserTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("member_user")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsGuid()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("email_address")
                .AsString(256)
                .NotNullable()

            .WithColumn("display_name")
                .AsString(256)
                .NotNullable()

            .WithColumn("first_name")
                .AsString(256)
                .Nullable()

            .WithColumn("last_name")
                .AsString(256)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("member_user").InSchema(DefaultSchema);
    }
}
