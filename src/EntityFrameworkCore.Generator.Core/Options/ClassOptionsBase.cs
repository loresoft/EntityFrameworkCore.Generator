using System;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Base class for Class generation
    /// </summary>
    public abstract class ClassOptionsBase : OptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassOptionsBase"/> class.
        /// </summary>
        protected ClassOptionsBase(VariableDictionary variables, string prefix)
            : base(variables, prefix)
        {
            Namespace = "{Project.Namespace}";
            Directory = @"{Project.Directory}\";
        }

        /// <summary>
        /// Gets or sets the class namespace.
        /// </summary>
        /// <value>
        /// The class namespace.
        /// </value>
        public string Namespace
        {
            get => GetProperty();
            set => SetProperty(value);
        }

        /// <summary>
        /// Gets or sets the output directory.  Default is the current working directory.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
        public string Directory
        {
            get => GetProperty();
            set => SetProperty(value);
        }
    }
}