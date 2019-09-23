using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Mapping
{
    public partial class SchemaversionsMap
        : IEntityTypeConfiguration<Tracker.Data.Entities.Schemaversions>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Data.Entities.Schemaversions> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("schemaversions", "public");

            // key
            builder.HasKey(t => t.Schemaversionsid);

            // properties
            builder.Property(t => t.Schemaversionsid)
                .IsRequired()
                .HasColumnName("schemaversionsid")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Scriptname)
                .IsRequired()
                .HasColumnName("scriptname")
                .HasColumnType("character varying(255)")
                .HasMaxLength(255);

            builder.Property(t => t.Applied)
                .IsRequired()
                .HasColumnName("applied")
                .HasColumnType("timestamp without time zone");

            // relationships
            #endregion
        }

    }
}
