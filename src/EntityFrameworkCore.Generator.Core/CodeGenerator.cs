using System;
using System.IO;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Parsing;
using EntityFrameworkCore.Generator.Templates;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
{
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

        public GeneratorOptions Options { get; set; }

        public bool Generate(GeneratorOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));

            var databaseProviders = GetDatabaseProviders();
            var databaseModel = GetDatabaseModel(databaseProviders.factory);

            if (databaseModel == null)
                throw new InvalidOperationException("Failed to create database model");

            _logger.LogInformation($"Loaded database model for: {databaseModel.DatabaseName}");

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
        }

        private void GenerateQueryExtensions(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                var directory = Options.Data.Query.Directory;
                var file = entity.EntityClass + "Extensions.cs";
                var path = Path.Combine(directory, file);

                _logger.LogInformation(File.Exists(path)
                    ? $"Updating query extensions class: {file}"
                    : $"Creating query extensions class: {file}");

                var template = new QueryExtensionTemplate(entity, Options);
                template.WriteCode(path);
            }

        }

        private void GenerateMappingClasses(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                var directory = Options.Data.Mapping.Directory;
                var file = entity.MappingClass + ".cs";
                var path = Path.Combine(directory, file);

                _logger.LogInformation(File.Exists(path)
                    ? $"Updating mapping class: {file}"
                    : $"Creating mapping class: {file}");

                var template = new MappingClassTemplate(entity, Options);
                template.WriteCode(path);
            }
        }

        private void GenerateEntityClasses(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                Options.Variables.Set("Entity.Name", entity.EntityClass);

                var directory = Options.Data.Entity.Directory;
                var file = entity.EntityClass + ".cs";
                var path = Path.Combine(directory, file);

                _logger.LogInformation(File.Exists(path)
                    ? $"Updating entity class: {file}"
                    : $"Creating entity class: {file}");

                var template = new EntityClassTemplate(entity, Options);
                template.WriteCode(path);
            }

            Options.Variables.Remove("Entity.Name");
        }

        private void GenerateDataContext(EntityContext entityContext)
        {

            var directory = Options.Data.Context.Directory;
            var file = entityContext.ContextClass + ".cs";
            var path = Path.Combine(directory, file);

            _logger.LogInformation(File.Exists(path)
                ? $"Updating data context class: {file}"
                : $"Creating data context class: {file}");

            var template = new DataContextTemplate(entityContext, Options);
            template.WriteCode(path);
        }


        private void GenerateModelClasses(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                Options.Variables.Set("Entity.Name", entity.EntityClass);
                if (entity.Models.Count <= 0)
                    continue;

                GenerateModelClasses(entity);
                GenerateValidatorClasses(entity);
                GenerateMapperClass(entity);
            }

            Options.Variables.Remove("Entity.Name");
        }


        private void GenerateModelClasses(Entity entity)
        {
            foreach (var model in entity.Models)
            {
                Options.Variables.Set("Model.Name", entity.EntityClass);

                var directory = GetModelDirectory(model);
                var file = model.ModelClass + ".cs";
                var path = Path.Combine(directory, file);

                _logger.LogInformation(File.Exists(path)
                    ? $"Updating model class: {file}"
                    : $"Creating model class: {file}");


                var template = new ModelClassTemplate(model, Options);
                template.WriteCode(path);
            }

            Options.Variables.Remove("Model.Name");
        }

        private string GetModelDirectory(Model model)
        {
            if (model.ModelType == ModelType.Create)
                return Options.Model.Create.Directory.HasValue()
                    ? Options.Model.Create.Directory
                    : Options.Model.Shared.Directory;

            if (model.ModelType == ModelType.Update)
                return Options.Model.Update.Directory.HasValue()
                    ? Options.Model.Update.Directory
                    : Options.Model.Shared.Directory;

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
                Options.Variables.Set("Model.Name", entity.EntityClass);

                // don't validate read models
                if (model.ModelType == ModelType.Read)
                    continue;

                var directory = Options.Model.Validator.Directory;
                var file = model.ValidatorClass + ".cs";
                var path = Path.Combine(directory, file);

                _logger.LogInformation(File.Exists(path)
                    ? $"Updating validation class: {file}"
                    : $"Creating validation class: {file}");

                var template = new ValidatorClassTemplate(model, Options);
                template.WriteCode(path);
            }

            Options.Variables.Remove("Model.Name");
        }


        private void GenerateMapperClass(Entity entity)
        {
            if (!Options.Model.Mapper.Generate)
                return;

            var directory = Options.Model.Mapper.Directory;
            var file = entity.MapperClass + ".cs";
            var path = Path.Combine(directory, file);

            _logger.LogInformation(File.Exists(path)
                ? $"Updating object mapper class: {file}"
                : $"Creating object mapper class: {file}");

            var template = new MapperClassTemplate(entity, Options);
            template.WriteCode(path);
        }


        private DatabaseModel GetDatabaseModel(IDatabaseModelFactory factory)
        {
            _logger.LogInformation("Loading database model ...");

            var database = Options.Database;
            var connectionString = ResolveConnectionString(database);
            var options = new DatabaseModelFactoryOptions(database.Tables, database.Schemas);

            return factory.Create(connectionString, options);
        }

        private string ResolveConnectionString(DatabaseOptions database)
        {
            if (database.ConnectionString.HasValue())
                return database.ConnectionString;

            if (database.UserSecretsId.HasValue())
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

            _logger.LogDebug($"Creating database model factory for: {provider}");

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


        private void ConfigureMySqlServices(IServiceCollection services)
        {
            var designTimeServices = new Pomelo.EntityFrameworkCore.MySql.Design.Internal.MySqlDesignTimeServices();
            designTimeServices.ConfigureDesignTimeServices(services);
        }
        
        private void ConfigurePostgresServices(IServiceCollection services)
        {
            var designTimeServices = new Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal.NpgsqlDesignTimeServices();
            designTimeServices.ConfigureDesignTimeServices(services);
        }

        private void ConfigureSqlServerServices(IServiceCollection services)
        {
            var designTimeServices = new Microsoft.EntityFrameworkCore.SqlServer.Design.Internal.SqlServerDesignTimeServices();
            designTimeServices.ConfigureDesignTimeServices(services);
        }

        private void ConfigureSqliteServices(IServiceCollection services)
        {
            var designTimeServices = new Microsoft.EntityFrameworkCore.Sqlite.Design.Internal.SqliteDesignTimeServices();
            designTimeServices.ConfigureDesignTimeServices(services);
        }
    }
}
