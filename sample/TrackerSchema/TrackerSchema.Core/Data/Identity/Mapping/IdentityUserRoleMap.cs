using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Identity.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole" />
    /// </summary>
    public partial class IdentityUserRoleMap
        : IEntityTypeConfiguration<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole>
    {
        /// <summary>
        /// Configures the entity of type <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserRole", "Identity");

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
            builder.HasOne(t => t.RoleIdentityRole)
                .WithMany(t => t.RoleIdentityUserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRole_Role_RoleId");

            builder.HasOne(t => t.UserIdentityUser)
                .WithMany(t => t.UserIdentityUserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRole_User_UserId");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole" /></summary>
            public const string Schema = "Identity";
            /// <summary>Table Name constant for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole" /></summary>
            public const string Name = "UserRole";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole.UserId" /></summary>
            public const string UserId = "UserId";
            /// <summary>Column Name constant for property <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole.RoleId" /></summary>
            public const string RoleId = "RoleId";
        }
        #endregion
    }
}
