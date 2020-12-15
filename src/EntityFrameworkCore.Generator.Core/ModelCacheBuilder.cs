using System;
using System.IO;
using System.Linq;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.ModelCache;
using EntityFrameworkCore.Generator.Options;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EntityFrameworkCore.Generator
{
    public class ModelCacheBuilder : IModelCacheBuilder
    {
        public const string DefaultModelCacheFileName = "dbmodel.cache.json";

        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public ModelCacheBuilder(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<ModelCacheBuilder>();
        }

        public bool Refresh(GeneratorOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return RefreshFromScratch(options);
        }

        public DatabaseModel LoadFromCache(GeneratorOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var dir = Path.GetFullPath(options.Project.Directory);
            var ser = Path.Combine(dir, DefaultModelCacheFileName);

            _logger.LogInformation("Computed DB model cache file: {modelCacheFile}", ser);

            if (!File.Exists(ser))
            {
                throw new Exception("No cached database model found; did you generate it?");
            }

            var dbModel = Deserialize(File.ReadAllText(ser));

            return dbModel;
        }

        private bool RefreshFromScratch(GeneratorOptions options)
        {
            var services = new ServiceCollection();

            _logger.LogInformation("Adding EF Design-time Services");
            services.AddEntityFrameworkDesignTimeServices();

            IDesignTimeServices dts;
            // This part is DB-specific
            var provider = options.Database.Provider;
            dts = provider switch {
                DatabaseProviders.SqlServer =>
                    new Microsoft.EntityFrameworkCore.SqlServer.Design.Internal.SqlServerDesignTimeServices(),
                DatabaseProviders.PostgreSQL =>
                    new Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal.NpgsqlDesignTimeServices(),
                DatabaseProviders.MySQL =>
                    new Pomelo.EntityFrameworkCore.MySql.Design.Internal.MySqlDesignTimeServices(),
                DatabaseProviders.Sqlite =>
                    new Microsoft.EntityFrameworkCore.Sqlite.Design.Internal.SqliteDesignTimeServices(),

                // TODO: add support for user-provided custom implementation class for IDesignTimeServices

                _ => throw new NotSupportedException($"The specified provider '{provider}' is not supported."),
            };

            _logger.LogInformation("Configuring EF Design-time Services for provider: {provider}", provider);
            dts.ConfigureDesignTimeServices(services);

            var serviceProviders = services.BuildServiceProvider();
            var dbmFactory = serviceProviders.GetRequiredService<IDatabaseModelFactory>();
            var tmSource = serviceProviders.GetRequiredService<IRelationalTypeMappingSource>();

            var connectionString = ResolveConnectionString(options.Database);
            var dbmFactoryOptions = new DatabaseModelFactoryOptions(
                tables: options.Database.Tables, // null to include all
                schemas: options.Database.Schemas // null to include all
            );

            // Extract the Database Model
            _logger.LogInformation("Building DB Model");
            var dbModel = dbmFactory.Create(connectionString, dbmFactoryOptions);

            // Add a few of our own annotations for additional context
            dbModel.AddAnnotation("EFG:CreatedTime",
                DateTime.Now);
            dbModel.AddAnnotation("EFG:CLRVersion",
                System.Environment.Version);
            dbModel.AddAnnotation("EFG:OS",
                System.Environment.OSVersion);
            dbModel.AddAnnotation("EFG:CLI",
                System.Environment.CommandLine);
            dbModel.AddAnnotation("EFG:Args",
                System.Environment.GetCommandLineArgs().ToList());

            var dir = Path.GetFullPath(options.Project.Directory);
            var ser = Path.Combine(dir, DefaultModelCacheFileName);
            _logger.LogInformation("Computed DB model cache file: {modelCacheFile}", ser);

            Directory.CreateDirectory(dir);
            File.WriteAllText(ser, Serialize(dbModel));
            _logger.LogInformation("Serialized DB Model to cache file");

            return true;
        }

        public static string ResolveConnectionString(DatabaseOptions database)
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

        static JsonSerializerSettings Settings { get; }

        static ModelCacheBuilder()
        {
            Settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Include,
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Converters = {
                    KeyValueTuplesJsonConverter.Instance,
                    AnnotationsJsonConverter.Instance,
                },
                ContractResolver = AnnotationsContractResolver.Instance,
            };
        }

        public static string Serialize(DatabaseModel model)
        {
            return JsonConvert.SerializeObject(model, Settings);
        }

        public static DatabaseModel Deserialize(string ser)
        {
            return JsonConvert.DeserializeObject<DatabaseModel>(ser, Settings);
        }
    }
}