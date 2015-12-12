using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FileHelpersAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ValidateRecordTypeAnalyzer : DiagnosticAnalyzer, IFileHelperAnalyzer
    {
        public string Id => DiagnosticId;
        public const string DiagnosticId = "FileHelpersValidateRecordType";

        public static readonly string Title = "You record type must be marked with [DelimitedRecord] or [FixedLengthRecord] attribute";
        public string FixTitle => "Mark record class with the right attribute";

        private static readonly string MessageFormat = "Mark record class with the right attribute";
        private static readonly string Description = "You record type must be marked with [DelimitedRecord] or [FixedLengthRecord] attribute";

        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: Description,
            helpLinkUri: "http://www.filehelpers.net/analizer/");

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);


        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ObjectCreationExpression);
            //context.RegisterCompilationAction();
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = (ObjectCreationExpressionSyntax)context.Node;

            var engineType = node.Type.ToString().TrimPhraseStart("FileHelpers.");

            if (engineType.StartsWithAnyIgnoreCase("FileHelperEngine", "FileHelperAsyncEngine"))
            {
                TypeSyntax recordSyntax = null;
                if (node.ArgumentList.Arguments.Count > 0 &&
                    node.ArgumentList.Arguments[0].Expression is TypeOfExpressionSyntax)
                {
                    var typeofSyntax = (TypeOfExpressionSyntax)node.ArgumentList.Arguments[0].Expression;
                    recordSyntax = typeofSyntax.Type;
                }

                if (recordSyntax == null)
                {
                    var type = node.Type as GenericNameSyntax;
                    if (type != null)
                        recordSyntax = type.TypeArgumentList.Arguments[0];
                }


                if (recordSyntax != null)
                {
                    var recordType = context.SemanticModel.GetTypeInfo(recordSyntax);

                    if (recordType.Type.BaseType.Name == "Object")
                    {
                        var attributes = recordType.Type.GetAttributes();
                        if (
                            attributes.Count(
                                x =>
                                    x.AttributeClass.Name == "DelimitedRecordAttribute" ||
                                    x.AttributeClass.Name == "FixedLengthRecordAttribute") == 0)
                        {
                            var diagnostic = Diagnostic.Create(Rule, recordType.Type.OriginalDefinition.Locations.FirstOrDefault(), (IEnumerable<Location>) new [] { recordSyntax.GetLocation() });
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }

            }
        }

    }
}