using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using TestHelper;

using FileHelpersAnalyzer;

namespace FileHelpersAnalyzer.Test
{
    using FileHelpers;

    [TestClass]
    public class FixIComparableRecordTest :
        FileHelpersCodeFixVerifier<FixIComparableRecordAnalyzer, FixIComparableRecordCodeFixProvider>
    {
        [TestMethod]
        public void FixIComparableRecord()
        {
            var test = @"using System;
public class Test: IComparableRecord<Test>  
{
    public bool IsEqualRecord(Test other)
    {
        var test = 0;

        // This is a test
        for (var i = 0; i < 100; i++)
        {
            if (i == 53)
                break;
        }

        return true;
    }
}";

            var expected = new DiagnosticResult { Message = "Use IComparable<T>" };

            var fixtest = @"using System;
public class Test: IComparable<Test>  
{
    public int CompareTo(Test other)
    {
        throw new NotImplementedException();
        /*
         * TODO: Implement CompareTo
         * See: https://msdn.microsoft.com/en-us/library/43hc6wht(v=vs.110).aspx
         *
        var test = 0;

        // This is a test
        for (var i = 0; i < 100; i++)
        {
            if (i == 53)
                break;
        }

        return true;
         */
    }
}";

            VerifyErrorInFile(test, expected, fixtest);
        }

        [TestMethod]
        public void FixIComparableRecord2()
        {
            var test = @"using System;
public class Test: IComparableRecord<Test>  
{
    public bool IsEqualRecord(Test other)
    {
        return true;
    }
}";

            var expected = new DiagnosticResult { Message = "Use IComparable<T>" };

            var fixtest = @"using System;
public class Test: IComparable<Test>  
{
    public int CompareTo(Test other)
    {
        throw new NotImplementedException();
        /*
         * TODO: Implement CompareTo
         * See: https://msdn.microsoft.com/en-us/library/43hc6wht(v=vs.110).aspx
         *
        return true;
         */
    }
}";

            VerifyErrorInFile(test, expected, fixtest);
        }
    }
}