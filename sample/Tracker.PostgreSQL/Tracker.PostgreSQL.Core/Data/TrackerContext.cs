using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tracker.PostgreSQL.Core.Data
{
    public partial class TrackerContext : DbContext
    {
        public TrackerContext(DbContextOptions<TrackerContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.Audit> Audits { get; set; }

        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.Priority> Priorities { get; set; }

        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.Role> Roles { get; set; }

        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.Status> Status { get; set; }

        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended> TaskExtended { get; set; }

        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.Task> Tasks { get; set; }

        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.UserLogin> UserLogins { get; set; }

        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.UserRole> UserRoles { get; set; }

        public virtual DbSet<Tracker.PostgreSQL.Core.Data.Entities.User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.AuditMap());
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.PriorityMap());
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.RoleMap());
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.StatusMap());
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.TaskExtendedMap());
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.TaskMap());
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.UserLoginMap());
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new Tracker.PostgreSQL.Core.Data.Mapping.UserRoleMap());
            #endregion
        }
    }
}
