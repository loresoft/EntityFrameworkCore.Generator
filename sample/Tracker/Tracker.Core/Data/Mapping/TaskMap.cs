using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="Tracker.Core.Data.Entities.Task" />
    /// </summary>
    public partial class TaskMap
        : IEntityTypeConfiguration<Tracker.Core.Data.Entities.Task>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Tracker.Core.Data.Entities.Task" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.Core.Data.Entities.Task> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Task", "dbo");

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
                .HasColumnName("RowVersion")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();

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
        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="Tracker.Core.Data.Entities.Task" /></summary>
            public const string Schema = "dbo";
            /// <summary>Table Name constant for entity <see cref="Tracker.Core.Data.Entities.Task" /></summary>
            public const string Name = "Task";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.Id" /></summary>
            public const string Id = "Id";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.StatusId" /></summary>
            public const string StatusId = "StatusId";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.PriorityId" /></summary>
            public const string PriorityId = "PriorityId";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.Title" /></summary>
            public const string Title = "Title";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.Description" /></summary>
            public const string Description = "Description";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.StartDate" /></summary>
            public const string StartDate = "StartDate";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.DueDate" /></summary>
            public const string DueDate = "DueDate";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.CompleteDate" /></summary>
            public const string CompleteDate = "CompleteDate";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.AssignedId" /></summary>
            public const string AssignedId = "AssignedId";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.TenantId" /></summary>
            public const string TenantId = "TenantId";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.Created" /></summary>
            public const string Created = "Created";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.CreatedBy" /></summary>
            public const string CreatedBy = "CreatedBy";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.Updated" /></summary>
            public const string Updated = "Updated";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.UpdatedBy" /></summary>
            public const string UpdatedBy = "UpdatedBy";
            /// <summary>Column Name constant for property <see cref="Tracker.Core.Data.Entities.Task.RowVersion" /></summary>
            public const string RowVersion = "RowVersion";
        }
        #endregion
    }
}
