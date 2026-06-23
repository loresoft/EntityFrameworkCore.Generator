using System.ComponentModel;

using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// EntityFramework <c>DbContext</c> generation options
/// </summary>
/// <seealso cref="ClassOptionsBase" />
public class ContextClass : ClassBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContextClass"/> class.
    /// </summary>
    public ContextClass()
    {
        Namespace = "{Project.Namespace}.Data";
        Directory = @"{Project.Directory}\Data";

        Name = "{Database.Name}Context";
        BaseClass = "DbContext";
        PropertyNaming = ContextNaming.Plural;
    }

    /// <summary>
    /// Gets or sets the property naming strategy for entity data set property.
    /// </summary>
    /// <value>
    /// The property naming strategy for entity data set property.
    /// </value>
    [DefaultValue(ContextNaming.Plural)]
    public ContextNaming PropertyNaming { get; set; }

}
