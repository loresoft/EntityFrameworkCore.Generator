using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator.Runner;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using Testcontainers.PostgreSql;

using XUnit.Hosting.Logging;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;

public class DatabaseFixture : TestApplicationFixture, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder("postgis/postgis:16-3.4")
        .WithDatabase("GeneratorTest")
        .Build();

    public async ValueTask InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public string? ConnectionString { get; set; }

    public string? ContainerConnection { get; set; }

    protected override void ConfigureApplication(HostApplicationBuilder builder)
    {
        base.ConfigureApplication(builder);

        builder.Logging.AddMemoryLogger();

        var services = builder.Services;

        // change database from container default
        string containerConnection = _postgreSqlContainer.GetConnectionString();
        var connectionBuilder = new Npgsql.NpgsqlConnectionStringBuilder(containerConnection)
        {
            Database = "GeneratorTest"
        };

        ConnectionString = connectionBuilder.ToString();
        ContainerConnection = containerConnection;

        // override connection string to use docker container
        var configurationData = new Dictionary<string, string?>
        {
            ["ConnectionStrings:GeneratorTest"] = ConnectionString,
            ["ConnectionStrings:ContainerConnection"] = ContainerConnection
        };
        builder.Configuration.AddInMemoryCollection(configurationData);

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(ConnectionString)
                .ScanIn(typeof(PostgreSqlDefault).Assembly, typeof(DatabaseFixture).Assembly)
                .For.All()
            );

        services
            .TryAddSingleton<IProviderDefault, PostgreSqlDefault>();

        services
            .AddHostedService<DatabaseInitializer>();
    }
}
