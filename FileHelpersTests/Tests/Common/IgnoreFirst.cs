using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class IgnoreFirsts
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine asyncEngine;

		[Test]
		public void DiscardFirst1()
		{
			engine = new FileHelperEngine(typeof (DiscardType0));

			DiscardType0[] res = (DiscardType0[]) TestCommon.ReadTest(engine, @"Good\DiscardFirst0.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

		[Test]
		public void DiscardFirst2()
		{
			engine = new FileHelperEngine(typeof (DiscardType1));

			DiscardType1[] res = (DiscardType1[]) TestCommon.ReadTest(engine, @"Good\DiscardFirst1.txt");

            Assert.AreEqual(engine.TotalRecords, res.Length);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

		[Test]
		public void DiscardFirst3()
		{
			engine = new FileHelperEngine(typeof (DiscardType11));

			DiscardType11[] res = (DiscardType11[]) TestCommon.ReadTest(engine, @"Good\DiscardFirst1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

		[Test]
		public void DiscardFirst4()
		{
			engine = new FileHelperEngine(typeof (DiscardType2));

			DiscardType2[] res = (DiscardType2[]) TestCommon.ReadTest(engine, @"Good\DiscardFirst2.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("you can get this lines\r\nwith the FileHelperEngine.HeaderText property\r\n", engine.HeaderText);
		}

		[Test]
		public void DiscardFirst5()
		{
			engine = new FileHelperEngine(typeof (DiscardType2));

			DiscardType2[] res = (DiscardType2[]) TestCommon.ReadTest(engine, @"Good\DiscardFirst3.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

		[Test]
		public void DiscardFirst6()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (DiscardType2));

			TestCommon.BeginReadTest(asyncEngine, @"Good\DiscardFirst2.txt");

			Assert.AreEqual("you can get this lines\r\nwith the FileHelperEngine.HeaderText property\r\n", asyncEngine.HeaderText);

			DiscardType2 res = (DiscardType2) asyncEngine.ReadNext();

			Assert.AreEqual(new DateTime(1314, 12, 11), res.Field1);
		}


		[Test]
		public void DiscardWriteRead()
		{
			engine = new FileHelperEngine(typeof (DiscardType1));

			DiscardType1[] res = (DiscardType1[]) TestCommon.ReadTest(engine, @"Good\DiscardFirst1.txt");
			engine.HeaderText = "This is a new header....";

			engine.WriteFile("tempo.txt", res);
			engine.HeaderText = "none none";

			DiscardType1[] res2 = (DiscardType1[]) engine.ReadFile(@"tempo.txt");

			Assert.AreEqual(res.Length, res2.Length);
			Assert.AreEqual("This is a new header....\r\n", engine.HeaderText);

			if (File.Exists("tempo.txt")) File.Delete("tempo.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

        [Test]
        public void DiscardWriteRead2()
        {
            engine = new FileHelperEngine(typeof(DiscardType1));

            DiscardType1[] res = (DiscardType1[])TestCommon.ReadTest(engine, @"Good\DiscardFirst1.txt");

            asyncEngine = new FileHelperAsyncEngine(typeof(DiscardType1));

            asyncEngine.HeaderText = "This is a new header....";

            asyncEngine.BeginWriteFile("tempo.txt");
            asyncEngine.WriteNexts(res);
            asyncEngine.EndsWrite();

            asyncEngine.HeaderText = "none none";

            asyncEngine.BeginReadFile(@"tempo.txt");

            while (asyncEngine.ReadNext() != null)
            {}

            Assert.AreEqual(res.Length, asyncEngine.TotalRecords);
            Assert.AreEqual("This is a new header....\r\n", asyncEngine.HeaderText);

            asyncEngine.EndsRead();

            Assert.AreEqual(res.Length, asyncEngine.TotalRecords);
            Assert.AreEqual("This is a new header....\r\n", asyncEngine.HeaderText);

            if (File.Exists("tempo.txt")) File.Delete("tempo.txt");

        }



	}

	[FixedLengthRecord]
	[IgnoreFirst(0)]
	public class DiscardType0
	{
		[FieldFixedLength(8)]
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime Field1;

		[FieldFixedLength(3)] public string Field2;

		[FieldFixedLength(3)]
		[FieldConverter(ConverterKind.Int32)] public int Field3;

	}

	[FixedLengthRecord]
	[IgnoreFirst()]
	public class DiscardType1
	{
		[FieldFixedLength(8)]
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime Field1;

		[FieldFixedLength(3)] public string Field2;

		[FieldFixedLength(3)]
		[FieldConverter(ConverterKind.Int32)] public int Field3;

	}

	[FixedLengthRecord]
	[IgnoreFirst(1)]
	public class DiscardType11
	{
		[FieldFixedLength(8)]
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime Field1;

		[FieldFixedLength(3)] public string Field2;

		[FieldFixedLength(3)]
		[FieldConverter(ConverterKind.Int32)] public int Field3;

	}

	[FixedLengthRecord]
	[IgnoreFirst(2)]
	public class DiscardType2
	{
		[FieldFixedLength(8)]
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime Field1;

		[FieldFixedLength(3)] public string Field2;

		[FieldFixedLength(3)]
		[FieldConverter(ConverterKind.Int32)] public int Field3;

	}

}