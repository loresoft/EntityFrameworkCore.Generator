using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Options;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EntityFrameworkCore.Generator
{
    [Command("initialize")]
    public class InitializeCommand : OptionsCommandBase
    {
        public InitializeCommand(ILoggerFactory logger, IConsole console, IGeneratorOptionsSerializer serializer)
            : base(logger, console, serializer)
        {
        }

        [Option("-p <Provider>", Description = "Database provider to reverse engineer")]
        public DatabaseProviders? Provider { get; set; }

        [Option("-c <ConnectionString>", Description = "Database connection string to reverse engineer")]
        public string ConnectionString { get; set; }


        protected override int OnExecute(CommandLineApplication application)
        {
            var workingDirectory = WorkingDirectory ?? Environment.CurrentDirectory;

            if (!Directory.Exists(workingDirectory))
            {
                Logger.LogTrace($"Creating directory: {workingDirectory}");
                Directory.CreateDirectory(workingDirectory);
            }

            var optionsFile = OptionsFile ?? GeneratorOptionsSerializer.OptionsFileName;

            GeneratorOptions options = null;

            if (Serializer.Exists(workingDirectory, optionsFile))
                options = Serializer.Load(workingDirectory, optionsFile);

            if (options == null)
                options = CreateOptionsFile(optionsFile);

            if (ConnectionString.HasValue())
                options = CreateUserSecret(options);

            if (Provider.HasValue)
                options.Database.Provider = Provider.Value;


            Serializer.Save(options, workingDirectory, optionsFile);

            return 0;
        }

        private GeneratorOptions CreateUserSecret(GeneratorOptions options)
        {
            if (options.Database.UserSecretsId.IsNullOrWhiteSpace())
                options.Database.UserSecretsId = Guid.NewGuid().ToString();

            if (options.Database.ConnectionName.IsNullOrWhiteSpace())
                options.Database.ConnectionName = "Generator:ConnectionString";

            Logger.LogInformation("Adding Connection String to User Secrets file");

            // save connection string to user secrets file
            var secretsStore = new SecretsStore(options.Database.UserSecretsId);
            secretsStore.Set(options.Database.ConnectionName, ConnectionString);
            secretsStore.Save();

            return options;
        }

        private GeneratorOptions CreateOptionsFile(string optionsFile)
        {
            var options = new GeneratorOptions();

            // set user secret values
            options.Database.UserSecretsId = Guid.NewGuid().ToString();
            options.Database.ConnectionName = "Generator:ConnectionString";

            // default all to generate
            options.Data.Query.Generate = true;
            options.Model.Read.Generate = true;
            options.Model.Create.Generate = true;
            options.Model.Update.Generate = true;
            options.Model.Validator.Generate = true;
            options.Model.Mapper.Generate = true;

            // null out collection for cleaner yaml file
            options.Database.Tables = null;
            options.Database.Schemas = null;
            options.Model.Shared.Include = null;
            options.Model.Shared.Exclude = null;
            options.Model.Read.Include = null;
            options.Model.Read.Exclude = null;
            options.Model.Create.Include = null;
            options.Model.Create.Exclude = null;
            options.Model.Update.Include = null;
            options.Model.Update.Exclude = null;

            Logger.LogInformation($"Creating options file: {optionsFile}");

            return options;
        }
    }
}