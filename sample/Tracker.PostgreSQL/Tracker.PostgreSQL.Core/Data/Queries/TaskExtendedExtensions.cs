using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Queries
{
    public static partial class TaskExtendedExtensions
    {
        #region Generated Extensions
        public static Tracker.PostgreSQL.Core.Data.Entities.TaskExtended GetByKey(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended> dbSet)
                return dbSet.Find(taskId);

            return queryable.FirstOrDefault(q => q.TaskId == taskId);
        }

        public static ValueTask<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended> GetByKeyAsync(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended> dbSet)
                return dbSet.FindAsync(taskId);

            var task = queryable.FirstOrDefaultAsync(q => q.TaskId == taskId);
            return new ValueTask<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended>(task);
        }

        #endregion

    }
}
