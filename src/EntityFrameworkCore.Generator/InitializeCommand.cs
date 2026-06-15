using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Options;

using Microsoft.Extensions.Logging;

using Spectre.Console.Cli;

namespace EntityFrameworkCore.Generator;

public class InitializeCommand : AsyncCommand<InitializeSettings>
{
    private readonly ILogger<InitializeCommand> _logger;
    private readonly IConfigurationSerializer _serializer;

    public InitializeCommand(ILogger<InitializeCommand> logger, IConfigurationSerializer serializer)
    {
        _logger = logger;
        _serializer = serializer;
    }

    protected override Task<int> ExecuteAsync(CommandContext context, InitializeSettings settings, CancellationToken cancellationToken)
    {
        var workingDirectory = settings.WorkingDirectory ?? Environment.CurrentDirectory;

        if (!Directory.Exists(workingDirectory))
        {
            _logger.LogTrace("Creating directory: {Directory}", workingDirectory);
            Directory.CreateDirectory(workingDirectory);
        }

        var optionsFile = settings.OptionsFile ?? ConfigurationSerializer.OptionsFileName;

        Serialization.GeneratorModel? options = null;

        if (_serializer.Exists(workingDirectory, optionsFile))
            options = _serializer.Load(workingDirectory, optionsFile);

        if (options == null)
            options = CreateOptionsFile(optionsFile);

        if (settings.UserSecretsId.HasValue())
            options.Database.UserSecretsId = settings.UserSecretsId;

        if (settings.ConnectionName.HasValue())
            options.Database.ConnectionName = settings.ConnectionName;

        if (settings.Provider.HasValue)
            options.Database.Provider = settings.Provider.Value;

        if (settings.ConnectionString.HasValue())
        {
            if (settings.UserSecretsId.HasValue())
                options = CreateUserSecret(options, settings.ConnectionString);
            else
                options.Database.ConnectionString = settings.ConnectionString;
        }

        _serializer.Save(options, workingDirectory, optionsFile);

        return Task.FromResult(0);
    }

    private Serialization.GeneratorModel CreateUserSecret(Serialization.GeneratorModel options, string connectionString)
    {
        if (options.Database.UserSecretsId.IsNullOrWhiteSpace())
            options.Database.UserSecretsId = Guid.NewGuid().ToString();

        if (options.Database.ConnectionName.IsNullOrWhiteSpace())
            options.Database.ConnectionName = "ConnectionStrings:Generator";

        _logger.LogInformation("Adding Connection String to User Secrets file");

        // save connection string to user secrets file
        var secretsStore = new SecretsStore(options.Database.UserSecretsId);
        secretsStore.Set(options.Database.ConnectionName, connectionString);
        secretsStore.Save();

        return options;
    }

    private Serialization.GeneratorModel CreateOptionsFile(string optionsFile)
    {
        var options = new Serialization.GeneratorModel();

        options.Project.Namespace = Directory.CreateDirectory(Environment.CurrentDirectory).Name ?? "Project.Core";
        options.Project.Directory = ".\\";
        options.Project.Nullable = true;
        options.Project.FileScopedNamespace = true;

        options.Data.Mapping.RowVersion = RowVersionMapping.Long;

        // default all to generate
        options.Data.Query.Generate = true;
        options.Model.Read.Generate = true;
        options.Model.Create.Generate = true;
        options.Model.Update.Generate = true;
        options.Model.Validator.Generate = true;
        options.Model.Mapper.Generate = true;

        _logger.LogInformation("Creating options file: {OptionsFile}", optionsFile);

        return options;
    }
}
