using System;
using NUnit.Framework;
using NFluent;
using System.Linq;
using FileHelpers.Converters;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class Autoproperties
    {
        /// <summary>
        /// Test an all properties class is supported
        /// </summary>
        [Test]
        public void AutopropertiesSimple()
        {
            var engine = new FileHelperEngine<AutoPropertiesSimple>();

            Check.That(engine.Options.FieldCount).IsEqualTo(2);

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("Tag");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("Usuario");
        }



        [Test]
        public void AutopropertiesLastIsFieldBad()
        {
            Assert.Throws<BadUsageException>(() =>
            {
                var engine = new FileHelperEngine<AutoPropertiesLastIsFieldBad>();
            });
        }


        [Test]
        public void AutopropertiesMixNoFieldOrder()
        {
            Assert.Throws<BadUsageException>(() =>
            {
                var engine = new FileHelperEngine<AutoPropertiesMixNoFieldOrder>();
            });
        }


        [Test]
        public void AutopropertiesLastIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesLastIsField>();

            Check.That(engine.Options.FieldCount).IsEqualTo(3);

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("Tag");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("Field1");
            Check.That(engine.Options.FieldsNames[2]).IsEqualTo("Usuario");
        }


        [Test]
        public void AutopropertiesFirstIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesFirstIsField>();

            Check.That(engine.Options.FieldCount).IsEqualTo(3);

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("Field1");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("Tag");
            Check.That(engine.Options.FieldsNames[2]).IsEqualTo("Usuario");
        }


        [Test]
        public void AutopropertiesMidIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesMidIsField>();

            Check.That(engine.Options.FieldCount).IsEqualTo(3);

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("Tag");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("Field1");
            Check.That(engine.Options.FieldsNames[2]).IsEqualTo("Usuario");
        }

        [Test]
        public void AutopropertiesFieldHidden()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields.Any(field=>field.FieldName == "Tag"))
                .IsEqualTo(false);
        }

        [Test]
        public void AutopropertiesFieldConverter()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields[1].Converter is DateTimeConverter)
                .IsEqualTo(true);
        }

        [Test]
        public void AutopropertiesFieldDelimiter()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields[2] is DelimitedField)
                .IsEqualTo(true);

            Check.That(((DelimitedField)engine.Options.Fields[2]).Separator)
                .IsEqualTo(";");
        }

        [Test]
        public void AutopropertiesFieldFixedLength()
        {
            var engine = new FileHelperEngine<AutoPropertiesFixedRecordAttribute>();

            Check.That(engine.Options.FieldCount).IsEqualTo(1);

            Check.That(engine.Options.Fields[0] is FixedLengthField)
                .IsEqualTo(true);

            Check.That(((FixedLengthField)engine.Options.Fields[0]).FieldLength)
                .IsEqualTo(10);
        }

        [Test]
        public void AutopropertiesFieldFieldAlign()
        {
            var engine = new FileHelperEngine<AutoPropertiesFixedRecordAttribute>();

            Check.That(engine.Options.FieldCount).IsEqualTo(1);

            Check.That(engine.Options.Fields[0] is FixedLengthField)
                .IsEqualTo(true);

            Check.That(((FixedLengthField)engine.Options.Fields[0]).Align.Align)
                .IsEqualTo(AlignMode.Right);
        }

        [Test]
        public void AutopropertiesFieldCaption()
        {
            var engine = new FileHelperEngine<AutoPropertiesFixedRecordAttribute>();

            Check.That(engine.Options.FieldCount).IsEqualTo(1);

            Check.That(engine.Options.Fields[0].FieldCaption)
                .IsEqualTo("Auto property Caption");
        }

        [Test]
        public void AutopropertiesFieldValueDiscarded()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields[3].Discarded)
                .IsEqualTo(true);
        }

        [Test]
        public void AutopropertiesFieldNullValue()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields[3].NullValue)
                .IsEqualTo("nobody");
        }

        [Test]
        public void AutopropertiesFieldNotEmpty()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields[3].IsNotEmpty)
                .IsEqualTo(true);
        }

        [Test]
        public void AutopropertiesFieldQuoted()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields[3] is DelimitedField)
                .IsEqualTo(true);

            Check.That(((DelimitedField)engine.Options.Fields[3]).QuoteChar)
                .IsEqualTo('\'');
        }

        [Test]
        public void AutopropertiesFieldTrim()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields[3].TrimMode)
                .IsEqualTo(TrimMode.Both);
        }



        [Test]
        public void AutopropertiesFieldOptional()
        {
            var engine = new FileHelperEngine<AutoPropertiesDelimitedRecord>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);

            Check.That(engine.Options.Fields[4].IsOptional)
                .IsEqualTo(true);
        }
        
        [DelimitedRecord("|")]
        public class AutoPropertiesSimple
        {
            public int Tag { get; set; }
            public string Usuario { get; set; }
        }

        [DelimitedRecord("|")]
        public class AutoPropertiesLastIsField
        {
            [FieldOrder(1)]
            public int Tag { get; set; }

            [FieldOrder(3)]
            public string Usuario { get; set; }

            [FieldOrder(2)]
            public string Field1;
        }



        [DelimitedRecord("|")]
        public class AutoPropertiesMixNoFieldOrder
        {
            public int Tag { get; set; }

            public string Usuario { get; set; }

            public string Field1;
        }


        [DelimitedRecord("|")]
        public class AutoPropertiesLastIsFieldBad
        {
            [FieldOrder(1)]
            public int Tag { get; set; }

            public string Usuario { get; set; }

            [FieldOrder(2)]
            public string Field1;
        }

        [DelimitedRecord("|")]
        public class AutoPropertiesFirstIsField
        {
            [FieldOrder(1)]
            public string Field1;
            [FieldOrder(2)]
            public int Tag { get; set; }
            [FieldOrder(3)]
            public string Usuario { get; set; }
        }

        [DelimitedRecord("|")]
        public class AutoPropertiesMidIsField
        {
            [FieldOrder(1)]
            public int Tag { get; set; }
            [FieldOrder(2)]
            public string Field1;
            [FieldOrder(3)]
            public string Usuario { get; set; }
        }

        [DelimitedRecord("|")]
        class AutoPropertiesDelimitedRecord
        {
            [FieldHidden()]
            public int Tag { get; set; }
            public string Usuario { get; set; }
            [DateTimeConverter]
            public DateTime DateTime { get; set; }
            [FieldDelimiter(";")]
            public string CustomDelimiter { get; set; }
            [FieldNullValue("nobody")]
            [FieldNotEmpty]
            [FieldQuoted('\'')]
            [FieldTrim(TrimMode.Both)]                                    
            [FieldValueDiscarded()]
            public string NullValue { get; set; }
            [FieldOptional()]
            public bool Optional { get; set; }            
        }

        [FixedLengthRecord]
        class AutoPropertiesFixedRecordAttribute
        {
            [FieldFixedLength(10)]
            [FieldAlign(AlignMode.Right)]
            [FieldCaption("Auto property Caption")]
            public string FixedLength { get; set; }
        }
    }
}