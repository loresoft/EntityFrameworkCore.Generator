using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Mapping
{
    public partial class PriorityMap
        : IEntityTypeConfiguration<Tracker.Data.Entities.Priority>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Data.Entities.Priority> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Priority", "public");

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

    }
}
