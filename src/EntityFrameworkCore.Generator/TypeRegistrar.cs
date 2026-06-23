using Spectre.Console.Cli;

namespace EntityFrameworkCore.Generator;

public sealed class TypeRegistrar(IServiceProvider provider) : ITypeRegistrar
{
    public ITypeResolver Build() => new TypeResolver(provider);

    // Not needed when using Microsoft.Extensions.DependencyInjection
    public void Register(Type service, Type implementation) { }

    // Not needed when using Microsoft.Extensions.DependencyInjection
    public void RegisterInstance(Type service, object implementation) { }

    // Not needed when using Microsoft.Extensions.DependencyInjection
    public void RegisterLazy(Type service, Func<object> factory) { }
}
