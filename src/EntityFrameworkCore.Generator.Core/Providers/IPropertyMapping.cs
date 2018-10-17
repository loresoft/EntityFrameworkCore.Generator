using System;
using System.Data;

namespace EntityFrameworkCore.Generator.Providers
{
    public interface IPropertyMapping
    {
        string StoreType { get; set; }

        string NativeType { get; set; }

        DbType DataType { get; set; }

        Type SystemType { get; set; }

        bool? IsMaxLength { get; set; }

        int? Size { get; set; }

        int? Precision { get; set; }

        int? Scale { get; set; }
    }
}