using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="Tracker.Core.Data.Entities.UserLogin" />
    /// </summary>
    public partial class UserLoginMap
        : IEntityTypeConfiguration<Tracker.Core.Data.Entities.UserLogin>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Tracker.Core.Data.Entities.UserLogin" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Core.Data.Entities.UserLogin> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserLogin", "dbo");

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
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserLogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserLogin_User_UserId");

            #endregion
        }

        #region Generated Constants
        /// <summary>Table Schema name constant for entity <see cref="Tracker.Core.Data.Entities.UserLogin" /></summary>
        public const string TableSchema = "dbo";
        /// <summary>Table Name constant for entity <see cref="Tracker.Core.Data.Entities.UserLogin" /></summary>
        public const string TableName = "UserLogin";

        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.Id" /></summary>
        public const string ColumnId = "Id";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.EmailAddress" /></summary>
        public const string ColumnEmailAddress = "EmailAddress";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.UserId" /></summary>
        public const string ColumnUserId = "UserId";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.UserAgent" /></summary>
        public const string ColumnUserAgent = "UserAgent";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.Browser" /></summary>
        public const string ColumnBrowser = "Browser";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.OperatingSystem" /></summary>
        public const string ColumnOperatingSystem = "OperatingSystem";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.DeviceFamily" /></summary>
        public const string ColumnDeviceFamily = "DeviceFamily";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.DeviceBrand" /></summary>
        public const string ColumnDeviceBrand = "DeviceBrand";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.DeviceModel" /></summary>
        public const string ColumnDeviceModel = "DeviceModel";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.IpAddress" /></summary>
        public const string ColumnIpAddress = "IpAddress";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.IsSuccessful" /></summary>
        public const string ColumnIsSuccessful = "IsSuccessful";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.FailureMessage" /></summary>
        public const string ColumnFailureMessage = "FailureMessage";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.Created" /></summary>
        public const string ColumnCreated = "Created";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.CreatedBy" /></summary>
        public const string ColumnCreatedBy = "CreatedBy";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.Updated" /></summary>
        public const string ColumnUpdated = "Updated";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.UpdatedBy" /></summary>
        public const string ColumnUpdatedBy = "UpdatedBy";
        /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserLogin.RowVersion" /></summary>
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
