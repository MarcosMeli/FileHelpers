using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class IgnoreFirsts
	{

	    private readonly string mExpectedLongHeaderText = "you can get this lines" + Environment.NewLine +
	                                                     "with the FileHelperEngine.HeaderText property" + Environment.NewLine;

	    private readonly string mExpectedShortHeaderText = "This is a new header...." + Environment.NewLine;

	    [Test]
		public void DiscardFirst1()
		{
            var res = FileTest.Good.DiscardFirst0.ReadWithEngine<DiscardType0>();

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

		[Test]
		public void DiscardFirst2()
		{
            var engine = new FileHelperEngine<DiscardType1>();
            var res = engine.ReadFile(FileTest.Good.DiscardFirst1.Path);

            Assert.AreEqual(engine.TotalRecords, res.Length);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

		[Test]
		public void DiscardFirst3()
		{
            var res = FileTest.Good.DiscardFirst1.ReadWithEngine<DiscardType11>();

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

		[Test]
		public void DiscardFirst4()
		{
		    var engine = new FileHelperEngine<DiscardType2>();
		    var res = engine.ReadFile(FileTest.Good.DiscardFirst2.Path);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual(mExpectedLongHeaderText, engine.HeaderText);
		}

		[Test]
		public void DiscardFirst5()
		{
            var res = FileTest.Good.DiscardFirst3
                .ReadWithEngine<DiscardType2>();

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

		[Test]
		public void DiscardFirst6()
		{
            var engine = FileTest.Good.DiscardFirst2
                .BeginRead<DiscardType2>();

            Assert.AreEqual(mExpectedLongHeaderText, engine.HeaderText);

            var res = engine.ReadNext();

			Assert.AreEqual(new DateTime(1314, 12, 11), res.Field1);
		}

		[Test]
		public void DiscardFirst7()
		{
            var engine = FileTest.Good.DiscardFirst2
                .BeginRead<DiscardType3>();

            var res = engine.ReadNext();

			Assert.AreEqual(null, res);
		}

		[Test]
		public void DiscardFirst8()
		{
            var res = FileTest.Good.DiscardFirst2
                            .ReadWithEngine<DiscardType3>();

			Assert.AreEqual(0, res.Length);
		}

		[Test]
		public void DiscardFirst9()
		{
            var engine = FileTest.Good.DiscardFirst2
                .BeginRead<DiscardType4>();

            var res = engine.ReadNext();

			Assert.AreEqual(null, res);
		}

		[Test]
		public void DiscardFirstA()
		{
            var res = FileTest.Good.DiscardFirst2
                                     .ReadWithEngine<DiscardType4>();

			Assert.AreEqual(0, res.Length);
		}

		[Test]
		public void DiscardWriteRead()
		{
			var engine = new FileHelperEngine<DiscardType1>();

			DiscardType1[] res = engine.ReadFile(FileTest.Good.DiscardFirst1.Path);
			engine.HeaderText = "This is a new header....";

			engine.WriteFile("tempo.txt", res);
			engine.HeaderText = "none none";

			var res2 = (DiscardType1[]) engine.ReadFile(@"tempo.txt");

			Assert.AreEqual(res.Length, res2.Length);
			Assert.AreEqual(mExpectedShortHeaderText, engine.HeaderText);

			if (File.Exists("tempo.txt")) File.Delete("tempo.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
		}

        [Test]
        public void DiscardWriteRead2()
        {
            var engine = new FileHelperEngine<DiscardType1>();

            DiscardType1[] res = engine.ReadFile(FileTest.Good.DiscardFirst1.Path);
			
            var asyncEngine = new FileHelperAsyncEngine<DiscardType1>();

            asyncEngine.HeaderText = "This is a new header....";

            asyncEngine.BeginWriteFile("tempo.txt");
            asyncEngine.WriteNexts(res);
            asyncEngine.Close();

            asyncEngine.HeaderText = "none none\r\n";

            asyncEngine.BeginReadFile(@"tempo.txt");

            while (asyncEngine.ReadNext() != null)
            {}

            Assert.AreEqual(res.Length, asyncEngine.TotalRecords);
            Assert.AreEqual(mExpectedShortHeaderText, asyncEngine.HeaderText);

            asyncEngine.Close();

            Assert.AreEqual(res.Length, asyncEngine.TotalRecords);
            Assert.AreEqual(mExpectedShortHeaderText, asyncEngine.HeaderText);

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

	
	[FixedLengthRecord]
	[IgnoreFirst(800000)]
	[IgnoreLast()]
	public class DiscardType3
	{
		[FieldFixedLength(8)]
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime Field1;

		[FieldFixedLength(3)] public string Field2;

		[FieldFixedLength(3)]
		[FieldConverter(ConverterKind.Int32)] public int Field3;
	}

	[FixedLengthRecord]
	[IgnoreLast(38000)]
	public class DiscardType4
	{
		[FieldFixedLength(8)]
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime Field1;

		[FieldFixedLength(3)] public string Field2;

		[FieldFixedLength(3)]
		[FieldConverter(ConverterKind.Int32)] public int Field3;
	}

}