using System.ComponentModel;

using EntityFrameworkCore.Generator.Options;

using Spectre.Console.Cli;

namespace EntityFrameworkCore.Generator;

public sealed class InitializeSettings : CommandSettings
{
    [CommandOption("-p|--provider <PROVIDER>")]
    [Description("Database provider to reverse engineer")]
    public DatabaseProviders? Provider { get; set; }

    [CommandOption("-c|--connection-string <CONNECTION_STRING>")]
    [Description("Database connection string to reverse engineer")]
    public string? ConnectionString { get; set; }

    [CommandOption("--id <USER_SECRETS_ID>")]
    [Description("The user secret ID to use")]
    public string? UserSecretsId { get; set; }

    [CommandOption("--name <CONNECTION_NAME>")]
    [Description("The user secret configuration name")]
    public string? ConnectionName { get; set; }

    [CommandOption("-d|--directory <DIRECTORY>")]
    [Description("The root working directory")]
    public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

    [CommandOption("-f|--file <FILE>")]
    [Description("The options file name")]
    public string OptionsFile { get; set; } = ConfigurationSerializer.OptionsFileName;
}
