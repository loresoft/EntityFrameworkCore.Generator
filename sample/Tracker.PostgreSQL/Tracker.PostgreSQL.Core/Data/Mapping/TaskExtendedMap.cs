using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Mapping
{
    public partial class TaskExtendedMap
        : IEntityTypeConfiguration<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("TaskExtended", "public");

            // key
            builder.HasKey(t => t.TaskId);

            // properties
            builder.Property(t => t.TaskId)
                .IsRequired()
                .HasColumnName("TaskId")
                .HasColumnType("uuid");

            builder.Property(t => t.UserAgent)
                .HasColumnName("UserAgent")
                .HasColumnType("text");

            builder.Property(t => t.Browser)
                .HasColumnName("Browser")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.OperatingSystem)
                .HasColumnName("OperatingSystem")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.Created)
                .IsRequired()
                .HasColumnName("Created")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("timezone('utc'::text, now())");

            builder.Property(t => t.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasColumnType("character varying(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Updated)
                .IsRequired()
                .HasColumnName("Updated")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("timezone('utc'::text, now())");

            builder.Property(t => t.UpdatedBy)
                .HasColumnName("UpdatedBy")
                .HasColumnType("character varying(100)")
                .HasMaxLength(100);

            builder.Property(t => t.RowVersion)
                .HasColumnName("RowVersion")
                .HasColumnType("bytea");

            // relationships
            builder.HasOne(t => t.Task)
                .WithOne(t => t.TaskExtended)
                .HasForeignKey<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended>(d => d.TaskId)
                .HasConstraintName("FK_TaskExtended_Task_TaskId");

            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "public";
        public const string TableName = "TaskExtended";

        public const string ColumnTaskId = "TaskId";
        public const string ColumnUserAgent = "UserAgent";
        public const string ColumnBrowser = "Browser";
        public const string ColumnOperatingSystem = "OperatingSystem";
        public const string ColumnCreated = "Created";
        public const string ColumnCreatedBy = "CreatedBy";
        public const string ColumnUpdated = "Updated";
        public const string ColumnUpdatedBy = "UpdatedBy";
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
