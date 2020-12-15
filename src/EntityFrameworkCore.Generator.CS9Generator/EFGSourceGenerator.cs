using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkCore.Generator.CS9Generator
{
    [Generator]
    public class EFGSourceGenerator : ISourceGenerator
    {
        private IServiceProvider _services;

        public void Initialize(GeneratorInitializationContext context)
        {
            _services = new ServiceCollection()
                .AddLogging()
                .AddTransient<IGeneratorOptionsSerializer, GeneratorOptionsSerializer>()
                .AddTransient<ICodeGenerator, CodeGenerator>()
                .AddTransient<IModelCacheBuilder, ModelCacheBuilder>()
                .BuildServiceProvider();
        }
        
        public void Execute(GeneratorExecutionContext context)
        {
            IGeneratorOptionsSerializer gos = _services.GetRequiredService<IGeneratorOptionsSerializer>();
            ICodeGenerator cg = _services.GetRequiredService<ICodeGenerator>();

            foreach (var addTxt in context.AdditionalFiles)
            {
                if (addTxt.Path.EndsWith(".efg.yml") || addTxt.Path.EndsWith(".efg.yaml"))
                {
                    var fullPath = addTxt.Path;
                    var directory = Path.GetDirectoryName(fullPath);
                    var fileName = Path.GetFileName(fullPath);
                    var fileNameWOext = Path.GetFileNameWithoutExtension(fullPath);
                    if (fileNameWOext.EndsWith(".efg", StringComparison.OrdinalIgnoreCase))
                        fileNameWOext = Path.GetFileNameWithoutExtension(fileNameWOext);

                    // throw new Exception("GENERATED: " + fileNameWOext);

                    context.AddSource($"EFG_{fileNameWOext}", $@"
namespace EFG.Generated
{{
    class {fileNameWOext}
    {{
        public const string Name = ""{fileNameWOext}"";
    }}
}}");

                    var options = gos.Load(directory, fileName);
                    var root = Path.GetFullPath(options.Project.Directory);

                    options.CodeWriter = (fullPath, content) =>
                    {
                        var relPath = Path.GetFullPath(fullPath).Replace(root, "");
                        var nameHint = relPath.Replace("\\", "__").Replace("/", "__");
                        //File.WriteAllText(fullPath, $"// This is a test [{fullPath}]\r\n// Rel: [{rel}]\r\n");
                        context.AddSource(nameHint, content);
                    };

                    cg.Generate(options, fromCache: true, updateFromSource: false);
                }
            }
        }
    }
}
