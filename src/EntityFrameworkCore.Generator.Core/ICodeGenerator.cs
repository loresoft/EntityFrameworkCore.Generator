using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator;

public interface ICodeGenerator
{
    Task<bool> GenerateAsync(GeneratorOptions options);
}
