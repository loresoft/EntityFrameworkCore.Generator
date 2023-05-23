using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Allows configuration for an entity type <see cref="Tracker.Core.Data.Entities.User" />
/// </summary>
public partial class UserMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.User>
{
    /// <summary>
    /// Configures the entity of type <see cref="Tracker.Core.Data.Entities.User" />
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Core.Data.Entities.User> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("User", "dbo");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(t => t.EmailAddress)
            .IsRequired()
            .HasColumnName("EmailAddress")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.IsEmailAddressConfirmed)
            .IsRequired()
            .HasColumnName("IsEmailAddressConfirmed")
            .HasColumnType("bit");

        builder.Property(t => t.DisplayName)
            .IsRequired()
            .HasColumnName("DisplayName")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.ResetHash)
            .HasColumnName("ResetHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.InviteHash)
            .HasColumnName("InviteHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.AccessFailedCount)
            .IsRequired()
            .HasColumnName("AccessFailedCount")
            .HasColumnType("int");

        builder.Property(t => t.LockoutEnabled)
            .IsRequired()
            .HasColumnName("LockoutEnabled")
            .HasColumnType("bit");

        builder.Property(t => t.LockoutEnd)
            .HasColumnName("LockoutEnd")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.LastLogin)
            .HasColumnName("LastLogin")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasColumnName("IsDeleted")
            .HasColumnType("bit");

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
    public readonly struct Table
    {
        /// <summary>Table Schema name constant for entity <see cref="Tracker.Core.Data.Entities.User" /></summary>
        public const string Schema = "dbo";
        /// <summary>Table Name constant for entity <see cref="Tracker.Core.Data.Entities.User" /></summary>
        public const string Name = "User";
    }

    public readonly struct Columns
    {
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.Id" /></summary>
        public const string Id = "Id";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.EmailAddress" /></summary>
        public const string EmailAddress = "EmailAddress";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.IsEmailAddressConfirmed" /></summary>
        public const string IsEmailAddressConfirmed = "IsEmailAddressConfirmed";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.DisplayName" /></summary>
        public const string DisplayName = "DisplayName";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.PasswordHash" /></summary>
        public const string PasswordHash = "PasswordHash";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.ResetHash" /></summary>
        public const string ResetHash = "ResetHash";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.InviteHash" /></summary>
        public const string InviteHash = "InviteHash";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.AccessFailedCount" /></summary>
        public const string AccessFailedCount = "AccessFailedCount";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.LockoutEnabled" /></summary>
        public const string LockoutEnabled = "LockoutEnabled";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.LockoutEnd" /></summary>
        public const string LockoutEnd = "LockoutEnd";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.LastLogin" /></summary>
        public const string LastLogin = "LastLogin";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.IsDeleted" /></summary>
        public const string IsDeleted = "IsDeleted";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.Created" /></summary>
        public const string Created = "Created";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.CreatedBy" /></summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.Updated" /></summary>
        public const string Updated = "Updated";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.UpdatedBy" /></summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.User.RowVersion" /></summary>
        public const string RowVersion = "RowVersion";
    }
    #endregion
}
