using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tracker.Core.Data;

/// <summary>
/// Represents a session with the <c>TrackerGenerator</c> database and provides access to generated entity sets.
/// </summary>
public partial class TrackerContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TrackerContext"/> class.
    /// </summary>
    /// <param name="options">The options used to configure this <see cref="DbContext" /> instance.</param>
    public TrackerContext(DbContextOptions<TrackerContext> options)
        : base(options)
    {
    }

    #region Generated Properties
    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.Audit" /> entities mapped to the <c>dbo.Audit</c> table.
    /// </summary>
    /// <value>
    /// The <c>Audits</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.Audit> Audits { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.Priority" /> entities mapped to the <c>dbo.Priority</c> table.
    /// </summary>
    /// <value>
    /// The <c>Priorities</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.Priority> Priorities { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.Role" /> entities mapped to the <c>dbo.Role</c> table.
    /// </summary>
    /// <value>
    /// The <c>Roles</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.Role> Roles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.Status" /> entities mapped to the <c>dbo.Status</c> table.
    /// </summary>
    /// <value>
    /// The <c>Statuses</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.Status> Statuses { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entities mapped to the <c>dbo.TaskExtended</c> table.
    /// </summary>
    /// <value>
    /// The <c>TaskExtendeds</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.TaskExtended> TaskExtendeds { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.Task" /> entities mapped to the <c>dbo.Task</c> table.
    /// </summary>
    /// <value>
    /// The <c>Tasks</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.Task> Tasks { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.Tenant" /> entities mapped to the <c>dbo.Tenant</c> table.
    /// </summary>
    /// <value>
    /// The <c>Tenants</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.Tenant> Tenants { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities mapped to the <c>dbo.UserLogin</c> table.
    /// </summary>
    /// <value>
    /// The <c>UserLogins</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.UserLogin> UserLogins { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.UserRole" /> entities mapped to the <c>dbo.UserRole</c> table.
    /// </summary>
    /// <value>
    /// The <c>UserRoles</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.UserRole> UserRoles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Tracker.Core.Data.Entities.User" /> entities mapped to the <c>dbo.User</c> table.
    /// </summary>
    /// <value>
    /// The <c>Users</c> entity set.
    /// </value>
    public virtual DbSet<Tracker.Core.Data.Entities.User> Users { get; set; } = null!;

    #endregion

    /// <summary>
    /// Configures entity mappings for the generated model.
    /// </summary>
    /// <param name="modelBuilder">The builder used to configure the generated entity model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Generated Configuration
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.AuditMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.PriorityMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.RoleMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.StatusMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.TaskExtendedMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.TaskMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.TenantMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserLoginMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserRoleMap());
        #endregion
    }
}
