using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Configures Entity Framework Core mapping for the <see cref="Tracker.Core.Data.Entities.Status" /> entity mapped to the <c>dbo.Status</c> table.
/// </summary>
public partial class StatusMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.Status>
{
    /// <summary>
    /// Configures the table, key, property, and relationship mappings for <see cref="Tracker.Core.Data.Entities.Status" />.
    /// </summary>
    /// <param name="builder">The builder used to configure <see cref="Tracker.Core.Data.Entities.Status" />.</param>
    public void Configure(EntityTypeBuilder<Tracker.Core.Data.Entities.Status> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("Status", "dbo");

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
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(255)")
            .HasMaxLength(255);

        builder.Property(t => t.DisplayOrder)
            .IsRequired()
            .HasColumnName("DisplayOrder")
            .HasColumnType("int")
            .HasDefaultValueSql("((0))");

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
    /// Contains table mapping constants for <see cref="Tracker.Core.Data.Entities.Status" />.
    /// </summary>
    public readonly struct Table
    {
        /// <summary>
        /// The database schema name for <see cref="Tracker.Core.Data.Entities.Status" />.
        /// </summary>
        public const string Schema = "dbo";
        /// <summary>
        /// The database table name for <see cref="Tracker.Core.Data.Entities.Status" />.
        /// </summary>
        public const string Name = "Status";
    }

    /// <summary>
    /// Contains column name constants for <see cref="Tracker.Core.Data.Entities.Status" /> properties.
    /// </summary>
    public readonly struct Columns
    {
        /// <summary>
        /// The <c>Id</c> column name for <see cref="Tracker.Core.Data.Entities.Status.Id" />.
        /// </summary>
        public const string Id = "Id";
        /// <summary>
        /// The <c>Name</c> column name for <see cref="Tracker.Core.Data.Entities.Status.Name" />.
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// The <c>Description</c> column name for <see cref="Tracker.Core.Data.Entities.Status.Description" />.
        /// </summary>
        public const string Description = "Description";
        /// <summary>
        /// The <c>DisplayOrder</c> column name for <see cref="Tracker.Core.Data.Entities.Status.DisplayOrder" />.
        /// </summary>
        public const string DisplayOrder = "DisplayOrder";
        /// <summary>
        /// The <c>IsActive</c> column name for <see cref="Tracker.Core.Data.Entities.Status.IsActive" />.
        /// </summary>
        public const string IsActive = "IsActive";
        /// <summary>
        /// The <c>Created</c> column name for <see cref="Tracker.Core.Data.Entities.Status.Created" />.
        /// </summary>
        public const string Created = "Created";
        /// <summary>
        /// The <c>CreatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.Status.CreatedBy" />.
        /// </summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>
        /// The <c>Updated</c> column name for <see cref="Tracker.Core.Data.Entities.Status.Updated" />.
        /// </summary>
        public const string Updated = "Updated";
        /// <summary>
        /// The <c>UpdatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.Status.UpdatedBy" />.
        /// </summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>
        /// The <c>RowVersion</c> column name for <see cref="Tracker.Core.Data.Entities.Status.RowVersion" />.
        /// </summary>
        public const string RowVersion = "RowVersion";
    }
    #endregion
}
