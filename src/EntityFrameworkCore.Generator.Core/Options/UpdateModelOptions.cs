using System;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Update model options
    /// </summary>
    /// <seealso cref="ModelOptionsBase" />
    public class UpdateModelOptions : ModelOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateModelOptions"/> class.
        /// </summary>
        public UpdateModelOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Update"))
        {
            Namespace = "{Project.Namespace}.Domain.Models";
            Directory = @"{Project.Directory}\Domain\Models";

            BaseClass = "EntityUpdateModel";
            Name = "{Entity.Name}UpdateModel";
        }
    }
}