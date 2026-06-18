using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Configures Entity Framework Core mapping for the <see cref="Tracker.Core.Data.Entities.UserRole" /> entity mapped to the <c>dbo.UserRole</c> table.
/// </summary>
public partial class UserRoleMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.UserRole>
{
    /// <summary>
    /// Configures the table, key, property, and relationship mappings for <see cref="Tracker.Core.Data.Entities.UserRole" />.
    /// </summary>
    /// <param name="builder">The builder used to configure <see cref="Tracker.Core.Data.Entities.UserRole" />.</param>
    public void Configure(EntityTypeBuilder<Tracker.Core.Data.Entities.UserRole> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("UserRole", "dbo");

        // key
        builder.HasKey(t => new { t.UserId, t.RoleId });

        // properties
        builder.Property(t => t.UserId)
            .IsRequired()
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.RoleId)
            .IsRequired()
            .HasColumnName("RoleId")
            .HasColumnType("uniqueidentifier");

        // relationships
        builder.HasOne(t => t.Role)
            .WithMany(t => t.UserRoles)
            .HasForeignKey(d => d.RoleId)
            .HasConstraintName("FK_UserRole_Role_RoleId");

        builder.HasOne(t => t.User)
            .WithMany(t => t.UserRoles)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserRole_User_UserId");

        #endregion
    }

    #region Generated Constants
    /// <summary>
    /// Contains table mapping constants for <see cref="Tracker.Core.Data.Entities.UserRole" />.
    /// </summary>
    public readonly struct Table
    {
        /// <summary>
        /// The database schema name for <see cref="Tracker.Core.Data.Entities.UserRole" />.
        /// </summary>
        public const string Schema = "dbo";
        /// <summary>
        /// The database table name for <see cref="Tracker.Core.Data.Entities.UserRole" />.
        /// </summary>
        public const string Name = "UserRole";
    }

    /// <summary>
    /// Contains column name constants for <see cref="Tracker.Core.Data.Entities.UserRole" /> properties.
    /// </summary>
    public readonly struct Columns
    {
        /// <summary>
        /// The <c>UserId</c> column name for <see cref="Tracker.Core.Data.Entities.UserRole.UserId" />.
        /// </summary>
        public const string UserId = "UserId";
        /// <summary>
        /// The <c>RoleId</c> column name for <see cref="Tracker.Core.Data.Entities.UserRole.RoleId" />.
        /// </summary>
        public const string RoleId = "RoleId";
    }
    #endregion
}
