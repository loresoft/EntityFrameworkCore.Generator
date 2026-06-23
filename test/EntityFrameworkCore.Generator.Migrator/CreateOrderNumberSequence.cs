using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010307)]
public class CreateOrderNumberSequence : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateOrderNumberSequence(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        if (!_providerDefault.SupportSequences)
            return;

        Create.Sequence("OrderNumberSequence")
            .InSchema(DefaultSchema)
            .StartWith(1000)
            .IncrementBy(1);
    }

    public override void Down()
    {
        if (!_providerDefault.SupportSequences)
            return;

        Delete.Sequence("OrderNumberSequence").InSchema(DefaultSchema);
    }
}
