using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Allows configuration for an entity type <see cref="Tracker.Core.Data.Entities.Tenant" />
/// </summary>
public partial class TenantMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.Tenant>
{
    /// <summary>
    /// Configures the entity of type <see cref="Tracker.Core.Data.Entities.Tenant" />
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Core.Data.Entities.Tenant> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("Tenant", "dbo");

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

        builder.Property(t => t.IsActive)
            .IsRequired()
            .HasColumnName("IsActive")
            .HasColumnType("bit")
            .HasDefaultValue(true);

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
            .IsConcurrencyToken()
            .HasColumnName("RowVersion")
            .HasColumnType("rowversion")
            .HasMaxLength(8)
            .ValueGeneratedOnAddOrUpdate();

        // relationships
        #endregion
    }

    #region Generated Constants
    public readonly struct Table
    {
        /// <summary>Table Schema name constant for entity <see cref="Tracker.Core.Data.Entities.Tenant" /></summary>
        public const string Schema = "dbo";
        /// <summary>Table Name constant for entity <see cref="Tracker.Core.Data.Entities.Tenant" /></summary>
        public const string Name = "Tenant";
    }

    public readonly struct Columns
    {
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.Id" /></summary>
        public const string Id = "Id";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.Name" /></summary>
        public const string Name = "Name";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.Description" /></summary>
        public const string Description = "Description";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.IsActive" /></summary>
        public const string IsActive = "IsActive";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.Created" /></summary>
        public const string Created = "Created";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.CreatedBy" /></summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.Updated" /></summary>
        public const string Updated = "Updated";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.UpdatedBy" /></summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Tenant.RowVersion" /></summary>
        public const string RowVersion = "RowVersion";
    }
    #endregion
}
