using System.Runtime.CompilerServices;
using EntityFrameworkCore.Generator.Extensions;

namespace EntityFrameworkCore.Generator.Options
{
    public class OptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsBase"/> class.
        /// </summary>
        public OptionsBase(VariableDictionary variables, string prefix)
        {
            Variables = variables;
            Prefix = prefix;
        }

        protected VariableDictionary Variables { get; }

        protected string Prefix { get; }


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
}