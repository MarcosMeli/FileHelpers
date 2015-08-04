using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using TestHelper;

namespace FileHelpersAnalyzer.Test
{
    public class FileHelpersCodeFixVerifier<TAnalyzer, TCodeFix>
        : CodeFixVerifier
        where TAnalyzer : DiagnosticAnalyzer,  new()
        where TCodeFix : CodeFixProvider, new()
    {
        private readonly string _diagnosticId;

        public FileHelpersCodeFixVerifier(string diagnosticId)
        {
            _diagnosticId = diagnosticId;
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


        public void VerifyWarningAndFixInMethod(string testCode, DiagnosticResult expected, string fixtestCode)
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


        public void VerifyWarningAndFixInClass(string testCode, DiagnosticResult expected, string fixtestCode)
        {
            var test = ClassEsqueleton(testCode);

            expected.Severity = DiagnosticSeverity.Warning;
            expected.Id = _diagnosticId;

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = ClassEsqueleton(fixtestCode);

            VerifyCSharpFix(test, fixtest);
        }
    }
}