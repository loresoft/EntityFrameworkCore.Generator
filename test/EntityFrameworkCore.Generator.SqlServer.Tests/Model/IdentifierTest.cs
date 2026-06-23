using System.Text.RegularExpressions;

using EntityFrameworkCore.Generator.SqlServer.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.SqlServer.Tests.Model;

/// <summary>
/// Verifies that SQL Server identifier edge cases are sanitized into legal C# names. Uses the
/// <c>Table1 $ Test</c> table (columns include <c>Test$</c>, <c>Blah #</c>, <c>1stNumber</c>,
/// <c>123Street</c>, and <c>123 Test 123</c>) created for the integration suite.
/// </summary>
public partial class IdentifierTest : ModelTestBase
{
    public IdentifierTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
    }

    [Fact]
    public async Task SpecialTableNameProducesLegalEntityClass()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "Table1 $ Test");

        Assert.Matches(IdentifierRegex(), entity.EntityClass);
    }

    [Fact]
    public async Task SpecialColumnNamesProduceLegalPropertyNames()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "Table1 $ Test");

        Assert.NotEmpty(entity.Properties);
        foreach (var property in entity.Properties)
            Assert.Matches(IdentifierRegex(), property.PropertyName);

        // generated property names must remain unique within the entity
        var propertyNames = entity.Properties.Select(p => p.PropertyName).ToList();
        Assert.Equal(propertyNames.Count, propertyNames.Distinct().Count());
    }

    [Fact]
    public async Task PrimaryKeyColumnWithTrailingSymbolIsSanitized()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "Table1 $ Test");

        var key = GetProperty(entity, "Test$");
        Assert.Equal("Test", key.PropertyName);
        Assert.True(key.IsPrimaryKey);
    }

    [Fact]
    public async Task ColumnWithLeadingDigitsIsSanitized()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "Table1 $ Test");

        // leading digits are stripped to form a legal identifier
        Assert.Equal("Street", GetProperty(entity, "123Street").PropertyName);
    }

    [GeneratedRegex("^[A-Za-z_][A-Za-z0-9_]*$")]
    private static partial Regex IdentifierRegex();
}
