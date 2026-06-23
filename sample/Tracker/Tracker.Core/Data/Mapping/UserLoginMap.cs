using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Configures Entity Framework Core mapping for the <see cref="Tracker.Core.Data.Entities.UserLogin" /> entity mapped to the <c>dbo.UserLogin</c> table.
/// </summary>
public partial class UserLoginMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.UserLogin>
{
    /// <summary>
    /// Configures the table, key, property, and relationship mappings for <see cref="Tracker.Core.Data.Entities.UserLogin" />.
    /// </summary>
    /// <param name="builder">The builder used to configure <see cref="Tracker.Core.Data.Entities.UserLogin" />.</param>
    public void Configure(EntityTypeBuilder<Tracker.Core.Data.Entities.UserLogin> builder)
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
            .HasColumnType("bit")
            .HasDefaultValue(false);

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
            .IsConcurrencyToken()
            .HasColumnName("RowVersion")
            .HasColumnType("rowversion")
            .ValueGeneratedOnAddOrUpdate();

        // relationships
        builder.HasOne(t => t.User)
            .WithMany(t => t.UserLogins)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserLogin_User_UserId");

        #endregion
    }

    #region Generated Constants
    /// <summary>
    /// Contains table mapping constants for <see cref="Tracker.Core.Data.Entities.UserLogin" />.
    /// </summary>
    public readonly struct Table
    {
        /// <summary>
        /// The database schema name for <see cref="Tracker.Core.Data.Entities.UserLogin" />.
        /// </summary>
        public const string Schema = "dbo";
        /// <summary>
        /// The database table name for <see cref="Tracker.Core.Data.Entities.UserLogin" />.
        /// </summary>
        public const string Name = "UserLogin";
    }

    /// <summary>
    /// Contains column name constants for <see cref="Tracker.Core.Data.Entities.UserLogin" /> properties.
    /// </summary>
    public readonly struct Columns
    {
        /// <summary>
        /// The <c>Id</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.Id" />.
        /// </summary>
        public const string Id = "Id";
        /// <summary>
        /// The <c>EmailAddress</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.EmailAddress" />.
        /// </summary>
        public const string EmailAddress = "EmailAddress";
        /// <summary>
        /// The <c>UserId</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.UserId" />.
        /// </summary>
        public const string UserId = "UserId";
        /// <summary>
        /// The <c>UserAgent</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.UserAgent" />.
        /// </summary>
        public const string UserAgent = "UserAgent";
        /// <summary>
        /// The <c>Browser</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.Browser" />.
        /// </summary>
        public const string Browser = "Browser";
        /// <summary>
        /// The <c>OperatingSystem</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.OperatingSystem" />.
        /// </summary>
        public const string OperatingSystem = "OperatingSystem";
        /// <summary>
        /// The <c>DeviceFamily</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.DeviceFamily" />.
        /// </summary>
        public const string DeviceFamily = "DeviceFamily";
        /// <summary>
        /// The <c>DeviceBrand</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.DeviceBrand" />.
        /// </summary>
        public const string DeviceBrand = "DeviceBrand";
        /// <summary>
        /// The <c>DeviceModel</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.DeviceModel" />.
        /// </summary>
        public const string DeviceModel = "DeviceModel";
        /// <summary>
        /// The <c>IpAddress</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.IpAddress" />.
        /// </summary>
        public const string IpAddress = "IpAddress";
        /// <summary>
        /// The <c>IsSuccessful</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.IsSuccessful" />.
        /// </summary>
        public const string IsSuccessful = "IsSuccessful";
        /// <summary>
        /// The <c>FailureMessage</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.FailureMessage" />.
        /// </summary>
        public const string FailureMessage = "FailureMessage";
        /// <summary>
        /// The <c>Created</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.Created" />.
        /// </summary>
        public const string Created = "Created";
        /// <summary>
        /// The <c>CreatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.CreatedBy" />.
        /// </summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>
        /// The <c>Updated</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.Updated" />.
        /// </summary>
        public const string Updated = "Updated";
        /// <summary>
        /// The <c>UpdatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.UpdatedBy" />.
        /// </summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>
        /// The <c>RowVersion</c> column name for <see cref="Tracker.Core.Data.Entities.UserLogin.RowVersion" />.
        /// </summary>
        public const string RowVersion = "RowVersion";
    }
    #endregion
}
