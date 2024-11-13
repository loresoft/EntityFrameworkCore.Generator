using System;
using System.IO;
using System.Reflection;
using System.Text;

using DbUp;
using DbUp.Engine.Output;

using Microsoft.Extensions.Configuration;

using Xunit.Abstractions;

namespace FluentCommand.SqlServer.Tests;

public class DatabaseFixture : IUpgradeLog, IDisposable
{
    private readonly StringBuilder _buffer;
    private readonly StringWriter _logger;

    public DatabaseFixture()
    {
        _buffer = new StringBuilder();
        _logger = new StringWriter(_buffer);

        ResolveConnectionString();

        CreateDatabase();
    }


    public string ConnectionString { get; set; }

    public string ConnectionName { get; set; } = "Tracker";


    private void CreateDatabase()
    {
        EnsureDatabase.For
            .SqlDatabase(ConnectionString, this);

        var upgradeEngine = DeployChanges.To
            .SqlDatabase(ConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogTo(this)
            .Build();

        var result = upgradeEngine.PerformUpgrade();

        if (result.Successful)
            return;

        _logger.WriteLine($"Exception: '{result.Error}'");

        throw result.Error;
    }

    private void ResolveConnectionString()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Test";
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString(ConnectionName);

        ConnectionString = connectionString;
    }


    public void Report(ITestOutputHelper output)
    {
        if (_buffer.Length == 0)
            return;

        _logger.Flush();
        output.WriteLine(_logger.ToString());

        // reset logger
        _buffer.Clear();
    }

    public void Dispose()
    {

    }


    public void LogDebug(string format, params object[] args)
    {
        _logger.Write("DEBUG : ");
        _logger.WriteLine(format, args);
    }

    public void LogError(string format, params object[] args)
    {
        _logger.Write("ERROR : ");
        _logger.WriteLine(format, args);
    }

    public void LogError(Exception ex, string format, params object[] args)
    {
        _logger.Write("ERROR : ");
        _logger.WriteLine(format, args);
        _logger.WriteLine(ex.ToString());
    }

    public void LogInformation(string format, params object[] args)
    {
        _logger.Write("INFO : ");
        _logger.WriteLine(format, args);
    }

    public void LogTrace(string format, params object[] args)
    {
        _logger.Write("TRACE : ");
        _logger.WriteLine(format, args);
    }

    public void LogWarning(string format, params object[] args)
    {
        _logger.Write("WARN : ");
        _logger.WriteLine(format, args);
    }

}
