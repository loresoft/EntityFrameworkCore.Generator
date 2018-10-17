using System;
using System.IO;
using EntityFrameworkCore.Generator.Options;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
{
    [Command("initialize")]
    public class InitializeCommand : OptionsCommandBase
    {
        public InitializeCommand(ILoggerFactory logger, IConsole console, IGeneratorOptionsSerializer serializer) 
            : base(logger, console, serializer)
        {
        }

        protected override int OnExecute(CommandLineApplication application)
        {
            var workingDirectory = WorkingDirectory ?? Environment.CurrentDirectory;

            if (!Directory.Exists(workingDirectory))
            {
                Logger.LogTrace($"Creating directory: {workingDirectory}");
                Directory.CreateDirectory(workingDirectory);
            }

            var optionsFile = OptionsFile ?? GeneratorOptionsSerializer.OptionsFileName;

            var options = new GeneratorOptions();
            options.Database.ConnectionString = "<ConnectionString>";
            options.Model.Read.Include = null;
            options.Model.Read.Exclude = null;
            options.Model.Create.Include = null;
            options.Model.Create.Exclude = null;
            options.Model.Update.Include = null;
            options.Model.Update.Exclude = null;

            Logger.LogInformation($"Creating options file: {optionsFile}");

            Serializer.Save(options, workingDirectory, optionsFile);

            return 0;
        }
    }
}