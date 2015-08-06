using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FileHelpersAnalyzer
{
    public abstract class FileHelpersCodeFixProvider<T> : CodeFixProvider
        where T:IFileHelperAnalyzer, new()
    {
        protected T Analyzer { get; } = new T();

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(Analyzer.Id);

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

            context.RegisterCodeFix(
                CodeAction.Create(
                    Analyzer.FixTitle, _ =>
                    ApplyFix(context, root, diagnostic), null), diagnostic);

        }

        protected abstract Task<Document> ApplyFix(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic);
    }
}