using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Queries
{
    public static partial class RoleExtensions
    {
        #region Generated Extensions
        public static Tracker.PostgreSQL.Core.Data.Entities.Role GetByKey(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.Role> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.Role> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Tracker.PostgreSQL.Core.Data.Entities.Role> GetByKeyAsync(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.Role> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.Role> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.PostgreSQL.Core.Data.Entities.Role>(task);
        }

        public static Tracker.PostgreSQL.Core.Data.Entities.Role GetByName(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.Role> queryable, string name)
        {
            return queryable.FirstOrDefault(q => q.Name == name);
        }

        public static Task<Tracker.PostgreSQL.Core.Data.Entities.Role> GetByNameAsync(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.Role> queryable, string name)
        {
            return queryable.FirstOrDefaultAsync(q => q.Name == name);
        }

        #endregion

    }
}
