using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Allows configuration for an entity type <see cref="Tracker.Core.Data.Entities.TaskExtended" />
/// </summary>
public partial class TaskExtendedMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.TaskExtended>
{
    /// <summary>
    /// Configures the entity of type <see cref="Tracker.Core.Data.Entities.TaskExtended" />
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Core.Data.Entities.TaskExtended> builder)
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
                .HasColumnName("RowVersion")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();

            // relationships
            builder.HasOne(t => t.Task)
                .WithOne(t => t.TaskExtended)
                .HasForeignKey<Tracker.Core.Data.Entities.TaskExtended>(d => d.TaskId)
                .HasConstraintName("FK_TaskExtended_Task_TaskId");

            #endregion
    }

    #region Generated Constants
        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="Tracker.Core.Data.Entities.TaskExtended" /></summary>
            public const string Schema = "dbo";
            /// <summary>Table Name constant for entity <see cref="Tracker.Core.Data.Entities.TaskExtended" /></summary>
            public const string Name = "TaskExtended";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.TaskId" /></summary>
            public const string TaskId = "TaskId";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.UserAgent" /></summary>
            public const string UserAgent = "UserAgent";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.Browser" /></summary>
            public const string Browser = "Browser";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.OperatingSystem" /></summary>
            public const string OperatingSystem = "OperatingSystem";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.Created" /></summary>
            public const string Created = "Created";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.CreatedBy" /></summary>
            public const string CreatedBy = "CreatedBy";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.Updated" /></summary>
            public const string Updated = "Updated";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.UpdatedBy" /></summary>
            public const string UpdatedBy = "UpdatedBy";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.TaskExtended.RowVersion" /></summary>
            public const string RowVersion = "RowVersion";
        }
        #endregion
}
