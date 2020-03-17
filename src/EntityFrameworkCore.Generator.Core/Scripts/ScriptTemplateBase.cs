using System;
using System.IO;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using ScriptOptions = Microsoft.CodeAnalysis.Scripting.ScriptOptions;

namespace EntityFrameworkCore.Generator.Scripts
{
    public abstract class ScriptTemplateBase<TVariable>
        where TVariable : ScriptVariablesBase
    {
        private Script<string> _scriptTemplate;

        protected ScriptTemplateBase(ILoggerFactory loggerFactory, GeneratorOptions generatorOptions, TemplateOptions templateOptions)
        {
            Logger = loggerFactory.CreateLogger(this.GetType());

            TemplateOptions = templateOptions;
            GeneratorOptions = generatorOptions;
        }

        protected ILogger Logger { get; }

        public TemplateOptions TemplateOptions { get; }

        public GeneratorOptions GeneratorOptions { get; }

        protected virtual void WriteCode()
        {
            var templatePath = TemplateOptions.TemplatePath;

            if (!File.Exists(templatePath))
            {
                Logger.LogWarning("Template '{template}' could not be found.", templatePath);
                return;
            }
            
            // save file
            var directory = TemplateOptions.Directory;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var fileName = TemplateOptions.FileName;
            var path = Path.Combine(directory, fileName);

            var exists = File.Exists(path);

            if (exists && !TemplateOptions.Overwrite)
            {
                Logger.LogDebug("Skipping template '{template}' because output '{fileName}' already exists.", templatePath, fileName);
                return;
            }
            
            Logger.LogInformation(exists
                ? $"Updating template script file: {fileName}"
                : $"Creating template script file: {fileName}");

            // get content
            var content = ExecuteScript();

            if (content.IsNullOrWhiteSpace())
            {
                Logger.LogDebug("Skipping template '{template}' because it didn't return any text.", templatePath);
                return;
            }

            File.WriteAllText(path, content);
        }

        protected virtual string ExecuteScript()
        {
            var templatePath = TemplateOptions.TemplatePath;
            var script = LoadScript(templatePath);
            var variables = CreateVariables();

            var scriptTask = script.RunAsync(variables);
            var scriptState = scriptTask.Result;

            return scriptState.ReturnValue;
        }

        protected abstract TVariable CreateVariables();

        protected Script<string> LoadScript(string scriptPath)
        {
            if (_scriptTemplate != null)
                return _scriptTemplate;

            Logger.LogDebug("Loading template script: {script}", scriptPath);
            
            var scriptContent = File.ReadAllText(scriptPath);

            var scriptOptions = ScriptOptions.Default
                .WithReferences(
                    typeof(ScriptVariablesBase).Assembly
                )
                .WithImports(
                    "System",
                    "System.Collections.Generic",
                    "System.Linq",
                    "System.Text",
                    "EntityFrameworkCore.Generator.Extensions",
                    "EntityFrameworkCore.Generator.Metadata.Generation",
                    "EntityFrameworkCore.Generator.Options",
                    "Microsoft.EntityFrameworkCore.Internal"
                );

            _scriptTemplate = CSharpScript.Create<string>(scriptContent, scriptOptions, typeof(TVariable));
            var diagnostics = _scriptTemplate.Compile();

            if (diagnostics.Length == 0) 
                return _scriptTemplate;

            Logger.LogInformation("Template Compile Diagnostics: ");
            foreach (var diagnostic in diagnostics)
            {
                var message = diagnostic.GetMessage();
                switch (diagnostic.Severity)
                {
                    case DiagnosticSeverity.Info:
                        Logger.LogDebug(message);
                        break;
                    case DiagnosticSeverity.Warning:
                        Logger.LogWarning(message);
                        break;
                    case DiagnosticSeverity.Error:
                        Logger.LogError(message);
                        break;
                    default:
                        Logger.LogDebug(message);
                        break;
                }
            }

            return _scriptTemplate;
        }
    }
}