using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
	public class FileEncoding
	{

        private const string expectedTextWithNTilde = "Ana Tru\u00f1i\u00f1o Emparedados y helados";
	    private const string expectedTextWithEGrave = "Blondesddsl p\u00e8re et fils";
	    private const string expectedTextWithEAcute1 = "Fr\u00e9d\u00e9rique Citeaux";
	    private const string expectedTextWithEAcute2 = "24, place Kl\u00e9ber";
	    private const string expectedTextWithADiaeresis = "Berguvsv\u00e4gen  8";
	    private const string expectedTextWithARing = "Lule\u00e5";

	    private const int ExpectedRecords = 7;

        private void RunTests(Encoding enc, params string[] pathElements)
		{
			var engine = new FileHelperEngine<CustomersVerticalBar>();
			engine.Encoding = enc;
			Assert.AreEqual(enc, engine.Encoding);
            CoreRunTest(engine,pathElements);
		}

        private void RunConstructor(Encoding enc, params string[] pathElements)
		{
			var engine = new FileHelperEngine<CustomersVerticalBar>( enc);
			Assert.AreEqual(enc, engine.Encoding);
            CoreRunTest(engine,pathElements);
		}

        private void CoreRunTest(FileHelperEngine<CustomersVerticalBar> engine, params string[] pathElements)
		{
            CustomersVerticalBar[] res = TestCommon.ReadTest<CustomersVerticalBar>(engine, pathElements);
	
			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual(ExpectedRecords, engine.TotalRecords);
	
			Assert.AreEqual(expectedTextWithNTilde, res[1].CompanyName);
			Assert.AreEqual(expectedTextWithEGrave, res[6].CompanyName);
			Assert.AreEqual(expectedTextWithEAcute1, res[6].ContactName);
	
			Assert.AreEqual(expectedTextWithEAcute2, res[6].Address);
			Assert.AreEqual(expectedTextWithADiaeresis, res[4].Address);
			Assert.AreEqual(expectedTextWithARing, res[4].City);
		}

		private void RunAsyncTests(FileHelperEngine<CustomersVerticalBar> engine, Encoding enc, params string[] pathElements)
		{
			var asyncEngine = new FileHelperAsyncEngine<CustomersVerticalBar>();
			asyncEngine.Encoding = enc;
			Assert.AreEqual(enc, asyncEngine.Encoding);

            CoreRunAsync(asyncEngine, engine, pathElements);
		}

		private void RunAsyncConstructor(Encoding enc, params string[] pathElements)
		{
			var asyncEngine = new FileHelperAsyncEngine<CustomersVerticalBar>( enc);
			Assert.AreEqual(enc, asyncEngine.Encoding);

            var engine = new FileHelperEngine<CustomersVerticalBar>();
            CoreRunAsync(asyncEngine, engine, pathElements);
		}

        private void CoreRunAsync(FileHelperAsyncEngine<CustomersVerticalBar> asyncEngine,
                                  FileHelperEngine<CustomersVerticalBar> engine, 
                                  params string[] pathElements)
		{
            var arr = new List<CustomersVerticalBar>();

            TestCommon.BeginReadTest<CustomersVerticalBar>(asyncEngine, pathElements);

			foreach (var record in asyncEngine)
			{
				arr.Add(record);
			}
	
			CustomersVerticalBar[] res = arr.ToArray();
            ExpectedRecords.AssertEqualTo<int>(res.Length, "Length mismatch in Encoding-CoreRunAsync");
            ExpectedRecords.AssertEqualTo<int>(asyncEngine.TotalRecords, "Total record count mismatch in Encoding-CoreRunAsync");
	
			Assert.AreEqual(expectedTextWithNTilde, res[1].CompanyName);
			Assert.AreEqual(expectedTextWithEGrave, res[6].CompanyName);
			Assert.AreEqual(expectedTextWithEAcute1, res[6].ContactName);
	
			Assert.AreEqual(expectedTextWithEAcute2, res[6].Address);
			Assert.AreEqual(expectedTextWithADiaeresis, res[4].Address);
			Assert.AreEqual(expectedTextWithARing, res[4].City);
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
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            RunAsyncTests(engine, Encoding.BigEndianUnicode, "Good", "EncodingUnicodeBig.txt");
		}

		[Test]
		public void EncodingAsyncANSI()
		{
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            RunAsyncTests(engine, Encoding.Default, "Good", "EncodingANSI.txt");
		}

		[Test]
		public void EncodingAsyncUTF8()
		{
            var engine = new FileHelperEngine<CustomersVerticalBar>();
			RunAsyncTests(engine, Encoding.UTF8, "Good", "EncodingUTF8.txt");
		}

		[Test]
		public void EncodingAsyncUnicode()
		{
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            RunAsyncTests(engine, Encoding.Unicode, "Good", "EncodingUnicode.txt");
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