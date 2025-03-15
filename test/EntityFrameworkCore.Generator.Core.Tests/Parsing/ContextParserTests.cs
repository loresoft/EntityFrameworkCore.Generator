using System.Text;
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

        var sb = new StringBuilder();
        sb.AppendLine(@"using System;");
        sb.AppendLine(@"using Microsoft.EntityFrameworkCore;");
        sb.AppendLine(@"using Microsoft.EntityFrameworkCore.Metadata;");
        sb.AppendLine(@"");
        sb.AppendLine(@"namespace Tracker.Data");
        sb.AppendLine(@"{");
        sb.AppendLine(@"    public partial class TrackerContext : DbContext");
        sb.AppendLine(@"    {");
        sb.AppendLine(@"        public TrackerContext(DbContextOptions<TrackerContext> options)");
        sb.AppendLine(@"            : base(options)");
        sb.AppendLine(@"        {");
        sb.AppendLine(@"        }");
        sb.AppendLine(@"");
        sb.AppendLine(@"        #region Generated Properties");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Audit> Audits { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Priority> Priorities { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Role> Roles { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Status> Statuses { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Task> Tasks { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.User> Users { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.TaskExtended> TaskExtended { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.UserLogin> UserLogins { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.UserRole> UserRoles { get; set; }");
        sb.AppendLine(@"        #endregion");
        sb.AppendLine(@"");
        sb.AppendLine(@"        protected override void OnModelCreating(ModelBuilder modelBuilder)");
        sb.AppendLine(@"        {");
        sb.AppendLine(@"            #region Generated Configuration");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.AuditMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.PriorityMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.RoleMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.StatusMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskExtendedMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserLoginMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserRoleMap());");
        sb.AppendLine(@"            #endregion");
        sb.AppendLine(@"        }");
        sb.AppendLine(@"    }");
        sb.AppendLine(@"}");

        var result = parser.ParseCode(sb.ToString());
        Assert.NotNull(result);
        Assert.Equal(9, result.Properties.Count);
        Assert.Equal("TrackerContext", result.ContextClass);

    }

    [Fact]
    public void ParseCodeIdentity()
    {
        var parser = new ContextParser(NullLoggerFactory.Instance);

        var sb = new StringBuilder();
        sb.AppendLine(@"using System;");
        sb.AppendLine(@"using Microsoft.EntityFrameworkCore;");
        sb.AppendLine(@"using Microsoft.EntityFrameworkCore.Metadata;");
        sb.AppendLine(@"");
        sb.AppendLine(@"namespace Tracker.Data");
        sb.AppendLine(@"{");
        sb.AppendLine(@"    public partial class TrackerContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>");
        sb.AppendLine(@"    {");
        sb.AppendLine(@"        public TrackerContext(DbContextOptions<TrackerContext> options)");
        sb.AppendLine(@"            : base(options)");
        sb.AppendLine(@"        {");
        sb.AppendLine(@"        }");
        sb.AppendLine(@"");
        sb.AppendLine(@"        #region Generated Properties");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Audit> Audits { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Priority> Priorities { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Role> Roles { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Status> Statuses { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.Task> Tasks { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.User> Users { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.TaskExtended> TaskExtended { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.UserLogin> UserLogins { get; set; }");
        sb.AppendLine(@"        public virtual DbSet<Tracker.Data.Entities.UserRole> UserRoles { get; set; }");
        sb.AppendLine(@"        #endregion");
        sb.AppendLine(@"");
        sb.AppendLine(@"        protected override void OnModelCreating(ModelBuilder modelBuilder)");
        sb.AppendLine(@"        {");
        sb.AppendLine(@"            #region Generated Configuration");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.AuditMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.PriorityMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.RoleMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.StatusMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.TaskExtendedMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserLoginMap());");
        sb.AppendLine(@"            modelBuilder.ApplyConfiguration(new Tracker.Data.Mapping.UserRoleMap());");
        sb.AppendLine(@"            #endregion");
        sb.AppendLine(@"        }");
        sb.AppendLine(@"    }");
        sb.AppendLine(@"}");

        var result = parser.ParseCode(sb.ToString());
        Assert.NotNull(result);
        Assert.Equal(9, result.Properties.Count);
        Assert.Equal("TrackerContext", result.ContextClass);

    }

    [Fact]
    public void ParseNonContextFile()
    {
        var parser = new ContextParser(NullLoggerFactory.Instance);

        var sb = new StringBuilder();
        sb.AppendLine(@"namespace InstructorIQ.Core.Options");
        sb.AppendLine(@"{");
        sb.AppendLine(@"    public class HostingConfiguration : Options<User>");
        sb.AppendLine(@"    {");
        sb.AppendLine(@"        public string Version { get; set; }");
        sb.AppendLine(@"    }");
        sb.AppendLine(@"}");

        var result = parser.ParseCode(sb.ToString());
        Assert.Null(result);

    }

}