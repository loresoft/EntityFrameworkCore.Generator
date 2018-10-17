using System;
using EntityFrameworkCore.Generator.Metadata.Generation;

namespace EntityFrameworkCore.Generator.Providers
{
    public interface IProviderTypeMapping
    {
        IPropertyMapping ParseType(string storeTypeName);
    }
}