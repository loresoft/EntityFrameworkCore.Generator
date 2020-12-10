using System;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator
{
    public interface ICodeGenerator
    {
        bool Generate(GeneratorOptions options);

        bool Generate(GeneratorOptions options, bool fromCache, bool updateFromSource);
    }
}