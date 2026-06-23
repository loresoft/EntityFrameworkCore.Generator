using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.SqlServer.Tests.Fixtures;
using EntityFrameworkCore.Generator.Templates;

namespace EntityFrameworkCore.Generator.SqlServer.Tests.Model;

/// <summary>
/// Verifies live SQL Server type mapping and configured <see cref="TypeMappingOptions"/> coverage
/// using the <c>SqlTypes</c> and <c>StringListUsage</c> tables created for the integration suite.
/// </summary>
public class TypeMappingTest : ModelTestBase
{
    public TypeMappingTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
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
        Assert.Equal(typeof(DateTime), GetProperty(entity, "DateTime").SystemType);
        Assert.Equal(typeof(decimal), GetProperty(entity, "Decimal").SystemType);
        Assert.Equal(typeof(double), GetProperty(entity, "Float").SystemType);
        Assert.Equal(typeof(int), GetProperty(entity, "Int").SystemType);
        Assert.Equal(typeof(decimal), GetProperty(entity, "Money").SystemType);
        Assert.Equal(typeof(string), GetProperty(entity, "NChar").SystemType);
        Assert.Equal(typeof(string), GetProperty(entity, "NVarChar").SystemType);
        Assert.Equal(typeof(float), GetProperty(entity, "Real").SystemType);
        Assert.Equal(typeof(short), GetProperty(entity, "SmallInt").SystemType);
        Assert.Equal(typeof(decimal), GetProperty(entity, "SmallMoney").SystemType);
        Assert.Equal(typeof(byte), GetProperty(entity, "TinyInt").SystemType);
        Assert.Equal(typeof(Guid), GetProperty(entity, "UniqueIdentifier").SystemType);
        Assert.Equal(typeof(byte[]), GetProperty(entity, "VarBinary").SystemType);
        Assert.Equal(typeof(string), GetProperty(entity, "VarChar").SystemType);
        Assert.Equal(typeof(string), GetProperty(entity, "Xml").SystemType);
    }

    [Fact]
    public async Task LiveTypeMappingRendersFriendlySystemTypeNames()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "SqlTypes");

        Assert.Equal("long", GetProperty(entity, "BigInt").SystemTypeName);
        Assert.Equal("bool", GetProperty(entity, "Bit").SystemTypeName);
        Assert.Equal("decimal", GetProperty(entity, "Money").SystemTypeName);
        Assert.Equal("short", GetProperty(entity, "SmallInt").SystemTypeName);
        Assert.Equal("byte", GetProperty(entity, "TinyInt").SystemTypeName);
        Assert.Equal("Guid", GetProperty(entity, "UniqueIdentifier").SystemTypeName);
        Assert.Equal("byte[]", GetProperty(entity, "VarBinary").SystemTypeName);
    }

    [Fact]
    public async Task ConfiguredTypeMappingOverridesGeneratedSystemType()
    {
        var options = new GeneratorOptions();
        options.Project.Nullable = true;
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = "dbo.StringList",
            SystemType = "List<string?>"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "StringListUsage");

        var values = GetProperty(entity, "Values");
        Assert.Equal("List<string?>", values.SystemTypeName);

        // the configured system type should flow through to generated entity code
        var entityCode = new EntityClassTemplate(entity, options).WriteCode();
        Assert.Contains("public List<string?>? Values { get; set; }", entityCode);
    }

    [Fact]
    public async Task ConfiguredTypeMappingIsCaseInsensitiveOnNativeType()
    {
        var options = new GeneratorOptions();
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = "DBO.STRINGLIST",
            SystemType = "List<string>"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "StringListUsage");

        Assert.Equal("List<string>", GetProperty(entity, "Values").SystemTypeName);
    }
}
