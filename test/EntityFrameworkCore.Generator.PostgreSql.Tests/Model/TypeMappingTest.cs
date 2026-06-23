using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Model;

/// <summary>
/// Verifies live PostgreSQL type mapping and configured <see cref="TypeMappingOptions"/> coverage
/// using the <c>DataType</c> and <c>SqlTypes</c> tables created for the integration suite.
/// </summary>
public class TypeMappingTest : ModelTestBase
{
    public TypeMappingTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
    }

    [Fact]
    public async Task LiveTypeMappingMapsDataTypeColumnsToExpectedSystemTypes()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "DataType");

        Assert.Equal(typeof(long), GetProperty(entity, "Id").SystemType);
        Assert.Equal(typeof(string), GetProperty(entity, "Name").SystemType);
        Assert.Equal(typeof(bool), GetProperty(entity, "Boolean").SystemType);
        Assert.Equal(typeof(short), GetProperty(entity, "Short").SystemType);
        Assert.Equal(typeof(long), GetProperty(entity, "Long").SystemType);
        Assert.Equal(typeof(float), GetProperty(entity, "Float").SystemType);
        Assert.Equal(typeof(double), GetProperty(entity, "Double").SystemType);
        Assert.Equal(typeof(decimal), GetProperty(entity, "Decimal").SystemType);
        Assert.Equal(typeof(DateTime), GetProperty(entity, "DateTime").SystemType);
        Assert.Equal(typeof(DateTimeOffset), GetProperty(entity, "DateTimeOffset").SystemType);
        Assert.Equal(typeof(Guid), GetProperty(entity, "Guid").SystemType);
        Assert.Equal(typeof(DateOnly), GetProperty(entity, "DateOnly").SystemType);
        Assert.Equal(typeof(TimeOnly), GetProperty(entity, "TimeOnly").SystemType);
    }

    [Fact]
    public async Task LiveTypeMappingMapsSqlTypesToExpectedSystemTypes()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "SqlTypes");

        Assert.Equal(typeof(int), GetProperty(entity, "Id").SystemType);
        Assert.Equal(typeof(long), GetProperty(entity, "BigInt").SystemType);
        Assert.Equal(typeof(byte[]), GetProperty(entity, "Binary").SystemType);
        Assert.Equal(typeof(bool), GetProperty(entity, "Bit").SystemType);
        Assert.Equal(typeof(string), GetProperty(entity, "Char").SystemType);
        Assert.Equal(typeof(decimal), GetProperty(entity, "Money").SystemType);
        Assert.Equal(typeof(Guid), GetProperty(entity, "UniqueIdentifier").SystemType);
        Assert.Equal(typeof(byte[]), GetProperty(entity, "VarBinary").SystemType);
    }

    [Fact]
    public async Task LiveTypeMappingRendersFriendlySystemTypeNames()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "DataType");

        Assert.Equal("long", GetProperty(entity, "Long").SystemTypeName);
        Assert.Equal("bool", GetProperty(entity, "Boolean").SystemTypeName);
        Assert.Equal("short", GetProperty(entity, "Short").SystemTypeName);
        Assert.Equal("decimal", GetProperty(entity, "Decimal").SystemTypeName);
        Assert.Equal("Guid", GetProperty(entity, "Guid").SystemTypeName);
        Assert.Equal("DateTimeOffset", GetProperty(entity, "DateTimeOffset").SystemTypeName);
    }

    [Fact]
    public async Task ConfiguredTypeMappingOverridesGeneratedSystemType()
    {
        var options = new GeneratorOptions();
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = "uuid",
            SystemType = "Ulid"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "DataType");

        Assert.Equal("Ulid", GetProperty(entity, "Guid").SystemTypeName);
    }

    [Fact]
    public async Task ConfiguredTypeMappingIsCaseInsensitiveOnNativeType()
    {
        var options = new GeneratorOptions();
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = "UUID",
            SystemType = "Ulid"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "DataType");

        Assert.Equal("Ulid", GetProperty(entity, "Guid").SystemTypeName);
    }
}
