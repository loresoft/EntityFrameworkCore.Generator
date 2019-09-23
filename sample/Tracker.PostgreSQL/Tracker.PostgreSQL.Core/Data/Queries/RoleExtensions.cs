using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tracker.Data.Entities;

namespace Tracker.Data.Queries
{
    public static partial class RoleExtensions
    {
        #region Generated Extensions
        public static Tracker.Data.Entities.Role GetByKey(this IQueryable<Tracker.Data.Entities.Role> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Data.Entities.Role> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Tracker.Data.Entities.Role> GetByKeyAsync(this IQueryable<Tracker.Data.Entities.Role> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Data.Entities.Role> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.Data.Entities.Role>(task);
        }

        public static Tracker.Data.Entities.Role GetByName(this IQueryable<Tracker.Data.Entities.Role> queryable, string name)
        {
            return queryable.FirstOrDefault(q => q.Name == name);
        }

        public static Task<Tracker.Data.Entities.Role> GetByNameAsync(this IQueryable<Tracker.Data.Entities.Role> queryable, string name)
        {
            return queryable.FirstOrDefaultAsync(q => q.Name == name);
        }

        #endregion

    }
}
