using FluentMigrator;

namespace EntityFrameworkCore.Generator.MySql.Tests.Migrations;

[Migration(2026020103)]
public class AddScripts : Migration
{
    public override void Up()
    {
        var assembly = GetType().Assembly;
        var names = assembly.GetManifestResourceNames();
        if (names.Length == 0 )
            return;

        foreach (var name in names)
        {
            if (!name.EndsWith(".mysql", StringComparison.OrdinalIgnoreCase))
                continue;

            Execute.EmbeddedScript(name);
        }

        Create.Table("CharacterSetType")
            .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
            .WithColumn("UnicodeText")
                .AsCustom("varchar(50) CHARACTER SET utf8mb4")
                .Nullable()
            .WithColumn("AnsiText")
                .AsCustom("varchar(50) CHARACTER SET latin1")
                .Nullable();
    }

    public override void Down()
    {

    }
}
