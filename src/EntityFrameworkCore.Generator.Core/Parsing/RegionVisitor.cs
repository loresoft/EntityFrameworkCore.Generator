using EntityFrameworkCore.Generator.Metadata.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityFrameworkCore.Generator.Parsing;

public class RegionVisitor : CSharpSyntaxWalker
{
    private readonly Stack<string> _classStack = new();
    private readonly Stack<CodeRegion> _regionStack = new();

    public RegionVisitor() : base(SyntaxWalkerDepth.StructuredTrivia)
    {
        Regions = [];
    }

    public List<CodeRegion> Regions { get; }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        var className = node.Identifier.Text;
        _classStack.Push(className);

        base.VisitClassDeclaration(node);

        _classStack.Pop();
    }

    public override void VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
    {
        if (node == null)
            return;

        _classStack.TryPeek(out var className);

        var region = new CodeRegion
        {
            StartIndex = node.FullSpan.Start,
            RegionName = ParseRegionName(node),
            ClassName = className ?? string.Empty
        };
        _regionStack.Push(region);

        base.VisitRegionDirectiveTrivia(node);
    }

    public override void VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
    {
        if (node == null || _regionStack.Count == 0)
            return;

        var region = _regionStack.Pop();
        region.EndIndex = node.FullSpan.End;

        Regions.Add(region);

        base.VisitEndRegionDirectiveTrivia(node);
    }

    private static string ParseRegionName(RegionDirectiveTriviaSyntax node)
    {
        var preprocessingMessage = node
            .DescendantTrivia()
            .FirstOrDefault(t => t.IsKind(SyntaxKind.PreprocessingMessageTrivia));

        return preprocessingMessage.ToString();
    }
}
