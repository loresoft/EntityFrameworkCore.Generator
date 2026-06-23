using System.Data;

using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.SqlServer.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.SqlServer.Tests.Model;

/// <summary>
/// Verifies temporal table metadata and period column handling using the <c>TemporalTask</c>
/// system-versioned table (history table <c>TemporalTaskHistory</c>, period columns
/// <c>ValidFrom</c>/<c>ValidTo</c>) created for the integration suite.
/// </summary>
public class TemporalTest : ModelTestBase
{
    public TemporalTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
    }

    [Fact]
    public async Task TemporalMetadataIsPopulatedWhenMappingEnabled()
    {
        // temporal mapping is enabled by default
        var context = await GenerateAsync();
        var entity = GetEntity(context, "TemporalTask");

        Assert.Equal("TemporalTaskHistory", entity.TemporalTableName);
        Assert.Equal("dbo", entity.TemporalTableSchema);

        Assert.Equal("ValidFrom", entity.TemporalStartColumn);
        Assert.Equal("ValidTo", entity.TemporalEndColumn);

        Assert.Equal("ValidFrom", entity.TemporalStartProperty);
        Assert.Equal("ValidTo", entity.TemporalEndProperty);
    }

    [Fact]
    public async Task PeriodColumnsAreGeneratedWhenTemporalMappingDisabled()
    {
        var options = new GeneratorOptions();
        options.Data.Mapping.Temporal = false;

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "TemporalTask");

        // entity-level temporal metadata is not populated when mapping is disabled
        Assert.Null(entity.TemporalTableName);

        // period columns are emitted as computed properties instead
        var validFrom = GetProperty(entity, "ValidFrom");
        Assert.True(validFrom.IsComputed);
        Assert.Equal(typeof(DateTime), validFrom.SystemType);
        Assert.Equal(DbType.DateTime2, validFrom.DataType);

        var validTo = GetProperty(entity, "ValidTo");
        Assert.True(validTo.IsComputed);
        Assert.Equal(typeof(DateTime), validTo.SystemType);
        Assert.Equal(DbType.DateTime2, validTo.DataType);
    }
}
