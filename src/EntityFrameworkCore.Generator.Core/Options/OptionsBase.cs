using System.Runtime.CompilerServices;

using EntityFrameworkCore.Generator.Extensions;

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

    public VariableDictionary Variables { get; }

    public string Prefix { get; }


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


    public static string AppendPrefix(string root, string prefix)
    {
        if (prefix.IsNullOrWhiteSpace())
            return root;

        return root.HasValue()
            ? $"{root}.{prefix}"
            : prefix;
    }
}
