using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Model;

/// <summary>
/// Verifies <see cref="EntityClassOptions.SystemTypeAnnotation"/> behavior using column annotations
/// surfaced by the PostgreSQL schema reader. The reader exposes an <c>NpgsqlDbType</c> annotation on
/// every column, which is used here as a live system type override source.
/// </summary>
public class AnnotationTypeMappingTest : ModelTestBase
{
    public AnnotationTypeMappingTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
    }

    [Fact]
    public async Task DefaultSystemTypeAnnotationIsAbsentSoLiveTypeIsUsed()
    {
        // the default annotation name ("Generator:SystemType") is not present on PostgreSQL columns
        var context = await GenerateAsync();
        var entity = GetEntity(context, "User");

        Assert.Equal("string", GetProperty(entity, "EmailAddress").SystemTypeName);
    }

    [Fact]
    public async Task CustomSystemTypeAnnotationNameIsHonored()
    {
        var options = new GeneratorOptions();
        options.Data.Entity.SystemTypeAnnotation = "NpgsqlDbType";

        var context = await GenerateAsync(options);

        // the live NpgsqlDbType annotation value overrides the generated system type name
        Assert.Equal("Varchar", GetProperty(GetEntity(context, "User"), "EmailAddress").SystemTypeName);
        Assert.Equal("Uuid", GetProperty(GetEntity(context, "DataType"), "Guid").SystemTypeName);
    }

    [Fact]
    public async Task SystemTypeAnnotationTakesPrecedenceOverTypeMapping()
    {
        // resolve the live native type of the column so the type mapping targets it directly
        var baseline = await GenerateAsync();
        var emailNativeType = GetProperty(GetEntity(baseline, "User"), "EmailAddress").NativeType;
        Assert.False(string.IsNullOrEmpty(emailNativeType));

        var options = new GeneratorOptions();
        options.Data.Entity.SystemTypeAnnotation = "NpgsqlDbType";
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = emailNativeType!,
            SystemType = "ShouldNotWin"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "User");

        // the column annotation should win over a matching configured type mapping
        Assert.Equal("Varchar", GetProperty(entity, "EmailAddress").SystemTypeName);
    }

    [Fact]
    public async Task SystemTypeAnnotationFallsBackToTypeMappingThenLiveType()
    {
        var baseline = await GenerateAsync();
        var emailNativeType = GetProperty(GetEntity(baseline, "User"), "EmailAddress").NativeType;
        Assert.False(string.IsNullOrEmpty(emailNativeType));

        var options = new GeneratorOptions();
        // point the annotation lookup at a name that is not present on any column
        options.Data.Entity.SystemTypeAnnotation = "Missing:SystemType";
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = emailNativeType!,
            SystemType = "MappedVarchar"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "User");

        // no annotation match -> configured type mapping is used
        Assert.Equal("MappedVarchar", GetProperty(entity, "EmailAddress").SystemTypeName);

        // no annotation match and no type mapping -> falls back to the live system type
        Assert.Equal("int", GetProperty(entity, "Id").SystemTypeName);
    }
}
