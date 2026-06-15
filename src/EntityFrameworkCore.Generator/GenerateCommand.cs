using EntityFrameworkCore.Generator.Extensions;

using Microsoft.Extensions.Logging;

using Spectre.Console.Cli;

namespace EntityFrameworkCore.Generator;

public class GenerateCommand : AsyncCommand<GenerateSettings>
{
    private readonly ILogger<GenerateCommand> _logger;
    private readonly IConfigurationSerializer _serializer;
    private readonly ICodeGenerator _codeGenerator;

    public GenerateCommand(
        ILogger<GenerateCommand> logger,
        IConfigurationSerializer serializer,
        ICodeGenerator codeGenerator)
    {
        _logger = logger;
        _serializer = serializer;
        _codeGenerator = codeGenerator;
    }

    protected override async Task<int> ExecuteAsync(CommandContext context, GenerateSettings settings, CancellationToken cancellationToken)
    {
        var workingDirectory = settings.WorkingDirectory ?? Environment.CurrentDirectory;
        var configurationFile = settings.OptionsFile ?? ConfigurationSerializer.OptionsFileName;

        var configuration = _serializer.Load(workingDirectory, configurationFile);
        if (configuration == null)
        {
            _logger.LogInformation("Using default options");
            configuration = new Serialization.GeneratorModel();
        }

        // override options
        if (settings.ConnectionString.HasValue())
            configuration.Database.ConnectionString = settings.ConnectionString;

        if (settings.Provider.HasValue)
            configuration.Database.Provider = settings.Provider.Value;

        if (settings.Extensions.HasValue)
            configuration.Data.Query.Generate = settings.Extensions.Value;


        if (settings.Models.HasValue)
        {
            configuration.Model.Read.Generate = settings.Models.Value;
            configuration.Model.Create.Generate = settings.Models.Value;
            configuration.Model.Update.Generate = settings.Models.Value;
        }

        if (settings.Mapper.HasValue)
            configuration.Model.Mapper.Generate = settings.Mapper.Value;

        if (settings.Validator.HasValue)
            configuration.Model.Validator.Generate = settings.Validator.Value;

        // convert to options format to support variables
        var options = OptionMapper.Map(configuration);

        var result = await _codeGenerator.GenerateAsync(options);

        return result ? 0 : 1;
    }
}
