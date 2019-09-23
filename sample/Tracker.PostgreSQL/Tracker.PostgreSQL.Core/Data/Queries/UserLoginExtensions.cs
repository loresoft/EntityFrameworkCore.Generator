using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Queries
{
    public static partial class UserLoginExtensions
    {
        #region Generated Extensions
        public static IQueryable<Tracker.Data.Entities.UserLogin> ByEmailAddress(this IQueryable<Tracker.Data.Entities.UserLogin> queryable, string emailAddress)
        {
            return queryable.Where(q => q.EmailAddress == emailAddress);
        }

        public static Tracker.Data.Entities.UserLogin GetByKey(this IQueryable<Tracker.Data.Entities.UserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Data.Entities.UserLogin> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Tracker.Data.Entities.UserLogin> GetByKeyAsync(this IQueryable<Tracker.Data.Entities.UserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Data.Entities.UserLogin> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.Data.Entities.UserLogin>(task);
        }

        public static IQueryable<Tracker.Data.Entities.UserLogin> ByUserId(this IQueryable<Tracker.Data.Entities.UserLogin> queryable, Guid? userId)
        {
            return queryable.Where(q => (q.UserId == userId || (userId == null && q.UserId == null)));
        }

        #endregion

    }
}
