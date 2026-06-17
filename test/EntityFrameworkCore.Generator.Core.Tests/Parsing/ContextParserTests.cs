using EntityFrameworkCore.Generator.Parsing;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests.Parsing;

public class ContextParserTests
{
    [Fact]
    public void ParseCodeBasic()
    {
        var parser = new ContextParser(NullLoggerFactory.Instance);

        var source = """
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
                    public virtual DbSet<Tracker.Data.Entities.Status> Statuses { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.Task> Tasks { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.User> Users { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.TaskExtended> TaskExtended { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.UserLogin> UserLogins { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.UserRole> UserRoles { get; set; }
                    #endregion

                    protected override void OnModelCreating(ModelBuilder modelBuilder)
                    {
                        #region Generated Configuration
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.AuditMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.PriorityMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.RoleMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.StatusMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskExtendedMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserLoginMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserRoleMap());
                        #endregion
                    }
                }
            }
            """;

        var result = parser.ParseCode(source);
        Assert.NotNull(result);
        Assert.Equal(9, result.Properties.Count);
        Assert.Equal("TrackerContext", result.ContextClass);

    }

    [Fact]
    public void ParseCodeIdentity()
    {
        var parser = new ContextParser(NullLoggerFactory.Instance);

        var source = """
            using System;
            using Microsoft.EntityFrameworkCore;
            using Microsoft.EntityFrameworkCore.Metadata;

            namespace Tracker.Data
            {
                public partial class TrackerContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
                {
                    public TrackerContext(DbContextOptions<TrackerContext> options)
                        : base(options)
                    {
                    }

                    #region Generated Properties
                    public virtual DbSet<Tracker.Data.Entities.Audit> Audits { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.Priority> Priorities { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.Role> Roles { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.Status> Statuses { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.Task> Tasks { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.User> Users { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.TaskExtended> TaskExtended { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.UserLogin> UserLogins { get; set; }
                    public virtual DbSet<Tracker.Data.Entities.UserRole> UserRoles { get; set; }
                    #endregion

                    protected override void OnModelCreating(ModelBuilder modelBuilder)
                    {
                        #region Generated Configuration
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.AuditMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.PriorityMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.RoleMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.StatusMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskExtendedMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserLoginMap());
                        modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserRoleMap());
                        #endregion
                    }
                }
            }
            """;

        var result = parser.ParseCode(source);
        Assert.NotNull(result);
        Assert.Equal(9, result.Properties.Count);
        Assert.Equal("TrackerContext", result.ContextClass);

    }

    [Fact]
    public void ParseNonContextFile()
    {
        var parser = new ContextParser(NullLoggerFactory.Instance);

        var source = """
            namespace InstructorIQ.Core.Options
            {
                public class HostingConfiguration : Options<User>
                {
                    public string Version { get; set; }
                }
            }
            """;

        var result = parser.ParseCode(source);
        Assert.Null(result);

    }

}
