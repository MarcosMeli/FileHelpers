using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class StringHelpers
	{
		[Test]
		public void RemoveBlanks()
		{
			Assert.AreEqual("123 ", StringHelper.RemoveBlanks(" 123 "));
			Assert.AreEqual("1  2  3", StringHelper.RemoveBlanks("		1  2  3"));
			Assert.AreEqual("123", StringHelper.RemoveBlanks("123"));
			Assert.AreEqual("-123", StringHelper.RemoveBlanks("  -  123"));

			Assert.AreEqual("123", StringHelper.RemoveBlanks("\t123"));
			Assert.AreEqual("123", StringHelper.RemoveBlanks("\t\n123"));
		}
	}

}