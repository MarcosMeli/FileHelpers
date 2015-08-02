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
    public class UnitTest : CodeFixVerifier
    {

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
            var test = @"using System;
public class Test
{
  public void Run()
{
var engine = new FileHelperEngine(typeof(RecordClass));
}
}
";

            var expected = new DiagnosticResult
            {
                Id = UseGenericEngineAnalyzer.DiagnosticId,
                Severity = DiagnosticSeverity.Warning,
                Locations = new []{new DiagnosticResultLocation("Test0.cs", 6,14), },
                Message = "FileHelpers: You can use the generic engine"
            };

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = @"using System;
public class Test
{
  public void Run()
{
var engine = new FileHelperEngine<RecordClass>();
}
}
";


            VerifyCSharpFix(test, fixtest);
        }

        [TestMethod]
        public void UsingGenericWithNameSpace()
        {
            var test = @"using System;
public class Test
{
  public void Run()
{
var engine = new FileHelpers.FileHelperEngine(typeof(RecordClass));
}
}
";

            var expected = new DiagnosticResult
            {
                Id = UseGenericEngineAnalyzer.DiagnosticId,
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 6, 14), },
                Message = "FileHelpers: You can use the generic engine"
            };

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = @"using System;
public class Test
{
  public void Run()
{
var engine = new FileHelpers.FileHelperEngine<RecordClass>();
}
}
";


            VerifyCSharpFix(test, fixtest);
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