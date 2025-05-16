using System;
using System.IO;

using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Options;

using McMaster.Extensions.CommandLineUtils;

using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator;

[Command("initialize", "init")]
public class InitializeCommand : OptionsCommandBase
{
    public InitializeCommand(ILoggerFactory logger, IConsole console, IConfigurationSerializer serializer)
        : base(logger, console, serializer)
    {
    }

    [Option("-p <Provider>", Description = "Database provider to reverse engineer")]
    public DatabaseProviders? Provider { get; set; }

    [Option("-c <ConnectionString>", Description = "Database connection string to reverse engineer")]
    public string? ConnectionString { get; set; }

    [Option("--id <UserSecretsId>", Description = "The user secret ID to use")]
    public string? UserSecretsId { get; set; }

    [Option("--name <ConnectionName>", Description = "The user secret configuration name")]
    public string? ConnectionName { get; set; }

    protected override int OnExecute(CommandLineApplication application)
    {
        var workingDirectory = WorkingDirectory ?? Environment.CurrentDirectory;

        if (!Directory.Exists(workingDirectory))
        {
            Logger.LogTrace($"Creating directory: {workingDirectory}");
            Directory.CreateDirectory(workingDirectory);
        }

        var optionsFile = OptionsFile ?? ConfigurationSerializer.OptionsFileName;

        Serialization.GeneratorModel? options = null;

        if (Serializer.Exists(workingDirectory, optionsFile))
            options = Serializer.Load(workingDirectory, optionsFile);

        if (options == null)
            options = CreateOptionsFile(optionsFile);

        if (UserSecretsId.HasValue())
            options.Database.UserSecretsId = UserSecretsId;

        if (ConnectionName.HasValue())
            options.Database.ConnectionName = ConnectionName;

        if (Provider.HasValue)
            options.Database.Provider = Provider.Value;

        if (ConnectionString.HasValue())
        {
            if (UserSecretsId.HasValue())
                options = CreateUserSecret(options, ConnectionString);
            else
                options.Database.ConnectionString = ConnectionString;
        }

        Serializer.Save(options, workingDirectory, optionsFile);

        return 0;
    }

    private Serialization.GeneratorModel CreateUserSecret(Serialization.GeneratorModel options, string connectionString)
    {
        if (options.Database.UserSecretsId.IsNullOrWhiteSpace())
            options.Database.UserSecretsId = Guid.NewGuid().ToString();

        if (options.Database.ConnectionName.IsNullOrWhiteSpace())
            options.Database.ConnectionName = "ConnectionStrings:Generator";

        Logger.LogInformation("Adding Connection String to User Secrets file");

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

        Logger.LogInformation($"Creating options file: {optionsFile}");

        return options;
    }
}
