namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Read model options
    /// </summary>
    /// <seealso cref="ModelOptionsBase" />
    public class ReadModelOptions : ModelOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadModelOptions"/> class.
        /// </summary>
        public ReadModelOptions()
        {
            Namespace = "{Project.Namespace}.Domain.Models";
            Directory = @".\Domain\Models";

            BaseClass = "EntityReadModel";
            Name = "{Entity.Name}ReadModel";
        }
    }
}