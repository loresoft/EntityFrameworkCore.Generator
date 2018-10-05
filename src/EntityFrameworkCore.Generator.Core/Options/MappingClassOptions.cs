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
        public MappingClassOptions()
        {
            Namespace = "{Project.Namespace}.Data.Mapping";
            Directory = @".\Data\Mapping";
        }
    }
}