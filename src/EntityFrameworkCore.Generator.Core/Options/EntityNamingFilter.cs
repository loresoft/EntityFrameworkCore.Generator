namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Entity naming filter options
    /// </summary>
    public class EntityNamingFilter
    {
        /// <summary>
        /// The regular expression pattern to match against the table name.
        /// The pattern should specify a named group with the name ClassName that will be used to extract the class name
        /// otherwise the first group will be used.
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// A prefix to apply after pattern matching
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// A suffix to apply after pattern matching
        /// </summary>
        public string Suffix { get; set; }
    }
}
