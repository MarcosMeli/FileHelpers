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
    public class UseGenericEngineAnalyzerTest 
        : FileHelpersCodeFixVerifier<UseGenericEngineAnalyzer, UseGenericEngineCodeFixProvider>
    {
        

        [TestMethod]
        public void UsingGeneric()
        {
            var test = @"var engine = new FileHelperEngine(typeof(RecordClass));";

            var expected = new DiagnosticResult
            {
                Message = "You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelperEngine<RecordClass>();";

            VerifyWarningInMethod(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericWithNameSpace()
        {
            var test = @"var engine = new FileHelpers.FileHelperEngine(typeof(RecordClass));";

            var expected = new DiagnosticResult
            {
                Message = "You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelpers.FileHelperEngine<RecordClass>();";

            VerifyWarningInMethod(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericAsync()
        {
            var test = @"var engine = new FileHelperAsyncEngine(typeof(RecordClass));";

            var expected = new DiagnosticResult
            {
                Message = "You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelperAsyncEngine<RecordClass>();";

            VerifyWarningInMethod(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericAsyncWithNameSpace()
        {
            var test = @"var engine = new FileHelpers.FileHelperAsyncEngine(typeof(RecordClass));";

            var expected = new DiagnosticResult
            {
                Message = "You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelpers.FileHelperAsyncEngine<RecordClass>();";

            VerifyWarningInMethod(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericClassNamespace()
        {
            var test = @"var engine = new FileHelperEngine(typeof(Namespace.RecordClass));";

            var expected = new DiagnosticResult
            {
                Message = "You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelperEngine<Namespace.RecordClass>();";

            VerifyWarningInMethod(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericClassNamespaceWithNameSpace()
        {
            var test = @"var engine = new FileHelpers.FileHelperEngine(typeof(Namespace.RecordClass));";

            var expected = new DiagnosticResult
            {
                Message = "You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelpers.FileHelperEngine<Namespace.RecordClass>();";

            VerifyWarningInMethod(test, expected, fixtest);
        }


        [TestMethod]
        public void UsingGenericWithParams1()
        {
            var test = @"var engine = new FileHelperEngine(typeof(RecordClass), Encoding.UTF8);";

            var expected = new DiagnosticResult
            {
                Message = "You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelperEngine<RecordClass>(Encoding.UTF8);";

            VerifyWarningInMethod(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingGenericWithParams2()
        {
            var test = @"var engine = new FileHelperEngine(typeof(RecordClass), new Encoding());";

            var expected = new DiagnosticResult
            {
                Message = "You can use the generic engine"
            };
            var fixtest = @"var engine = new FileHelperEngine<RecordClass>(new Encoding());";

            VerifyWarningInMethod(test, expected, fixtest);
        }
    }
}