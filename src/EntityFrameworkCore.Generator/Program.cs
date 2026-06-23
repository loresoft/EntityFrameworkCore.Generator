using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using Spectre.Console.Cli;

namespace EntityFrameworkCore.Generator;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            var builder = Host.CreateApplicationBuilder(args);
            ConfigureServices(builder);

            var host = builder.Build();
            var app = ConfigureCommands(host);

            return await app.RunAsync(args);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Host terminated unexpectedly: {ex.Message}");
            Console.Error.WriteLine(ex.ToString());
            return 1;
        }
    }

    private static void ConfigureServices(HostApplicationBuilder builder)
    {
        builder.Logging.SetMinimumLevel(LogLevel.Trace);

        builder.Logging.ClearProviders();

        builder.Logging
           .AddConsole(options => options.FormatterName = ColorConsoleFormatter.FormatterName)
           .AddConsoleFormatter<ColorConsoleFormatter, SimpleConsoleFormatterOptions>(options =>
           {
               options.ColorBehavior = LoggerColorBehavior.Enabled;
               options.SingleLine = true;
               options.TimestampFormat = null;
               options.IncludeScopes = false;
           });

        builder.Services
            .AddTransient<IConfigurationSerializer, ConfigurationSerializer>()
            .AddTransient<ICodeGenerator, CodeGenerator>()
            .AddTransient<GenerateCommand>()
            .AddTransient<InitializeCommand>();
    }

    private static CommandApp ConfigureCommands(IHost host)
    {
        // Create a type registrar that uses our DI container
        var registrar = new TypeRegistrar(host.Services);
        var app = new CommandApp(registrar);

        app.Configure(config =>
        {
            config.SetApplicationName("efg");
            config.SetApplicationVersion(ThisAssembly.InformationalVersion);

            config.AddCommand<InitializeCommand>("initialize")
                .WithAlias("init")
                .WithDescription("Create the configuration file and optionally set the connection string.");

            config.AddCommand<GenerateCommand>("generate")
                .WithAlias("gen")
                .WithDescription("Generate source code files from a database schema.");
        });

        return app;
    }
}
