using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;

using Microsoft.Extensions.Logging.Abstractions;

using SchemaSaurus.Metadata;
using SchemaSaurus.Metadata.Provider;
using SchemaSaurus.PostgreSql;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Model;

/// <summary>
/// Base class for tests that inspect generated metadata and template output built from the
/// live PostgreSQL schema created for the integration suite. The schema is read once with
/// <see cref="PostgreSqlSchemaReader"/> and shared across the test collection; each test maps
/// the resulting <see cref="DatabaseModel"/> with its own <see cref="GeneratorOptions"/>.
/// </summary>
public abstract class ModelTestBase : DatabaseTestBase
{
    /// <summary>
    /// The default schema name for the PostgreSQL integration database.
    /// </summary>
    protected const string DefaultSchema = "public";

    private static readonly SemaphoreSlim _readerLock = new(1, 1);
    private static DatabaseModel? _databaseModel;

    protected ModelTestBase(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
    }

    /// <summary>
    /// Reads the live PostgreSQL schema once and caches it for the duration of the test run.
    /// </summary>
    protected async Task<DatabaseModel> GetDatabaseModelAsync()
    {
        if (_databaseModel is not null)
            return _databaseModel;

        await _readerLock.WaitAsync(TestCancellation);
        try
        {
            if (_databaseModel is not null)
                return _databaseModel;

            // touch the host so database migrations have run and the connection string is available
            _ = Services;

            var connectionString = Fixture.ConnectionString;
            Assert.False(string.IsNullOrWhiteSpace(connectionString));

            var reader = new PostgreSqlSchemaReader();
            var options = new SchemaReaderOptions { IncludeViews = true };

            _databaseModel = await reader.ReadAsync(connectionString!, options, TestCancellation);
            return _databaseModel;
        }
        finally
        {
            _readerLock.Release();
        }
    }

    /// <summary>
    /// Builds the generated <see cref="EntityContext"/> from the live schema using the supplied options.
    /// </summary>
    protected async Task<EntityContext> GenerateAsync(GeneratorOptions? options = null)
    {
        options ??= new GeneratorOptions();

        var databaseModel = await GetDatabaseModelAsync();
        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        return generator.Generate(options, databaseModel);
    }

    /// <summary>
    /// Resolves the generated entity for the given table, failing the test when it is missing.
    /// </summary>
    protected static Entity GetEntity(EntityContext context, string tableName, string schema = DefaultSchema)
    {
        var entity = context.Entities.ByTable(tableName, schema);
        Assert.NotNull(entity);
        return entity;
    }

    /// <summary>
    /// Resolves the generated property for the given column, failing the test when it is missing.
    /// </summary>
    protected static Property GetProperty(Entity entity, string columnName)
    {
        var property = entity.Properties.ByColumn(columnName);
        Assert.NotNull(property);
        return property;
    }
}
