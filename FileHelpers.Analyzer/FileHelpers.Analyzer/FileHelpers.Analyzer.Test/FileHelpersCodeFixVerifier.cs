using Microsoft.CodeAnalysis;
using TestHelper;

namespace FileHelpersAnalyzer.Test
{
    public class FileHelpersCodeFixVerifier : CodeFixVerifier
    {
        private readonly string _diagnosticId;

        public FileHelpersCodeFixVerifier(string diagnosticId)
        {
            _diagnosticId = diagnosticId;
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

        public void VerifyWarningAndFix(string testCode, DiagnosticResult expected, string fixtestCode)
        {
            var test = MethodEsqueleton(testCode);

            expected.Severity = DiagnosticSeverity.Warning;
            expected.Id = _diagnosticId;

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = MethodEsqueleton(fixtestCode);

            VerifyCSharpFix(test, fixtest);
        }
    }
}