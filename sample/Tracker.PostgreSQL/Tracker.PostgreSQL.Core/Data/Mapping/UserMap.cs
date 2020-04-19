using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Mapping
{
    public partial class UserMap
        : IEntityTypeConfiguration<Tracker.PostgreSQL.Core.Data.Entities.User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.PostgreSQL.Core.Data.Entities.User> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("User", "public");

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

            builder.Property(t => t.IsEmailAddressConfirmed)
                .IsRequired()
                .HasColumnName("IsEmailAddressConfirmed")
                .HasColumnType("boolean");

            builder.Property(t => t.DisplayName)
                .IsRequired()
                .HasColumnName("DisplayName")
                .HasColumnType("character varying(256)")
                .HasMaxLength(256);

            builder.Property(t => t.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasColumnType("text");

            builder.Property(t => t.ResetHash)
                .HasColumnName("ResetHash")
                .HasColumnType("text");

            builder.Property(t => t.InviteHash)
                .HasColumnName("InviteHash")
                .HasColumnType("text");

            builder.Property(t => t.AccessFailedCount)
                .IsRequired()
                .HasColumnName("AccessFailedCount")
                .HasColumnType("integer");

            builder.Property(t => t.LockoutEnabled)
                .IsRequired()
                .HasColumnName("LockoutEnabled")
                .HasColumnType("boolean");

            builder.Property(t => t.LockoutEnd)
                .HasColumnName("LockoutEnd")
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.LastLogin)
                .HasColumnName("LastLogin")
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.IsDeleted)
                .IsRequired()
                .HasColumnName("IsDeleted")
                .HasColumnType("boolean");

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
            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "public";
        public const string TableName = "User";

        public const string ColumnId = "Id";
        public const string ColumnEmailAddress = "EmailAddress";
        public const string ColumnIsEmailAddressConfirmed = "IsEmailAddressConfirmed";
        public const string ColumnDisplayName = "DisplayName";
        public const string ColumnPasswordHash = "PasswordHash";
        public const string ColumnResetHash = "ResetHash";
        public const string ColumnInviteHash = "InviteHash";
        public const string ColumnAccessFailedCount = "AccessFailedCount";
        public const string ColumnLockoutEnabled = "LockoutEnabled";
        public const string ColumnLockoutEnd = "LockoutEnd";
        public const string ColumnLastLogin = "LastLogin";
        public const string ColumnIsDeleted = "IsDeleted";
        public const string ColumnCreated = "Created";
        public const string ColumnCreatedBy = "CreatedBy";
        public const string ColumnUpdated = "Updated";
        public const string ColumnUpdatedBy = "UpdatedBy";
        public const string ColumnRowVersion = "RowVersion";
        #endregion
    }
}
