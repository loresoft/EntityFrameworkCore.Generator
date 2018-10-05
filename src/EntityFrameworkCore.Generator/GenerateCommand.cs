using System;
using EntityFrameworkCore.Generator.Options;
using McMaster.Extensions.CommandLineUtils;

namespace EntityFrameworkCore.Generator
{
    [Command("generate")]
    public class GenerateCommand : CommandBase
    {
        [Option("-d <Directory>", Description = "The root working directory")]
        public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

        [Option("-f <File>", Description = "The options file name")]
        public string OptionsFile { get; set; } = GeneratorOptionsSerializer.OptionsFileName;

        [Option("-p <Provider>", Description = "Database provider to reverse engineer")]
        public DatabaseProviders? Provider { get; set; }

        [Option("-c <ConnectionString>", Description = "Database connection string to reverse engineer")]
        public string ConnectionString { get; set; }


        [Option("--views", Description = "Include views in generation")]
        public bool? Views { get; set; }

        [Option("--extensions", Description = "Include query extensions in generation")]
        public bool? Extensions { get; set; }

        [Option("--models", Description = "Include view models in generation")]
        public bool? Models { get; set; }


        protected override int OnExecute(CommandLineApplication application)
        {
            return 0;
        }
    }
}