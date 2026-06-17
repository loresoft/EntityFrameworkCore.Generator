using FluentMigrator.Runner;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.SqlServer.Tests.Fixtures;

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

        // enable change tracking
        _migrationRunner.Processor.Execute("""
            IF NOT EXISTS (
                SELECT 1
                FROM sys.change_tracking_databases
                WHERE database_id = DB_ID()
            )
            BEGIN
                ALTER DATABASE CURRENT
                SET CHANGE_TRACKING = ON
                (CHANGE_RETENTION = 7 DAYS, AUTO_CLEANUP = ON);
            END;
            """);

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

        // connect to master database to create target database if it doesn't exist
        var connectionString = _configuration.GetConnectionString("ContainerConnection");

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'GeneratorTest')
            BEGIN
                CREATE DATABASE [GeneratorTest];
            END;
            """;

        command.ExecuteNonQuery();

        connection.Close();
    }
}
