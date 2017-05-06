using System;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class RealLineNumbers
    {
        [Test]
        public void RealLineNumberAppliesIgnoreEmpty()
        {
            var engine = new FileHelperEngine<IgnoreEmpties.IgnoreEmptyType1>();

            var res = TestCommon.ReadTest<IgnoreEmpties.IgnoreEmptyType1>(engine, "Good", "IgnoreEmpty1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(0, engine.GetRealLineNumber(0));
            Assert.AreEqual(2, engine.GetRealLineNumber(1));
            Assert.AreEqual(4, engine.GetRealLineNumber(2));
            Assert.AreEqual(6, engine.GetRealLineNumber(3));
            Assert.AreEqual(7, engine.GetRealLineNumber(4));
            Assert.AreEqual(0, engine.GetRealLineNumber(5));
        }

        [Test]
        public void RealLineNumberAppliesIgnoreEmpty2()
        {
            var engine = new FileHelperEngine<IgnoreEmpties.IgnoreEmptyType1>();

            object[] res = TestCommon.ReadTest(engine, "Good", "IgnoreEmpty2.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(0, engine.GetRealLineNumber(0));
            Assert.AreEqual(1, engine.GetRealLineNumber(1));
            Assert.AreEqual(4, engine.GetRealLineNumber(2));
            Assert.AreEqual(6, engine.GetRealLineNumber(3));
            Assert.AreEqual(7, engine.GetRealLineNumber(4));
            Assert.AreEqual(0, engine.GetRealLineNumber(5));
        }

        [Test]
        public void RealLineNumberAppliesDiscardFirst()
        {
            var engine = new FileHelperEngine<DiscardType2>();
            var res = engine.ReadFile(FileTest.Good.DiscardFirst2.Path);

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(0, engine.GetRealLineNumber(0));
            Assert.AreEqual(3, engine.GetRealLineNumber(1));
            Assert.AreEqual(4, engine.GetRealLineNumber(2));
            Assert.AreEqual(5, engine.GetRealLineNumber(3));
            Assert.AreEqual(6, engine.GetRealLineNumber(4));
            Assert.AreEqual(0, engine.GetRealLineNumber(5));
        }

        [Test]
        public void RealLineNumberAppliesDiscardLast()
        {
            var engine = new FileHelperEngine<DiscardLastType1>();
            var res = TestCommon.ReadTest<DiscardLastType1>(engine, "Good", "DiscardLast1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(0, engine.GetRealLineNumber(0));
            Assert.AreEqual(1, engine.GetRealLineNumber(1));
            Assert.AreEqual(2, engine.GetRealLineNumber(2));
            Assert.AreEqual(3, engine.GetRealLineNumber(3));
            Assert.AreEqual(4, engine.GetRealLineNumber(4));
            Assert.AreEqual(0, engine.GetRealLineNumber(5));
        }
    }
}