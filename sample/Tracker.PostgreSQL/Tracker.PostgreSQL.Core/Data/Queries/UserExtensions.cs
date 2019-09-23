using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Queries
{
    public static partial class UserExtensions
    {
        #region Generated Extensions
        public static Tracker.Data.Entities.User GetByEmailAddress(this IQueryable<Tracker.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefault(q => q.EmailAddress == emailAddress);
        }

        public static Task<Tracker.Data.Entities.User> GetByEmailAddressAsync(this IQueryable<Tracker.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefaultAsync(q => q.EmailAddress == emailAddress);
        }

        public static Tracker.Data.Entities.User GetByKey(this IQueryable<Tracker.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Data.Entities.User> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Tracker.Data.Entities.User> GetByKeyAsync(this IQueryable<Tracker.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Data.Entities.User> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.Data.Entities.User>(task);
        }

        #endregion

    }
}
