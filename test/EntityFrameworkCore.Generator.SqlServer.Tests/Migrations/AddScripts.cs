using FluentMigrator;

namespace EntityFrameworkCore.Generator.SqlServer.Tests.Migrations;

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
            if (!name.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
                continue;

            Execute.EmbeddedScript(name);
        }
    }

    public override void Down()
    {

    }
}
