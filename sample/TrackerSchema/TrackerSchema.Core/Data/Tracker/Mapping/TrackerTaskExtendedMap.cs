using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Tracker.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended" />
    /// </summary>
    public partial class TrackerTaskExtendedMap
        : IEntityTypeConfiguration<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended>
    {
        /// <summary>
        /// Configures the entity of type <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("TaskExtended", "Tracker");

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
                .HasColumnName("RowVersion")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();

            // relationships
            builder.HasOne(t => t.TaskTrackerTask)
                .WithOne(t => t.TaskTrackerTaskExtended)
                .HasForeignKey<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended>(d => d.TaskId)
                .HasConstraintName("FK_TaskExtended_Task_TaskId");

            #endregion
        }

        #region Generated Constants
        /// <summary>Table Schema name constant for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended" /></summary>
        public const string TableSchema = "Tracker";
        /// <summary>Table Name constant for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended" /></summary>
        public const string TableName = "TaskExtended";

        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.TaskId" /></summary>
        public const string ColumnTaskId = "TaskId";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.UserAgent" /></summary>
        public const string ColumnUserAgent = "UserAgent";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.Browser" /></summary>
        public const string ColumnBrowser = "Browser";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.OperatingSystem" /></summary>
        public const string ColumnOperatingSystem = "OperatingSystem";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.Created" /></summary>
        public const string ColumnCreated = "Created";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.CreatedBy" /></summary>
        public const string ColumnCreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.Updated" /></summary>
        public const string ColumnUpdated = "Updated";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.UpdatedBy" /></summary>
        public const string ColumnUpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended.RowVersion" /></summary>
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
