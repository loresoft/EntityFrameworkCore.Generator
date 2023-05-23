using System;
using System.Reflection;

using DbUp;

using Microsoft.Extensions.Configuration;

namespace Tracker.Database;

public static class Program
{
    static int Main(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("Tracker");

        EnsureDatabase.For
            .SqlDatabase(connectionString);

        var upgradeEngine = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgradeEngine.PerformUpgrade();

        if (result.Successful)
            return 0;

        Console.Error.WriteLine($"Exception: '{result.Error}'");

        return 1;
    }
}
