using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.PostgreSql.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Model;

/// <summary>
/// Verifies PostgreSQL specific metadata flags produced for generated entities: identity, primary
/// key, required/nullable, unique constraints, foreign keys, relationship cardinality, and the
/// <c>xid</c> row version mapping. Uses the <c>User</c> and <c>UserRole</c> integration tables.
/// </summary>
public class MetadataTest : ModelTestBase
{
    public MetadataTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
    }

    [Fact]
    public async Task IdentityColumnIsFlaggedAsIdentityPrimaryKey()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "User");

        var id = GetProperty(entity, "Id");
        Assert.True(id.IsIdentity);
        Assert.True(id.IsPrimaryKey);
        Assert.False(id.IsNullable);
    }

    [Fact]
    public async Task NullableAndRequiredColumnsAreFlagged()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "User");

        var userName = GetProperty(entity, "UserName");
        Assert.False(userName.IsNullable);
        Assert.True(userName.IsRequired);

        var firstName = GetProperty(entity, "FirstName");
        Assert.True(firstName.IsNullable);
        Assert.True(firstName.IsOptional);
    }

    [Fact]
    public async Task UniqueConstraintColumnIsFlaggedUnique()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "User");

        // EmailAddress is covered by a unique index/constraint
        Assert.True(GetProperty(entity, "EmailAddress").IsUnique);

        // a non-indexed column is not unique
        Assert.NotEqual(true, GetProperty(entity, "DisplayName").IsUnique);
    }

    [Fact]
    public async Task CompositePrimaryKeyColumnsAreFlagged()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "UserRole");

        Assert.True(GetProperty(entity, "UserId").IsPrimaryKey);
        Assert.True(GetProperty(entity, "RoleId").IsPrimaryKey);

        var primaryKeys = entity.Properties.PrimaryKeys.Select(p => p.ColumnName).ToList();
        Assert.Equal(2, primaryKeys.Count);
        Assert.Contains("UserId", primaryKeys);
        Assert.Contains("RoleId", primaryKeys);
    }

    [Fact]
    public async Task ForeignKeyColumnsAreFlagged()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "UserRole");

        Assert.True(GetProperty(entity, "UserId").IsForeignKey);
        Assert.True(GetProperty(entity, "RoleId").IsForeignKey);
    }

    [Fact]
    public async Task XidRowVersionColumnMapsToUnsignedInteger()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "User");

        // PostgreSQL emulates row version with the xid system type, mapped to uint and not flagged
        // as a row version / concurrency token (unlike SQL Server's rowversion)
        var rowVersion = GetProperty(entity, "RowVersion");
        Assert.Equal(typeof(uint), rowVersion.SystemType);
        Assert.NotEqual(true, rowVersion.IsRowVersion);
        Assert.NotEqual(true, rowVersion.IsConcurrencyToken);
    }

    [Fact]
    public async Task ForeignKeyRelationshipsHaveExpectedCardinality()
    {
        var context = await GenerateAsync();

        var userRole = GetEntity(context, "UserRole");

        // dependent side: required (NOT NULL) foreign key to User
        var toUser = userRole.Relationships
            .Single(r => r.IsForeignKey && r.PrimaryEntity.EntityClass == "User");
        Assert.Equal(Cardinality.One, toUser.Cardinality);
        Assert.Contains(toUser.Properties, p => p.ColumnName == "UserId");

        // principal side: User has a collection of UserRole
        var user = GetEntity(context, "User");
        var fromUserRole = user.Relationships
            .Single(r => !r.IsForeignKey && r.PrimaryEntity.EntityClass == "UserRole");
        Assert.Equal(Cardinality.Many, fromUserRole.Cardinality);
    }
}
