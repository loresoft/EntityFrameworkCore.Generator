using System.Collections.Generic;
using EntityFrameworkCore.Generator.Metadata.Parsing;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class RegionParser
    {
        public Dictionary<string, CodeRegion> ParseRegions(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var root = (CompilationUnitSyntax)syntaxTree.GetRoot();

            var visitor = new RegionVisitor();
            visitor.Visit(root);

            var regions = visitor.Regions;

            // extract content using start and end indexes
            foreach (var pair in regions)
            {
                var region = pair.Value;

                var start = region.StartIndex;
                var end = region.EndIndex;
                var length = end - start;

                region.Content = code.Substring(start, length);
            }

            return regions;
        }
    }
}