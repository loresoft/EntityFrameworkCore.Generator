using System.Data;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

[DebuggerDisplay("Property: {PropertyName}, Column: {ColumnName}, Type: {NativeType}")]
public class Property : ModelBase
{
    public Entity Entity { get; set; } = null!;

    public string PropertyName { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public string? NativeType { get; set; }

    public DbType DataType { get; set; }

    public Type SystemType { get; set; } = null!;

    public string SystemTypeName { get; set; } = null!;

    public bool? IsNullable { get; set; }

    public bool IsRequired => IsNullable == false;

    public bool IsOptional => IsNullable == true;

    public bool? IsPrimaryKey { get; set; }

    public bool? IsForeignKey { get; set; }

    public bool? IsReadOnly { get; set; }

    public bool? IsRowVersion { get; set; }

    public bool? IsConcurrencyToken { get; set; }

    public bool? IsUnique { get; set; }

    public bool? IsComputed { get; set; }

    public bool? IsIdentity { get; set; }

    public int? Size { get; set; }

    public object? DefaultValue { get; set; }

    public string? Default { get; set; }

    public bool? AllowInsert { get; set; }

    public bool? AllowUpdate { get; set; }
}
