using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Tracker.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus" />
    /// </summary>
    public partial class TrackerStatusMap
        : IEntityTypeConfiguration<TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus>
    {
        /// <summary>
        /// Configures the entity of type <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Status", "Tracker");

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
                .HasColumnType("int");

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
                .HasColumnName("RowVersion")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();

            // relationships
            #endregion
        }

        #region Generated Constants
        /// <summary>Table Schema name constant for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus" /></summary>
        public const string TableSchema = "Tracker";
        /// <summary>Table Name constant for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus" /></summary>
        public const string TableName = "Status";

        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.Id" /></summary>
        public const string ColumnId = "Id";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.Name" /></summary>
        public const string ColumnName = "Name";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.Description" /></summary>
        public const string ColumnDescription = "Description";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.DisplayOrder" /></summary>
        public const string ColumnDisplayOrder = "DisplayOrder";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.IsActive" /></summary>
        public const string ColumnIsActive = "IsActive";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.Created" /></summary>
        public const string ColumnCreated = "Created";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.CreatedBy" /></summary>
        public const string ColumnCreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.Updated" /></summary>
        public const string ColumnUpdated = "Updated";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.UpdatedBy" /></summary>
        public const string ColumnUpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus.RowVersion" /></summary>
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
