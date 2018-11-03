using EntityFrameworkCore.Generator.Metadata.Parsing;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System.IO;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class ContextParser
    {
        private readonly ILogger _logger;

        public ContextParser(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ContextParser>();
        }

        public ParsedContext Parse(string contextFile)
        {
            if (string.IsNullOrEmpty(contextFile) || !File.Exists(contextFile))
                return null;

            var code = File.ReadAllText(contextFile);
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var root = (CompilationUnitSyntax)syntaxTree.GetRoot();

            var visitor = new ContextVisitor();
            visitor.Visit(root);

            var parsedContext = visitor.ParsedContext;

            if (parsedContext != null)
                _logger.LogDebug(
                    "Parsed Context File: '{0}'; Entities: {1}",
                    Path.GetFileName(contextFile),
                    parsedContext.Properties.Count);

            return parsedContext;
        }
    }
}
