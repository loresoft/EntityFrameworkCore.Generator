using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010222)]
public class CreateTemplateDependTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateTemplateDependTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;
    public bool SupportForeignKeys => _providerDefault.SupportForeignKeys;

    public override void Up()
    {
        Create.Table("TemplateDepend")
            .InSchema(DefaultSchema)

            .WithColumn("LinkID")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("DependID")
                .AsInt32()
                .NotNullable()
                .PrimaryKey();

        if (!SupportForeignKeys)
            return;

        Create.ForeignKey("FK_TemplateDepend_Template_LinkID")
            .FromTable("TemplateDepend").InSchema(DefaultSchema).ForeignColumn("LinkID")
            .ToTable("Template").InSchema(DefaultSchema).PrimaryColumn("TemplateID");

        Create.ForeignKey("FK_TemplateDepend_Template_DependID")
            .FromTable("TemplateDepend").InSchema(DefaultSchema).ForeignColumn("DependID")
            .ToTable("Template").InSchema(DefaultSchema).PrimaryColumn("TemplateID");
    }

    public override void Down()
    {
        Delete.Table("TemplateDepend").InSchema(DefaultSchema);
    }
}
