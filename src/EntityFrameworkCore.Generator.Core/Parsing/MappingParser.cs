using EntityFrameworkCore.Generator.Metadata.Parsing;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System.IO;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class MappingParser
    {
        private readonly ILogger _logger;

        public MappingParser(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MappingParser>();
        }

        public ParsedEntity Parse(string mappingFile)
        {
            if (string.IsNullOrEmpty(mappingFile) || !File.Exists(mappingFile))
                return null;

            var code = File.ReadAllText(mappingFile);
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var root = (CompilationUnitSyntax)syntaxTree.GetRoot();

            var visitor = new MappingVisitor();
            visitor.Visit(root);

            var parsedEntity = visitor.ParsedEntity;

            if (parsedEntity != null)
                _logger.LogDebug(
                    "Parsed Mapping File: '{0}'; Properties: {1}; Relationships: {2}",
                    Path.GetFileName(mappingFile),
                    parsedEntity.Properties.Count,
                    parsedEntity.Relationships.Count);

            return parsedEntity;
        }
    }
}
