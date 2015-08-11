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
    public class FixNamespacesTest 
        : FileHelpersCodeFixVerifier<FixNamespaceAnalyzer, FixNamespaceCodeFixProvider>
    {
       

        [TestMethod]
        public void FixNamespaces1()
        {
            var test = @"using FileHelpers.RunTime;

public class Test {}";

            var expected = new DiagnosticResult
            {
                Message = "Use the FileHelpers.Dynamic namespace"
            };

            var fixtest = @"using FileHelpers.Dynamic;

public class Test {}";


            VerifyErrorInFile(test, expected, fixtest);
        }
   }
}