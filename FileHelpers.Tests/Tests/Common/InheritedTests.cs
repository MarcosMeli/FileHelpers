using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;
using System.Collections;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class InheritedTests
	{
		FileHelperEngine engine;

		[Test]
		public void Inherited1()
		{
			engine = new FileHelperEngine(typeof (SampleInheritType));

			SampleInheritType[] res;
			res = (SampleInheritType[]) Common.ReadTest(engine, @"Good\test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}

		[Test]
		public void InheritedEmpty()
		{
			engine = new FileHelperEngine(typeof (SampleInheritEmpty));

			SampleInheritEmpty[] res;
			res = (SampleInheritEmpty[]) Common.ReadTest(engine, @"Good\test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}




		[Test]
		public void Inherited2()
		{
			engine = new FileHelperEngine(typeof (DelimitedSampleInheritType));


		}

		[Test]
		public void InheritedEmptyDelimited()
		{
			engine = new FileHelperEngine(typeof (DelimitedSampleInheritEmpty));
		}


		[FixedLengthRecord]
			public class SampleBase
		{
			[FieldFixedLength(8)]
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
			public DateTime Field1;

			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Left, ' ')]
			[FieldTrim(TrimMode.Both)]
			public string Field2;

		}

		[FixedLengthRecord]
		public class SampleInheritType
			: SampleBase
		{
			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Right, '0')]
			[FieldTrim(TrimMode.Both)]
			public int Field3;
		}

		[FixedLengthRecord]
		public class SampleInheritEmpty
		: SampleInheritType
		{
			[FieldIgnored]
			public int Field5854;
		}

		




		[FixedLengthRecord]
			public class DelimitedSampleBase
		{
			[FieldFixedLength(8)]
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
			public DateTime Field1;

			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Left, ' ')]
			[FieldTrim(TrimMode.Both)]
			public string Field2;

		}

		[FixedLengthRecord]
			public class DelimitedSampleInheritType
			: DelimitedSampleBase
		{
			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Right, '0')]
			[FieldTrim(TrimMode.Both)]
			public int Field3;
		}

		[FixedLengthRecord]
			public class DelimitedSampleInheritEmpty
			: DelimitedSampleInheritType
		{
			[FieldIgnored]
			public int Field5854;
		}

	}
}