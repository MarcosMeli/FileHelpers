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
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseGenericEngineCodeFixProvider)), Shared]
    public class UseGenericEngineCodeFixProvider : CodeFixProvider
    {
        private const string title = "FileHelpers: Use generic version of the engine";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(UseGenericEngineAnalyzer.DiagnosticId); }
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


            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.

            var creation = root.FindNode(diagnosticSpan) as ObjectCreationExpressionSyntax;


            context.RegisterCodeFix(
                CodeAction.Create(
                    title, _ =>
                    ChangeToGenericAsync(context, root, creation), null), diagnostic);
            
        }

        private async Task<Document> ChangeToGenericAsync(CodeFixContext context, SyntaxNode root, ObjectCreationExpressionSyntax creation)
        {
            // Compute new uppercase name.
            var typeofSyntax = (TypeOfExpressionSyntax) creation.ArgumentList.Arguments[0].Expression;
            var type = typeofSyntax.Type;
            var originalEngine = creation.Type;
            

            var newRoot = root.ReplaceNode(creation,
                SyntaxFactory.ObjectCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword),
                    SyntaxFactory.ParseTypeName(originalEngine + "<" + type + ">"), SyntaxFactory.ArgumentList(), null));

            return context.Document.WithSyntaxRoot(newRoot);

        }
    }
}