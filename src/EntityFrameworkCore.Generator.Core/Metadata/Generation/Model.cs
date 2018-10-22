using System;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    public class Model : ModelBase
    {
        public Model()
        {
            Properties = new PropertyCollection();
        }

        public Entity Entity { get; set; }

        public ModelType ModelType { get; set; }

        public string ModelNamespace { get; set; }

        public string ModelClass { get; set; }

        public string ModelBaseClass { get; set; }


        public string ValidatorNamespace { get; set; }

        public string ValidatorClass { get; set; }

        public string ValidatorBaseClass { get; set; }


        public PropertyCollection Properties { get; set; }
    }
}