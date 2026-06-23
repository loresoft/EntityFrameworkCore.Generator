using EntityFrameworkCore.Generator.Migrator.Providers;
using EntityFrameworkCore.Generator.Sqlite.Tests.Migrations;

using FluentMigrator.Runner;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using XUnit.Hosting.Logging;

namespace EntityFrameworkCore.Generator.Sqlite.Tests.Fixtures;

public class DatabaseFixture : TestApplicationFixture
{
    public string? ConnectionString { get; set; }

    protected override void ConfigureApplication(HostApplicationBuilder builder)
    {
        base.ConfigureApplication(builder);

        builder.Logging.AddMemoryLogger();

        var services = builder.Services;

        ConnectionString = "Data Source=GeneratorTest.db";
        var configurationData = new Dictionary<string, string?>
        {
            ["ConnectionStrings:GeneratorTest"] = ConnectionString,
        };
        builder.Configuration.AddInMemoryCollection(configurationData);

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(ConnectionString)
                .ScanIn(typeof(SqliteDefault).Assembly, typeof(CreateSpatialDataTable).Assembly)
                .For.All()
            );

        services
            .TryAddSingleton<IProviderDefault, SqliteDefault>();

        services
            .AddHostedService<DatabaseInitializer>();
    }
}
