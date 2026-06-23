using FluentMigrator.Runner;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;

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

        using var connection = new Npgsql.NpgsqlConnection(connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            DO $$
            BEGIN
                IF NOT EXISTS (SELECT FROM pg_database WHERE datname = 'GeneratorTest') THEN
                    EXECUTE 'CREATE DATABASE "GeneratorTest"';
                END IF;
            END
            $$;
            """;

        command.ExecuteNonQuery();

        connection.Close();
    }
}
