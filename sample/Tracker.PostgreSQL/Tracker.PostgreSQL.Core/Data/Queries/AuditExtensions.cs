using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Queries
{
    public static partial class AuditExtensions
    {
        #region Generated Extensions
        public static Tracker.PostgreSQL.Core.Data.Entities.Audit GetByKey(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.Audit> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.Audit> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Tracker.PostgreSQL.Core.Data.Entities.Audit> GetByKeyAsync(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.Audit> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.Audit> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.PostgreSQL.Core.Data.Entities.Audit>(task);
        }

        #endregion

    }
}
