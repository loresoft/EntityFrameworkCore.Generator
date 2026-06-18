using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Provides query extension methods for <see cref="Tracker.Core.Data.Entities.UserRole" /> entities mapped to the <c>dbo.UserRole</c> table.
/// </summary>
public static partial class UserRoleExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Filters <see cref="Tracker.Core.Data.Entities.UserRole" /> entities by <c>RoleId</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.UserRole" /> entities.</param>
    /// <param name="roleId">The value to match against <see cref="Tracker.Core.Data.Entities.UserRole.RoleId" /> mapped to the <c>RoleId</c> column.</param>
    /// <returns>An <see cref="IQueryable{T}" /> of <see cref="Tracker.Core.Data.Entities.UserRole" /> entities matching the specified values.</returns>
    public static IQueryable<Tracker.Core.Data.Entities.UserRole> ByRoleId(this IQueryable<Tracker.Core.Data.Entities.UserRole> queryable, Guid roleId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.RoleId == roleId);
    }

    /// <summary>
    /// Filters <see cref="Tracker.Core.Data.Entities.UserRole" /> entities by <c>UserId</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.UserRole" /> entities.</param>
    /// <param name="userId">The value to match against <see cref="Tracker.Core.Data.Entities.UserRole.UserId" /> mapped to the <c>UserId</c> column.</param>
    /// <returns>An <see cref="IQueryable{T}" /> of <see cref="Tracker.Core.Data.Entities.UserRole" /> entities matching the specified values.</returns>
    public static IQueryable<Tracker.Core.Data.Entities.UserRole> ByUserId(this IQueryable<Tracker.Core.Data.Entities.UserRole> queryable, Guid userId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.UserId == userId);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.UserRole" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.UserRole" /> entities.</param>
    /// <param name="userId">The value to match against <see cref="Tracker.Core.Data.Entities.UserRole.UserId" /> mapped to the <c>UserId</c> column.</param>
    /// <param name="roleId">The value to match against <see cref="Tracker.Core.Data.Entities.UserRole.RoleId" /> mapped to the <c>RoleId</c> column.</param>
    /// <returns>The matching <see cref="Tracker.Core.Data.Entities.UserRole" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static Tracker.Core.Data.Entities.UserRole? GetByKey(this IQueryable<Tracker.Core.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.UserRole> dbSet)
            return dbSet.Find(userId, roleId);

        return queryable.FirstOrDefault(q => q.UserId == userId
            && q.RoleId == roleId);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.UserRole" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.UserRole" /> entities.</param>
    /// <param name="userId">The value to match against <see cref="Tracker.Core.Data.Entities.UserRole.UserId" /> mapped to the <c>UserId</c> column.</param>
    /// <param name="roleId">The value to match against <see cref="Tracker.Core.Data.Entities.UserRole.RoleId" /> mapped to the <c>RoleId</c> column.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching <see cref="Tracker.Core.Data.Entities.UserRole" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.UserRole?> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.UserRole> queryable, Guid userId, Guid roleId, CancellationToken cancellationToken = default)
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
