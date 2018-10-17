using System;
using System.Data;

namespace EntityFrameworkCore.Generator.Providers
{
    public class PropertyMapping : IPropertyMapping
    {
        public string StoreType { get; set; }

        public string NativeType { get; set; }

        public DbType DataType { get; set; }

        public Type SystemType { get; set; }

        public bool? IsMaxLength { get; set; }

        public int? Size { get; set; }

        public int? Precision { get; set; }

        public int? Scale { get; set; }
    }
}