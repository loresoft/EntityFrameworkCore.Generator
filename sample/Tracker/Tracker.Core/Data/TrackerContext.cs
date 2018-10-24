using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tracker.Core.Data
{
    /// <summary>
    /// A <see cref="DbContext" /> instance represents a session with the database and can be used to query and save instances of entities. 
    /// </summary>
    public partial class TrackerContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by this <see cref="DbContext" />.</param>
        public TrackerContext(DbContextOptions<TrackerContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Audit"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Audit"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.Audit> Audits { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Priority"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Priority"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.Priority> Priorities { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Role"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Role"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Status"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Status"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.Status> Statuses { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Task"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.Task"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.Task> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.User"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.User"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.User> Users { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.TaskExtended"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.TaskExtended"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.TaskExtended> TaskExtendeds { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.UserLogin"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.UserLogin"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.UserLogin> UserLogins { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.UserRole"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="Tracker.Core.Data.Entities.UserRole"/>.
        /// </value>
        public virtual DbSet<Tracker.Core.Data.Entities.UserRole> UserRoles { get; set; }

        #endregion

        /// <summary>
        /// Configure the model that was discovered from the entity types exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on this context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.AuditMap());
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.PriorityMap());
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.RoleMap());
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.StatusMap());
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.TaskMap());
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.TaskExtendedMap());
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserLoginMap());
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserRoleMap());
            #endregion
        }
    }
}
