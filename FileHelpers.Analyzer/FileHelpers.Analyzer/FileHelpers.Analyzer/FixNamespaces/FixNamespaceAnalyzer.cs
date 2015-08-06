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
    public class FixNamespaceAnalyzer: DiagnosticAnalyzer, IFileHelperAnalyzer
    {
        public string Id => DiagnosticId;
        public const string DiagnosticId = "FileHelpersFixNamespace";

        public static readonly string Title = "Use the new namespaces";
        public string FixTitle => "Use the correct namespace";

        private static readonly string MessageFormat = "Use the {0} namespace";
        private static readonly string Description = "Use the new namespaces";
        
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
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.UsingDirective);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = (UsingDirectiveSyntax) context.Node;

            var namespaceName = node.Name.ToString();
            //if (engineType.StartsWith("FileHelpers."))
            //    engineType = engineType.Substring("FileHelpers.".Length);

            if (namespaceName == "FileHelpers.RunTime")
            {
                var diagnostic = Diagnostic.Create(Rule, node.GetLocation(), "FileHelpers.Dynamic");
                context.ReportDiagnostic(diagnostic);
            }
        }

    }
}
