using System;
using System.IO;
using EntityFrameworkCore.Generator.Core;
using EntityFrameworkCore.Generator.Options;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
{
    [Command("refresh")]
    public class RefreshCommand : OptionsCommandBase
    {
        private IModelCacheBuilder _mcb;

        public RefreshCommand(ILoggerFactory logger, IConsole console,
            IGeneratorOptionsSerializer serializer, IModelCacheBuilder mcb)
            : base(logger, console, serializer)
        {
            _mcb = mcb;
        }

        protected override int OnExecute(CommandLineApplication application)
        {
            var workingDirectory = WorkingDirectory ?? Environment.CurrentDirectory;
            var optionsFile = OptionsFile ?? GeneratorOptionsSerializer.OptionsFileName;

            var options = Serializer.Load(workingDirectory, optionsFile);
            if (options == null)
            {
                Logger.LogInformation("Using default options");
                options = new GeneratorOptions();

                // Options file meta data
                options.Options.SetFullPath(Path.Combine(workingDirectory, optionsFile));
            }

            _mcb.Refresh(options);

            return 0;
        }
    }
}