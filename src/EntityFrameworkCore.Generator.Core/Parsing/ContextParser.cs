using EntityFrameworkCore.Generator.Metadata.Parsing;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Parsing;

public partial class ContextParser
{
    private readonly ILogger _logger;

    public ContextParser(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ContextParser>();
    }

    public ParsedContext? ParseFile(string contextFile)
    {
        if (string.IsNullOrEmpty(contextFile) || !File.Exists(contextFile))
            return null;

        LogParsingContextFile(_logger, Path.GetFileName(contextFile));

        var code = File.ReadAllText(contextFile);
        return ParseCode(code);
    }

    public ParsedContext? ParseCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var root = (CompilationUnitSyntax)syntaxTree.GetRoot();

        var visitor = new ContextVisitor();
        visitor.Visit(root);

        var parsedContext = visitor.ParsedContext;
        if (parsedContext == null)
            return null;

        LogParsedContextClass(_logger, parsedContext.ContextClass, parsedContext.Properties.Count);

        return parsedContext;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Debug, Message = "Parsing Context File: '{contextFile}'")]
    private static partial void LogParsingContextFile(ILogger logger, string contextFile);

    [LoggerMessage(EventId = 2, Level = LogLevel.Debug, Message = "Parsed Context Class: {contextClass}; Entities: {entities}")]
    private static partial void LogParsedContextClass(ILogger logger, string contextClass, int entities);
}
