using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Identity.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin" />
    /// </summary>
    public partial class IdentityUserLoginMap
        : IEntityTypeConfiguration<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin>
    {
        /// <summary>
        /// Configures the entity of type <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserLogin", "Identity");

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

            builder.Property(t => t.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier");

            builder.Property(t => t.UserAgent)
                .HasColumnName("UserAgent")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.Browser)
                .HasColumnName("Browser")
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256);

            builder.Property(t => t.OperatingSystem)
                .HasColumnName("OperatingSystem")
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256);

            builder.Property(t => t.DeviceFamily)
                .HasColumnName("DeviceFamily")
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256);

            builder.Property(t => t.DeviceBrand)
                .HasColumnName("DeviceBrand")
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256);

            builder.Property(t => t.DeviceModel)
                .HasColumnName("DeviceModel")
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256);

            builder.Property(t => t.IpAddress)
                .HasColumnName("IpAddress")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.IsSuccessful)
                .IsRequired()
                .HasColumnName("IsSuccessful")
                .HasColumnType("bit");

            builder.Property(t => t.FailureMessage)
                .HasColumnName("FailureMessage")
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256);

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
            builder.HasOne(t => t.UserIdentityUser)
                .WithMany(t => t.UserIdentityUserLogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserLogin_User_UserId");

            #endregion
        }

        #region Generated Constants
        /// <summary>Table Schema name constant for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin" /></summary>
        public const string TableSchema = "Identity";
        /// <summary>Table Name constant for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin" /></summary>
        public const string TableName = "UserLogin";

        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.Id" /></summary>
        public const string ColumnId = "Id";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.EmailAddress" /></summary>
        public const string ColumnEmailAddress = "EmailAddress";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.UserId" /></summary>
        public const string ColumnUserId = "UserId";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.UserAgent" /></summary>
        public const string ColumnUserAgent = "UserAgent";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.Browser" /></summary>
        public const string ColumnBrowser = "Browser";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.OperatingSystem" /></summary>
        public const string ColumnOperatingSystem = "OperatingSystem";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.DeviceFamily" /></summary>
        public const string ColumnDeviceFamily = "DeviceFamily";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.DeviceBrand" /></summary>
        public const string ColumnDeviceBrand = "DeviceBrand";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.DeviceModel" /></summary>
        public const string ColumnDeviceModel = "DeviceModel";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.IpAddress" /></summary>
        public const string ColumnIpAddress = "IpAddress";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.IsSuccessful" /></summary>
        public const string ColumnIsSuccessful = "IsSuccessful";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.FailureMessage" /></summary>
        public const string ColumnFailureMessage = "FailureMessage";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.Created" /></summary>
        public const string ColumnCreated = "Created";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.CreatedBy" /></summary>
        public const string ColumnCreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.Updated" /></summary>
        public const string ColumnUpdated = "Updated";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.UpdatedBy" /></summary>
        public const string ColumnUpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin.RowVersion" /></summary>
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
