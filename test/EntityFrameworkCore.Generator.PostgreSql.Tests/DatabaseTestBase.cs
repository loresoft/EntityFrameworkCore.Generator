using EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests;

[Collection(DatabaseCollection.CollectionName)]
public abstract class DatabaseTestBase(DatabaseFixture databaseFixture)
    : TestHostBase<DatabaseFixture>(databaseFixture)
{
}
