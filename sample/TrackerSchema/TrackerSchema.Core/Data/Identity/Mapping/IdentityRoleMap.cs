using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Identity.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole" />
    /// </summary>
    public partial class IdentityRoleMap
        : IEntityTypeConfiguration<TrackerSchema.Core.Data.Identity.Entities.IdentityRole>
    {
        /// <summary>
        /// Configures the entity of type <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Role", "Identity");

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
        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole" /></summary>
            public const string Schema = "Identity";
            /// <summary>Table Name constant for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole" /></summary>
            public const string Name = "Role";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole.Id" /></summary>
            public const string Id = "Id";
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole.Name" /></summary>
            public const string Name = "Name";
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole.Description" /></summary>
            public const string Description = "Description";
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole.Created" /></summary>
            public const string Created = "Created";
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole.CreatedBy" /></summary>
            public const string CreatedBy = "CreatedBy";
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole.Updated" /></summary>
            public const string Updated = "Updated";
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole.UpdatedBy" /></summary>
            public const string UpdatedBy = "UpdatedBy";
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole.RowVersion" /></summary>
            public const string RowVersion = "RowVersion";
        }
        #endregion
    }
}
