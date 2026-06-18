using EntityFrameworkCore.Generator.Metadata.Parsing;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Parsing;

public class ModelParser
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

        _logger.LogDebug(
            "Parsing Model File: '{ModelFile}'",
            Path.GetFileName(modelFile));

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

        _logger.LogDebug(
            "Parsed Model Class: '{ModelClass}'; Properties: {Properties}",
            parsedModel.ModelClass,
            parsedModel.Properties.Count);

        return parsedModel;
    }
}
