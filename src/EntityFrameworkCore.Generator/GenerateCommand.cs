using System;

using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Options;

using McMaster.Extensions.CommandLineUtils;

using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator;

[Command("generate", "gen")]
public class GenerateCommand : OptionsCommandBase
{
    private readonly ICodeGenerator _codeGenerator;

    public GenerateCommand(ILoggerFactory logger, IConsole console, IConfigurationSerializer serializer, ICodeGenerator codeGenerator)
        : base(logger, console, serializer)
    {
        _codeGenerator = codeGenerator;
    }

    [Option("-p <Provider>", Description = "Database provider to reverse engineer")]
    public DatabaseProviders? Provider { get; set; }

    [Option("-c <ConnectionString>", Description = "Database connection string to reverse engineer")]
    public string? ConnectionString { get; set; }


    [Option("--extensions", Description = "Include query extensions in generation")]
    public bool? Extensions { get; set; }

    [Option("--models", Description = "Include view models in generation")]
    public bool? Models { get; set; }

    [Option("--mapper", Description = "Include object mapper in generation")]
    public bool? Mapper { get; set; }

    [Option("--validator", Description = "Include model validation in generation")]
    public bool? Validator { get; set; }


    protected override int OnExecute(CommandLineApplication application)
    {
        var workingDirectory = WorkingDirectory ?? Environment.CurrentDirectory;
        var configurationFile = OptionsFile ?? ConfigurationSerializer.OptionsFileName;

        var configuration = Serializer.Load(workingDirectory, configurationFile);
        if (configuration == null)
        {
            Logger.LogInformation("Using default options");
            configuration = new Serialization.GeneratorModel();
        }

        // override options
        if (ConnectionString.HasValue())
            configuration.Database.ConnectionString = ConnectionString;

        if (Provider.HasValue)
            configuration.Database.Provider = Provider.Value;

        if (Extensions.HasValue)
            configuration.Data.Query.Generate = Extensions.Value;


        if (Models.HasValue)
        {
            configuration.Model.Read.Generate = Models.Value;
            configuration.Model.Create.Generate = Models.Value;
            configuration.Model.Update.Generate = Models.Value;
        }

        if (Mapper.HasValue)
            configuration.Model.Mapper.Generate = Mapper.Value;

        if (Validator.HasValue)
            configuration.Model.Validator.Generate = Validator.Value;

        // conver to options format to support variables
        var options = OptionMapper.Map(configuration);

        var result = _codeGenerator.Generate(options);

        return result ? 0 : 1;
    }

}
