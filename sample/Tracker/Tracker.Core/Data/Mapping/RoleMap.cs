using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="Tracker.Core.Data.Entities.Role" />
    /// </summary>
    public partial class RoleMap
        : IEntityTypeConfiguration<Tracker.Core.Data.Entities.Role>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Tracker.Core.Data.Entities.Role" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Core.Data.Entities.Role> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Role", "dbo");

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
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256);

            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(max)");

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
        /// <summary>Table Schema name constant for entity <see cref="Tracker.Core.Data.Entities.Role" /></summary>
        public const string TableSchema = "dbo";
        /// <summary>Table Name constant for entity <see cref="Tracker.Core.Data.Entities.Role" /></summary>
        public const string TableName = "Role";

        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Role.Id" /></summary>
        public const string ColumnId = "Id";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Role.Name" /></summary>
        public const string ColumnName = "Name";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Role.Description" /></summary>
        public const string ColumnDescription = "Description";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Role.Created" /></summary>
        public const string ColumnCreated = "Created";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Role.CreatedBy" /></summary>
        public const string ColumnCreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Role.Updated" /></summary>
        public const string ColumnUpdated = "Updated";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Role.UpdatedBy" /></summary>
        public const string ColumnUpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Role.RowVersion" /></summary>
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
