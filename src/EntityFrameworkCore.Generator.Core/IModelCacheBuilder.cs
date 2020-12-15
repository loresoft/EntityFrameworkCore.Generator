using EntityFrameworkCore.Generator.Options;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace EntityFrameworkCore.Generator
{
    public interface IModelCacheBuilder
    {
         bool Refresh(GeneratorOptions options);
         DatabaseModel LoadFromCache(GeneratorOptions options);
    }
}