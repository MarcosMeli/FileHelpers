using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class FileEncoding
    {
        private const string ExpectedTextWithNTilde = "Ana Tru\u00f1i\u00f1o Emparedados y helados";
        private const string ExpectedTextWithEGrave = "Blondesddsl p\u00e8re et fils";
        private const string ExpectedTextWithEAcute1 = "Fr\u00e9d\u00e9rique Citeaux";
        private const string ExpectedTextWithEAcute2 = "24, place Kl\u00e9ber";
        private const string ExpectedTextWithADiaeresis = "Berguvsv\u00e4gen  8";
        private const string ExpectedTextWithARing = "Lule\u00e5";

        private const int ExpectedRecords = 7;

        private static void RunTests(Encoding enc, params string[] pathElements)
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            engine.Encoding = enc;
            Assert.AreEqual(enc, engine.Encoding);
            CoreRunTest(engine, pathElements);
        }

        private static void RunConstructor(Encoding enc, params string[] pathElements)
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>(enc);
            Assert.AreEqual(enc, engine.Encoding);
            CoreRunTest(engine, pathElements);
        }

        private static void CoreRunTest(FileHelperEngine<CustomersVerticalBar> engine, params string[] pathElements)
        {
            CustomersVerticalBar[] res = TestCommon.ReadTest(engine, pathElements);

            Assert.AreEqual(ExpectedRecords, res.Length);
            Assert.AreEqual(ExpectedRecords, engine.TotalRecords);

            Assert.AreEqual(ExpectedTextWithNTilde, res[1].CompanyName);
            Assert.AreEqual(ExpectedTextWithEGrave, res[6].CompanyName);
            Assert.AreEqual(ExpectedTextWithEAcute1, res[6].ContactName);

            Assert.AreEqual(ExpectedTextWithEAcute2, res[6].Address);
            Assert.AreEqual(ExpectedTextWithADiaeresis, res[4].Address);
            Assert.AreEqual(ExpectedTextWithARing, res[4].City);
        }

        private static void RunAsyncTests(Encoding enc, params string[] pathElements)
        {
            var asyncEngine = new FileHelperAsyncEngine<CustomersVerticalBar>();
            asyncEngine.Encoding = enc;
            Assert.AreEqual(enc, asyncEngine.Encoding);

            CoreRunAsync(asyncEngine, pathElements);
        }

        private static void RunAsyncConstructor(Encoding enc, params string[] pathElements)
        {
            var asyncEngine = new FileHelperAsyncEngine<CustomersVerticalBar>(enc);
            Assert.AreEqual(enc, asyncEngine.Encoding);

            CoreRunAsync(asyncEngine, pathElements);
        }

        private static void CoreRunAsync(FileHelperAsyncEngine<CustomersVerticalBar> asyncEngine, params string[] pathElements)
        {
            var arr = new List<CustomersVerticalBar>();

            TestCommon.BeginReadTest(asyncEngine, pathElements);

            foreach (var record in asyncEngine)
                arr.Add(record);

            CustomersVerticalBar[] res = arr.ToArray();
            Check.That(ExpectedRecords).IsEqualTo(res.Length);
            Check.That(ExpectedRecords).IsEqualTo(asyncEngine.TotalRecords);

            Assert.AreEqual(ExpectedTextWithNTilde, res[1].CompanyName);
            Assert.AreEqual(ExpectedTextWithEGrave, res[6].CompanyName);
            Assert.AreEqual(ExpectedTextWithEAcute1, res[6].ContactName);

            Assert.AreEqual(ExpectedTextWithEAcute2, res[6].Address);
            Assert.AreEqual(ExpectedTextWithADiaeresis, res[4].Address);
            Assert.AreEqual(ExpectedTextWithARing, res[4].City);
        }

        [Test]
        public void EncodingANSI()
        {
            RunTests(Encoding.Default, "Good", "EncodingANSI.txt");
        }

        [Test]
        public void EncodingUTF8()
        {
            RunTests(Encoding.UTF8, "Good", "EncodingUTF8.txt");
        }

        [Test]
        public void EncodingUnicode()
        {
            RunTests(Encoding.Unicode, "Good", "EncodingUnicode.txt");
        }

        [Test]
        public void EncodingAsyncUnicodeBig()
        {
            RunAsyncTests(Encoding.BigEndianUnicode, "Good", "EncodingUnicodeBig.txt");
        }

        [Test]
        public void EncodingAsyncANSI()
        {
            RunAsyncTests(Encoding.Default, "Good", "EncodingANSI.txt");
        }

        [Test]
        public void EncodingAsyncUTF8()
        {
            RunAsyncTests(Encoding.UTF8, "Good", "EncodingUTF8.txt");
        }

        [Test]
        public void EncodingAsyncUnicode()
        {
            RunAsyncTests(Encoding.Unicode, "Good", "EncodingUnicode.txt");
        }

        [Test]
        public void EncodingUnicodeBig()
        {
            RunTests(Encoding.BigEndianUnicode, "Good", "EncodingUnicodeBig.txt");
        }

        [Test]
        public void EncodingAsyncANSIConstructor()
        {
            RunConstructor(Encoding.Default, "Good", "EncodingANSI.txt");
        }

        [Test]
        public void EncodingAsyncUTFConstructor()
        {
            RunConstructor(Encoding.UTF8, "Good", "EncodingUTF8.txt");
        }

        [Test]
        public void EncodingAsyncUnicodeConstructor()
        {
            RunConstructor(Encoding.Unicode, "Good", "EncodingUnicode.txt");
        }

        [Test]
        public void EncodingANSIConstructorAsync()
        {
            RunAsyncConstructor(Encoding.Default, "Good", "EncodingANSI.txt");
        }

        [Test]
        public void EncodingUTF8ConstructorAsync()
        {
            RunAsyncConstructor(Encoding.UTF8, "Good", "EncodingUTF8.txt");
        }

        [Test]
        public void EncodingUnicodeConstructorAsync()
        {
            RunAsyncConstructor(Encoding.Unicode, "Good", "EncodingUnicode.txt");
        }
    }
}