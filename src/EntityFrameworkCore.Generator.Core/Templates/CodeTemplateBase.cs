using System.IO;
using System.Text;

using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Parsing;

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.Generator.Templates;

public abstract class CodeTemplateBase
{

    protected CodeTemplateBase(GeneratorOptions options)
    {
        Options = options;
        CodeBuilder = new IndentedStringBuilder();
        RegionReplace = new RegionReplace();
    }

    public GeneratorOptions Options { get; }

    protected RegionReplace RegionReplace { get; }

    protected IndentedStringBuilder CodeBuilder { get; }

    public virtual void WriteCode(string path)
    {
        var fullPath = Path.GetFullPath(path);
        var directory = Path.GetDirectoryName(fullPath);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var output = WriteCode();

        if (File.Exists(fullPath))
            RegionReplace.MergeFile(fullPath, output);
        else
            File.WriteAllText(fullPath, output);
    }

    public abstract string WriteCode();
}
