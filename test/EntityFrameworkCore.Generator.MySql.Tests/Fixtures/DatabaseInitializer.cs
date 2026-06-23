using FluentMigrator.Runner;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MySqlConnector;

namespace EntityFrameworkCore.Generator.MySql.Tests.Fixtures;

public class DatabaseInitializer : IHostedService
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IMigrationRunner _migrationRunner;
    private readonly IConfiguration _configuration;

    public DatabaseInitializer(
        ILogger<DatabaseInitializer> logger,
        IMigrationRunner migrationRunner,
        IConfiguration configuration)
    {
        _logger = logger;
        _migrationRunner = migrationRunner;
        _configuration = configuration;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Starting database migration for {DatabaseType}...",
            _migrationRunner.Processor.DatabaseType);

        // ensure database exists before running migrations
        EnsureDatabase();

        // run migrations
        _migrationRunner.MigrateUp();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }


    private void EnsureDatabase()
    {
        _logger.LogInformation("Ensuring target database 'GeneratorTest' exists...");

        // connect to database to create target database if it doesn't exist
        var connectionString = _configuration.GetConnectionString("ContainerConnection");

        var connectionBuilder = new MySqlConnectionStringBuilder(connectionString!);
        var userId = EscapeMySqlString(connectionBuilder.UserID);
        connectionBuilder.UserID = "root";

        using var connection = new MySqlConnection(connectionBuilder.ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "CREATE DATABASE IF NOT EXISTS `GeneratorTest`;";

        command.ExecuteNonQuery();

        command.CommandText = $"GRANT ALL PRIVILEGES ON `GeneratorTest`.* TO '{userId}'@'%';";

        command.ExecuteNonQuery();

        connection.Close();
    }

    private static string EscapeMySqlString(string value)
    {
        return value.Replace("'", "''", StringComparison.Ordinal);
    }
}
