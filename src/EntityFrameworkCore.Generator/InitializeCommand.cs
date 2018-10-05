using System;
using System.IO;
using EntityFrameworkCore.Generator.Options;
using McMaster.Extensions.CommandLineUtils;

namespace EntityFrameworkCore.Generator
{
    [Command("initialize")]
    public class InitializeCommand : CommandBase
    {
        [Option("-d <directory>", Description = "The root working directory")]
        public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

        [Option("-f <file>", Description = "The options file name")]
        public string OptionsFile { get; set; } = GeneratorOptionsSerializer.OptionsFileName;

        protected override int OnExecute(CommandLineApplication application)
        {
            var workingDirectory = WorkingDirectory ?? Environment.CurrentDirectory;

            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);

            var optionsFile = OptionsFile ?? GeneratorOptionsSerializer.OptionsFileName;

            var options = new GeneratorOptions();
            options.Database.ConnectionString = "<ConnectionString>";

            Console.WriteLine($"Creating Options file: {optionsFile}");

            var serializer = new GeneratorOptionsSerializer();
            serializer.Save(options, workingDirectory, optionsFile);

            return 0;
        }
    }
}