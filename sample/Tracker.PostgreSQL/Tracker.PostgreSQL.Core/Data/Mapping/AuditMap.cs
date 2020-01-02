using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Mapping
{
    public partial class AuditMap
        : IEntityTypeConfiguration<Tracker.Data.Entities.Audit>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Data.Entities.Audit> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Audit", "public");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("uuid");

            builder.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("timestamp without time zone");

            builder.Property(t => t.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uuid");

            builder.Property(t => t.TaskId)
                .HasColumnName("TaskId")
                .HasColumnType("uuid");

            builder.Property(t => t.Content)
                .IsRequired()
                .HasColumnName("Content")
                .HasColumnType("text");

            builder.Property(t => t.Username)
                .IsRequired()
                .HasColumnName("Username")
                .HasColumnType("character varying(50)")
                .HasMaxLength(50);

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

    }
}
