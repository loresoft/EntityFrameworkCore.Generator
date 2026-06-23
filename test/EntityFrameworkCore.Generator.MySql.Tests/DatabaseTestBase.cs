using EntityFrameworkCore.Generator.MySql.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.MySql.Tests;

[Collection(DatabaseCollection.CollectionName)]
public abstract class DatabaseTestBase(DatabaseFixture databaseFixture)
    : TestHostBase<DatabaseFixture>(databaseFixture)
{
}
