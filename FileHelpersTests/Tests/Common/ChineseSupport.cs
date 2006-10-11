using System;
using System.Data;
using System.IO;
using System.Text;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class ChineseSupport
	{
		FileHelperEngine engine;

		[Test]
		public void ReadString()
		{
			engine = new FileHelperEngine(typeof (ChineseTest));

			ChineseTest[] res;
			//engine.Encoding = Encoding.Unicode;
			res = (ChineseTest[]) engine.ReadString(@"A123456789台北市民權東東路3號           20061008
A987654321台北市民權東東路5號           20061008");

			Assert.AreEqual(2, res.Length);
			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(2006, 10, 08), res[0].fecha);
			Assert.AreEqual("台北市民權東東路3號           ", res[0].chino);
			Assert.AreEqual("A123456789", res[0].id);
		}

		[Test]
		public void ReadFile()
		{
			engine = new FileHelperEngine(typeof (ChineseTest));

			ChineseTest[] res;
			//engine.Encoding = Encoding.Unicode;
			res = (ChineseTest[]) Common.ReadTest(engine, @"Good\Chinese.txt");

			Assert.AreEqual(2, res.Length);
			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(2006, 10, 08), res[0].fecha);
			Assert.AreEqual("台北市民權東東路3號           ", res[0].chino);
			Assert.AreEqual("A123456789", res[0].id);
		}


		[Test]
		public void ReadFileUnicode()
		{
			engine = new FileHelperEngine(typeof (ChineseTest));

			ChineseTest[] res;
			engine.Encoding = Encoding.Unicode;
			res = (ChineseTest[]) Common.ReadTest(engine, @"Good\ChineseUnicode.txt");

			Assert.AreEqual(2, res.Length);
			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(2006, 10, 08), res[0].fecha);
			Assert.AreEqual("台北市民權東東路3號           ", res[0].chino);
			Assert.AreEqual("A123456789", res[0].id);
		}

		[FixedLengthRecord(FixedMode.ExactLength)]
		private class ChineseTest
		{
			[FieldFixedLength(10)]
			public string id;
			[FieldFixedLength(30)]
			public string chino;

			[FieldFixedLength(8)]
			[FieldConverter(ConverterKind.Date, "yyyyMMdd")]
			public DateTime fecha;

		}


	}
}