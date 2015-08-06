using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace FileHelpersAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, 
        Name = nameof(UseFieldHiddenCodeFixProvider)), Shared]
    public class UseFieldHiddenCodeFixProvider 
        : FileHelpersCodeFixProvider<UseFieldHiddenAnalyzer>
    {
     
        protected override async Task<Document> ApplyFix(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic)
        {
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var node = root.FindNode(diagnosticSpan) as AttributeSyntax;

            var newRoot = root.ReplaceNode(node,
                SyntaxFactory.Attribute(SyntaxFactory.ParseName("FieldHidden")));

            return context.Document.WithSyntaxRoot(newRoot);

        }
    }
}