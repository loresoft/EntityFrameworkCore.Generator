using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Parsing;
using EntityFrameworkCore.Generator.Scripts;
using EntityFrameworkCore.Generator.Templates;

using Microsoft.Extensions.Logging;

using SchemaSaurus.Metadata;
using SchemaSaurus.Metadata.Provider;

namespace EntityFrameworkCore.Generator;

public partial class CodeGenerator : ICodeGenerator
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;
    private readonly ModelGenerator _modelGenerator;
    private readonly SourceSynchronizer _synchronizer;

    public CodeGenerator(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<CodeGenerator>();
        _modelGenerator = new ModelGenerator(loggerFactory);
        _synchronizer = new SourceSynchronizer(loggerFactory);
    }

    public GeneratorOptions Options { get; set; } = null!;

    public async Task<bool> GenerateAsync(GeneratorOptions options)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));

        var databaseProvider = GetDatabaseProvider();
        var databaseModel = await GetDatabaseModel(databaseProvider);

        if (databaseModel == null)
            throw new InvalidOperationException("Failed to create database model");

        LogLoadedDatabaseModel(_logger, databaseModel.DatabaseName);

        var context = _modelGenerator.Generate(Options, databaseModel);

        _synchronizer.UpdateFromSource(context, options);

        GenerateFiles(context);

        return true;
    }

    private void GenerateFiles(EntityContext entityContext)
    {
        GenerateDataContext(entityContext);
        GenerateEntityClasses(entityContext);
        GenerateMappingClasses(entityContext);

        if (Options.Data.Query.Generate)
            GenerateQueryExtensions(entityContext);

        GenerateModelClasses(entityContext);

        GenerateScriptTemplates(entityContext);
    }

    private void GenerateQueryExtensions(EntityContext entityContext)
    {
        foreach (var entity in entityContext.Entities)
        {
            Options.Variables.Set(entity);

            var directory = Options.Data.Query.Directory ?? "Data\\Queries";
            var file = entity.EntityClass + "Extensions.cs";
            var path = Path.Combine(directory, file);

            LogClassFileAction(_logger, File.Exists(path) ? "Updating" : "Creating", "query extensions", file);

            var template = new QueryExtensionTemplate(entity, Options);
            template.WriteCode(path);

            Options.Variables.Remove(entity);
        }
    }

    private void GenerateMappingClasses(EntityContext entityContext)
    {
        if (Options.Data.Mapping.DeleteUnusedFiles)
            DeleteUnusedFiles(Options.Data.Mapping.Directory, entityContext.Entities.Select(e => e.MappingClass).ToHashSet(), "mapping");

        foreach (var entity in entityContext.Entities)
        {
            Options.Variables.Set(entity);

            var directory = Options.Data.Mapping.Directory ?? "Data\\Mapping";
            var file = entity.MappingClass + ".cs";
            var path = Path.Combine(directory, file);

            LogClassFileAction(_logger, File.Exists(path) ? "Updating" : "Creating", "mapping", file);

            var template = new MappingClassTemplate(entity, Options);
            template.WriteCode(path);

            Options.Variables.Remove(entity);
        }
    }

    private void GenerateEntityClasses(EntityContext entityContext)
    {
        if (Options.Data.Entity.DeleteUnusedFiles)
            DeleteUnusedFiles(Options.Data.Entity.Directory, entityContext.Entities.Select(e => e.EntityClass).ToHashSet(), "entity");

        foreach (var entity in entityContext.Entities)
        {
            Options.Variables.Set(entity);

            var directory = Options.Data.Entity.Directory ?? "Data\\Entities";
            var file = entity.EntityClass + ".cs";
            var path = Path.Combine(directory, file);

            LogClassFileAction(_logger, File.Exists(path) ? "Updating" : "Creating", "entity", file);

            var template = new EntityClassTemplate(entity, Options);
            template.WriteCode(path);

            Options.Variables.Remove(entity);
        }
    }

    private void DeleteUnusedFiles(string directory, HashSet<string> fileNamesToBeGenerated, string fileType)
    {
        var existingFiles = Directory.EnumerateFiles(directory, "*.cs");
        foreach (var existingFile in existingFiles)
        {
            var fileName = Path.GetFileNameWithoutExtension(existingFile);
            if (!fileNamesToBeGenerated.Contains(fileName))
            {
                _logger.LogInformation("Deleting {fileType} class: {file}.cs", fileType, fileName);
                File.Delete(existingFile);
            }
        }
    }

    private void GenerateDataContext(EntityContext entityContext)
    {

        var directory = Options.Data.Context.Directory ?? "Data";
        var file = entityContext.ContextClass + ".cs";
        var path = Path.Combine(directory, file);

        LogClassFileAction(_logger, File.Exists(path) ? "Updating" : "Creating", "data context", file);

        var template = new DataContextTemplate(entityContext, Options);
        template.WriteCode(path);
    }


    private void GenerateModelClasses(EntityContext entityContext)
    {
        foreach (var entity in entityContext.Entities)
        {
            if (entity.Models.Count == 0)
                continue;

            Options.Variables.Set(entity);

            GenerateModelClasses(entity);
            GenerateValidatorClasses(entity);
            GenerateMapperClass(entity);

            Options.Variables.Remove(entity);
        }
    }


    private void GenerateModelClasses(Entity entity)
    {
        foreach (var model in entity.Models)
        {
            Options.Variables.Set(model);

            var directory = GetModelDirectory(model) ?? "Data\\Models";
            var file = model.ModelClass + ".cs";
            var path = Path.Combine(directory, file);

            LogClassFileAction(_logger, File.Exists(path) ? "Updating" : "Creating", "model", file);

            var template = new ModelClassTemplate(model, Options);
            template.WriteCode(path);

            Options.Variables.Remove(model);
        }

    }

    private string? GetModelDirectory(Model model)
    {
        if (model.ModelType == ModelType.Create)
        {
            return Options.Model.Create.Directory.HasValue()
                ? Options.Model.Create.Directory
                : Options.Model.Shared.Directory;
        }

        if (model.ModelType == ModelType.Update)
        {
            return Options.Model.Update.Directory.HasValue()
                ? Options.Model.Update.Directory
                : Options.Model.Shared.Directory;
        }

        return Options.Model.Read.Directory.HasValue()
            ? Options.Model.Read.Directory
            : Options.Model.Shared.Directory;
    }


    private void GenerateValidatorClasses(Entity entity)
    {
        if (!Options.Model.Validator.Generate)
            return;

        foreach (var model in entity.Models)
        {
            Options.Variables.Set(model);

            // don't validate read models
            if (model.ModelType == ModelType.Read)
                continue;

            var directory = Options.Model.Validator.Directory ?? "Data\\Validation";
            var file = model.ValidatorClass + ".cs";
            var path = Path.Combine(directory, file);

            LogClassFileAction(_logger, File.Exists(path) ? "Updating" : "Creating", "validation", file);

            var template = new ValidatorClassTemplate(model, Options);
            template.WriteCode(path);

            Options.Variables.Remove(model);
        }
    }


    private void GenerateMapperClass(Entity entity)
    {
        if (!Options.Model.Mapper.Generate)
            return;

        var directory = Options.Model.Mapper.Directory ?? "Data\\Mapper";
        var file = entity.MapperClass + ".cs";
        var path = Path.Combine(directory, file);

        LogClassFileAction(_logger, File.Exists(path) ? "Updating" : "Creating", "mapper", file);

        var template = new MapperClassTemplate(entity, Options);
        template.WriteCode(path);
    }


    private void GenerateScriptTemplates(EntityContext entityContext)
    {
        GenerateContextScriptTemplates(entityContext);
        GenerateEntityScriptTemplates(entityContext);
        GenerateModelScriptTemplates(entityContext);
    }

    private void GenerateModelScriptTemplates(EntityContext entityContext)
    {
        if (Options?.Script?.Model == null || Options.Script.Model.Count == 0)
            return;

        foreach (var templateOption in Options.Script.Model)
        {
            if (!VerifyScriptTemplate(templateOption))
                continue;

            try
            {
                var template = new ModelScriptTemplate(_loggerFactory, Options, templateOption);

                foreach (var entity in entityContext.Entities)
                {
                    Options.Variables.Set(entity);

                    foreach (var model in entity.Models)
                    {
                        Options.Variables.Set(model);

                        template.RunScript(model);

                        Options.Variables.Remove(model);
                    }

                    Options.Variables.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                LogErrorRunningTemplate(_logger, ex, "Model", ex.Message);
            }
        }
    }

    private void GenerateEntityScriptTemplates(EntityContext entityContext)
    {
        if (Options?.Script?.Entity == null || Options.Script.Entity.Count == 0)
            return;

        foreach (var templateOption in Options.Script.Entity)
        {
            if (!VerifyScriptTemplate(templateOption))
                continue;

            try
            {
                var template = new EntityScriptTemplate(_loggerFactory, Options, templateOption);

                foreach (var entity in entityContext.Entities)
                {
                    Options.Variables.Set(entity);

                    template.RunScript(entity);

                    Options.Variables.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                LogErrorRunningTemplate(_logger, ex, "Entity", ex.Message);
            }
        }
    }

    private void GenerateContextScriptTemplates(EntityContext entityContext)
    {
        if (Options?.Script?.Context == null || Options.Script.Context.Count! < 0)
            return;

        foreach (var templateOption in Options.Script.Context)
        {
            if (!VerifyScriptTemplate(templateOption))
                continue;

            try
            {
                var template = new ContextScriptTemplate(_loggerFactory, Options, templateOption);
                template.RunScript(entityContext);
            }
            catch (Exception ex)
            {
                LogErrorRunningTemplate(_logger, ex, "Context", ex.Message);
            }
        }
    }

    private bool VerifyScriptTemplate(TemplateOptions templateOption)
    {
        var templatePath = templateOption.TemplatePath;

        if (File.Exists(templatePath))
            return true;

        LogTemplateNotFound(_logger, templatePath);
        return false;
    }


    private async Task<DatabaseModel> GetDatabaseModel(IDatabaseSchemaReader factory)
    {
        LogLoadingDatabaseModel(_logger);

        var database = Options.Database;

        var connectionString = ResolveConnectionString(database);
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Could not find connection string.");

        var options = new SchemaReaderOptions
        {
            Schemas = Options.Database.Schemas,
            Tables = Options.Database.Tables
        };

        return await factory.ReadAsync(connectionString, options);
    }

    private static string? ResolveConnectionString(DatabaseOptions database)
    {
        if (database.ConnectionString.HasValue())
            return database.ConnectionString;

        if (database.UserSecretsId.HasValue() && database.ConnectionName.HasValue())
        {
            var secretsStore = new SecretsStore(database.UserSecretsId);
            if (secretsStore.ContainsKey(database.ConnectionName))
                return secretsStore[database.ConnectionName];
        }

        throw new InvalidOperationException("Could not find connection string.");
    }


    private IDatabaseSchemaReader GetDatabaseProvider()
    {
        var provider = Options.Database.Provider;

        LogCreatingDatabaseModelFactory(_logger, provider);

        return provider switch
        {
            DatabaseProviders.SqlServer => new SchemaSaurus.SqlServer.SqlServerSchemaReader(),
            DatabaseProviders.PostgreSQL => new SchemaSaurus.PostgreSql.PostgreSqlSchemaReader(),
            DatabaseProviders.MySQL => new SchemaSaurus.MySql.MySqlSchemaReader(),
            DatabaseProviders.Sqlite => new SchemaSaurus.Sqlite.SqliteSchemaReader(),
            DatabaseProviders.Oracle => new SchemaSaurus.Oracle.OracleSchemaReader(),
            _ => throw new NotSupportedException($"The specified provider '{provider}' is not supported."),
        };
    }


    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Loaded database model for: {databaseName}")]
    private static partial void LogLoadedDatabaseModel(ILogger logger, string? databaseName);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "{action} {kind} class: {file}")]
    private static partial void LogClassFileAction(ILogger logger, string action, string kind, string file);

    [LoggerMessage(EventId = 3, Level = LogLevel.Error, Message = "Error Running {templateType} Template: {errorMessage}")]
    private static partial void LogErrorRunningTemplate(ILogger logger, Exception exception, string templateType, string errorMessage);

    [LoggerMessage(EventId = 4, Level = LogLevel.Warning, Message = "Template '{template}' could not be found.")]
    private static partial void LogTemplateNotFound(ILogger logger, string? template);

    [LoggerMessage(EventId = 5, Level = LogLevel.Information, Message = "Loading database model ...")]
    private static partial void LogLoadingDatabaseModel(ILogger logger);

    [LoggerMessage(EventId = 6, Level = LogLevel.Debug, Message = "Creating database model factory for: {provider}")]
    private static partial void LogCreatingDatabaseModelFactory(ILogger logger, DatabaseProviders provider);
}
