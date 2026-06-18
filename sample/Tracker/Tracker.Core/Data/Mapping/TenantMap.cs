using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Configures Entity Framework Core mapping for the <see cref="Tracker.Core.Data.Entities.Tenant" /> entity mapped to the <c>dbo.Tenant</c> table.
/// </summary>
public partial class TenantMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.Tenant>
{
    /// <summary>
    /// Configures the table, key, property, and relationship mappings for <see cref="Tracker.Core.Data.Entities.Tenant" />.
    /// </summary>
    /// <param name="builder">The builder used to configure <see cref="Tracker.Core.Data.Entities.Tenant" />.</param>
    public void Configure(EntityTypeBuilder<Tracker.Core.Data.Entities.Tenant> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("Tenant", "dbo");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(t => t.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.IsActive)
            .IsRequired()
            .HasColumnName("IsActive")
            .HasColumnType("bit")
            .HasDefaultValueSql("((1))");

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
    /// Contains table mapping constants for <see cref="Tracker.Core.Data.Entities.Tenant" />.
    /// </summary>
    public readonly struct Table
    {
        /// <summary>
        /// The database schema name for <see cref="Tracker.Core.Data.Entities.Tenant" />.
        /// </summary>
        public const string Schema = "dbo";
        /// <summary>
        /// The database table name for <see cref="Tracker.Core.Data.Entities.Tenant" />.
        /// </summary>
        public const string Name = "Tenant";
    }

    /// <summary>
    /// Contains column name constants for <see cref="Tracker.Core.Data.Entities.Tenant" /> properties.
    /// </summary>
    public readonly struct Columns
    {
        /// <summary>
        /// The <c>Id</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.Id" />.
        /// </summary>
        public const string Id = "Id";
        /// <summary>
        /// The <c>Name</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.Name" />.
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// The <c>Description</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.Description" />.
        /// </summary>
        public const string Description = "Description";
        /// <summary>
        /// The <c>IsActive</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.IsActive" />.
        /// </summary>
        public const string IsActive = "IsActive";
        /// <summary>
        /// The <c>Created</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.Created" />.
        /// </summary>
        public const string Created = "Created";
        /// <summary>
        /// The <c>CreatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.CreatedBy" />.
        /// </summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>
        /// The <c>Updated</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.Updated" />.
        /// </summary>
        public const string Updated = "Updated";
        /// <summary>
        /// The <c>UpdatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.UpdatedBy" />.
        /// </summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>
        /// The <c>RowVersion</c> column name for <see cref="Tracker.Core.Data.Entities.Tenant.RowVersion" />.
        /// </summary>
        public const string RowVersion = "RowVersion";
    }
    #endregion
}
