using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;

using ScriptOptions = Microsoft.CodeAnalysis.Scripting.ScriptOptions;

namespace EntityFrameworkCore.Generator.Scripts;

public abstract partial class ScriptTemplateBase<TVariable>
    where TVariable : ScriptVariablesBase
{
    private Script<string>? _scriptTemplate;

    protected ScriptTemplateBase(ILoggerFactory loggerFactory, GeneratorOptions generatorOptions, TemplateOptions templateOptions)
    {
        Logger = loggerFactory.CreateLogger(this.GetType());

        TemplateOptions = templateOptions;
        GeneratorOptions = generatorOptions;
        RegionReplace = new RegionReplace();

    }

    protected ILogger Logger { get; }

    protected RegionReplace RegionReplace { get; }


    public TemplateOptions TemplateOptions { get; }

    public GeneratorOptions GeneratorOptions { get; }


    protected virtual void WriteCode()
    {
        var templatePath = TemplateOptions.TemplatePath;

        if (!File.Exists(templatePath))
        {
            LogTemplateNotFound(Logger, templatePath);
            return;
        }

        // save file
        var directory = TemplateOptions.Directory;
        var fileName = TemplateOptions.FileName;

        if (directory.IsNullOrEmpty() || fileName.IsNullOrEmpty())
        {
            LogTemplateCouldNotResolveOutputFile(Logger, templatePath);
            return;
        }

        var path = Path.Combine(directory, fileName);
        var exists = File.Exists(path);

        if (exists && !(TemplateOptions.Merge || TemplateOptions.Overwrite))
        {
            LogSkippingTemplateBecauseOutputExists(Logger, templatePath, fileName);
            return;
        }

        LogTemplateScriptFileAction(Logger, File.Exists(path) ? "Updating" : "Creating", fileName);

        // get content
        var content = ExecuteScript();

        if (content.IsNullOrWhiteSpace())
        {
            LogSkippingTemplateBecauseNoText(Logger, templatePath);
            return;
        }

        if (directory.HasValue() && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        if (exists && TemplateOptions.Merge && !TemplateOptions.Overwrite)
            RegionReplace.MergeFile(path, content);
        else
            File.WriteAllText(path, content);
    }

    protected virtual string ExecuteScript()
    {
        var templatePath = TemplateOptions.TemplatePath;
        if (!File.Exists(templatePath))
        {
            LogTemplateNotFound(Logger, templatePath);
            return string.Empty;
        }

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

        LogLoadingTemplateScript(Logger, scriptPath);

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
                "EntityFrameworkCore.Generator.Options"
            );

        _scriptTemplate = CSharpScript.Create<string>(scriptContent, scriptOptions, typeof(TVariable));
        var diagnostics = _scriptTemplate.Compile();

        if (diagnostics.Length == 0)
            return _scriptTemplate;

        LogTemplateCompileDiagnostics(Logger);
        foreach (var diagnostic in diagnostics)
        {
            var message = diagnostic.GetMessage();
            switch (diagnostic.Severity)
            {
                case DiagnosticSeverity.Info:
                    LogTemplateDiagnosticDebug(Logger, message);
                    break;
                case DiagnosticSeverity.Warning:
                    LogTemplateDiagnosticWarning(Logger, message);
                    break;
                case DiagnosticSeverity.Error:
                    LogTemplateDiagnosticError(Logger, message);
                    break;
                default:
                    LogTemplateDiagnosticDebug(Logger, message);
                    break;
            }
        }

        return _scriptTemplate;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Warning, Message = "Template '{template}' could not be found.")]
    private static partial void LogTemplateNotFound(ILogger logger, string? template);

    [LoggerMessage(EventId = 2, Level = LogLevel.Warning, Message = "Template '{template}' could not resolve output file.")]
    private static partial void LogTemplateCouldNotResolveOutputFile(ILogger logger, string? template);

    [LoggerMessage(EventId = 3, Level = LogLevel.Debug, Message = "Skipping template '{template}' because output '{fileName}' already exists.")]
    private static partial void LogSkippingTemplateBecauseOutputExists(ILogger logger, string? template, string fileName);

    [LoggerMessage(EventId = 4, Level = LogLevel.Information, Message = "{action} template script file: {fileName}")]
    private static partial void LogTemplateScriptFileAction(ILogger logger, string action, string fileName);

    [LoggerMessage(EventId = 5, Level = LogLevel.Debug, Message = "Skipping template '{template}' because it didn't return any text.")]
    private static partial void LogSkippingTemplateBecauseNoText(ILogger logger, string? template);

    [LoggerMessage(EventId = 6, Level = LogLevel.Debug, Message = "Loading template script: {script}")]
    private static partial void LogLoadingTemplateScript(ILogger logger, string script);

    [LoggerMessage(EventId = 7, Level = LogLevel.Information, Message = "Template Compile Diagnostics: ")]
    private static partial void LogTemplateCompileDiagnostics(ILogger logger);

    [LoggerMessage(EventId = 8, Level = LogLevel.Debug, Message = "{message}")]
    private static partial void LogTemplateDiagnosticDebug(ILogger logger, string message);

    [LoggerMessage(EventId = 9, Level = LogLevel.Warning, Message = "{message}")]
    private static partial void LogTemplateDiagnosticWarning(ILogger logger, string message);

    [LoggerMessage(EventId = 10, Level = LogLevel.Error, Message = "{message}")]
    private static partial void LogTemplateDiagnosticError(ILogger logger, string message);
}
