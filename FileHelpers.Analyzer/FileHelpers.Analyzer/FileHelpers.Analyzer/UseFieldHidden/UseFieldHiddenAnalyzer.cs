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
    public class UseFieldHiddenAnalyzer 
        : DiagnosticAnalyzer, IFileHelperAnalyzer
    {
        public string Id => DiagnosticId;
        public const string DiagnosticId = "FileHelpersUseFieldHidden";

        public static readonly string Title = "You must use [FieldHidden]";
        public string FixTitle => "Use [FieldHidden] attribute";
        private static readonly string MessageFormat = "You must use [FieldHidden]";
        private static readonly string Description = "[FieldIgnored] and [FieldNotInFile] are obsolete";
        
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);


        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.Attribute);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var attribute = (AttributeSyntax)context.Node;
            var attributeName = attribute.Name.ToString();

            if (attributeName.StartsWith("FileHelpers."))
                attributeName = attributeName.Substring("FileHelpers.".Length);

            if (attributeName == "FieldNotInFile" ||
                attributeName == "FieldNotInFileAttribute" ||
                attributeName == "FieldIgnored" ||
                attributeName == "FieldIgnoredAttribute")
            {
                var symbol = context.SemanticModel.GetTypeInfo(attribute);
                if (symbol.Type != null &&
                    symbol.Type.ContainingNamespace.Name == "FileHelpers" &&
                    symbol.Type.ContainingAssembly.Name == "FileHelpers")
                {
                    var diagnostic = Diagnostic.Create(Rule, attribute.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
            
        }

    }
}
