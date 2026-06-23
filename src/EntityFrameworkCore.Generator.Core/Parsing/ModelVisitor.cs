using EntityFrameworkCore.Generator.Metadata.Parsing;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityFrameworkCore.Generator.Parsing;

public class ModelVisitor : CSharpSyntaxWalker
{
    private string? _currentClass;

    public ParsedModel? ParsedModel { get; set; }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        var previousClass = _currentClass;
        _currentClass = node.Identifier.ValueText;

        ParsedModel ??= new ParsedModel { ModelClass = _currentClass };

        base.VisitClassDeclaration(node);

        _currentClass = previousClass;
    }

    public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        ParseProperty(node);
        base.VisitPropertyDeclaration(node);
    }

    private void ParseProperty(PropertyDeclarationSyntax node)
    {
        if (string.IsNullOrEmpty(_currentClass) || ParsedModel == null || node.AttributeLists.Count == 0)
            return;

        var propertyName = node.Identifier.ValueText;
        if (string.IsNullOrEmpty(propertyName))
            return;

        var attributes = node.AttributeLists
            .Select(a => a.ToString())
            .Where(a => !string.IsNullOrWhiteSpace(a))
            .ToList();

        if (attributes.Count == 0)
            return;

        var parsedProperty = new ParsedModelProperty
        {
            PropertyName = propertyName
        };

        parsedProperty.Attributes.AddRange(attributes);
        ParsedModel.Properties.Add(parsedProperty);
    }
}
