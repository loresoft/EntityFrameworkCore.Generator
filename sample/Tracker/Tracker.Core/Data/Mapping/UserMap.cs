using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Configures Entity Framework Core mapping for the <see cref="Tracker.Core.Data.Entities.User" /> entity mapped to the <c>dbo.User</c> table.
/// </summary>
public partial class UserMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.User>
{
    /// <summary>
    /// Configures the table, key, property, and relationship mappings for <see cref="Tracker.Core.Data.Entities.User" />.
    /// </summary>
    /// <param name="builder">The builder used to configure <see cref="Tracker.Core.Data.Entities.User" />.</param>
    public void Configure(EntityTypeBuilder<Tracker.Core.Data.Entities.User> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("User", "dbo");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(t => t.EmailAddress)
            .IsRequired()
            .HasColumnName("EmailAddress")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.IsEmailAddressConfirmed)
            .IsRequired()
            .HasColumnName("IsEmailAddressConfirmed")
            .HasColumnType("bit")
            .HasDefaultValueSql("((0))");

        builder.Property(t => t.DisplayName)
            .IsRequired()
            .HasColumnName("DisplayName")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.ResetHash)
            .HasColumnName("ResetHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.InviteHash)
            .HasColumnName("InviteHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.AccessFailedCount)
            .IsRequired()
            .HasColumnName("AccessFailedCount")
            .HasColumnType("int")
            .HasDefaultValueSql("((0))");

        builder.Property(t => t.LockoutEnabled)
            .IsRequired()
            .HasColumnName("LockoutEnabled")
            .HasColumnType("bit")
            .HasDefaultValueSql("((0))");

        builder.Property(t => t.LockoutEnd)
            .HasColumnName("LockoutEnd")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.LastLogin)
            .HasColumnName("LastLogin")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasColumnName("IsDeleted")
            .HasColumnType("bit")
            .HasDefaultValueSql("((0))");

        builder.Property(t => t.Created)
            .IsRequired()
            .HasColumnName("Created")
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.CreatedBy)
            .HasColumnName("CreatedBy")
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.Updated)
            .IsRequired()
            .HasColumnName("Updated")
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.UpdatedBy)
            .HasColumnName("UpdatedBy")
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.RowVersion)
            .IsRequired()
            .IsRowVersion()
            .IsConcurrencyToken()
            .HasColumnName("RowVersion")
            .HasColumnType("rowversion")
            .ValueGeneratedOnAddOrUpdate();

        // relationships
        #endregion
    }

    #region Generated Constants
    /// <summary>
    /// Contains table mapping constants for <see cref="Tracker.Core.Data.Entities.User" />.
    /// </summary>
    public readonly struct Table
    {
        /// <summary>
        /// The database schema name for <see cref="Tracker.Core.Data.Entities.User" />.
        /// </summary>
        public const string Schema = "dbo";
        /// <summary>
        /// The database table name for <see cref="Tracker.Core.Data.Entities.User" />.
        /// </summary>
        public const string Name = "User";
    }

    /// <summary>
    /// Contains column name constants for <see cref="Tracker.Core.Data.Entities.User" /> properties.
    /// </summary>
    public readonly struct Columns
    {
        /// <summary>
        /// The <c>Id</c> column name for <see cref="Tracker.Core.Data.Entities.User.Id" />.
        /// </summary>
        public const string Id = "Id";
        /// <summary>
        /// The <c>EmailAddress</c> column name for <see cref="Tracker.Core.Data.Entities.User.EmailAddress" />.
        /// </summary>
        public const string EmailAddress = "EmailAddress";
        /// <summary>
        /// The <c>IsEmailAddressConfirmed</c> column name for <see cref="Tracker.Core.Data.Entities.User.IsEmailAddressConfirmed" />.
        /// </summary>
        public const string IsEmailAddressConfirmed = "IsEmailAddressConfirmed";
        /// <summary>
        /// The <c>DisplayName</c> column name for <see cref="Tracker.Core.Data.Entities.User.DisplayName" />.
        /// </summary>
        public const string DisplayName = "DisplayName";
        /// <summary>
        /// The <c>PasswordHash</c> column name for <see cref="Tracker.Core.Data.Entities.User.PasswordHash" />.
        /// </summary>
        public const string PasswordHash = "PasswordHash";
        /// <summary>
        /// The <c>ResetHash</c> column name for <see cref="Tracker.Core.Data.Entities.User.ResetHash" />.
        /// </summary>
        public const string ResetHash = "ResetHash";
        /// <summary>
        /// The <c>InviteHash</c> column name for <see cref="Tracker.Core.Data.Entities.User.InviteHash" />.
        /// </summary>
        public const string InviteHash = "InviteHash";
        /// <summary>
        /// The <c>AccessFailedCount</c> column name for <see cref="Tracker.Core.Data.Entities.User.AccessFailedCount" />.
        /// </summary>
        public const string AccessFailedCount = "AccessFailedCount";
        /// <summary>
        /// The <c>LockoutEnabled</c> column name for <see cref="Tracker.Core.Data.Entities.User.LockoutEnabled" />.
        /// </summary>
        public const string LockoutEnabled = "LockoutEnabled";
        /// <summary>
        /// The <c>LockoutEnd</c> column name for <see cref="Tracker.Core.Data.Entities.User.LockoutEnd" />.
        /// </summary>
        public const string LockoutEnd = "LockoutEnd";
        /// <summary>
        /// The <c>LastLogin</c> column name for <see cref="Tracker.Core.Data.Entities.User.LastLogin" />.
        /// </summary>
        public const string LastLogin = "LastLogin";
        /// <summary>
        /// The <c>IsDeleted</c> column name for <see cref="Tracker.Core.Data.Entities.User.IsDeleted" />.
        /// </summary>
        public const string IsDeleted = "IsDeleted";
        /// <summary>
        /// The <c>Created</c> column name for <see cref="Tracker.Core.Data.Entities.User.Created" />.
        /// </summary>
        public const string Created = "Created";
        /// <summary>
        /// The <c>CreatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.User.CreatedBy" />.
        /// </summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>
        /// The <c>Updated</c> column name for <see cref="Tracker.Core.Data.Entities.User.Updated" />.
        /// </summary>
        public const string Updated = "Updated";
        /// <summary>
        /// The <c>UpdatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.User.UpdatedBy" />.
        /// </summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>
        /// The <c>RowVersion</c> column name for <see cref="Tracker.Core.Data.Entities.User.RowVersion" />.
        /// </summary>
        public const string RowVersion = "RowVersion";
    }
    #endregion
}
