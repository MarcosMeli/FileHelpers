using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
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
    using System.Text;

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(FixIComparableRecordCodeFixProvider))]
    [Shared]
    public class FixIComparableRecordCodeFixProvider : FileHelpersCodeFixProvider<FixIComparableRecordAnalyzer>
    {
        protected override async Task<Document> ApplyFix(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic)
        {
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var node = root.FindNode(diagnosticSpan) as SimpleBaseTypeSyntax;

            var newRoot = root;

            var classDecl = (ClassDeclarationSyntax)node.Parent.Parent;

            var method =
                classDecl.Members.FirstOrDefault(
                    x =>
                    x is MethodDeclarationSyntax
                    && ((MethodDeclarationSyntax)x).Identifier.ToString() == "IsEqualRecord") as MethodDeclarationSyntax;

            if (method != null)
            {
                var leadingComments = new[]
                                          {
                                              @"/*",
                                              @" * TODO: Implement CompareTo",
                                              @" * See: https://msdn.microsoft.com/en-us/library/43hc6wht(v=vs.110).aspx",
                                              @" *"
                                          };
                var comment = new StringBuilder();
                var leadingTrivia = method.Body.Statements.Any() && method.Body.Statements[0].HasLeadingTrivia
                                        ? method.Body.Statements[0].GetLeadingTrivia().ToString()
                                        : "";

                foreach (var leadingComment in leadingComments)
                {
                    comment.AppendLine(leadingTrivia + leadingComment);
                }

                foreach (var statementSyntax in method.Body.Statements)
                {
                    comment.Append(
                        statementSyntax.GetLeadingTrivia() + statementSyntax.ToString()
                        + statementSyntax.GetTrailingTrivia());
                }

                comment.AppendLine(leadingTrivia + @" */");

                var newMethodBlock = SyntaxFactory.Block(
                    SyntaxFactory.SingletonList<StatementSyntax>(
                        SyntaxFactory.ThrowStatement(
                            SyntaxFactory.ObjectCreationExpression(
                                SyntaxFactory.IdentifierName(@"NotImplementedException"))
                                .WithNewKeyword(
                                    SyntaxFactory.Token(
                                        SyntaxFactory.TriviaList(),
                                        SyntaxKind.NewKeyword,
                                        SyntaxFactory.TriviaList(SyntaxFactory.Space)))
                                .WithArgumentList(
                                    SyntaxFactory.ArgumentList()
                                        .WithOpenParenToken(SyntaxFactory.Token(SyntaxKind.OpenParenToken))
                                        .WithCloseParenToken(SyntaxFactory.Token(SyntaxKind.CloseParenToken))))
                            .WithThrowKeyword(
                                SyntaxFactory.Token(
                                    SyntaxFactory.TriviaList(SyntaxFactory.Whitespace(@" ")),
                                    SyntaxKind.ThrowKeyword,
                                    SyntaxFactory.TriviaList(SyntaxFactory.Space)))
                            .WithSemicolonToken(
                                SyntaxFactory.Token(
                                    SyntaxFactory.TriviaList(),
                                    SyntaxKind.SemicolonToken,
                                    SyntaxFactory.TriviaList(
                                        new[] { SyntaxFactory.CarriageReturnLineFeed, SyntaxFactory.Comment(comment.ToString()) })))))
                    .WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken))
                    .WithCloseBraceToken(
                        SyntaxFactory.Token(
                            SyntaxFactory.TriviaList(method.Body.GetLeadingTrivia()),
                            SyntaxKind.CloseBraceToken,
                            SyntaxFactory.TriviaList()))
                    .WithLeadingTrivia(method.Body.GetLeadingTrivia())
                    .WithTrailingTrivia(method.Body.GetTrailingTrivia());

                newRoot = newRoot.ReplaceNode(
                    method,
                    SyntaxFactory.MethodDeclaration(
                        attributeLists: method.AttributeLists,
                        modifiers: method.Modifiers,
                        body: newMethodBlock,
                        returnType: SyntaxFactory.ParseTypeName("int"),
                        explicitInterfaceSpecifier: method.ExplicitInterfaceSpecifier,
                        identifier: SyntaxFactory.Identifier("CompareTo"),
                        typeParameterList: method.TypeParameterList,
                        parameterList: method.ParameterList,
                        constraintClauses: method.ConstraintClauses,
                        expressionBody: method.ExpressionBody));
            }

            var oldNode = newRoot.FindNode(diagnosticSpan) as SimpleBaseTypeSyntax;

            newRoot = newRoot.ReplaceNode(
                oldNode,
                // Add trailing trivia from oldNode to maintain layout
                SyntaxFactory.SimpleBaseType(
                    SyntaxFactory.ParseTypeName(node.Type.ToString().Replace("IComparableRecord<", "IComparable<")))
                    .WithTrailingTrivia(oldNode.GetTrailingTrivia()));

            return context.Document.WithSyntaxRoot(newRoot);
        }
    }
}