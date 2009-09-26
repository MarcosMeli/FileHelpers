using System;
using System.Collections;
using System.Text;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class FileEncoding
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine asyncEngine;

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
	
			Assert.AreEqual("Ana Truñiño Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual("Blondesddsl père et fils", res[6].CompanyName);
			Assert.AreEqual("Frédérique Citeaux", res[6].ContactName);
	
			Assert.AreEqual("24, place Kléber", res[6].Address);
			Assert.AreEqual("Berguvsvägen  8", res[4].Address);
			Assert.AreEqual("Luleå", res[4].City);
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
	
			Assert.AreEqual("Ana Truñiño Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual("Blondesddsl père et fils", res[6].CompanyName);
			Assert.AreEqual("Frédérique Citeaux", res[6].ContactName);
	
			Assert.AreEqual("24, place Kléber", res[6].Address);
			Assert.AreEqual("Berguvsvägen  8", res[4].Address);
			Assert.AreEqual("Luleå", res[4].City);
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