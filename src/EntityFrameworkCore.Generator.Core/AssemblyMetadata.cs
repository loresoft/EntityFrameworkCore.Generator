using System.Reflection;

namespace EntityFrameworkCore.Generator;

public static class AssemblyMetadata
{
    private static readonly Lazy<string> _fileVersion = new(() =>
    {
        var assembly = typeof(AssemblyMetadata).Assembly;
        var attribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
        return attribute?.Version;
    });

    private static readonly Lazy<string> _assemblyVersion = new(() =>
    {
        var assembly = typeof(AssemblyMetadata).Assembly;
        var attribute = assembly.GetCustomAttribute<AssemblyVersionAttribute>();
        return attribute?.Version;
    });

    private static readonly Lazy<string> _informationVersion = new(() =>
    {
        var assembly = typeof(AssemblyMetadata).Assembly;
        var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        return attribute?.InformationalVersion;
    });

    private static readonly Lazy<string> _assemblyDescription = new(() =>
    {
        var assembly = typeof(AssemblyMetadata).Assembly;
        var attribute = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
        return attribute?.Description;
    });

    private static readonly Lazy<string> _assemblyName = new(() =>
    {
        var assembly = typeof(AssemblyMetadata).Assembly;
        return assembly.GetName().Name;
    });

    public static string FileVersion => _fileVersion.Value;

    public static string AssemblyVersion => _assemblyVersion.Value;

    public static string AssemblyDescription => _assemblyDescription.Value;

    public static string AssemblyName => _assemblyName.Value;

    public static string InformationalVersion => _informationVersion.Value;
}
