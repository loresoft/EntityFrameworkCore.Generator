using System;

using Xunit;
using Xunit.Abstractions;

namespace FluentCommand.SqlServer.Tests;

[Collection(DatabaseCollection.CollectionName)]
public abstract class DatabaseTestBase : IDisposable
{
    protected DatabaseTestBase(ITestOutputHelper output, DatabaseFixture databaseFixture)
    {
        Output = output;
        Database = databaseFixture;
    }


    public ITestOutputHelper Output { get; }

    public DatabaseFixture Database { get; }

    public void Dispose()
    {
        Database?.Report(Output);
    }
}