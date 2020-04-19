using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.PostgreSQL.Core.Data.Queries
{
    public static partial class UserRoleExtensions
    {
        #region Generated Extensions
        public static IQueryable<Tracker.PostgreSQL.Core.Data.Entities.UserRole> ByRoleId(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.UserRole> queryable, Guid roleId)
        {
            return queryable.Where(q => q.RoleId == roleId);
        }

        public static IQueryable<Tracker.PostgreSQL.Core.Data.Entities.UserRole> ByUserId(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.UserRole> queryable, Guid userId)
        {
            return queryable.Where(q => q.UserId == userId);
        }

        public static Tracker.PostgreSQL.Core.Data.Entities.UserRole GetByKey(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.UserRole> dbSet)
                return dbSet.Find(userId, roleId);

            return queryable.FirstOrDefault(q => q.UserId == userId
                && q.RoleId == roleId);
        }

        public static ValueTask<Tracker.PostgreSQL.Core.Data.Entities.UserRole> GetByKeyAsync(this IQueryable<Tracker.PostgreSQL.Core.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<Tracker.PostgreSQL.Core.Data.Entities.UserRole> dbSet)
                return dbSet.FindAsync(userId, roleId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserId == userId
                && q.RoleId == roleId);
            return new ValueTask<Tracker.PostgreSQL.Core.Data.Entities.UserRole>(task);
        }

        #endregion

    }
}
