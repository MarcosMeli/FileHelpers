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
    public class FixIComparableRecordTest
        : FileHelpersCodeFixVerifier<FixIComparableRecordAnalyzer, FixIComparableRecordCodeFixProvider>
    {
       
        [TestMethod]
        public void FixIComparableRecord()
        {
            var test = @"using System;
public class Test: IComparableRecord<Test>  
{
  public bool IsEqualRecord(Record other)
  {
    return true;
  }
}";

            var expected = new DiagnosticResult
            {
                Message = "Use IComparable<T>"
            };

            var fixtest = @"using System;
public class Test: IComparable<Test>  
{
  public int CompareTo(Record other)
  {
    return true;
  }
}";


            VerifyErrorInFile(test, expected, fixtest);
        }
   }
}