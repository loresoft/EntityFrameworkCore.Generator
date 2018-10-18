using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class MappingParser
    {
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
                Debug.WriteLine("Parsed Mapping File: '{0}'; Properties: {1}; Relationships: {2}",
                  Path.GetFileName(mappingFile),
                  parsedEntity.Properties.Count,
                  parsedEntity.Relationships.Count);

            return parsedEntity;
        }
    }
}
