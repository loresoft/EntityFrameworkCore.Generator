using System;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Query extensions options
    /// </summary>
    /// <seealso cref="EntityFrameworkCore.Generator.Options.ClassOptionsBase" />
    public class QueryExtensionOptions : ClassOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryExtensionOptions"/> class.
        /// </summary>
        public QueryExtensionOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Query"))
        {
            Namespace = "{Project.Namespace}.Data.Queries";
            Directory = @"{Project.Directory}\Data\Queries";

            Generate = true;
            IndexPrefix = "By";
            UniquePrefix = "GetBy";
        }

        /// <summary>
        /// Gets or sets a value indicating whether this option is generated.
        /// </summary>
        /// <value>
        ///   <c>true</c> to generate; otherwise, <c>false</c>.
        /// </value>
        public bool Generate { get; set; }

        /// <summary>
        /// Gets or sets the prefix of query method names.
        /// </summary>
        /// <value>
        /// The prefix of query method names
        /// </value>
        public string IndexPrefix
        {
            get => GetProperty();
            set => SetProperty(value);
        }

        /// <summary>
        /// Gets or sets the prefix of unique query method names.
        /// </summary>
        /// <value>
        /// The prefix of unique query method names.
        /// </value>
        public string UniquePrefix
        {
            get => GetProperty();
            set => SetProperty(value);
        }
    }
}