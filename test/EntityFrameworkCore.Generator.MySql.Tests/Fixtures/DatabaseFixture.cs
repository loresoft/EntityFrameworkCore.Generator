using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator.Runner;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using MySqlConnector;

using Testcontainers.MySql;

using XUnit.Hosting.Logging;

namespace EntityFrameworkCore.Generator.MySql.Tests.Fixtures;

public class DatabaseFixture : TestApplicationFixture, IAsyncLifetime
{
    private readonly MySqlContainer _mySqlContainer = new MySqlBuilder("mysql:8")
        .WithCommand("--log-bin-trust-function-creators=1")
        .Build();

    public async ValueTask InitializeAsync()
    {
        await _mySqlContainer.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _mySqlContainer.DisposeAsync();
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
        string containerConnection = _mySqlContainer.GetConnectionString();
        var connectionBuilder = new MySqlConnectionStringBuilder(containerConnection)
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
                .AddMySql8()
                .WithGlobalConnectionString(ConnectionString)
                .ScanIn(typeof(MySqlDefault).Assembly, typeof(DatabaseFixture).Assembly)
                .For.All()
            );

        services
            .TryAddSingleton<IProviderDefault, MySqlDefault>();

        services
            .AddHostedService<DatabaseInitializer>();
    }
}
