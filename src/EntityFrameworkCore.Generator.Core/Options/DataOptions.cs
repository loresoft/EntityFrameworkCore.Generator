using System;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// The data options group
    /// </summary>
    public class DataOptions : OptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataOptions" /> class.
        /// </summary>
        /// <param name="variables">The shared variables dictionary.</param>
        /// <param name="prefix">The variable key prefix.</param>
        public DataOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Data"))
        {
            Mapping = new MappingClassOptions(Variables, Prefix);
            Entity = new EntityClassOptions(Variables, Prefix);
            Context = new ContextClassOptions(Variables, Prefix);
            View = new ViewClassOptions(Variables, Prefix);
            Query = new QueryExtensionOptions(Variables, Prefix);
        }

        /// <summary>
        /// Gets or sets the data context generation options.
        /// </summary>
        /// <value>
        /// The data context options
        /// </value>
        public ContextClassOptions Context { get; set; }


        /// <summary>
        /// Gets or sets the entity class generation options.
        /// </summary>
        /// <value>
        /// The entity class generation options.
        /// </value>
        public EntityClassOptions Entity { get; set; }

        /// <summary>
        /// Gets or sets the mapping class generation options.
        /// </summary>
        /// <value>
        /// The mapping class generation options.
        /// </value>
        public MappingClassOptions Mapping { get; set; }

        /// <summary>
        /// Gets or sets the query extension options.
        /// </summary>
        /// <value>
        /// The query extension options.
        /// </value>
        public QueryExtensionOptions Query { get; set; }
    }
}