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
    public class UseFieldHiddenTest 
        : FileHelpersCodeFixVerifier<UseFieldHiddenAnalyzer, UseFieldHiddenCodeFixProvider>
    {
        public UseFieldHiddenTest()
            :base(UseFieldHiddenAnalyzer.DiagnosticId)
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
        public void UsingFieldIgnored1()
        {
            var test = @"[FieldIgnored]
                         public int Field;";

            var expected = new DiagnosticResult
            {
                Message = "You must use [FieldHidden]"
            };

            var fixtest = @"[FieldHidden]
                         public int Field;";

            VerifyWarningAndFixInClass(test, expected, fixtest);
        }


        [TestMethod]
        public void UsingFieldIgnored2()
        {
            var test = @"[FieldIgnored()]
                         public int Field;";

            var expected = new DiagnosticResult
            {
                Message = "You must use [FieldHidden]"
            };

            var fixtest = @"[FieldHidden]
                         public int Field;";

            VerifyWarningAndFixInClass(test, expected, fixtest);
        }

        [TestMethod]
        public void UsingFieldIgnored3()
        {
            var test = @"[FieldIgnoredAttribute]
                         public int Field;";

            var expected = new DiagnosticResult
            {
                Message = "You must use [FieldHidden]"
            };

            var fixtest = @"[FieldHidden]
                         public int Field;";

            VerifyWarningAndFixInClass(test, expected, fixtest);
        }



        [TestMethod]
        public void UsingFieldNotInFile1()
        {
            var test = @"[FieldNotInFile]
                         public int Field;";

            var expected = new DiagnosticResult
            {
                Message = "You must use [FieldHidden]"
            };

            var fixtest = @"[FieldHidden]
                         public int Field;";

            VerifyWarningAndFixInClass(test, expected, fixtest);
        }


        [TestMethod]
        public void UsingFieldNotInFile2()
        {
            var test = @"[FieldNotInFile()]
                         public int Field;";

            var expected = new DiagnosticResult
            {
                Message = "You must use [FieldHidden]"
            };

            var fixtest = @"[FieldHidden]
                         public int Field;";

            VerifyWarningAndFixInClass(test, expected, fixtest);
        }


        [TestMethod]
        public void UsingFieldNotInFile3()
        {
            var test = @"[FieldNotInFileAttribute]
                         public int Field;";

            var expected = new DiagnosticResult
            {
                Message = "You must use [FieldHidden]"
            };

            var fixtest = @"[FieldHidden]
                         public int Field;";

            VerifyWarningAndFixInClass(test, expected, fixtest);
        }


    }
}