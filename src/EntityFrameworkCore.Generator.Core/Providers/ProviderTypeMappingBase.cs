using System;
using System.Data;
using EntityFrameworkCore.Generator.Metadata.Generation;

namespace EntityFrameworkCore.Generator.Providers
{
    public abstract class ProviderTypeMappingBase : IProviderTypeMapping
    {
        public virtual IPropertyMapping ParseType(string storeTypeName)
        {
            var property = ParseNativeType(storeTypeName);

            property.SystemType = MapSystemType(property.NativeType);
            property.DataType = MapDataType(property.NativeType);

            return property;
        }

        protected abstract DbType MapDataType(string nativeType);

        protected abstract Type MapSystemType(string nativeType);

        protected virtual IPropertyMapping ParseNativeType(string storeTypeName)
        {
            var property = new PropertyMapping();

            property.StoreType = storeTypeName;
            property.NativeType = storeTypeName;

            if (storeTypeName == null)
                return property;

            var openParen = storeTypeName.IndexOf("(", StringComparison.Ordinal);
            if (openParen <= 0)
                return property;

            var closeParen = storeTypeName.IndexOf(")", openParen + 1, StringComparison.Ordinal);
            if (closeParen <= openParen)
                return property;

            var comma = storeTypeName.IndexOf(",", openParen + 1, StringComparison.Ordinal);
            if (comma > openParen && comma < closeParen)
            {
                if (int.TryParse(storeTypeName.Substring(openParen + 1, comma - openParen - 1), out var parsedPrecision))
                    property.Precision = parsedPrecision;

                if (int.TryParse(storeTypeName.Substring(comma + 1, closeParen - comma - 1), out var parsedScale))
                    property.Scale = parsedScale;
            }
            else
            {
                var sizeString = storeTypeName.Substring(openParen + 1, closeParen - openParen - 1).Trim();
                if (sizeString.Equals("max", StringComparison.OrdinalIgnoreCase))
                {
                    property.IsMaxLength = true;
                }
                else if (int.TryParse(sizeString, out var parsedSize))
                {
                    property.Size = parsedSize;
                    property.Precision = parsedSize;
                }
            }

            property.NativeType = storeTypeName.Substring(0, openParen);

            return property;
        }
    }
}
