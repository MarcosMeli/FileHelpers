using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        Name = nameof(UseGenericEngineCodeFixProvider)), Shared]
    public class UseGenericEngineCodeFixProvider
        : FileHelpersCodeFixProvider<UseGenericEngineAnalyzer>
    {
    
        protected override async Task<Document> ApplyFix(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic)
        {
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var node = root.FindNode(diagnosticSpan) as ObjectCreationExpressionSyntax;


            // Compute new uppercase name.
            var typeofSyntax = (TypeOfExpressionSyntax)node.ArgumentList.Arguments[0].Expression;
            var type = typeofSyntax.Type;
            var originalEngine = node.Type;

            var newRoot = root.ReplaceNode(node,
                SyntaxFactory.ObjectCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword),
                    SyntaxFactory.ParseTypeName(originalEngine + "<" + type + ">"),
                    SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(node.ArgumentList.Arguments.Skip(1))), null));

            return context.Document.WithSyntaxRoot(newRoot);

        }
    }
}