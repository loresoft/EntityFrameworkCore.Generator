using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping
{
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
                .ValueGeneratedOnAddOrUpdate();

            // relationships
            builder.HasOne(t => t.Task)
                .WithOne(t => t.TaskExtended)
                .HasForeignKey<Tracker.Core.Data.Entities.TaskExtended>(d => d.TaskId)
                .HasConstraintName("FK_TaskExtended_Task_TaskId");

            #endregion
        }

    }
}
