using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    [DebuggerDisplay("Suffix: {NameSuffix}, IsKey: {IsKey}, IsUnique: {IsUnique}")]
    public class Method : ModelBase
    {
        public Method()
        {
            Properties = new List<Property>();
        }

        public string NameSuffix { get; set; }
        public string SourceName { get; set; }

        public bool IsKey { get; set; }
        public bool IsUnique { get; set; }
        public bool IsIndex { get; set; }

        public List<Property> Properties { get; set; }
    }
}
