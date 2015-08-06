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
    [ExportCodeFixProvider(LanguageNames.CSharp, 
        Name = nameof(FixIComparableRecordCodeFixProvider)), Shared]
    public class FixIComparableRecordCodeFixProvider
        : FileHelpersCodeFixProvider<FixIComparableRecordAnalyzer>
    {
    
        protected override async Task<Document> ApplyFix(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic)
        {
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var node = root.FindNode(diagnosticSpan) as SimpleBaseTypeSyntax;

            var newRoot = root;
         

            var classDecl = (ClassDeclarationSyntax)node.Parent.Parent;

//            var classDecl = newRoot.FindNode(classDeclOriginnode.Parent.Parent.GetLocation().SourceSpan) as ClassDeclarationSyntax;

            var method = classDecl.Members.FirstOrDefault(x =>
                x is MethodDeclarationSyntax &&
                ((MethodDeclarationSyntax) x).Identifier.ToString() == "IsEqualRecord"
                ) as MethodDeclarationSyntax;


            if (method != null)
            {
                newRoot = newRoot.ReplaceNode(method, 
                    SyntaxFactory.MethodDeclaration(
                        attributeLists: method.AttributeLists,modifiers:method.Modifiers, body:method.Body,
                        returnType: SyntaxFactory.ParseTypeName("int"),
                        explicitInterfaceSpecifier: method.ExplicitInterfaceSpecifier,
                        identifier: SyntaxFactory.Identifier("CompareTo"),
                        typeParameterList:method.TypeParameterList,
                        parameterList: method.ParameterList,
                        constraintClauses:method.ConstraintClauses,
                        expressionBody: method.ExpressionBody
                        )
                    );
            }

            var newNode = newRoot.FindNode(diagnosticSpan) as SimpleBaseTypeSyntax; 

            newRoot = newRoot.ReplaceNode(newNode,
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(node.Type.ToString().Replace("IComparableRecord<", "IComparable<"))));


            return context.Document.WithSyntaxRoot(newRoot);

        }
    }
}