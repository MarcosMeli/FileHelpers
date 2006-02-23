using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class QuotedStrings
	{
		[Test]
		public void Test1()
		{
			int index;
			string res = StringHelper.ExtractQuotedString("\"WorkFine\"", '\"', out index);

			Assert.AreEqual("WorkFine", res);
		}

		[Test]
		public void Test2()
		{
			int index;
			string res = StringHelper.ExtractQuotedString("\"Work\"\"Fine\"", '\"', out index);

			Assert.AreEqual("Work\"Fine", res);
		}

		[Test]
		public void Test3()
		{
			int index;
			string res = StringHelper.ExtractQuotedString("\"Work\"\"\"\"Fine\"", '\"', out index);

			Assert.AreEqual("Work\"\"Fine", res);
		}

		[Test]
		[ExpectedException(typeof (QuotedStringException))]
		public void Test4()
		{
			int index;
			StringHelper.ExtractQuotedString("\"Work Fine", '\"', out index);
		}

		[Test]
		[ExpectedException(typeof (QuotedStringException))]
		public void Test5()
		{
			int index;
			StringHelper.ExtractQuotedString("Work Fine\"", '\"', out index);
		}

		[Test]
		[ExpectedException(typeof (QuotedStringException))]
		public void Test6()
		{
			int index;
			StringHelper.ExtractQuotedString(null, '\"', out index);
		}

		[Test]
		[ExpectedException(typeof (QuotedStringException))]
		public void Test7()
		{
			int index;
			StringHelper.ExtractQuotedString("", '\"', out index);
		}

	}
}