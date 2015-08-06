using FileHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace FileHelpersAnalyzer.Test
{
    public class FileHelpersCodeFixVerifier<TAnalyzer, TCodeFix>
        : CodeFixVerifier
        where TAnalyzer : DiagnosticAnalyzer,IFileHelperAnalyzer,  new()
        where TCodeFix : CodeFixProvider, new()
    {
        private readonly string _diagnosticId;

        public FileHelpersCodeFixVerifier()
        {
            _diagnosticId = new TAnalyzer().Id;
            if (DiagnosticVerifier.ExtraReferences.Count == 0)
            {
                DiagnosticVerifier.ExtraReferences.Add(typeof (FileHelperEngine).Assembly);
            }
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new TCodeFix();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new TAnalyzer(); 
        }

        protected string MethodEsqueleton(string code)
        {
            return @"using System;
using FileHelpers;

public class TestClass
{
  public void Run()
  {
    " + code + @"
  }
}
";
        }


        public void VerifyWarningInMethod(string testCode, DiagnosticResult expected, string fixtestCode)
        {
            var test = MethodEsqueleton(testCode);

            expected.Severity = DiagnosticSeverity.Warning;
            expected.Id = _diagnosticId;

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = MethodEsqueleton(fixtestCode);

            VerifyCSharpFix(test, fixtest);
        }



        protected string ClassEsqueleton(string code)
        {
            return @"using System;
using FileHelpers;

public class TestClass
{
    " + code + @"
}
";
        }


        public void VerifyWarningInClass(string testCode, DiagnosticResult expected, string fixtestCode)
        {
            var test = ClassEsqueleton(testCode);

            expected.Severity = DiagnosticSeverity.Warning;
            expected.Id = _diagnosticId;

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = ClassEsqueleton(fixtestCode);

            VerifyCSharpFix(test, fixtest);
        }


        [TestMethod]
        public void NoDiagnostics()
        {
            //No diagnostics expected to show up

            var test = @"";
            VerifyCSharpDiagnostic(test);
        }

        public void VerifyWarningInFile(string testCode, DiagnosticResult expected, string fixtestCode)
        {
           
            expected.Severity = DiagnosticSeverity.Warning;
            expected.Id = _diagnosticId;

            VerifyCSharpDiagnostic(testCode, expected);

            VerifyCSharpFix(testCode, fixtestCode);
        }

        public void VerifyErrorInFile(string testCode, DiagnosticResult expected, string fixtestCode)
        {

            expected.Severity = DiagnosticSeverity.Error;
            expected.Id = _diagnosticId;

            VerifyCSharpDiagnostic(testCode, expected);

            VerifyCSharpFix(testCode, fixtestCode);
        }

    }
}