using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Configures Entity Framework Core mapping for the <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entity mapped to the <c>dbo.TaskExtended</c> table.
/// </summary>
public partial class TaskExtendedMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.TaskExtended>
{
    /// <summary>
    /// Configures the table, key, property, and relationship mappings for <see cref="Tracker.Core.Data.Entities.TaskExtended" />.
    /// </summary>
    /// <param name="builder">The builder used to configure <see cref="Tracker.Core.Data.Entities.TaskExtended" />.</param>
    public void Configure(EntityTypeBuilder<Tracker.Core.Data.Entities.TaskExtended> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("TaskExtended", "dbo");

        // key
        builder.HasKey(t => t.TaskId);

        // properties
        builder.Property(t => t.TaskId)
            .IsRequired()
            .HasColumnName("TaskId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.UserAgent)
            .HasColumnName("UserAgent")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.Browser)
            .HasColumnName("Browser")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.OperatingSystem)
            .HasColumnName("OperatingSystem")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

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
        builder.HasOne(t => t.Task)
            .WithOne(t => t.TaskExtended)
            .HasForeignKey<Tracker.Core.Data.Entities.TaskExtended>(d => d.TaskId)
            .HasConstraintName("FK_TaskExtended_Task_TaskId");

        #endregion
    }

    #region Generated Constants
    /// <summary>
    /// Contains table mapping constants for <see cref="Tracker.Core.Data.Entities.TaskExtended" />.
    /// </summary>
    public readonly struct Table
    {
        /// <summary>
        /// The database schema name for <see cref="Tracker.Core.Data.Entities.TaskExtended" />.
        /// </summary>
        public const string Schema = "dbo";
        /// <summary>
        /// The database table name for <see cref="Tracker.Core.Data.Entities.TaskExtended" />.
        /// </summary>
        public const string Name = "TaskExtended";
    }

    /// <summary>
    /// Contains column name constants for <see cref="Tracker.Core.Data.Entities.TaskExtended" /> properties.
    /// </summary>
    public readonly struct Columns
    {
        /// <summary>
        /// The <c>TaskId</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.TaskId" />.
        /// </summary>
        public const string TaskId = "TaskId";
        /// <summary>
        /// The <c>UserAgent</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.UserAgent" />.
        /// </summary>
        public const string UserAgent = "UserAgent";
        /// <summary>
        /// The <c>Browser</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.Browser" />.
        /// </summary>
        public const string Browser = "Browser";
        /// <summary>
        /// The <c>OperatingSystem</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.OperatingSystem" />.
        /// </summary>
        public const string OperatingSystem = "OperatingSystem";
        /// <summary>
        /// The <c>Created</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.Created" />.
        /// </summary>
        public const string Created = "Created";
        /// <summary>
        /// The <c>CreatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.CreatedBy" />.
        /// </summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>
        /// The <c>Updated</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.Updated" />.
        /// </summary>
        public const string Updated = "Updated";
        /// <summary>
        /// The <c>UpdatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.UpdatedBy" />.
        /// </summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>
        /// The <c>RowVersion</c> column name for <see cref="Tracker.Core.Data.Entities.TaskExtended.RowVersion" />.
        /// </summary>
        public const string RowVersion = "RowVersion";
    }
    #endregion
}
