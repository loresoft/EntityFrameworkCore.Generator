using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Query extensions for entity <see cref="Tracker.Core.Data.Entities.UserRole" />.
/// </summary>
public static partial class UserRoleExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Filters a sequence of values based on a predicate.
    /// </summary>
    /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
    /// <param name="roleId">The value to filter by.</param>
    /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
    public static System.Linq.IQueryable<Tracker.Core.Data.Entities.UserRole> ByRoleId(this System.Linq.IQueryable<Tracker.Core.Data.Entities.UserRole> queryable, Guid roleId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.RoleId == roleId);
    }

    /// <summary>
    /// Filters a sequence of values based on a predicate.
    /// </summary>
    /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
    /// <param name="userId">The value to filter by.</param>
    /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
    public static System.Linq.IQueryable<Tracker.Core.Data.Entities.UserRole> ByUserId(this System.Linq.IQueryable<Tracker.Core.Data.Entities.UserRole> queryable, Guid userId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.UserId == userId);
    }

    /// <summary>
    /// Gets an instance by the primary key.
    /// </summary>
    /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
    /// <param name="userId">The value to filter by.</param>
    /// <param name="roleId">The value to filter by.</param>
    /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.UserRole"/> or null if not found.</returns>
    public static Tracker.Core.Data.Entities.UserRole? GetByKey(this System.Linq.IQueryable<Tracker.Core.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.UserRole> dbSet)
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
    /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.UserRole"/> or null if not found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.UserRole?> GetByKeyAsync(this System.Linq.IQueryable<Tracker.Core.Data.Entities.UserRole> queryable, Guid userId, Guid roleId, System.Threading.CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.UserRole> dbSet)
            return await dbSet.FindAsync(new object[] { userId, roleId }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.UserId == userId
            && q.RoleId == roleId, cancellationToken);
    }

    #endregion

}
