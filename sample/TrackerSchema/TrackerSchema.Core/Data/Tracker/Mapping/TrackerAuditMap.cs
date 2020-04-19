using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Tracker.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit" />
    /// </summary>
    public partial class TrackerAuditMap
        : IEntityTypeConfiguration<TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit>
    {
        /// <summary>
        /// Configures the entity of type <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Audit", "Tracker");

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
                .HasColumnName("RowVersion")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();

            // relationships
            #endregion
        }

        #region Generated Constants
        /// <summary>Table Schema name constant for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit" /></summary>
        public const string TableSchema = "Tracker";
        /// <summary>Table Name constant for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit" /></summary>
        public const string TableName = "Audit";

        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.Id" /></summary>
        public const string ColumnId = "Id";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.Date" /></summary>
        public const string ColumnDate = "Date";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.UserId" /></summary>
        public const string ColumnUserId = "UserId";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.TaskId" /></summary>
        public const string ColumnTaskId = "TaskId";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.Content" /></summary>
        public const string ColumnContent = "Content";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.Username" /></summary>
        public const string ColumnUsername = "Username";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.Created" /></summary>
        public const string ColumnCreated = "Created";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.CreatedBy" /></summary>
        public const string ColumnCreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.Updated" /></summary>
        public const string ColumnUpdated = "Updated";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.UpdatedBy" /></summary>
        public const string ColumnUpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit.RowVersion" /></summary>
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
