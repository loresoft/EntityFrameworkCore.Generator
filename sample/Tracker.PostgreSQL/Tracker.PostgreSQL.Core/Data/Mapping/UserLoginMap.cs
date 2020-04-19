using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Mapping
{
    public partial class UserLoginMap
        : IEntityTypeConfiguration<Tracker.PostgreSQL.Core.Data.Entities.UserLogin>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.PostgreSQL.Core.Data.Entities.UserLogin> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserLogin", "public");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("uuid");

            builder.Property(t => t.EmailAddress)
                .IsRequired()
                .HasColumnName("EmailAddress")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uuid");

            builder.Property(t => t.UserAgent)
                .HasColumnName("UserAgent")
                .HasColumnType("text");

            builder.Property(t => t.Browser)
                .HasColumnName("Browser")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.OperatingSystem)
                .HasColumnName("OperatingSystem")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.DeviceFamily)
                .HasColumnName("DeviceFamily")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.DeviceBrand)
                .HasColumnName("DeviceBrand")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.DeviceModel)
                .HasColumnName("DeviceModel")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.IpAddress)
                .HasColumnName("IpAddress")
                .HasColumnType("character varying(50)")
                .HasMaxLength(50);

            builder.Property(t => t.IsSuccessful)
                .IsRequired()
                .HasColumnName("IsSuccessful")
                .HasColumnType("boolean");

            builder.Property(t => t.FailureMessage)
                .HasColumnName("FailureMessage")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

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
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserLogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserLogin_User_UserId");

            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "public";
        public const string TableName = "UserLogin";

        public const string ColumnId = "Id";
        public const string ColumnEmailAddress = "EmailAddress";
        public const string ColumnUserId = "UserId";
        public const string ColumnUserAgent = "UserAgent";
        public const string ColumnBrowser = "Browser";
        public const string ColumnOperatingSystem = "OperatingSystem";
        public const string ColumnDeviceFamily = "DeviceFamily";
        public const string ColumnDeviceBrand = "DeviceBrand";
        public const string ColumnDeviceModel = "DeviceModel";
        public const string ColumnIpAddress = "IpAddress";
        public const string ColumnIsSuccessful = "IsSuccessful";
        public const string ColumnFailureMessage = "FailureMessage";
        public const string ColumnCreated = "Created";
        public const string ColumnCreatedBy = "CreatedBy";
        public const string ColumnUpdated = "Updated";
        public const string ColumnUpdatedBy = "UpdatedBy";
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
