using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NFluent;

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

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("<Tag>k__BackingField");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("<Usuario>k__BackingField");
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

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("<Tag>k__BackingField");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("Field1");
            Check.That(engine.Options.FieldsNames[2]).IsEqualTo("<Usuario>k__BackingField");
        }


        [Test]
        public void AutopropertiesFirstIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesFirstIsField>();

            Check.That(engine.Options.FieldCount).IsEqualTo(3);

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("Field1");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("<Tag>k__BackingField");
            Check.That(engine.Options.FieldsNames[2]).IsEqualTo("<Usuario>k__BackingField");
        }


        [Test]
        public void AutopropertiesMidIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesMidIsField>();

            Check.That(engine.Options.FieldCount).IsEqualTo(3);

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("<Tag>k__BackingField");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("Field1");
            Check.That(engine.Options.FieldsNames[2]).IsEqualTo("<Usuario>k__BackingField");
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
    }
}