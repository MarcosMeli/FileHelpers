using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace FileHelpersAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, 
        Name = nameof(ValidateRecordTypeCodeFixProvider)), Shared]
    public class ValidateRecordTypeCodeFixProvider
        : CodeFixProvider
    {
    

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(ValidateRecordTypeAnalyzer.DiagnosticId);

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
                    "Add [DelimitedRecord]" , _ =>
                    AddDelimitedRecord(context, root, diagnostic), null), diagnostic);

    //        context.RegisterCodeFix(
    //CodeAction.Create(
    //    "Add [FixedLengthRecord]", _ =>
    //   AddFixedLengthRecord(context, root, diagnostic), null), diagnostic);

        }

        
        protected async Task<Document> AddDelimitedRecord(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic)
        {
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var node = root.FindNode(diagnosticSpan).Parent as TypeDeclarationSyntax;

            // Compute new uppercase name.
            //var typeofSyntax = (TypeOfExpressionSyntax)node.ArgumentList.Arguments[0].Expression;
            //var type = typeofSyntax.Type;
            //var originalEngine = node.Type;
            var attbAnte = node.AttributeLists;

            var attributes = new SyntaxList<AttributeListSyntax>();

            foreach (var attb in node.AttributeLists.ToArray())
            {
                attributes.Add(attb);
            }

            //attributes.AddRange(node.AttributeLists);
            attributes.Add(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(
                new[]
                {
                    SyntaxFactory.Attribute(SyntaxFactory.ParseName("DelimitedRecord"),
                        SyntaxFactory.AttributeArgumentList())
                }
                )));
                
            
            //var newRoot = root.ReplaceNode(attbAnte, attributes);

                //SyntaxFactory.ObjectCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword),
                //    SyntaxFactory.ParseTypeName(originalEngine + "<" + type + ">"),
                //    SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(node.ArgumentList.Arguments.Skip(1))), null));

            return context.Document.WithSyntaxRoot(newRoot);

        }

    }
}