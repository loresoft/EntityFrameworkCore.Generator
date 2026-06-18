using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Represents the <c>Role</c> entity mapped to the <c>dbo.Role</c> table.
/// </summary>
[Table("Role", Schema = "dbo")]
public partial class Role
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class and its collection navigation properties.
    /// </summary>
    public Role()
    {
        #region Generated Constructor
        UserRoles = new HashSet<UserRole>();
        #endregion
    }

    #region Generated Properties
    /// <summary>
    /// Gets or sets the <c>Id</c> value mapped to the <c>Id</c> column.
    /// </summary>
    /// <value>
    /// The <c>Id</c> entity value.
    /// </value>
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the <c>Name</c> value mapped to the <c>Name</c> column.
    /// </summary>
    /// <value>
    /// The <c>Name</c> entity value.
    /// </value>
    [Column("Name", TypeName = "nvarchar(256)")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>Description</c> value mapped to the <c>Description</c> column.
    /// </summary>
    /// <value>
    /// The <c>Description</c> entity value.
    /// </value>
    [Column("Description", TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the <c>Created</c> value mapped to the <c>Created</c> column.
    /// </summary>
    /// <value>
    /// The <c>Created</c> entity value.
    /// </value>
    [Column("Created", TypeName = "datetimeoffset")]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Gets or sets the <c>CreatedBy</c> value mapped to the <c>CreatedBy</c> column.
    /// </summary>
    /// <value>
    /// The <c>CreatedBy</c> entity value.
    /// </value>
    [Column("CreatedBy", TypeName = "nvarchar(100)")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the <c>Updated</c> value mapped to the <c>Updated</c> column.
    /// </summary>
    /// <value>
    /// The <c>Updated</c> entity value.
    /// </value>
    [Column("Updated", TypeName = "datetimeoffset")]
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Gets or sets the <c>UpdatedBy</c> value mapped to the <c>UpdatedBy</c> column.
    /// </summary>
    /// <value>
    /// The <c>UpdatedBy</c> entity value.
    /// </value>
    [Column("UpdatedBy", TypeName = "nvarchar(100)")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the <c>RowVersion</c> value mapped to the <c>RowVersion</c> column.
    /// </summary>
    /// <value>
    /// The <c>RowVersion</c> entity value.
    /// </value>
    [ConcurrencyCheck]
    [Column("RowVersion", TypeName = "rowversion")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public byte[] RowVersion { get; set; } = null!;

    #endregion

    #region Generated Relationships
    /// <summary>
    /// Gets or sets the related <see cref="UserRole" /> entity collection.
    /// </summary>
    /// <value>
    /// The related <see cref="UserRole" /> entity collection.
    /// </value>
    public virtual ICollection<UserRole> UserRoles { get; set; }

    #endregion

}
