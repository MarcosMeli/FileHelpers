using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class RealLineNumbers
    {
        [Test]
        public void RealLineNumberAppliesIgnoreEmpty_WithAfterReadRecordEvent()
        {
            var engine = new FileHelperEngine<IgnoreEmpties.IgnoreEmptyType1>();

            var recordToLineNumber = new Dictionary<int, int>();
            var currentRecord = 0;
            engine.AfterReadRecord += (sender, afterReadEventArgs) =>
            {
                recordToLineNumber[currentRecord] = afterReadEventArgs.LineNumber;
                currentRecord++;
            };

            var res = TestCommon.ReadTest(engine, "Good", "IgnoreEmpty1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(2, recordToLineNumber[0]);
            Assert.AreEqual(4, recordToLineNumber[1]);
            Assert.AreEqual(6, recordToLineNumber[2]);
            Assert.AreEqual(7, recordToLineNumber[3]);
        }

        [Test]
        public void RealLineNumberAppliesDiscardFirst()
        {
            var engine = new FileHelperEngine<DiscardType2>();
            var recordToLineNumber = new Dictionary<int, int>();
            var currentRecord = 0;
            engine.AfterReadRecord += (sender, afterReadEventArgs) =>
            {
                recordToLineNumber[currentRecord] = afterReadEventArgs.LineNumber;
                currentRecord++;
            };

            var res = engine.ReadFile(FileTest.Good.DiscardFirst2.Path);

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(3, recordToLineNumber[0]);
            Assert.AreEqual(4, recordToLineNumber[1]);
            Assert.AreEqual(5, recordToLineNumber[2]);
            Assert.AreEqual(6, recordToLineNumber[3]);
        }

        [Test]
        public void RealLineNumberAppliesDiscardLast()
        {
            var engine = new FileHelperEngine<DiscardLastType1>();
            var recordToLineNumber = new Dictionary<int, int>();
            var currentRecord = 0;
            engine.AfterReadRecord += (sender, afterReadEventArgs) =>
            {
                recordToLineNumber[currentRecord] = afterReadEventArgs.LineNumber;
                currentRecord++;
            };

            var res = TestCommon.ReadTest(engine, "Good", "DiscardLast1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(1, recordToLineNumber[0]);
            Assert.AreEqual(2, recordToLineNumber[1]);
            Assert.AreEqual(3, recordToLineNumber[2]);
            Assert.AreEqual(4, recordToLineNumber[3]);
        }
    }
}