using System.IO;
using System.Text;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Parsing;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.Generator.Templates
{
    public delegate void CodeTemplateWriter(string fullPath, string content);

    public abstract class CodeTemplateBase
    {
        protected CodeTemplateBase(GeneratorOptions options)
        {
            Options = options;
            CodeBuilder = new IndentedStringBuilder();
            RegionParser = new RegionParser();

            CodeWriter = options.CodeWriter;
        }

        public CodeTemplateWriter CodeWriter { get; set; }

        public GeneratorOptions Options { get; }

        protected RegionParser RegionParser { get; }

        protected IndentedStringBuilder CodeBuilder { get; }

        public virtual void WriteCode(string path)
        {
            var fullPath = Path.GetFullPath(path);
            var output = WriteCode();

            var cw = CodeWriter ?? DefaultCodeWriter;
            cw(fullPath, output);
        }

        public abstract string WriteCode();

        protected void DefaultCodeWriter(string fullPath, string content)
        {
            var directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(fullPath))
                MergeOutput(fullPath, content);
            else
                File.WriteAllText(fullPath, content);
        }

        protected virtual void MergeOutput(string fullPath, string outputContent)
        {
            var outputRegions = RegionParser.ParseRegions(outputContent);

            var originalContent = File.ReadAllText(fullPath);
            var originalRegions = RegionParser.ParseRegions(originalContent);
            var originalBuilder = new StringBuilder(originalContent);

            int offset = 0;
            foreach (var pair in outputRegions)
            {
                var outputRegion = pair.Value;
                if (!originalRegions.TryGetValue(pair.Key, out var originalRegion))
                {
                    // log error
                    continue;
                }

                int startIndex = originalRegion.StartIndex + offset;
                int beforeReplace = originalBuilder.Length;
                int length = (originalRegion.EndIndex + offset) - startIndex;

                originalBuilder.Remove(startIndex, length);
                originalBuilder.Insert(startIndex, outputRegion.Content);

                int afterReplace = originalBuilder.Length;

                offset = offset + (afterReplace - beforeReplace);
            }

            var finalContent = originalBuilder.ToString();
            if (originalContent == finalContent)
                return;

            File.WriteAllText(fullPath, finalContent);
        }
    }
}