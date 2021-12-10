using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="Tracker.Core.Data.Entities.UserRole" />
    /// </summary>
    public partial class UserRoleMap
        : IEntityTypeConfiguration<Tracker.Core.Data.Entities.UserRole>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Tracker.Core.Data.Entities.UserRole" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Core.Data.Entities.UserRole> builder)
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
        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="Tracker.Core.Data.Entities.UserRole" /></summary>
            public const string Schema = "dbo";
            /// <summary>Table Name constant for entity <see cref="Tracker.Core.Data.Entities.UserRole" /></summary>
            public const string Name = "UserRole";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserRole.UserId" /></summary>
            public const string UserId = "UserId";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.UserRole.RoleId" /></summary>
            public const string RoleId = "RoleId";
        }
        #endregion
    }
}
