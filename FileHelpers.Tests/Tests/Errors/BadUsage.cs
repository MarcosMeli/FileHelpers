using System;
using System.IO;
using System.Reflection;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Errors
{
	[TestFixture]
	public class BadUsage
	{
		#region  "  DuplicateAttributes  "

		[FixedLengthRecord]
		public class TestDupli
		{
			[FieldFixedLength(10)]
			[FieldDelimiter("|")] public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void DuplicatedDefinition()
		{
			new FileHelperEngine(typeof (TestDupli));
		}

		#endregion

		#region  "  SwitchedAttributes "

		[FixedLengthRecord]
		public class SwitchedAttributes1
		{
			[FieldDelimiter("|")] public string Field1;
		}

		[DelimitedRecord("|")]
		public class SwitchedAttributes2
		{
			[FieldFixedLength(12)] public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void SwitchedAttb1()
		{
			new FileHelperEngine(typeof (SwitchedAttributes1));
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void SwitchedAttb2()
		{
			new FileHelperEngine(typeof (SwitchedAttributes2));
		}

		#endregion

		#region  "  NullValue "

		[DelimitedRecord("|")]
		public class NullValue1Type
		{
			[FieldNullValue(22)] public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NullValue1()
		{
			new FileHelperEngine(typeof (NullValue1Type));
		}

		#endregion

		#region  "  NoMarkedClass "

		public class NoMarkedClass
		{
			[FieldNullValue(22)] public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoMarked()
		{
			new FileHelperEngine(typeof (NoMarkedClass));
		}

		#endregion

		#region  "  FixedWithOutLength "

		[FixedLengthRecord]
		public class FixedWithOutLengthClass
		{
			public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void FixedWithOutLength()
		{
			new FileHelperEngine(typeof (FixedWithOutLengthClass)); ;
		}

		#endregion

		#region  "  NoFields "

		[FixedLengthRecord]
		public class NoFieldsClass
		{
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoFields()
		{
			new FileHelperEngine(typeof (NoFieldsClass));
		}

		#endregion

		#region  "  NoFields2"

		[DelimitedRecord(",")]
		public class NoFieldsClass2
		{
			[FieldIgnored]
			public string MyField;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoFields2()
		{
			new FileHelperEngine(typeof (NoFieldsClass2));
		}

		#endregion

		#region  "  NoFields3  "

		[DelimitedRecord(",")]
			public class NoFieldsClass3
		{
			[FieldIgnored]
			public string MyField;

			[FieldIgnored]
			public string MyField2;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoFields3()
		{
			new FileHelperEngine(typeof (NoFieldsClass3));
		}

		#endregion


		#region  "  NoConstructor  "

		[FixedLengthRecord]
		public class NoConstructorClass
		{
			[FieldFixedLength(22)] public string Field1;

			public NoConstructorClass(bool foo)
			{
				foo = true;
			}
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoConstructor()
		{
			new FileHelperEngine(typeof (NoConstructorClass));
		}

		#endregion


		#region  "  NoConstructorConverter  "

		private class ConvClass: ConverterBase
		{
			public ConvClass(bool foo)
			{}

			public override object StringToField(string from)
			{
				throw new NotImplementedException();
			}
		}


		[DelimitedRecord(",")]
		public class NoConstructorConvClass
		{
			[FieldConverter(typeof(ConvClass))]
			public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoConstructorConverter()
		{
			new FileHelperEngine(typeof (NoConstructorConvClass));
		}


		[DelimitedRecord(",")]
		public class NoConstructorConvClass2
		{
			[FieldConverter(typeof(ConvClass), "hola")]
			public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoConstructorConverter2()
		{
			new FileHelperEngine(typeof (NoConstructorConvClass2));
		}


		[DelimitedRecord(",")]
		public class NoConstructorConvClass3
		{
			[FieldConverter(typeof(ConvClass), 123)]
			public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoConstructorConverter3()
		{
			new FileHelperEngine(typeof (NoConstructorConvClass3));
		}

		[DelimitedRecord(",")]
		public class NoConstructorConvClass4
		{
			[FieldConverter(typeof(FakeConverter), 123)]
			public string Field1;

			private class FakeConverter
			{}

		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NoConstructorConverter4()
		{
			new FileHelperEngine(typeof (NoConstructorConvClass4));
		}

		#endregion

		#region  "  DateFormat  "

		[DelimitedRecord("|")]
		public class DateFormat1Class
		{
			[FieldConverter(ConverterKind.Date, null)] public DateTime DateField;
		}

		[DelimitedRecord("|")]
		public class DateFormat2Class
		{
			[FieldConverter(ConverterKind.Date, "")] public DateTime DateField;
		}

		[DelimitedRecord("|")]
		public class DateFormat3Class
		{
			[FieldConverter(ConverterKind.Date, "d€€#|||??¡¡3&&...dddMMyyyy")] public DateTime DateField;
		}


		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadDateFormat1()
		{
			new FileHelperEngine(typeof (DateFormat1Class));
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadDateFormat2()
		{
			new FileHelperEngine(typeof (DateFormat2Class));
		}

//		[Test]
//		[ExpectedException(typeof (BadUsageException))]
//		public void BadDateFormat3()
//		{
//			new FileHelperEngine(typeof (DateFormat3Class));
//		}

		#endregion

//		#region  "  TrimBad  "
//
//		[DelimitedRecord("|")]
//		public class TrimClass
//		{
//			[Trim(TrimMode.Both)]
//			public int Field1;
//		}
//
//		[Test]
//		[ExpectedException(typeof (BadUsageException))]
//		public void TrimOtherThanString()
//		{
//			new FileHelperEngine(typeof (TrimClass));
//		}
//
//		#endregion

		#region  "  AlignBad  "

		[DelimitedRecord("|")]
		public class AlignClass
		{
			[FieldAlign(AlignMode.Left)] public int Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void AlignError()
		{
			new FileHelperEngine(typeof (AlignClass));
		}

		#endregion

		#region  "  NonSystemType  "

		[DelimitedRecord("|")]
		public class NonSystemTypeClass
		{
			// One non system type ex FileInfo
			public FileInfo Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NonSystemType()
		{
			new FileHelperEngine(typeof (NonSystemTypeClass));
		}

		#endregion

		#region  "  ValueType  "

		public struct ValueTypeClass
		{
			// One non system type ex FileInfo
			public FileInfo Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void ValueType()
		{
			new FileHelperEngine(typeof (ValueTypeClass));
		}

		#endregion


		#region  "  BadRecordTypes  "

		[DelimitedRecord("|")]
		public class BadRecordTypeClass
		{
			public string Field1;
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadRecordType()
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (BadRecordTypeClass));
			string res = engine.WriteString(new string[] {"hola"});
			Console.Write(res);
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void NullRecordType()
		{
			new FileHelperEngine(null);
		}

		#endregion

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void WriteBadUsage()
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			engine.WriteString(res);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void WriteBadUsage2()
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (SampleType));
			engine.WriteString(null);
		}


	}
}