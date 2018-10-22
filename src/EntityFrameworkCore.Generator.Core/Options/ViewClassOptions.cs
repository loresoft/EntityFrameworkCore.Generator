using System;
using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// View class options.
    /// </summary>
    /// <seealso cref="ClassOptionsBase" />
    public class ViewClassOptions : ClassOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewClassOptions"/> class.
        /// </summary>
        public ViewClassOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "View"))
        {
            Namespace = "{Project.Namespace}.Data.Entities";
            Directory = @"{Project.Directory}\Data\Entities";

            Generate = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this option is generated.
        /// </summary>
        /// <value>
        ///   <c>true</c> to generate; otherwise, <c>false</c>.
        /// </value>
        public bool Generate { get; set; }

        /// <summary>
        /// Gets or sets the base class to inherit from.
        /// </summary>
        /// <value>
        /// The base class.
        /// </value>
        public string BaseClass
        {
            get => GetProperty();
            set => SetProperty(value);
        }

        /// <summary>
        /// Gets or sets the view class naming strategy.
        /// </summary>
        /// <value>
        /// The view class naming strategy.
        /// </value>
        [DefaultValue(EntityNaming.Singular)]
        public EntityNaming EntityNaming { get; set; } = EntityNaming.Singular;
    }
}