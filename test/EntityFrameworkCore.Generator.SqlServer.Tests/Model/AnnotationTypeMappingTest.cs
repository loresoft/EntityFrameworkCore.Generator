using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.SqlServer.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.SqlServer.Tests.Model;

/// <summary>
/// Verifies the <see cref="EntityClassOptions.SystemTypeAnnotation"/> behavior using SQL Server
/// extended properties surfaced as column annotations by the schema reader. The integration suite
/// configures a <c>Generator:SystemType</c> annotation on <c>StringListUsage.AnnotatedValues</c> and
/// a <c>GeneratorTest:IsSensitive</c> annotation on <c>User.EmailAddress</c>.
/// </summary>
public class AnnotationTypeMappingTest : ModelTestBase
{
    public AnnotationTypeMappingTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
    }

    [Fact]
    public async Task DefaultSystemTypeAnnotationOverridesGeneratedType()
    {
        // default SystemTypeAnnotation is "Generator:SystemType"
        var context = await GenerateAsync();
        var entity = GetEntity(context, "StringListUsage");

        var annotated = GetProperty(entity, "AnnotatedValues");
        Assert.Equal("string[]", annotated.SystemTypeName);
    }

    [Fact]
    public async Task SystemTypeAnnotationTakesPrecedenceOverTypeMapping()
    {
        // resolve the live native type of the annotated column so the type mapping targets it directly
        var baseline = await GenerateAsync();
        var annotatedNativeType = GetProperty(GetEntity(baseline, "StringListUsage"), "AnnotatedValues").NativeType;
        Assert.False(string.IsNullOrEmpty(annotatedNativeType));

        var options = new GeneratorOptions();
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = annotatedNativeType!,
            SystemType = "ShouldNotWin"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "StringListUsage");

        // the column annotation should win over a matching configured type mapping
        Assert.Equal("string[]", GetProperty(entity, "AnnotatedValues").SystemTypeName);
    }

    [Fact]
    public async Task SystemTypeAnnotationFallsBackToTypeMappingThenLiveType()
    {
        var options = new GeneratorOptions();
        // point the annotation lookup at a name that is not present on any column
        options.Data.Entity.SystemTypeAnnotation = "Missing:SystemType";
        options.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = "dbo.StringList",
            SystemType = "List<string?>"
        });

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "StringListUsage");

        // no annotation match -> configured type mapping is used
        Assert.Equal("List<string?>", GetProperty(entity, "Values").SystemTypeName);

        // no annotation match and no type mapping -> falls back to the live system type
        Assert.Equal("string", GetProperty(entity, "AnnotatedValues").SystemTypeName);
    }

    [Fact]
    public async Task CustomSystemTypeAnnotationNameIsHonored()
    {
        var options = new GeneratorOptions();
        options.Data.Entity.SystemTypeAnnotation = "GeneratorTest:IsSensitive";

        var context = await GenerateAsync(options);
        var entity = GetEntity(context, "User");

        // EmailAddress carries the custom extended property used as the system type override
        Assert.Equal("True", GetProperty(entity, "EmailAddress").SystemTypeName);

        // a column without that annotation falls back to its live system type
        Assert.Equal("string", GetProperty(entity, "UserName").SystemTypeName);
    }
}
