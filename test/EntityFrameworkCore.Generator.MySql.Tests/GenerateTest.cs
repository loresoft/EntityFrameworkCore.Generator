using System.Diagnostics;

using EntityFrameworkCore.Generator.MySql.Tests.Fixtures;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Spectre.Console.Cli;

namespace EntityFrameworkCore.Generator.MySql.Tests;

public class GenerateTest : DatabaseTestBase
{
    private readonly DatabaseFixture _databaseFixture;

    public GenerateTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public async Task GenerateCommandGeneratesExpectedFiles()
    {
        var services = Services;

        Assert.False(string.IsNullOrWhiteSpace(_databaseFixture.ConnectionString));

        var solutionDirectory = GetSolutionDirectory();
        var testResultsDirectory = Path.Combine(solutionDirectory, "TestResults", "MySql");
        var outputDirectory = Path.Combine(testResultsDirectory, "GenerateCommand");

        PrepareDirectory(outputDirectory);

        var optionsFile = PrepareOptionsFile(solutionDirectory, outputDirectory);

        await using var serviceProvider = CreateServiceProvider();
        var app = CreateCommandApp(serviceProvider);

        // generate the code
        var exitCode = await app.RunAsync([
            "generate",
            "--directory", solutionDirectory,
            "--file", Path.GetRelativePath(solutionDirectory, optionsFile),
            "--connection-string", _databaseFixture.ConnectionString
        ], TestContext.CancellationToken);

        Assert.Equal(0, exitCode);

        // compile the generated code to ensure it is valid
        var projectFile = CreateProjectFile(outputDirectory);
        await CompileProjectAsync(projectFile, TestContext.CancellationToken);
    }

    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        services
            .AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace))
            .AddTransient<IConfigurationSerializer, ConfigurationSerializer>()
            .AddTransient<ICodeGenerator, CodeGenerator>()
            .AddTransient<GenerateCommand>();

        return services.BuildServiceProvider();
    }

    private static CommandApp CreateCommandApp(IServiceProvider serviceProvider)
    {
        var app = new CommandApp(new TypeRegistrar(serviceProvider));
        app.Configure(config => config.AddCommand<GenerateCommand>("generate"));

        return app;
    }

    private static void PrepareDirectory(string directory)
    {
        if (Directory.Exists(directory))
            Directory.Delete(directory, true);

        Directory.CreateDirectory(directory);
    }

    private static string PrepareOptionsFile(string solutionDirectory, string outputDirectory)
    {
        var sourceFile = Path.Combine(
            solutionDirectory,
            "test",
            "EntityFrameworkCore.Generator.MySql.Tests",
            ConfigurationSerializer.OptionsFileName);

        var targetFile = Path.Combine(outputDirectory, ConfigurationSerializer.OptionsFileName);

        var yaml = File.ReadAllText(sourceFile)
            .Replace("  directory: .\\", $"  directory: '{outputDirectory.Replace("'", "''")}'", StringComparison.Ordinal);

        File.WriteAllText(targetFile, yaml);

        return targetFile;
    }

    private static string CreateProjectFile(string outputDirectory)
    {
        var projectFile = Path.Combine(outputDirectory, "GeneratedProject.Core.csproj");
        var project = """
            <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
                <ImplicitUsings>enable</ImplicitUsings>
                <Nullable>enable</Nullable>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="AutoMapper" />
                <PackageReference Include="FluentValidation" />
                <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" />
                <PackageReference Include="Pomelo.EntityFrameworkCore.MySql.NetTopologySuite" />
              </ItemGroup>
            </Project>
            """;

        File.WriteAllText(projectFile, project);
        return projectFile;
    }

    private static async Task CompileProjectAsync(string projectFile, CancellationToken cancellationToken)
    {
        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            ArgumentList = { "build", projectFile },
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        process.Start();

        var standardOutput = process.StandardOutput.ReadToEndAsync(cancellationToken);
        var standardError = process.StandardError.ReadToEndAsync(cancellationToken);

        await process.WaitForExitAsync(cancellationToken);

        Assert.True(process.ExitCode == 0, string.Concat(await standardOutput, await standardError));
    }

    private static string GetSolutionDirectory([System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
    {
        var directory = new DirectoryInfo(Path.GetDirectoryName(sourceFilePath)!);

        while (directory != null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "EntityFrameworkCore.Generator.slnx")))
                return directory.FullName;

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Could not find the solution directory.");
    }
}
