using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Configures Entity Framework Core mapping for the <see cref="Tracker.Core.Data.Entities.Audit" /> entity mapped to the <c>dbo.Audit</c> table.
/// </summary>
public partial class AuditMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.Audit>
{
    /// <summary>
    /// Configures the table, key, property, and relationship mappings for <see cref="Tracker.Core.Data.Entities.Audit" />.
    /// </summary>
    /// <param name="builder">The builder used to configure <see cref="Tracker.Core.Data.Entities.Audit" />.</param>
    public void Configure(EntityTypeBuilder<Tracker.Core.Data.Entities.Audit> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("Audit", "dbo");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(t => t.Date)
            .IsRequired()
            .HasColumnName("Date")
            .HasColumnType("datetime");

        builder.Property(t => t.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.TaskId)
            .HasColumnName("TaskId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.Content)
            .IsRequired()
            .HasColumnName("Content")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.Username)
            .IsRequired()
            .HasColumnName("Username")
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50);

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

        builder.Property(t => t.Attributes)
            .HasColumnName("Attributes")
            .HasColumnType("StringList");

        // relationships
        #endregion
    }

    #region Generated Constants
    /// <summary>
    /// Contains table mapping constants for <see cref="Tracker.Core.Data.Entities.Audit" />.
    /// </summary>
    public readonly struct Table
    {
        /// <summary>
        /// The database schema name for <see cref="Tracker.Core.Data.Entities.Audit" />.
        /// </summary>
        public const string Schema = "dbo";
        /// <summary>
        /// The database table name for <see cref="Tracker.Core.Data.Entities.Audit" />.
        /// </summary>
        public const string Name = "Audit";
    }

    /// <summary>
    /// Contains column name constants for <see cref="Tracker.Core.Data.Entities.Audit" /> properties.
    /// </summary>
    public readonly struct Columns
    {
        /// <summary>
        /// The <c>Id</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.Id" />.
        /// </summary>
        public const string Id = "Id";
        /// <summary>
        /// The <c>Date</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.Date" />.
        /// </summary>
        public const string Date = "Date";
        /// <summary>
        /// The <c>UserId</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.UserId" />.
        /// </summary>
        public const string UserId = "UserId";
        /// <summary>
        /// The <c>TaskId</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.TaskId" />.
        /// </summary>
        public const string TaskId = "TaskId";
        /// <summary>
        /// The <c>Content</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.Content" />.
        /// </summary>
        public const string Content = "Content";
        /// <summary>
        /// The <c>Username</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.Username" />.
        /// </summary>
        public const string Username = "Username";
        /// <summary>
        /// The <c>Created</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.Created" />.
        /// </summary>
        public const string Created = "Created";
        /// <summary>
        /// The <c>CreatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.CreatedBy" />.
        /// </summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>
        /// The <c>Updated</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.Updated" />.
        /// </summary>
        public const string Updated = "Updated";
        /// <summary>
        /// The <c>UpdatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.UpdatedBy" />.
        /// </summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>
        /// The <c>RowVersion</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.RowVersion" />.
        /// </summary>
        public const string RowVersion = "RowVersion";
        /// <summary>
        /// The <c>Attributes</c> column name for <see cref="Tracker.Core.Data.Entities.Audit.Attributes" />.
        /// </summary>
        public const string Attributes = "Attributes";
    }
    #endregion
}
