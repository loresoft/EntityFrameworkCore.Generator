namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Project options
    /// </summary>
    public class ProjectOptions : OptionsBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectOptions"/> class.
        /// </summary>
        public ProjectOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Project"))
        {
            Namespace = "{Database.Name}";
            Directory = @".\";
        }

        /// <summary>
        /// Gets or sets the project root namespace.
        /// </summary>
        /// <value>
        /// The project root namespace.
        /// </value>
        public string Namespace
        {
            get => GetProperty();
            set => SetProperty(value);
        }

        /// <summary>
        /// Gets or sets the project directory.
        /// </summary>
        /// <value>
        /// The project directory.
        /// </value>
        public string Directory
        {
            get => GetProperty();
            set => SetProperty(value);
        }

    }
}