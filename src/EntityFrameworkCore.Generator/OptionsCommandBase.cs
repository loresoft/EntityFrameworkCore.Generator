using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator;

public abstract class OptionsCommandBase : CommandBase
{
    protected OptionsCommandBase(ILoggerFactory logger, IConsole console, IConfigurationSerializer serializer)
        : base(logger, console)
    {
        Serializer = serializer;
    }

    protected IConfigurationSerializer Serializer { get; }

    [Option("-d <directory>", Description = "The root working directory")]
    public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

    [Option("-f <file>", Description = "The options file name")]
    public string OptionsFile { get; set; } = ConfigurationSerializer.OptionsFileName;
}