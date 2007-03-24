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

		private void RunTests(string fileName, Encoding enc)
		{
			engine = new FileHelperEngine(typeof (CustomersVerticalBar));
			engine.Encoding = enc;
			Assert.AreEqual(enc, engine.Encoding);
			CoreRunTest(fileName);
		}

		private void RunConstructor(string fileName, Encoding enc)
		{
			engine = new FileHelperEngine(typeof (CustomersVerticalBar), enc);
			Assert.AreEqual(enc, engine.Encoding);
			CoreRunTest(fileName);
		}

		private void CoreRunTest(string fileName)
		{
	
			CustomersVerticalBar[] res = (CustomersVerticalBar[]) Common.ReadTest(engine, fileName);
	
			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual(ExpectedRecords, engine.TotalRecords);
	
			Assert.AreEqual("Ana Truñiño Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual("Blondesddsl père et fils", res[6].CompanyName);
			Assert.AreEqual("Frédérique Citeaux", res[6].ContactName);
	
			Assert.AreEqual("24, place Kléber", res[6].Address);
			Assert.AreEqual("Berguvsvägen  8", res[4].Address);
			Assert.AreEqual("Luleå", res[4].City);
		}

		private void RunAsyncTests(string fileName, Encoding enc)
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (CustomersVerticalBar));
			asyncEngine.Encoding = enc;
			Assert.AreEqual(enc, asyncEngine.Encoding);

			CoreRunAsync(fileName);
		}

		private void RunAsyncConstructor(string fileName, Encoding enc)
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (CustomersVerticalBar), enc);
			Assert.AreEqual(enc, asyncEngine.Encoding);

			CoreRunAsync(fileName);
		}

		private void CoreRunAsync(string fileName)
		{
			ArrayList arr = new ArrayList();
	
			Common.BeginReadTest(asyncEngine, fileName);

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
			RunTests(@"Good\EncodingANSI.txt", Encoding.Default);
		}

		[Test]
		public void EncodingUTF8()
		{
			RunTests(@"Good\EncodingUTF8.txt", Encoding.UTF8);
		}

		[Test]
		public void EncodingUnicode()
		{
			RunTests(@"Good\EncodingUnicode.txt", Encoding.Unicode);
		}

		[Test]
		public void EncodingAsyncUnicodeBig()
		{
			RunAsyncTests(@"Good\EncodingUnicodeBig.txt", Encoding.BigEndianUnicode);
		}

		[Test]
		public void EncodingAsyncANSI()
		{
			RunAsyncTests(@"Good\EncodingANSI.txt", Encoding.Default);
		}

		[Test]
		public void EncodingAsyncUTF8()
		{
			RunAsyncTests(@"Good\EncodingUTF8.txt", Encoding.UTF8);
		}

		[Test]
		public void EncodingAsyncUnicode()
		{
			RunAsyncTests(@"Good\EncodingUnicode.txt", Encoding.Unicode);
		}

		[Test]
		public void EncodingUnicodeBig()
		{
			RunTests(@"Good\EncodingUnicodeBig.txt", Encoding.BigEndianUnicode);
		}


		[Test]
		public void EncodingAsyncANSIConstructor()
		{
			RunConstructor(@"Good\EncodingANSI.txt", Encoding.Default);
		}

		[Test]
		public void EncodingAsyncUTFConstructor()
		{
			RunConstructor(@"Good\EncodingUTF8.txt", Encoding.UTF8);
		}

		[Test]
		public void EncodingAsyncUnicodeConstructor()
		{
			RunConstructor(@"Good\EncodingUnicode.txt", Encoding.Unicode);
		}

		[Test]
		public void EncodingANSIConstructorAsync()
		{
			RunAsyncConstructor(@"Good\EncodingANSI.txt", Encoding.Default);
		}

		[Test]
		public void EncodingUTF8ConstructorAsync()
		{
			RunAsyncConstructor(@"Good\EncodingUTF8.txt", Encoding.UTF8);
		}

		[Test]
		public void EncodingUnicodeConstructorAsync()
		{
			RunAsyncConstructor(@"Good\EncodingUnicode.txt", Encoding.Unicode);
		}

	}
}