using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Allows configuration for an entity type <see cref="Tracker.Core.Data.Entities.Audit" />
/// </summary>
public partial class AuditMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.Audit>
{
    /// <summary>
    /// Configures the entity of type <see cref="Tracker.Core.Data.Entities.Audit" />
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Core.Data.Entities.Audit> builder)
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
                .HasColumnName("RowVersion")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();

            // relationships
            #endregion
    }

    #region Generated Constants
        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="Tracker.Core.Data.Entities.Audit" /></summary>
            public const string Schema = "dbo";
            /// <summary>Table Name constant for entity <see cref="Tracker.Core.Data.Entities.Audit" /></summary>
            public const string Name = "Audit";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.Id" /></summary>
            public const string Id = "Id";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.Date" /></summary>
            public const string Date = "Date";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.UserId" /></summary>
            public const string UserId = "UserId";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.TaskId" /></summary>
            public const string TaskId = "TaskId";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.Content" /></summary>
            public const string Content = "Content";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.Username" /></summary>
            public const string Username = "Username";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.Created" /></summary>
            public const string Created = "Created";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.CreatedBy" /></summary>
            public const string CreatedBy = "CreatedBy";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.Updated" /></summary>
            public const string Updated = "Updated";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.UpdatedBy" /></summary>
            public const string UpdatedBy = "UpdatedBy";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Audit.RowVersion" /></summary>
            public const string RowVersion = "RowVersion";
        }
        #endregion
}
