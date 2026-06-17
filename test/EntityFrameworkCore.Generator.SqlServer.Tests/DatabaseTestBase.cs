using EntityFrameworkCore.Generator.SqlServer.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.SqlServer.Tests;

[Collection(DatabaseCollection.CollectionName)]
public abstract class DatabaseTestBase(DatabaseFixture databaseFixture)
    : TestHostBase<DatabaseFixture>(databaseFixture)
{
}
