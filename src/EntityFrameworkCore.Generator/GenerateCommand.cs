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

    public GenerateCommand(ILoggerFactory logger, IConsole console, IGeneratorOptionsSerializer serializer, ICodeGenerator codeGenerator)
        : base(logger, console, serializer)
    {
        _codeGenerator = codeGenerator;
    }

    [Option("-p <Provider>", Description = "Database provider to reverse engineer")]
    public DatabaseProviders? Provider { get; set; }

    [Option("-c <ConnectionString>", Description = "Database connection string to reverse engineer")]
    public string ConnectionString { get; set; }


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
        var optionsFile = OptionsFile ?? GeneratorOptionsSerializer.OptionsFileName;

        var options = Serializer.Load(workingDirectory, optionsFile);
        if (options == null)
        {
            Logger.LogInformation("Using default options");
            options = new GeneratorOptions();
        }

        // override options
        if (ConnectionString.HasValue())
            options.Database.ConnectionString = ConnectionString;

        if (Provider.HasValue)
            options.Database.Provider = Provider.Value;

        if (Extensions.HasValue)
            options.Data.Query.Generate = Extensions.Value;


        if (Models.HasValue)
        {
            options.Model.Read.Generate = Models.Value;
            options.Model.Create.Generate = Models.Value;
            options.Model.Update.Generate = Models.Value;
        }

        if (Mapper.HasValue)
            options.Model.Mapper.Generate = Mapper.Value;

        if (Validator.HasValue)
            options.Model.Validator.Generate = Validator.Value;

        options.Model.Read.Validate(Logger);
        options.Model.Create.Validate(Logger);
        options.Model.Update.Validate(Logger);
        options.Model.Mapper.Validate(Logger);
        options.Model.Shared.Validate(Logger);
        options.Model.Update.Validate(Logger);
        options.Model.Validator.Validate(Logger);
        options.Data.Mapping.Validate(Logger);
        options.Data.Query.Validate(Logger);
        options.Data.Entity.Validate(Logger);
        options.Data.Context.Validate(Logger);
        options.Database.Validate(Logger);
        options.Project.Validate(Logger);
        options.Script.Validate(Logger);

        var result = _codeGenerator.Generate(options);

        return result ? 0 : 1;
    }

}
