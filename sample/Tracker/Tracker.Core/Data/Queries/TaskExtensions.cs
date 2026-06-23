using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Provides query extension methods for <see cref="Tracker.Core.Data.Entities.Task" /> entities mapped to the <c>dbo.Task</c> table.
/// </summary>
public static partial class TaskExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Filters <see cref="Tracker.Core.Data.Entities.Task" /> entities by <c>AssignedId</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Task" /> entities.</param>
    /// <param name="assignedId">The value to match against <see cref="Tracker.Core.Data.Entities.Task.AssignedId" /> mapped to the <c>AssignedId</c> column.</param>
    /// <returns>An <see cref="IQueryable{T}" /> of <see cref="Tracker.Core.Data.Entities.Task" /> entities matching the specified values.</returns>
    public static IQueryable<Tracker.Core.Data.Entities.Task> ByAssignedId(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid? assignedId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => (q.AssignedId == assignedId || (assignedId == null && q.AssignedId == null)));
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.Task" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Task" /> entities.</param>
    /// <param name="id">The value to match against <see cref="Tracker.Core.Data.Entities.Task.Id" /> mapped to the <c>Id</c> column.</param>
    /// <returns>The matching <see cref="Tracker.Core.Data.Entities.Task" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static Tracker.Core.Data.Entities.Task? GetByKey(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid id)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.Task> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.Task" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Task" /> entities.</param>
    /// <param name="id">The value to match against <see cref="Tracker.Core.Data.Entities.Task.Id" /> mapped to the <c>Id</c> column.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching <see cref="Tracker.Core.Data.Entities.Task" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.Task?> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid id, CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.Task> dbSet)
            return await dbSet.FindAsync(new object[] { id }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    /// <summary>
    /// Filters <see cref="Tracker.Core.Data.Entities.Task" /> entities by <c>PriorityId</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Task" /> entities.</param>
    /// <param name="priorityId">The value to match against <see cref="Tracker.Core.Data.Entities.Task.PriorityId" /> mapped to the <c>PriorityId</c> column.</param>
    /// <returns>An <see cref="IQueryable{T}" /> of <see cref="Tracker.Core.Data.Entities.Task" /> entities matching the specified values.</returns>
    public static IQueryable<Tracker.Core.Data.Entities.Task> ByPriorityId(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid? priorityId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => (q.PriorityId == priorityId || (priorityId == null && q.PriorityId == null)));
    }

    /// <summary>
    /// Filters <see cref="Tracker.Core.Data.Entities.Task" /> entities by <c>StatusId</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Task" /> entities.</param>
    /// <param name="statusId">The value to match against <see cref="Tracker.Core.Data.Entities.Task.StatusId" /> mapped to the <c>StatusId</c> column.</param>
    /// <returns>An <see cref="IQueryable{T}" /> of <see cref="Tracker.Core.Data.Entities.Task" /> entities matching the specified values.</returns>
    public static IQueryable<Tracker.Core.Data.Entities.Task> ByStatusId(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid statusId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.StatusId == statusId);
    }

    /// <summary>
    /// Filters <see cref="Tracker.Core.Data.Entities.Task" /> entities by <c>TenantId</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Task" /> entities.</param>
    /// <param name="tenantId">The value to match against <see cref="Tracker.Core.Data.Entities.Task.TenantId" /> mapped to the <c>TenantId</c> column.</param>
    /// <returns>An <see cref="IQueryable{T}" /> of <see cref="Tracker.Core.Data.Entities.Task" /> entities matching the specified values.</returns>
    public static IQueryable<Tracker.Core.Data.Entities.Task> ByTenantId(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid tenantId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.TenantId == tenantId);
    }

    #endregion

}
