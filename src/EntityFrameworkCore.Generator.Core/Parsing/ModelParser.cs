using EntityFrameworkCore.Generator.Metadata.Parsing;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Parsing;

public partial class ModelParser
{
    private readonly ILogger _logger;

    public ModelParser(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ModelParser>();
    }

    public ParsedModel? ParseFile(string modelFile)
    {
        if (string.IsNullOrEmpty(modelFile) || !File.Exists(modelFile))
            return null;

        LogParsingModelFile(_logger, Path.GetFileName(modelFile));

        var code = File.ReadAllText(modelFile);
        return ParseCode(code);
    }

    public ParsedModel? ParseCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var root = (CompilationUnitSyntax)syntaxTree.GetRoot();

        var visitor = new ModelVisitor();
        visitor.Visit(root);

        var parsedModel = visitor.ParsedModel;
        if (parsedModel == null || parsedModel.Properties.Count == 0)
            return null;

        LogParsedModelClass(_logger, parsedModel.ModelClass, parsedModel.Properties.Count);

        return parsedModel;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Debug, Message = "Parsing Model File: '{modelFile}'")]
    private static partial void LogParsingModelFile(ILogger logger, string modelFile);

    [LoggerMessage(EventId = 2, Level = LogLevel.Debug, Message = "Parsed Model Class: '{modelClass}'; Properties: {properties}")]
    private static partial void LogParsedModelClass(ILogger logger, string modelClass, int properties);
}
