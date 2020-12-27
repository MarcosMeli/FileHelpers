using System;
using System.IO;
using FileHelpers.Converters;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class BadUsage
    {
        #region  "  DuplicateAttributes  "

        [FixedLengthRecord]
        public class TestDupli
        {
            [FieldFixedLength(10)]
            [FieldDelimiter("|")]
            public string Field1;
        }

        [Test]
        public void DuplicatedDefinition()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<TestDupli>());
        }

        #endregion

        #region  "  SwitchedAttributes "

        [FixedLengthRecord]
        public class SwitchedAttributes1
        {
            [FieldDelimiter("|")]
            public string Field1;
        }

        [DelimitedRecord("|")]
        public class SwitchedAttributes2
        {
            [FieldFixedLength(12)]
            public string Field1;
        }

        [Test]
        public void SwitchedAttb1()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<SwitchedAttributes1>());
        }

        [Test]
        public void SwitchedAttb2()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<SwitchedAttributes2>());
        }

        #endregion

        #region  "  NullValue "

        [DelimitedRecord("|")]
        public class NullValue1Type
        {
            [FieldNullValue(22)]
            public string Field1;
        }

        [Test]
        public void NullValue1()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NullValue1Type>());
        }

        #endregion

        #region  "  NoMarkedClass "

        public class NoMarkedClass
        {
            [FieldNullValue(22)]
            public string Field1;
        }

        [Test]
        public void NoMarked()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NoMarkedClass>());
        }

        #endregion

        #region  "  FixedWithOutLength "

        [FixedLengthRecord]
        public class FixedWithOutLengthClass
        {
            public string Field1;
        }

        [Test]
        public void FixedWithOutLength()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<FixedWithOutLengthClass>());
        }

        #endregion

        #region  "  NoFields "

        [FixedLengthRecord]
        public class NoFieldsClass {}

        [Test]
        public void NoFields()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NoFieldsClass>());
        }

        #endregion

        #region  "  NoFields2"

        [DelimitedRecord(",")] 
        public class NoFieldsClass2
        {
            [FieldHidden]
            public string MyField;
        }

        [Test]
        public void NoFields2()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NoFieldsClass2>());
        }

        [DelimitedRecord(",")]
        public class NoFieldsClass2Obsoletes1
        {
#pragma warning disable 618
            [FieldNotInFile]
#pragma warning restore 618
            public string MyField;
        }

        [Test]
        public void NoFields2Obsoletes1()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NoFieldsClass2Obsoletes1>());
        }

        [DelimitedRecord(",")]
        public class NoFieldsClass2Obsoletes2
        {
#pragma warning disable 618
            [FieldIgnored]
#pragma warning restore 618
            public string MyField;
        }

        [Test]
        public void NoFields2Obsoletes2()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NoFieldsClass2Obsoletes2>());
        }

        #endregion

        #region  "  NoFields3  "

        [DelimitedRecord(",")]
        public class NoFieldsClass3
        {
            [FieldHidden]
            public string MyField;

            [FieldHidden]
            public string MyField2;
        }

        [Test]
        public void NoFields3()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NoFieldsClass3>());
        }

        #endregion

        #region  "  NoConstructor  "

        [FixedLengthRecord]
        public class NoConstructorClass
        {
            [FieldFixedLength(22)]
            public string Field1;

            public NoConstructorClass(bool foo)
            {
                foo = true;
            }
        }

        [Test]
        public void NoConstructor()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NoConstructorClass>());
        }

        #endregion

        #region  "  NoConstructorConverter  "

        private class ConvClass : ConverterBase
        {
            public ConvClass(bool foo) {}

            public override object StringToField(string from)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region  "  DateFormat  "

        [DelimitedRecord("|")]
        public class DateFormat1Class
        {
            [DateTimeConverter(null)]
            public DateTime DateField;
        }

        [DelimitedRecord("|")]
        public class DateFormat2Class
        {
            [DateTimeConverter("")]
            public DateTime DateField;
        }

        [DelimitedRecord("|")]
        public class DateFormat3Class
        {
            [DateTimeConverter("d€€#|||??¡¡3&&...dddMMyyyy")]
            public DateTime DateField;
        }


        [Test]
        public void BadDateFormat1()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<DateFormat1Class>());
        }

        [Test]
        public void BadDateFormat2()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<DateFormat2Class>());
        }

//		[Test]
//		public void BadDateFormat3()
//		{
        //Assert.Throws<BadUsageException>(() 
        //        => new FileHelperEngine(typeof (DateFormat3Class));
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
            [FieldAlign(AlignMode.Left)]
            public int Field1;
        }

        [Test]
        public void AlignError()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<AlignClass>());
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
        public void NonSystemType()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<NonSystemTypeClass>());
        }

        #endregion

        #region  "  ValueType  "

        public struct ValueTypeClass
        {
            // One non system type ex FileInfo
            public FileInfo Field1;
        }

        [Test]
        public void ValueType()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof (ValueTypeClass)));
        }

        #endregion

        #region  "  BadRecordTypes  "

        [DelimitedRecord("|")]
        public class BadRecordTypeClass
        {
            public string Field1;
        }

        [Test]
        public void BadRecordType()
        {
            var engine = new FileHelperEngine(typeof (BadRecordTypeClass));
            Assert.Throws<BadUsageException>(()
                => engine.WriteString(new[] {"hola"}));
        }

        [Test]
        public void NullRecordType()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine((Type) null));
        }

        #endregion

        [Test]
        public void WriteBadUsage()
        {
            var engine = new FileHelperEngine<SampleType>();

            var res = new SampleType[2];

            res[0] = new SampleType();

            res[0].Field1 = DateTime.Now.AddDays(1);
            res[0].Field2 = "je";
            res[0].Field3 = 0;

            Assert.Throws<BadUsageException>(()
                => engine.WriteString(res));
        }

        [Test]
        public void WriteBadUsage2()
        {
            var engine = new FileHelperEngine<SampleType>();
            Assert.Throws<ArgumentNullException>(()
                => engine.WriteString(null));
        }
    }
}