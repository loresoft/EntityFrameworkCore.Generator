using Spectre.Console.Cli;

namespace EntityFrameworkCore.Generator;

public sealed class TypeResolver(IServiceProvider provider) : ITypeResolver
{
    public object? Resolve(Type? type)
    {
        return type != null
            ? provider.GetService(type)
            : null;
    }

    public void Dispose()
    {
        if (provider is IDisposable disposable)
            disposable.Dispose();
    }
}
