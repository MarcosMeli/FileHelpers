using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
	public class FileEncoding
	{
        FileHelperEngine<CustomersVerticalBar> engine;
        FileHelperAsyncEngine<CustomersVerticalBar> asyncEngine;
	    private const string expectedTextWithNTilde = "Ana Tru\u00f1i\u00f1o Emparedados y helados";
	    private const string expectedTextWithEGrave = "Blondesddsl p\u00e8re et fils";
	    private const string expectedTextWithEAcute1 = "Fr\u00e9d\u00e9rique Citeaux";
	    private const string expectedTextWithEAcute2 = "24, place Kl\u00e9ber";
	    private const string expectedTextWithADiaeresis = "Berguvsv\u00e4gen  8";
	    private const string expectedTextWithARing = "Lule\u00e5";

	    private const int ExpectedRecords = 7;

        private void RunTests(Encoding enc, params string[] pathElements)
		{
			engine = new FileHelperEngine<CustomersVerticalBar>();
			engine.Encoding = enc;
			enc.AssertEqualTo<Encoding>(engine.Encoding, "Normal engine encoding mismatch, runtests" );
			CoreRunTest(pathElements);
		}

        private void RunConstructor(Encoding enc, params string[] pathElements)
		{
			engine = new FileHelperEngine<CustomersVerticalBar>(enc);
            enc.AssertEqualTo<Encoding>(engine.Encoding, "Normal engine encoding mismatch, runconstructor");
			CoreRunTest(pathElements);
		}

        private void CoreRunTest(params string[] pathElements)
		{

            CustomersVerticalBar[] res = TestCommon.ReadTest<CustomersVerticalBar>(engine, pathElements);
	
			ExpectedRecords.AssertEqualTo<int>( res.Length, "Length Mismatch");
			ExpectedRecords.AssertEqualTo<int>( engine.TotalRecords, "Record count mismatch");

            expectedTextWithNTilde.AssertEqualTo<String>(res[1].CompanyName);
			expectedTextWithEGrave.AssertEqualTo<String>( res[6].CompanyName);
			expectedTextWithEAcute1.AssertEqualTo<String>( res[6].ContactName);
	
			expectedTextWithEAcute2.AssertEqualTo<String>( res[6].Address);
            expectedTextWithADiaeresis.AssertEqualTo<String>(res[4].Address);
            expectedTextWithARing.AssertEqualTo<String>( res[4].City);
		}

		private void RunAsyncTests(Encoding enc, params string[] pathElements)
		{
			asyncEngine = new FileHelperAsyncEngine<CustomersVerticalBar>();
			asyncEngine.Encoding = enc;
            enc.AssertEqualTo<Encoding>(asyncEngine.Encoding, "Async engine encoding mismatch, runasyncTests");

			CoreRunAsync(pathElements);
		}

		private void RunAsyncConstructor(Encoding enc, params string[] pathElements)
		{
			asyncEngine = new FileHelperAsyncEngine<CustomersVerticalBar>(enc);
            enc.AssertEqualTo<Encoding>(asyncEngine.Encoding, "Async encoding mismatch");

			CoreRunAsync(pathElements);
		}

		private void CoreRunAsync(params string[] pathElements)
		{
			var arr = new ArrayList();
	
			TestCommon.BeginReadTest(asyncEngine, pathElements);

			foreach (object record in asyncEngine)
			{
				arr.Add(record);
			}
	
			var res = (CustomersVerticalBar[]) arr.ToArray(typeof (CustomersVerticalBar));
			ExpectedRecords.AssertEqualTo<int>( res.Length, "Length is not equal");
			ExpectedRecords.AssertEqualTo<int>( asyncEngine.TotalRecords, "Total number of records not equal");
	
			expectedTextWithNTilde.AssertEqualTo<String>( res[1].CompanyName);
            expectedTextWithEGrave.AssertEqualTo<String>(res[6].CompanyName);
			expectedTextWithEAcute1.AssertEqualTo<String>( res[6].ContactName);
	
			expectedTextWithEAcute2.AssertEqualTo<String>( res[6].Address);
			expectedTextWithADiaeresis.AssertEqualTo<String>( res[4].Address);
			expectedTextWithARing.AssertEqualTo<String>( res[4].City);
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