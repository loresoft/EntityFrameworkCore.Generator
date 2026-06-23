// support capturing console and trace output in xunit v3
[assembly: CaptureConsole]
[assembly: CaptureTrace]

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;

[CollectionDefinition(CollectionName)]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    public const string CollectionName = nameof(DatabaseCollection);
}
