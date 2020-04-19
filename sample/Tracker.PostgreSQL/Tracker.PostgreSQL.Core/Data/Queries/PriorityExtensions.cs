using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Queries
{
    public static partial class PriorityExtensions
    {
        #region Generated Extensions
        public static Tracker.PostgreSQL.Core.Data.Entities.Priority GetByKey(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.Priority> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.Priority> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Tracker.PostgreSQL.Core.Data.Entities.Priority> GetByKeyAsync(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.Priority> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.Priority> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.PostgreSQL.Core.Data.Entities.Priority>(task);
        }

        #endregion

    }
}
