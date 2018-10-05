using System;
using System.IO;
using System.Reflection;
using EntityFrameworkCore.Generator.Options;
using McMaster.Extensions.CommandLineUtils;
using Serilog;
using Serilog.Events;

namespace EntityFrameworkCore.Generator
{
    [Command("efg", Description = "Entity Framework Core model generation tool")]
    [VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
    [Subcommand("initialize", typeof(InitializeCommand))]
    [Subcommand("generate", typeof(GenerateCommand))]
    public class Program : CommandBase
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting host");
                
                return CommandLineApplication.Execute<Program>(args);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }

        private static string GetVersion()
        {
            var type = typeof(Program);
            var versionAttribute = type.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            return versionAttribute.InformationalVersion;
        }

        protected override int OnExecute(CommandLineApplication application)
        {
            application.ShowHelp();
            return 1;
        }
    }
}
