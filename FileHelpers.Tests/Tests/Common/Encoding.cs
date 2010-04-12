using System;
using System.Collections;
using System.Text;
using FileHelpers;
using NUnit.Framework;
using FileHelpers.Tests;

namespace FileHelpers.Tests
{
    [TestFixture]
	public class FileEncoding
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine asyncEngine;
	    private const string expectedTextWithNTilde = "Ana Tru\u00f1i\u00f1o Emparedados y helados";
	    private const string expectedTextWithEGrave = "Blondesddsl p\u00e8re et fils";
	    private const string expectedTextWithEAcute1 = "Fr\u00e9d\u00e9rique Citeaux";
	    private const string expectedTextWithEAcute2 = "24, place Kl\u00e9ber";
	    private const string expectedTextWithADiaeresis = "Berguvsv\u00e4gen  8";
	    private const string expectedTextWithARing = "Lule\u00e5";

	    private const int ExpectedRecords = 7;

        private void RunTests(Encoding enc, params string[] pathElements)
		{
			engine = new FileHelperEngine(typeof (CustomersVerticalBar));
			engine.Encoding = enc;
			Assert.AreEqual(enc, engine.Encoding);
			CoreRunTest(pathElements);
		}

        private void RunConstructor(Encoding enc, params string[] pathElements)
		{
			engine = new FileHelperEngine(typeof (CustomersVerticalBar), enc);
			Assert.AreEqual(enc, engine.Encoding);
			CoreRunTest(pathElements);
		}

        private void CoreRunTest(params string[] pathElements)
		{
	
			CustomersVerticalBar[] res = (CustomersVerticalBar[]) TestCommon.ReadTest(engine, pathElements);
	
			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual(ExpectedRecords, engine.TotalRecords);
	
			Assert.AreEqual(expectedTextWithNTilde, res[1].CompanyName);
			Assert.AreEqual(expectedTextWithEGrave, res[6].CompanyName);
			Assert.AreEqual(expectedTextWithEAcute1, res[6].ContactName);
	
			Assert.AreEqual(expectedTextWithEAcute2, res[6].Address);
			Assert.AreEqual(expectedTextWithADiaeresis, res[4].Address);
			Assert.AreEqual(expectedTextWithARing, res[4].City);
		}

		private void RunAsyncTests(Encoding enc, params string[] pathElements)
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (CustomersVerticalBar));
			asyncEngine.Encoding = enc;
			Assert.AreEqual(enc, asyncEngine.Encoding);

			CoreRunAsync(pathElements);
		}

		private void RunAsyncConstructor(Encoding enc, params string[] pathElements)
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (CustomersVerticalBar), enc);
			Assert.AreEqual(enc, asyncEngine.Encoding);

			CoreRunAsync(pathElements);
		}

		private void CoreRunAsync(params string[] pathElements)
		{
			ArrayList arr = new ArrayList();
	
			TestCommon.BeginReadTest(asyncEngine, pathElements);

			foreach (object record in asyncEngine)
			{
				arr.Add(record);
			}
	
			CustomersVerticalBar[] res = (CustomersVerticalBar[]) arr.ToArray(typeof (CustomersVerticalBar));
			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual(ExpectedRecords, engine.TotalRecords);
	
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