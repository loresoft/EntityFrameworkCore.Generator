using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Mapping
{
    public partial class UserRoleMap
        : IEntityTypeConfiguration<Tracker.Data.Entities.UserRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Data.Entities.UserRole> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserRole", "public");

            // key
            builder.HasKey(t => new { t.UserId, t.RoleId });

            // properties
            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("uuid");

            builder.Property(t => t.RoleId)
                .IsRequired()
                .HasColumnName("RoleId")
                .HasColumnType("uuid");

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

    }
}
