using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Core.Data.Mapping;

/// <summary>
/// Configures Entity Framework Core mapping for the <see cref="Tracker.Core.Data.Entities.Task" /> entity mapped to the <c>dbo.Task</c> table.
/// </summary>
public partial class TaskMap
    : IEntityTypeConfiguration<Tracker.Core.Data.Entities.Task>
{
    /// <summary>
    /// Configures the table, key, property, and relationship mappings for <see cref="Tracker.Core.Data.Entities.Task" />.
    /// </summary>
    /// <param name="builder">The builder used to configure <see cref="Tracker.Core.Data.Entities.Task" />.</param>
    public void Configure(EntityTypeBuilder<Tracker.Core.Data.Entities.Task> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("Task", "dbo");

        builder
            .ToTable(tableBuilder => tableBuilder
                .IsTemporal(temporalBuilder =>
                {
                    temporalBuilder
                        .UseHistoryTable("Task", "History");
                    temporalBuilder
                        .HasPeriodStart("PeriodStart")
                        .HasColumnName("PeriodStart");
                    temporalBuilder
                        .HasPeriodEnd("PeriodEnd")
                        .HasColumnName("PeriodEnd");
                })
            );

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(t => t.StatusId)
            .IsRequired()
            .HasColumnName("StatusId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.PriorityId)
            .HasColumnName("PriorityId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("nvarchar(255)")
            .HasMaxLength(255);

        builder.Property(t => t.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.StartDate)
            .HasColumnName("StartDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.DueDate)
            .HasColumnName("DueDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.CompleteDate)
            .HasColumnName("CompleteDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.AssignedId)
            .HasColumnName("AssignedId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.TenantId)
            .IsRequired()
            .HasColumnName("TenantId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.Created)
            .IsRequired()
            .HasColumnName("Created")
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.CreatedBy)
            .HasColumnName("CreatedBy")
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.Updated)
            .IsRequired()
            .HasColumnName("Updated")
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.UpdatedBy)
            .HasColumnName("UpdatedBy")
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.RowVersion)
            .IsRequired()
            .IsRowVersion()
            .IsConcurrencyToken()
            .HasColumnName("RowVersion")
            .HasColumnType("rowversion")
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(t => t.PeriodStart1)
            .IsRequired()
            .HasColumnName("PeriodStart")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.PeriodEnd1)
            .IsRequired()
            .HasColumnName("PeriodEnd")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("('9999-12-31 23:59:59.9999999')");

        // relationships
        builder.HasOne(t => t.Priority)
            .WithMany(t => t.Tasks)
            .HasForeignKey(d => d.PriorityId)
            .HasConstraintName("FK_Task_Priority_PriorityId");

        builder.HasOne(t => t.Status)
            .WithMany(t => t.Tasks)
            .HasForeignKey(d => d.StatusId)
            .HasConstraintName("FK_Task_Status_StatusId");

        builder.HasOne(t => t.Tenant)
            .WithMany(t => t.Tasks)
            .HasForeignKey(d => d.TenantId)
            .HasConstraintName("FK_Task_Tenant_TenantId");

        builder.HasOne(t => t.AssignedUser)
            .WithMany(t => t.AssignedTasks)
            .HasForeignKey(d => d.AssignedId)
            .HasConstraintName("FK_Task_User_AssignedId");

        #endregion
    }

    #region Generated Constants
    /// <summary>
    /// Contains table mapping constants for <see cref="Tracker.Core.Data.Entities.Task" />.
    /// </summary>
    public readonly struct Table
    {
        /// <summary>
        /// The database schema name for <see cref="Tracker.Core.Data.Entities.Task" />.
        /// </summary>
        public const string Schema = "dbo";
        /// <summary>
        /// The database table name for <see cref="Tracker.Core.Data.Entities.Task" />.
        /// </summary>
        public const string Name = "Task";
    }

    /// <summary>
    /// Contains column name constants for <see cref="Tracker.Core.Data.Entities.Task" /> properties.
    /// </summary>
    public readonly struct Columns
    {
        /// <summary>
        /// The <c>Id</c> column name for <see cref="Tracker.Core.Data.Entities.Task.Id" />.
        /// </summary>
        public const string Id = "Id";
        /// <summary>
        /// The <c>StatusId</c> column name for <see cref="Tracker.Core.Data.Entities.Task.StatusId" />.
        /// </summary>
        public const string StatusId = "StatusId";
        /// <summary>
        /// The <c>PriorityId</c> column name for <see cref="Tracker.Core.Data.Entities.Task.PriorityId" />.
        /// </summary>
        public const string PriorityId = "PriorityId";
        /// <summary>
        /// The <c>Title</c> column name for <see cref="Tracker.Core.Data.Entities.Task.Title" />.
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// The <c>Description</c> column name for <see cref="Tracker.Core.Data.Entities.Task.Description" />.
        /// </summary>
        public const string Description = "Description";
        /// <summary>
        /// The <c>StartDate</c> column name for <see cref="Tracker.Core.Data.Entities.Task.StartDate" />.
        /// </summary>
        public const string StartDate = "StartDate";
        /// <summary>
        /// The <c>DueDate</c> column name for <see cref="Tracker.Core.Data.Entities.Task.DueDate" />.
        /// </summary>
        public const string DueDate = "DueDate";
        /// <summary>
        /// The <c>CompleteDate</c> column name for <see cref="Tracker.Core.Data.Entities.Task.CompleteDate" />.
        /// </summary>
        public const string CompleteDate = "CompleteDate";
        /// <summary>
        /// The <c>AssignedId</c> column name for <see cref="Tracker.Core.Data.Entities.Task.AssignedId" />.
        /// </summary>
        public const string AssignedId = "AssignedId";
        /// <summary>
        /// The <c>TenantId</c> column name for <see cref="Tracker.Core.Data.Entities.Task.TenantId" />.
        /// </summary>
        public const string TenantId = "TenantId";
        /// <summary>
        /// The <c>Created</c> column name for <see cref="Tracker.Core.Data.Entities.Task.Created" />.
        /// </summary>
        public const string Created = "Created";
        /// <summary>
        /// The <c>CreatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.Task.CreatedBy" />.
        /// </summary>
        public const string CreatedBy = "CreatedBy";
        /// <summary>
        /// The <c>Updated</c> column name for <see cref="Tracker.Core.Data.Entities.Task.Updated" />.
        /// </summary>
        public const string Updated = "Updated";
        /// <summary>
        /// The <c>UpdatedBy</c> column name for <see cref="Tracker.Core.Data.Entities.Task.UpdatedBy" />.
        /// </summary>
        public const string UpdatedBy = "UpdatedBy";
        /// <summary>
        /// The <c>RowVersion</c> column name for <see cref="Tracker.Core.Data.Entities.Task.RowVersion" />.
        /// </summary>
        public const string RowVersion = "RowVersion";
        /// <summary>
        /// The <c>PeriodStart</c> column name for <see cref="Tracker.Core.Data.Entities.Task.PeriodStart1" />.
        /// </summary>
        public const string PeriodStart1 = "PeriodStart";
        /// <summary>
        /// The <c>PeriodEnd</c> column name for <see cref="Tracker.Core.Data.Entities.Task.PeriodEnd1" />.
        /// </summary>
        public const string PeriodEnd1 = "PeriodEnd";
    }
    #endregion
}
