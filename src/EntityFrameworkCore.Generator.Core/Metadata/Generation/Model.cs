using System;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    public class Model : ModelBase
    {
        public Entity Entity { get; set; }

        public string ModelNamespace { get; set; }

        public string ModelClass { get; set; }

        public string ModelBaseClass { get; set; }

        public string ValidatorNamespace { get; set; }

        public string ValidatorClass { get; set; }

        public string ValidatorBaseClass { get; set; }

        public PropertyCollection<Model> Properties { get; set; }
    }
}