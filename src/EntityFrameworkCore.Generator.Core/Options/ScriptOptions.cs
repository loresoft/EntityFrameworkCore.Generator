using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Options;

public class ScriptOptions : OptionsBase
{
    public ScriptOptions(VariableDictionary variables, string prefix)
        : base(variables, AppendPrefix(prefix, "Script"))
    {
        Context = new List<TemplateOptions>();
        Entity = new List<TemplateOptions>();
        Model = new List<TemplateOptions>();
    }

    /// <summary>
    /// Gets or sets the list context script templates.
    /// </summary>
    /// <value>The list context script templates.</value>
    public List<TemplateOptions> Context { get; set; }

    /// <summary>
    /// Gets or sets the list entity script templates.
    /// </summary>
    /// <value>The list entity script templates.</value>
    public List<TemplateOptions> Entity { get; set; }

    /// <summary>
    /// Gets or sets the list model script templates.
    /// </summary>
    /// <value>The list model script templates.</value>
    public List<TemplateOptions> Model { get; set; }

}