using System.ComponentModel;

using EntityFrameworkCore.Generator.Options;

using Spectre.Console.Cli;

namespace EntityFrameworkCore.Generator;

public sealed class GenerateSettings : CommandSettings
{
    [CommandOption("-p|--provider <PROVIDER>")]
    [Description("Database provider to reverse engineer")]
    public DatabaseProviders? Provider { get; set; }

    [CommandOption("-c|--connection-string <CONNECTION_STRING>")]
    [Description("Database connection string to reverse engineer")]
    public string? ConnectionString { get; set; }

    [CommandOption("--extensions")]
    [Description("Include query extensions in generation")]
    public bool? Extensions { get; set; }

    [CommandOption("--models")]
    [Description("Include view models in generation")]
    public bool? Models { get; set; }

    [CommandOption("--mapper")]
    [Description("Include object mapper in generation")]
    public bool? Mapper { get; set; }

    [CommandOption("--validator")]
    [Description("Include model validation in generation")]
    public bool? Validator { get; set; }

    [CommandOption("-d|--directory <DIRECTORY>")]
    [Description("The root working directory")]
    public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

    [CommandOption("-f|--file <FILE>")]
    [Description("The options file name")]
    public string OptionsFile { get; set; } = ConfigurationSerializer.OptionsFileName;
}
