using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class Trimming
	{
		[Test]
		public void TrimLeft()
		{
			var engine = new FileHelperEngine<TrimClass>();

			var res = TestCommon.ReadTest<TrimClass>(engine, "Good", "Trim1.txt");

			Assert.AreEqual(7, res.Length);
			Assert.AreEqual("ALFKI      ", res[0].CustomerID);
			Assert.AreEqual("ANATR    ", res[1].CustomerID);
			Assert.AreEqual("ANTON      ", res[2].CustomerID);
			Assert.AreEqual("AROUT    ", res[3].CustomerID);
			Assert.AreEqual("BERGS    ", res[4].CustomerID);
			Assert.AreEqual("", res[5].CustomerID);
			Assert.AreEqual("BLAUS      ", res[6].CustomerID);
		}

		[Test]
		public void TrimRigth()
		{
			var engine = new FileHelperEngine<TrimClass>();

            TrimClass[] res = TestCommon.ReadTest<TrimClass>(engine, "Good", "Trim1.txt");

			Assert.AreEqual(7, res.Length);
			Assert.AreEqual("Alfreds Futterkiste", res[0].CompanyName);
			Assert.AreEqual("Ana Trujillo Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual("  Antonio Moreno Taquería", res[2].CompanyName);
			Assert.AreEqual("  Around the Horn", res[3].CompanyName);
			Assert.AreEqual("Berglunds snabbkp", res[4].CompanyName);
			Assert.AreEqual("", res[5].CompanyName);
			Assert.AreEqual(" Blauer See Delikatessen", res[6].CompanyName);
		}

		[Test]
		public void TrimBoth()
		{
			var engine = new FileHelperEngine<TrimClass>();

            var res = TestCommon.ReadTest<TrimClass>(engine, "Good", "Trim1.txt");

			Assert.AreEqual(7, res.Length);
			Assert.AreEqual("Maria Anders", res[0].ContactName);
			Assert.AreEqual("Ana Trujillo", res[1].ContactName);
			Assert.AreEqual("Antonio Moreno", res[2].ContactName);
			Assert.AreEqual("Thomas Hardy", res[3].ContactName);
			Assert.AreEqual("Christina Berglund", res[4].ContactName);
			Assert.AreEqual("", res[5].ContactName);
			Assert.AreEqual("Hanna Moos", res[6].ContactName);
		}

		[Test]
		public void TrimNone()
		{
			var engine = new FileHelperEngine<TrimClass>();

            var res = TestCommon.ReadTest<TrimClass>(engine, "Good", "Trim1.txt");

			Assert.AreEqual(7, res.Length);
			Assert.AreEqual("Sales Representative                  ", res[0].ContactTitle);
			Assert.AreEqual("   Owner                              ", res[1].ContactTitle);
			Assert.AreEqual("                             Owner    ", res[2].ContactTitle);
			Assert.AreEqual("  Sales Representative                ", res[3].ContactTitle);
			Assert.AreEqual("   Order Administrator                ", res[4].ContactTitle);
			Assert.AreEqual("                                      ", res[5].ContactTitle);
			Assert.AreEqual("     Sales Representative             ", res[6].ContactTitle);
		}

		[FixedLengthRecord]
		private class TrimClass
		{
			[FieldFixedLength(11)]
			[FieldTrim(TrimMode.Left)]
			public string CustomerID;

			[FieldFixedLength(50 - 12)]
			[FieldTrim(TrimMode.Right)]
			public string CompanyName;

			[FieldFixedLength(72 - 50)]
			[FieldTrim(TrimMode.Both)]
			public string ContactName;

			[FieldFixedLength(110 - 72)]
			[FieldTrim(TrimMode.None)]
			public string ContactTitle;
		}

	}


}