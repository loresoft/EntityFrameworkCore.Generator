using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tracker.Data
{
    public partial class TrackerContext : DbContext
    {
        public TrackerContext(DbContextOptions<TrackerContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<Tracker.Data.Entities.Audit> Audits { get; set; }

        public virtual DbSet<Tracker.Data.Entities.Priority> Priorities { get; set; }

        public virtual DbSet<Tracker.Data.Entities.Role> Roles { get; set; }

        public virtual DbSet<Tracker.Data.Entities.Schemaversions> Schemaversions { get; set; }

        public virtual DbSet<Tracker.Data.Entities.Status> Statuses { get; set; }

        public virtual DbSet<Tracker.Data.Entities.TaskExtended> TaskExtendeds { get; set; }

        public virtual DbSet<Tracker.Data.Entities.Task> Tasks { get; set; }

        public virtual DbSet<Tracker.Data.Entities.UserLogin> UserLogins { get; set; }

        public virtual DbSet<Tracker.Data.Entities.UserRole> UserRoles { get; set; }

        public virtual DbSet<Tracker.Data.Entities.User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.AuditMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.PriorityMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.RoleMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.SchemaversionsMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.StatusMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskExtendedMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserLoginMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserRoleMap());
            #endregion
        }
    }
}
