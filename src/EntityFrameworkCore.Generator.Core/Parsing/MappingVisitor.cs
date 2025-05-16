using EntityFrameworkCore.Generator.Metadata.Parsing;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityFrameworkCore.Generator.Parsing;

public class MappingVisitor : CSharpSyntaxWalker
{
    private ParsedProperty? _currentProperty;
    private ParsedRelationship? _currentRelationship;


    public MappingVisitor()
    {
        MappingBaseType = "IEntityTypeConfiguration";
    }

    public string MappingBaseType { get; set; }

    public ParsedEntity? ParsedEntity { get; set; }


    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        ParseClassNames(node);
        base.VisitClassDeclaration(node);
    }

    public override void VisitInvocationExpression(InvocationExpressionSyntax node)
    {

        var methodName = ParseMethodName(node);

        switch (methodName)
        {
            case "HasConstraintName":
                ParseConstraintName(node);
                break;
            case "HasForeignKey":
                ParseForeignKey(node);
                break;
            case "WithMany":
            case "WithOne":
                ParseWithMany(node);
                break;
            case "HasMany":
            case "HasOne":
                ParseHasOne(node);
                break;
            case "HasColumnName":
                ParseColumnName(node);
                break;
            case "Property":
                ParseProperty(node);
                break;
            case "ToTable":
            case "ToView":
                ParseTable(node);
                break;
        }

        base.VisitInvocationExpression(node);
    }


    private static string ParseMethodName(InvocationExpressionSyntax node)
    {
        var memberAccess = node
            .ChildNodes()
            .OfType<MemberAccessExpressionSyntax>()
            .FirstOrDefault();

        if (memberAccess == null)
            return string.Empty;

        var methodName = memberAccess
            .ChildNodes()
            .OfType<IdentifierNameSyntax>()
            .Select(s => s.Identifier.ValueText)
            .LastOrDefault();

        return methodName ?? string.Empty;
    }

    private static string? ParseLambaExpression(InvocationExpressionSyntax node)
    {
        if (node == null)
            return null;

        var lambaExpression = node
            .ArgumentList
            .DescendantNodes()
            .OfType<LambdaExpressionSyntax>()
            .FirstOrDefault();

        if (lambaExpression == null)
            return null;

        var simpleExpression = lambaExpression
            .ChildNodes()
            .OfType<MemberAccessExpressionSyntax>()
            .FirstOrDefault();

        if (simpleExpression == null)
            return null;

        var propertyName = simpleExpression
            .ChildNodes()
            .OfType<IdentifierNameSyntax>()
            .Select(s => s.Identifier.ValueText)
            .LastOrDefault();

        return propertyName;
    }


    private void ParseHasOne(InvocationExpressionSyntax node)
    {
        if (node == null || ParsedEntity == null)
            return;

        _currentRelationship ??= new ParsedRelationship();

        var propertyName = ParseLambaExpression(node);
        if (!string.IsNullOrEmpty(propertyName))
            _currentRelationship.PropertyName = propertyName;

        // add and reset current relationship
        if (_currentRelationship.IsValid())
            ParsedEntity.Relationships.Add(_currentRelationship);

        _currentRelationship = null;
    }

    private void ParseWithMany(InvocationExpressionSyntax node)
    {
        if (node == null || ParsedEntity == null)
            return;

        var propertyName = ParseLambaExpression(node);
        if (string.IsNullOrEmpty(propertyName))
            return;

        _currentRelationship ??= new ParsedRelationship();
        _currentRelationship.PrimaryPropertyName = propertyName;
    }

    private void ParseForeignKey(InvocationExpressionSyntax node)
    {
        if (node == null || ParsedEntity == null)
            return;

        var propertyName = ParseLambaExpression(node);

        if (string.IsNullOrEmpty(propertyName))
            return;

        _currentRelationship ??= new ParsedRelationship();
        _currentRelationship.Properties.Add(propertyName);
    }

    private void ParseConstraintName(InvocationExpressionSyntax node)
    {
        if (node == null || ParsedEntity == null)
            return;

        var constraitName = node
            .ArgumentList
            .DescendantNodes()
            .OfType<LiteralExpressionSyntax>()
            .Select(t => t.Token.ValueText)
            .FirstOrDefault();

        _currentRelationship ??= new ParsedRelationship();
        _currentRelationship.RelationshipName = constraitName;
    }


    private void ParseProperty(InvocationExpressionSyntax node)
    {
        if (node == null || _currentProperty == null || ParsedEntity == null)
            return;

        var propertyName = ParseLambaExpression(node);
        if (!string.IsNullOrEmpty(propertyName))
            _currentProperty.PropertyName = propertyName;

        // add and reset current property
        if (_currentProperty.IsValid())
            ParsedEntity.Properties.Add(_currentProperty);

        _currentProperty = null;
    }

    private void ParseColumnName(InvocationExpressionSyntax node)
    {
        if (node == null || ParsedEntity == null)
            return;

        var columnName = node
            .ArgumentList
            .DescendantNodes()
            .OfType<LiteralExpressionSyntax>()
            .Select(t => t.Token.ValueText)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(columnName))
            return;

        _currentProperty = new ParsedProperty { ColumnName = columnName };
    }

    private void ParseTable(InvocationExpressionSyntax node)
    {
        if (node == null || ParsedEntity == null)
            return;

        var arguments = node
            .ArgumentList
            .DescendantNodes()
            .OfType<LiteralExpressionSyntax>()
            .Select(t => t.Token.ValueText)
            .ToList();

        if (arguments.Count == 0)
            return;

        if (arguments.Count >= 1)
            ParsedEntity.TableName = arguments[0];

        if (arguments.Count >= 2)
            ParsedEntity.TableSchema = arguments[1];
    }

    private void ParseClassNames(ClassDeclarationSyntax node)
    {
        if (node == null)
            return;

        var baseType = node.BaseList
            ?.DescendantNodes()
            .OfType<GenericNameSyntax>()
            .FirstOrDefault(t => t.Identifier.ValueText == MappingBaseType);

        if (baseType == null)
            return;

        var firstArgument = baseType
            .TypeArgumentList
            .Arguments
            .FirstOrDefault();

        if (firstArgument == null)
            return;

        // last identifier is class name
        var entityClass = firstArgument
            .DescendantNodesAndSelf()
            .OfType<IdentifierNameSyntax>()
            .Select(s => s.Identifier.ValueText)
            .LastOrDefault();

        var mappingClass = node.Identifier.Text;

        if (string.IsNullOrEmpty(entityClass) || string.IsNullOrEmpty(mappingClass))
            return;

        if (ParsedEntity == null)
            ParsedEntity = new ParsedEntity();

        ParsedEntity.MappingClass = mappingClass;
        ParsedEntity.EntityClass = entityClass;
    }
}
