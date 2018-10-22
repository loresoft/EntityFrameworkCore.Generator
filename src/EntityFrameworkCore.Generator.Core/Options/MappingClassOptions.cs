using System;

namespace EntityFrameworkCore.Generator.Options
{
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
    }
}