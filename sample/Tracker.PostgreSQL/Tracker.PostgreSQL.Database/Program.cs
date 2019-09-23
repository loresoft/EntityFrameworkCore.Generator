using System;
using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;

namespace Tracker.PostgreSQL.Database
{
    class Program
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
                .PostgresqlDatabase(connectionString);

            var upgradeEngine = DeployChanges.To
                .PostgresqlDatabase(connectionString)
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
}
