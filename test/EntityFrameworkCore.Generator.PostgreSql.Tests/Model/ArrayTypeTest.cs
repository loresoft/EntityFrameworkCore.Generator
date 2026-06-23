using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;
using EntityFrameworkCore.Generator.Templates;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Model;

/// <summary>
/// Verifies PostgreSQL array column mapping using the <c>DataType</c> array columns
/// (<c>IntegerArray</c>, <c>TextArray</c>, <c>NullableVarcharArray</c>) added for the integration suite.
/// Array support is the PostgreSQL specific data shape, analogous to temporal tables on SQL Server.
/// </summary>
public class ArrayTypeTest : ModelTestBase
{
    public ArrayTypeTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
    }

    [Fact]
    public async Task ArrayColumnsMapToArraySystemTypes()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "DataType");

        var integerArray = GetProperty(entity, "IntegerArray");
        Assert.Equal(typeof(int[]), integerArray.SystemType);
        Assert.Equal("int[]", integerArray.SystemTypeName);
        Assert.False(integerArray.IsNullable);

        var textArray = GetProperty(entity, "TextArray");
        Assert.Equal(typeof(string[]), textArray.SystemType);
        Assert.Equal("string[]", textArray.SystemTypeName);
        Assert.False(textArray.IsNullable);

        var nullableArray = GetProperty(entity, "NullableVarcharArray");
        Assert.Equal(typeof(string[]), nullableArray.SystemType);
        Assert.True(nullableArray.IsNullable);
    }

    [Fact]
    public async Task ArrayColumnsRenderExpectedPropertyDeclarations()
    {
        var options = new GeneratorOptions();
        options.Project.Nullable = true;

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "DataType");

        var entityCode = new EntityClassTemplate(entity, options).WriteCode();

        Assert.Contains("public int[] IntegerArray { get; set; } = null!;", entityCode);
        Assert.Contains("public string[] TextArray { get; set; } = null!;", entityCode);
        Assert.Contains("public string[]? NullableVarcharArray { get; set; }", entityCode);
    }

    [Fact]
    public async Task ConfiguredTypeMappingOverridesArraySystemType()
    {
        var options = new GeneratorOptions();
        options.Project.Nullable = true;
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = "text[]",
            SystemType = "List<string>"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "DataType");

        Assert.Equal("List<string>", GetProperty(entity, "TextArray").SystemTypeName);

        var entityCode = new EntityClassTemplate(entity, options).WriteCode();
        Assert.Contains("public List<string> TextArray { get; set; } = null!;", entityCode);
    }
}
