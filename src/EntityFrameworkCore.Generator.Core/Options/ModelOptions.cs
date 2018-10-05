using System;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Model options group
    /// </summary>
    public class ModelOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelOptions"/> class.
        /// </summary>
        public ModelOptions()
        {
            Read = new ReadModelOptions();
            Create = new CreateModelOptions();
            Update = new UpdateModelOptions();
            Mapper = new MapperClassOptions();
            Validator = new ValidatorClassOptions();
        }


        /// <summary>
        /// Gets or sets the read model options.
        /// </summary>
        /// <value>
        /// The read model options.
        /// </value>
        public ReadModelOptions Read { get; set; }

        /// <summary>
        /// Gets or sets the create model options.
        /// </summary>
        /// <value>
        /// The create model options.
        /// </value>
        public CreateModelOptions Create { get; set; }

        /// <summary>
        /// Gets or sets the update model options.
        /// </summary>
        /// <value>
        /// The update model options.
        /// </value>
        public UpdateModelOptions Update { get; set; }

        /// <summary>
        /// Gets or sets the view model mapper options.
        /// </summary>
        /// <value>
        /// The view model mapper options.
        /// </value>
        public MapperClassOptions Mapper { get; set; }

        /// <summary>
        /// Gets or sets the model validator options.
        /// </summary>
        /// <value>
        /// The model validator options.
        /// </value>
        public ValidatorClassOptions Validator { get; set; }

    }
}