using System.Runtime.CompilerServices;

using EntityFrameworkCore.Generator.Extensions;

using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Options;

public class OptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OptionsBase" /> class.
    /// </summary>
    /// <param name="variables">The shared variables dictionary.</param>
    /// <param name="prefix">The variable key prefix.</param>
    public OptionsBase(VariableDictionary variables, string prefix)
    {
        Variables = variables;
        Prefix = prefix;
    }

    protected VariableDictionary Variables { get; }

    protected string Prefix { get; }

    public virtual bool Validate(ILogger logger) => true;

    protected string GetProperty([CallerMemberName] string propertyName = null)
    {
        var name = AppendPrefix(Prefix, propertyName);
        return Variables.Get(name);
    }

    protected void SetProperty(string value, [CallerMemberName] string propertyName = null)
    {
        var name = AppendPrefix(Prefix, propertyName);
        Variables.Set(name, value);
    }


    protected static string AppendPrefix(string root, string prefix)
    {
        if (prefix.IsNullOrWhiteSpace())
            return root;

        return root.HasValue()
            ? $"{root}.{prefix}"
            : prefix;
    }
}
