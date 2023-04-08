using System;
using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// EntityFramework mapping class generation options
/// </summary>
/// <seealso cref="ClassOptionsBase" />
public class MappingClassOptions : ClassOptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingClassOptions"/> class.
    /// </summary>
    public MappingClassOptions(VariableDictionary variables, string prefix)
        : base(variables, AppendPrefix(prefix, "Mapping"))
    {
        Namespace = "{Project.Namespace}.Data.Mapping";
        Directory = @"{Project.Directory}\Data\Mapping";
    }

    /// <summary>
    /// Specifies the <see cref="System.DateTimeKind"/> to use for date/time columns.
    /// </summary>
    [DefaultValue(DateTimeKind.Local)]
    public DateTimeKind DateTimeKind { get; set; } = DateTimeKind.Local;

    /// <summary>
    /// The name of the class that will generate default values for date/time columns that have default value defined in the database.
    /// </summary>
    [DefaultValue(DateTimeKind.Local)]
    public string DateTimeDefaultValueGenerator { get; set; } = string.Empty;
}
