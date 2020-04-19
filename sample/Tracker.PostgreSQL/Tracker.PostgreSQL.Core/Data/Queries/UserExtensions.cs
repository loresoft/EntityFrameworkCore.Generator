using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Queries
{
    public static partial class UserExtensions
    {
        #region Generated Extensions
        public static Tracker.PostgreSQL.Core.Data.Entities.User GetByEmailAddress(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefault(q => q.EmailAddress == emailAddress);
        }

        public static Task<Tracker.PostgreSQL.Core.Data.Entities.User> GetByEmailAddressAsync(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefaultAsync(q => q.EmailAddress == emailAddress);
        }

        public static Tracker.PostgreSQL.Core.Data.Entities.User GetByKey(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.User> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Tracker.PostgreSQL.Core.Data.Entities.User> GetByKeyAsync(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.User> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.PostgreSQL.Core.Data.Entities.User>(task);
        }

        #endregion

    }
}
