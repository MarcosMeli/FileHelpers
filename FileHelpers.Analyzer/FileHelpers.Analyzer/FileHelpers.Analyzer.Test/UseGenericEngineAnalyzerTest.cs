using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using FileHelpersAnalyzer;

namespace FileHelpersAnalyzer.Test
{
    [TestClass]
    public class UseGenericEngineAnalyzerTest : FileHelpersCodeFixVerifier
    {

        public UseGenericEngineAnalyzerTest()
            :base(UseGenericEngineAnalyzer.DiagnosticId)
        {
        }

        //No diagnostics expected to show up
        [TestMethod]
        public void NoDiagnostics()
        {
            var test = @"";

            VerifyCSharpDiagnostic(test);
        }


        [TestMethod]
        public void UsingGeneric()
        {
            var test = @"var engine = new FileHelperEngine(typeof(RecordClass));";

            var expected = new DiagnosticResult
            {
                Locations = new []{new DiagnosticResultLocation("Test0.cs", 8, 18), },
                Message = "FileHelpers: You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelperEngine<RecordClass>();";

            VerifyWarningAndFix(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericWithNameSpace()
        {
            var test = @"var engine = new FileHelpers.FileHelperEngine(typeof(RecordClass));";

            var expected = new DiagnosticResult
            {
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 8, 18), },
                Message = "FileHelpers: You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelpers.FileHelperEngine<RecordClass>();";

            VerifyWarningAndFix(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericAsync()
        {
            var test = @"var engine = new FileHelperAsyncEngine(typeof(RecordClass));";

            var expected = new DiagnosticResult
            {
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 8, 18), },
                Message = "FileHelpers: You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelperAsyncEngine<RecordClass>();";

            VerifyWarningAndFix(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericAsyncWithNameSpace()
        {
            var test = @"var engine = new FileHelpers.FileHelperAsyncEngine(typeof(RecordClass));";

            var expected = new DiagnosticResult
            {
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 8, 18), },
                Message = "FileHelpers: You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelpers.FileHelperAsyncEngine<RecordClass>();";

            VerifyWarningAndFix(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericClassNamespace()
        {
            var test = @"var engine = new FileHelperEngine(typeof(Namespace.RecordClass));";

            var expected = new DiagnosticResult
            {
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 8, 18), },
                Message = "FileHelpers: You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelperEngine<Namespace.RecordClass>();";

            VerifyWarningAndFix(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericClassNamespaceWithNameSpace()
        {
            var test = @"var engine = new FileHelpers.FileHelperEngine(typeof(Namespace.RecordClass));";

            var expected = new DiagnosticResult
            {
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 8, 18), },
                Message = "FileHelpers: You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelpers.FileHelperEngine<Namespace.RecordClass>();";

            VerifyWarningAndFix(test, expected, fixtest);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new UseGenericEngineCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new UseGenericEngineAnalyzer();
        }

      
    }
}