using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Mapping
{
    public partial class RoleMap
        : IEntityTypeConfiguration<Tracker.Data.Entities.Role>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Data.Entities.Role> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Role", "public");

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
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("text");

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
