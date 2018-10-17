using System;
using System.Diagnostics;
using System.IO;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly ILogger _logger;
        private readonly IDiagnosticsLogger<DbLoggerCategory.Scaffolding> _diagnosticsLogger;
        private readonly ModelGenerator _modelGenerator;

        public CodeGenerator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CodeGenerator>();
            _diagnosticsLogger = new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(loggerFactory, new LoggingOptions(), new DiagnosticListener(""));
            _modelGenerator = new ModelGenerator(loggerFactory);
        }

        public GeneratorOptions Options { get; set; }

        public bool Generate(GeneratorOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));

            var factory = GetDatabaseModelFactory();
            var databaseModel = GetDatabaseModel(factory);

            if (databaseModel == null)
                throw new InvalidOperationException("Failed to create database model");

            // update database variables
            Options.Database.Name = databaseModel.DatabaseName;

            _logger.LogTrace($"Loaded database model for: {databaseModel.DatabaseName}");

            var context = _modelGenerator.Generate(Options, databaseModel);
            GenerateFiles(context);

            return true;
        }

        private void GenerateFiles(EntityContext entityContext)
        {
            GenerateDataContext(entityContext);
            GenerateEntityClasses(entityContext);
            GenerateMappingClasses(entityContext);
        }

        private void GenerateMappingClasses(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                var directory = NameFormatter.Format(Options.Data.Mapping.Directory, Options);
                var file = entity.MappingClass + ".cs";
                var path = Path.Combine(directory, file);

                var template = new MappingClassTemplate(entity);
                template.WriteCode(path);
            }
        }

        private void GenerateEntityClasses(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                var directory = NameFormatter.Format(Options.Data.Entity.Directory, Options);
                var file = entity.EntityClass + ".cs";
                var path = Path.Combine(directory, file);

                var template = new EntityClassTemplate(entity);
                template.WriteCode(path);
            }
        }

        private void GenerateDataContext(EntityContext entityContext)
        {
            var directory = NameFormatter.Format(Options.Data.Context.Directory, Options);
            var file = entityContext.ContextClass + ".cs";
            var path = Path.Combine(directory, file);

            var template = new DataContextTemplate(entityContext);
            template.WriteCode(path);
        }

        private DatabaseModel GetDatabaseModel(IDatabaseModelFactory factory)
        {
            _logger.LogTrace("Creating database model ...");

            var database = Options.Database;
            return factory.Create(database.ConnectionString, database.Tables, database.Schemas);
        }

        private IDatabaseModelFactory GetDatabaseModelFactory()
        {
            var provider = Options.Database.Provider;

            _logger.LogTrace($"Creating database model factory for: {provider}");
            if (provider == DatabaseProviders.SqlServer)
                return new Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal.SqlServerDatabaseModelFactory(_diagnosticsLogger);

            if (provider == DatabaseProviders.PostgreSQL)
                return new Npgsql.EntityFrameworkCore.PostgreSQL.Scaffolding.Internal.NpgsqlDatabaseModelFactory(_diagnosticsLogger);

            //if (Options.Database.Provider == DatabaseProviders.Sqlite)
            //    return new Microsoft.EntityFrameworkCore.Sqlite.Scaffolding.Internal.SqliteDatabaseModelFactory(_diagnosticsLogger, null);

            throw new NotSupportedException($"The specified provider '{provider}' is not supported.");
        }
    }
}
