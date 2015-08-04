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
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseFieldHiddenCodeFixProvider)), Shared]
    public class UseFieldHiddenCodeFixProvider : CodeFixProvider
    {
     
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(UseFieldHiddenAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            if (context.CancellationToken.IsCancellationRequested)
                return;

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.
            var attribute = root.FindNode(diagnosticSpan) as AttributeSyntax;

            context.RegisterCodeFix(
                CodeAction.Create(
                    UseFieldHiddenAnalyzer.FixTitle, _ =>

                    ChangeToFieldHidden(context, root, attribute), null), diagnostic);
            
        }

        private async Task<Document> ChangeToFieldHidden(CodeFixContext context, SyntaxNode root, AttributeSyntax attribute)
        {
            // Compute new uppercase name.

            var newRoot = root.ReplaceNode(attribute,
                SyntaxFactory.Attribute(SyntaxFactory.ParseName("FieldHidden")));

            return context.Document.WithSyntaxRoot(newRoot);

        }
    }
}