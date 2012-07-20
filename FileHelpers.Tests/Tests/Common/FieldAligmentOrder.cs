using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class FieldAligmentOrder
	{
        AlignClass2[] res;

		private void RunAlignTest()
		{
			var engine = new FileHelperEngine<AlignClass>();
			var resTemp = TestCommon.ReadTest<AlignClass>(engine, "Good", "Trim1.txt");

			string tmp = engine.WriteString(resTemp);

			var engine2 = new FileHelperEngine<AlignClass2>();
			res = engine2.ReadString(tmp);
		}

		[Test]
		public void AlignLeft()
		{
			RunAlignTest();

			Assert.AreEqual(7, res.Length);
			Assert.AreEqual("ALFKI      ", res[0].CustomerID);
			Assert.AreEqual("ANATR      ", res[1].CustomerID);
			Assert.AreEqual("ANTON      ", res[2].CustomerID);
			Assert.AreEqual("AROUT      ", res[3].CustomerID);
			Assert.AreEqual("BERGS      ", res[4].CustomerID);
			Assert.AreEqual("           ", res[5].CustomerID);
			Assert.AreEqual("BLAUS      ", res[6].CustomerID);
		}


		[Test]
		public void AlignRight()
		{
			RunAlignTest();

			Assert.AreEqual(7, res.Length);
			Assert.AreEqual("                   Alfreds Futterkiste", res[0].CompanyName);
			Assert.AreEqual("    Ana Trujillo Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual("               Antonio Moreno Taquería", res[2].CompanyName);
			Assert.AreEqual("                       Around the Horn", res[3].CompanyName);
			Assert.AreEqual("                     Berglunds snabbkp", res[4].CompanyName);
			Assert.AreEqual("                                      ", res[5].CompanyName);
			Assert.AreEqual("               Blauer See Delikatessen", res[6].CompanyName);
		}

		[Test]
		public void AlignCenter()
		{
			RunAlignTest();

			Assert.AreEqual(7, res.Length);
			Assert.AreEqual("     Maria Anders     ", res[0].ContactName);
			Assert.AreEqual("     Ana Trujillo     ", res[1].ContactName);
			Assert.AreEqual("    Antonio Moreno    ", res[2].ContactName);
			Assert.AreEqual("     Thomas Hardy     ", res[3].ContactName);
			Assert.AreEqual("  Christina Berglund  ", res[4].ContactName);
			Assert.AreEqual("                      ", res[5].ContactName);
			Assert.AreEqual("      Hanna Moos      ", res[6].ContactName);
		}


		[FixedLengthRecord]
		private class AlignClass
		{
			[FieldFixedLength(11)]
			[FieldTrim(TrimMode.Both)]
			[FieldAlign(AlignMode.Left)] public string CustomerID;

			[FieldFixedLength(50 - 12)]
			[FieldTrim(TrimMode.Both)]
			[FieldAlign(AlignMode.Right)] public string CompanyName;

			[FieldFixedLength(72 - 50)]
			[FieldTrim(TrimMode.Both)]
			[FieldAlign(AlignMode.Center)] public string ContactName;

			[FieldFixedLength(110 - 72)]
			[FieldTrim(TrimMode.Both)] public string ContactTitle;
		}

		[FixedLengthRecord]
		private class AlignClass2
		{
			[FieldFixedLength(11)] public string CustomerID;

			[FieldFixedLength(50 - 12)] public string CompanyName;

			[FieldFixedLength(72 - 50)] public string ContactName;

			[FieldFixedLength(110 - 72)] public string ContactTitle;
		}
	}
}