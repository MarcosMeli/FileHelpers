using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class InterfaceEventsTests
	{
		[Test]
		public void ReadEvents()
		{
			var engine = new FileHelperEngine<SampleType>();
			var res = engine.ReadFile(FileTest.Good.Test1.Path);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);

			Assert.AreEqual(true, res[0].AfterReadNotif);
			Assert.AreEqual(true, res[1].AfterReadNotif);
			Assert.AreEqual(true, res[2].AfterReadNotif);
			Assert.AreEqual(true, res[3].AfterReadNotif);
		}
		

		[Test]
		public void WriteEvents()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteString(res);

			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(true, res[0].BeforeWriteNotif);
			Assert.AreEqual(true, res[1].BeforeWriteNotif);

		}

		[FixedLengthRecord]
		private class SampleType: INotifyRead, INotifyWrite
		{
			[FieldFixedLength(8)]
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
			public DateTime Field1;

			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Left, ' ')]
			[FieldTrim(TrimMode.Both)]
			public string Field2;

			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Right, '0')]
			[FieldTrim(TrimMode.Both)]
			public int Field3;

			[FieldIgnored]
			public bool AfterReadNotif = false;
			[FieldIgnored]
			public bool BeforeWriteNotif = false;

			public void AfterRead(EngineBase engine, string line)
			{
				AfterReadNotif = true;
			}

			public void BeforeWrite(EngineBase engine)
			{
				BeforeWriteNotif = true;
			}
		}

	}
}