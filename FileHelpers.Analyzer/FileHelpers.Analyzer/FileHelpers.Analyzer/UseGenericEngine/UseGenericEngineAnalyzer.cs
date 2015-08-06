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
    public class UseGenericEngineAnalyzer : DiagnosticAnalyzer, IFileHelperAnalyzer
    {
        public string Id => DiagnosticId;
        public const string DiagnosticId = "FileHelpersUseGenericEngine";

        public static readonly string Title = "You can use the generic engine";
        public string FixTitle => "Use the generic engine";

        private static readonly string MessageFormat = "You can use the generic engine";
        private static readonly string Description = "It is recommended to use the generic version of the engine";
        
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, 
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true, 
            description: Description,
            helpLinkUri: "http://www.filehelpers.net/analizer/");

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);


        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ObjectCreationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = (ObjectCreationExpressionSyntax)context.Node;

            var engineType = node.Type.ToString();
            if (engineType.StartsWith("FileHelpers."))
                engineType = engineType.Substring("FileHelpers.".Length);

            if (engineType == "FileHelperEngine" ||
                engineType == "FileHelperAsyncEngine"
                )
            {
                if (node.ArgumentList.Arguments.Count > 0 &&
                    node.ArgumentList.Arguments[0].Expression is TypeOfExpressionSyntax)
                {
                    var symbol = context.SemanticModel.GetTypeInfo(node);
                    if (symbol.Type != null &&
                        symbol.Type.ContainingNamespace.Name == "FileHelpers" &&
                        symbol.Type.ContainingAssembly.Name == "FileHelpers")
                    {
                        var diagnostic = Diagnostic.Create(Rule, node.GetLocation());
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }

    }
}
