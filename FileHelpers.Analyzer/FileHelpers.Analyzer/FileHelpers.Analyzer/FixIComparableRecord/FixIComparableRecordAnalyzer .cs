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
    public class FixIComparableRecordAnalyzer : DiagnosticAnalyzer, IFileHelperAnalyzer
    {
        public string Id => DiagnosticId;
        public const string DiagnosticId = "FileHelpersIComparable";

        public static readonly string Title = "Use IComparable<T>";
        public string FixTitle => "Use IComparable<T>";

        private static readonly string MessageFormat = "Use IComparable<T>";
        private static readonly string Description = "Use IComparable<T> instead of IComparableRecord<T>";
        
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
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.SimpleBaseType);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = (SimpleBaseTypeSyntax) context.Node;

            var typeName = node.Type.ToString();
            //if (engineType.StartsWith("FileHelpers."))
            //    engineType = engineType.Substring("FileHelpers.".Length);

            if (typeName.StartsWith("IComparableRecord<"))
            {
                var diagnostic = Diagnostic.Create(Rule, node.GetLocation(), "FileHelpers.Dynamic");
                context.ReportDiagnostic(diagnostic);
            }
        }

    }
}
