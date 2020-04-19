using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrackerSchema.Core.Data
{
    /// <summary>
    /// A <see cref="DbContext" /> instance represents a session with the database and can be used to query and save instances of entities. 
    /// </summary>
    public partial class TrackerSchemaContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerSchemaContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by this <see cref="DbContext" />.</param>
        public TrackerSchemaContext(DbContextOptions<TrackerSchemaContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> IdentityRoles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> IdentityUserLogins { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> IdentityUserRoles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> IdentityUsers { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit> TrackerAudits { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority> TrackerPriorities { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus> TrackerStatuses { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended> TrackerTaskExtendeds { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTask"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTask"/>.
        /// </value>
        public virtual DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> TrackerTasks { get; set; }

        #endregion

        /// <summary>
        /// Configure the model that was discovered from the entity types exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on this context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Identity.Mapping.IdentityRoleMap());
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Identity.Mapping.IdentityUserLoginMap());
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Identity.Mapping.IdentityUserMap());
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Identity.Mapping.IdentityUserRoleMap());
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Tracker.Mapping.TrackerAuditMap());
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Tracker.Mapping.TrackerPriorityMap());
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Tracker.Mapping.TrackerStatusMap());
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Tracker.Mapping.TrackerTaskExtendedMap());
            modelBuilder.ApplyConfiguration(new TrackerSchema.Core.Data.Tracker.Mapping.TrackerTaskMap());
            #endregion
        }
    }
}
