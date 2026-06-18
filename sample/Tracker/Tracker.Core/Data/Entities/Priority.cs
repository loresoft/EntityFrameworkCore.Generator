using System;
using System.Collections.Generic;

using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Entity class representing data for table 'Priority'.
/// </summary>
[System.ComponentModel.DataAnnotations.Schema.Table("Priority", Schema = "dbo")]
public partial class Priority : IHaveIdentifier, ITrackCreated, ITrackUpdated
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Priority"/> class.
    /// </summary>
    public Priority()
    {
        #region Generated Constructor
        Tasks = new HashSet<Task>();
        #endregion
    }

    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value representing column <c>Id</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Id</c>.
    /// </value>
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Name</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Name</c>.
    /// </value>
    [Column("Name", TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column <c>Description</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Description</c>.
    /// </value>
    [Column("Description", TypeName = "nvarchar(255)")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>DisplayOrder</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>DisplayOrder</c>.
    /// </value>
    [Column("DisplayOrder", TypeName = "int")]
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>IsActive</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>IsActive</c>.
    /// </value>
    [Column("IsActive", TypeName = "bit")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Created</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Created</c>.
    /// </value>
    [Column("Created", TypeName = "datetimeoffset")]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>CreatedBy</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>CreatedBy</c>.
    /// </value>
    [Column("CreatedBy", TypeName = "nvarchar(100)")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Updated</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Updated</c>.
    /// </value>
    [Column("Updated", TypeName = "datetimeoffset")]
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>UpdatedBy</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>UpdatedBy</c>.
    /// </value>
    [Column("UpdatedBy", TypeName = "nvarchar(100)")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>RowVersion</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>RowVersion</c>.
    /// </value>
    [ConcurrencyCheck]
    [Column("RowVersion", TypeName = "rowversion")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public byte[] RowVersion { get; set; } = null!;

    #endregion

    #region Generated Relationships
    /// <summary>
    /// Gets or sets the navigation collection for entity <see cref="Task" />.
    /// </summary>
    /// <value>
    /// The navigation collection for entity <see cref="Task" />.
    /// </value>
    public virtual ICollection<Task> Tasks { get; set; }

    #endregion

}
