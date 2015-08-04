using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FileHelpersAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UseGenericEngineAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "UseGenericEngine";

        public static readonly string Title = "FileHelpers: You can use the generic engine";
        public static readonly string FixTitle = "FileHelpers: Use the generic engine";
        private static readonly string MessageFormat = "FileHelpers: You can use the generic engine";
        private static readonly string Description = "It is recommended to use the generic version of the engine";
        
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);


        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ObjectCreationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var creation = (ObjectCreationExpressionSyntax)context.Node;

            var engineType = creation.Type.ToString();

            if (engineType == "FileHelperEngine" ||
                engineType == "FileHelpers.FileHelperEngine" ||
                engineType == "FileHelperAsyncEngine" ||
                engineType == "FileHelpers.FileHelperAsyncEngine"
                )
            {
                if (creation.ArgumentList.Arguments.Count > 0 &&
                    creation.ArgumentList.Arguments[0].Expression is TypeOfExpressionSyntax)
                {
                    var diagnostic = Diagnostic.Create(Rule, creation.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

    }
}
