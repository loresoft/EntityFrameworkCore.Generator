using System;
using System.IO;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Parsing;
using EntityFrameworkCore.Generator.Scripts;
using EntityFrameworkCore.Generator.Templates;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator;

public class CodeGenerator : ICodeGenerator
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

    public bool Generate(GeneratorOptions options)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));

        var databaseProviders = GetDatabaseProviders();
        var databaseModel = GetDatabaseModel(databaseProviders.factory);

        if (databaseModel == null)
            throw new InvalidOperationException("Failed to create database model");

        _logger.LogInformation("Loaded database model for: {databaseName}", databaseModel.DatabaseName);

        var context = _modelGenerator.Generate(Options, databaseModel, databaseProviders.mapping);

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

            if (File.Exists(path))
                _logger.LogInformation("Updating query extensions class: {file}", file);
            else
                _logger.LogInformation("Creating query extensions class: {file}", file);

            var template = new QueryExtensionTemplate(entity, Options);
            template.WriteCode(path);

            Options.Variables.Remove(entity);
        }
    }

    private void GenerateMappingClasses(EntityContext entityContext)
    {
        if (!Options.Data.Mapping.Generate)
            return;

        foreach (var entity in entityContext.Entities)
        {
            Options.Variables.Set(entity);

            var directory = Options.Data.Mapping.Directory ?? "Data\\Mapping";
            var file = entity.MappingClass + ".cs";
            var path = Path.Combine(directory, file);

            if (File.Exists(path))
                _logger.LogInformation("Updating mapping class: {file}", file);
            else
                _logger.LogInformation("Creating mapping class: {file}", file);

            var template = new MappingClassTemplate(entity, Options);
            template.WriteCode(path);

            Options.Variables.Remove(entity);
        }
    }

    private void GenerateEntityClasses(EntityContext entityContext)
    {
        foreach (var entity in entityContext.Entities)
        {
            Options.Variables.Set(entity);

            var directory = Options.Data.Entity.Directory ?? "Data\\Entities";
            var file = entity.EntityClass + ".cs";
            var path = Path.Combine(directory, file);

            if (File.Exists(path))
                _logger.LogInformation("Updating entity class: {file}", file);
            else
                _logger.LogInformation("Creating entity class: {file}", file);

            var template = new EntityClassTemplate(entity, Options);
            template.WriteCode(path);

            Options.Variables.Remove(entity);
        }
    }

    private void GenerateDataContext(EntityContext entityContext)
    {

        var directory = Options.Data.Context.Directory ?? "Data";
        var file = entityContext.ContextClass + ".cs";
        var path = Path.Combine(directory, file);

        if (File.Exists(path))
            _logger.LogInformation("Updating data context class: {file}", file);
        else
            _logger.LogInformation("Creating data context class: {file}", file);

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

            if (File.Exists(path))
                _logger.LogInformation("Updating model class: {file}", file);
            else
                _logger.LogInformation("Creating model class: {file}", file);

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

            if (File.Exists(path))
                _logger.LogInformation("Updating validation class: {file}", file);
            else
                _logger.LogInformation("Creating validation class: {file}", file);

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

        if (File.Exists(path))
            _logger.LogInformation("Updating mapper class: {file}", file);
        else
            _logger.LogInformation("Creating mapper class: {file}", file);

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
                _logger.LogError(ex, "Error Running Model Template: {message}", ex.Message);
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
                _logger.LogError(ex, "Error Running Entity Template: {message}", ex.Message);
            }
        }
    }

    private void GenerateContextScriptTemplates(EntityContext entityContext)
    {
        if (Options?.Script?.Context == null || Options.Script.Context.Count !< 0)
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
                _logger.LogError(ex, "Error Running Context Template: {message}", ex.Message);
            }
        }
    }

    private bool VerifyScriptTemplate(TemplateOptions templateOption)
    {
        var templatePath = templateOption.TemplatePath;

        if (File.Exists(templatePath))
            return true;

        _logger.LogWarning("Template '{template}' could not be found.", templatePath);
        return false;
    }


    private DatabaseModel GetDatabaseModel(IDatabaseModelFactory factory)
    {
        _logger.LogInformation("Loading database model ...");

        var database = Options.Database;

        var connectionString = ResolveConnectionString(database);
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Could not find connection string.");

        var options = new DatabaseModelFactoryOptions(database.Tables, database.Schemas);

        return factory.Create(connectionString, options);
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


    private (IDatabaseModelFactory factory, IRelationalTypeMappingSource mapping) GetDatabaseProviders()
    {
        var provider = Options.Database.Provider;

        _logger.LogDebug("Creating database model factory for: {provider}", provider);

        // start a new service container to create the database model factory
        var services = new ServiceCollection()
            .AddSingleton(_loggerFactory)
            .AddEntityFrameworkDesignTimeServices();

        switch (provider)
        {
            case DatabaseProviders.SqlServer:
                ConfigureSqlServerServices(services);
                break;
            case DatabaseProviders.PostgreSQL:
                ConfigurePostgresServices(services);
                break;
            case DatabaseProviders.MySQL:
                ConfigureMySqlServices(services);
                break;
            case DatabaseProviders.Sqlite:
                ConfigureSqliteServices(services);
                break;
            case DatabaseProviders.Oracle:
                ConfigureOracleServices(services);
                break;
            default:
                throw new NotSupportedException($"The specified provider '{provider}' is not supported.");
        }

        var serviceProvider = services
            .BuildServiceProvider();

        var databaseModelFactory = serviceProvider
            .GetRequiredService<IDatabaseModelFactory>();

        var typeMappingSource = serviceProvider
            .GetRequiredService<IRelationalTypeMappingSource>();

        return (databaseModelFactory, typeMappingSource);
    }


    private static void ConfigureMySqlServices(IServiceCollection services)
    {
        var designTimeServices = new Pomelo.EntityFrameworkCore.MySql.Design.Internal.MySqlDesignTimeServices();
        designTimeServices.ConfigureDesignTimeServices(services);
            services.AddEntityFrameworkMySqlNetTopologySuite();
    }

    private static void ConfigurePostgresServices(IServiceCollection services)
    {
        var designTimeServices = new Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal.NpgsqlDesignTimeServices();
        designTimeServices.ConfigureDesignTimeServices(services);
            services.AddEntityFrameworkNpgsqlNetTopologySuite();
    }

    private static void ConfigureSqlServerServices(IServiceCollection services)
    {
        var designTimeServices = new Microsoft.EntityFrameworkCore.SqlServer.Design.Internal.SqlServerDesignTimeServices();
        designTimeServices.ConfigureDesignTimeServices(services);
            services.AddEntityFrameworkSqlServerNetTopologySuite();
    }

    private static void ConfigureSqliteServices(IServiceCollection services)
    {
        var designTimeServices = new Microsoft.EntityFrameworkCore.Sqlite.Design.Internal.SqliteDesignTimeServices();
        designTimeServices.ConfigureDesignTimeServices(services);
            services.AddEntityFrameworkSqliteNetTopologySuite();
    }

    private static void ConfigureOracleServices(IServiceCollection services)
    {
        var designTimeServices = new Oracle.EntityFrameworkCore.Design.Internal.OracleDesignTimeServices();
        designTimeServices.ConfigureDesignTimeServices(services);
    }
}
