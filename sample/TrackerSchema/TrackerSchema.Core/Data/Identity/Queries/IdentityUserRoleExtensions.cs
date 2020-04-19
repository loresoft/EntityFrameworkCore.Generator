using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Identity.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole" />.
    /// </summary>
    public static partial class IdentityUserRoleExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="roleId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> ByRoleId(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> queryable, Guid roleId)
        {
            return queryable.Where(q => q.RoleId == roleId);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> ByUserId(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> queryable, Guid userId)
        {
            return queryable.Where(q => q.UserId == userId);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <param name="roleId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole GetByKey(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> dbSet)
                return dbSet.Find(userId, roleId);

            return queryable.FirstOrDefault(q => q.UserId == userId
                && q.RoleId == roleId);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <param name="roleId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole"/> or null if not found.</returns>
        public static ValueTask<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> GetByKeyAsync(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole> dbSet)
                return dbSet.FindAsync(userId, roleId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserId == userId
                && q.RoleId == roleId);
            return new ValueTask<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole>(task);
        }

        #endregion

    }
}
