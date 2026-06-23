using EntityFrameworkCore.Generator.Sqlite.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.Sqlite.Tests;

[Collection(DatabaseCollection.CollectionName)]
public abstract class DatabaseTestBase(DatabaseFixture databaseFixture)
    : TestHostBase<DatabaseFixture>(databaseFixture)
{
}
