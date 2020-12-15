using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tracker.Core.Data
{
    public partial class TrackerContextBase : DbContext
    {
        public TrackerContextBase(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.TenantMap());
        }
    }
}