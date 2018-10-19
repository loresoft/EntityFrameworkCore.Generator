using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using EntityFrameworkCore.Generator.Metadata.Parsing;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class ContextParser
    {
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
                Debug.WriteLine("Parsed Context File: '{0}'; Entities: {1}",
                  Path.GetFileName(contextFile),
                  parsedContext.Properties.Count);

            return parsedContext;
        }
    }
}
