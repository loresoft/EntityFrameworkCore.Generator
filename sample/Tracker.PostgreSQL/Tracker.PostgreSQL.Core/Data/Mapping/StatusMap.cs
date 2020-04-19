using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Mapping
{
    public partial class StatusMap
        : IEntityTypeConfiguration<Tracker.PostgreSQL.Core.Data.Entities.Status>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.PostgreSQL.Core.Data.Entities.Status> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Status", "public");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("uuid");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("character varying(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("character varying(255)")
                .HasMaxLength(255);

            builder.Property(t => t.DisplayOrder)
                .IsRequired()
                .HasColumnName("DisplayOrder")
                .HasColumnType("integer");

            builder.Property(t => t.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("boolean")
                .HasDefaultValueSql("true");

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
            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "public";
        public const string TableName = "Status";

        public const string ColumnId = "Id";
        public const string ColumnName = "Name";
        public const string ColumnDescription = "Description";
        public const string ColumnDisplayOrder = "DisplayOrder";
        public const string ColumnIsActive = "IsActive";
        public const string ColumnCreated = "Created";
        public const string ColumnCreatedBy = "CreatedBy";
        public const string ColumnUpdated = "Updated";
        public const string ColumnUpdatedBy = "UpdatedBy";
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
