using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Queries
{
    public static partial class TaskExtendedExtensions
    {
        #region Generated Extensions
        public static Tracker.Data.Entities.TaskExtended GetByKey(this IQueryable<Tracker.Data.Entities.TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<Tracker.Data.Entities.TaskExtended> dbSet)
                return dbSet.Find(taskId);

            return queryable.FirstOrDefault(q => q.TaskId == taskId);
        }

        public static ValueTask<Tracker.Data.Entities.TaskExtended> GetByKeyAsync(this IQueryable<Tracker.Data.Entities.TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<Tracker.Data.Entities.TaskExtended> dbSet)
                return dbSet.FindAsync(taskId);

            var task = queryable.FirstOrDefaultAsync(q => q.TaskId == taskId);
            return new ValueTask<Tracker.Data.Entities.TaskExtended>(task);
        }

        #endregion

    }
}
