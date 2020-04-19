using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Identity.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser" />
    /// </summary>
    public partial class IdentityUserMap
        : IEntityTypeConfiguration<TrackerSchema.Core.Data.Identity.Entities.IdentityUser>
    {
        /// <summary>
        /// Configures the entity of type <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("User", "Identity");

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
        /// <summary>Table Schema name constant for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser" /></summary>
        public const string TableSchema = "Identity";
        /// <summary>Table Name constant for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser" /></summary>
        public const string TableName = "User";

        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.Id" /></summary>
        public const string ColumnId = "Id";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.EmailAddress" /></summary>
        public const string ColumnEmailAddress = "EmailAddress";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.IsEmailAddressConfirmed" /></summary>
        public const string ColumnIsEmailAddressConfirmed = "IsEmailAddressConfirmed";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.DisplayName" /></summary>
        public const string ColumnDisplayName = "DisplayName";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.PasswordHash" /></summary>
        public const string ColumnPasswordHash = "PasswordHash";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.ResetHash" /></summary>
        public const string ColumnResetHash = "ResetHash";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.InviteHash" /></summary>
        public const string ColumnInviteHash = "InviteHash";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.AccessFailedCount" /></summary>
        public const string ColumnAccessFailedCount = "AccessFailedCount";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.LockoutEnabled" /></summary>
        public const string ColumnLockoutEnabled = "LockoutEnabled";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.LockoutEnd" /></summary>
        public const string ColumnLockoutEnd = "LockoutEnd";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.LastLogin" /></summary>
        public const string ColumnLastLogin = "LastLogin";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.IsDeleted" /></summary>
        public const string ColumnIsDeleted = "IsDeleted";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.Created" /></summary>
        public const string ColumnCreated = "Created";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.CreatedBy" /></summary>
        public const string ColumnCreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.Updated" /></summary>
        public const string ColumnUpdated = "Updated";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.UpdatedBy" /></summary>
        public const string ColumnUpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser.RowVersion" /></summary>
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
