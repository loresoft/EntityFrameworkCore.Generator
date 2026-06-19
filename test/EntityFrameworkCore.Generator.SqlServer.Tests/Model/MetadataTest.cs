using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.SqlServer.Tests.Fixtures;

namespace EntityFrameworkCore.Generator.SqlServer.Tests.Model;

/// <summary>
/// Verifies SQL Server specific metadata flags produced for generated entities: identity, primary
/// key, required/nullable, unique indexes, foreign keys, row version/concurrency, and relationship
/// cardinality. Uses the <c>User</c>, <c>UserRole</c>, and <c>SqlTypes</c> integration tables.
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
    public async Task UniqueIndexColumnIsFlaggedUnique()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "User");

        // EmailAddress is covered by the unique index UX_User_EmailAddress
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
    public async Task RowVersionColumnIsFlaggedAsConcurrencyToken()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "User");

        var rowVersion = GetProperty(entity, "RowVersion");
        Assert.True(rowVersion.IsRowVersion);
        Assert.True(rowVersion.IsConcurrencyToken);
        Assert.Equal(typeof(byte[]), rowVersion.SystemType);
    }

    [Fact]
    public async Task ForeignKeyRelationshipsHaveExpectedCardinality()
    {
        var context = await GenerateAsync();

        var userRole = GetEntity(context, "UserRole");

        // dependent side: required (NOT NULL) foreign key to User
        var toUser = userRole.Relationships
            .Single(r => r.IsForeignKey && r.PrimaryEntity.EntityClass == "User");

        Assert.Equal("FK_UserRole_User_UserId", toUser.RelationshipName);
        Assert.Equal(Cardinality.One, toUser.Cardinality);
        Assert.Contains(toUser.Properties, p => p.ColumnName == "UserId");

        // principal side: User has a collection of UserRole
        var user = GetEntity(context, "User");
        var fromUserRole = user.Relationships
            .Single(r => !r.IsForeignKey && r.PrimaryEntity.EntityClass == "UserRole");

        Assert.Equal(Cardinality.Many, fromUserRole.Cardinality);
    }

    [Fact]
    public async Task BooleanDefaultValueIsParsed()
    {
        var context = await GenerateAsync();
        var entity = GetEntity(context, "User");

        // IsEmailAddressConfirmed defaults to 0 (false) in the schema
        var confirmed = GetProperty(entity, "IsEmailAddressConfirmed");
        Assert.Equal(typeof(bool), confirmed.SystemType);
        Assert.Equal(false, confirmed.DefaultValue);
    }
}
