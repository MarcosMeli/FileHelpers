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
        [Ignore("Mixing properties and fields is not supported")]
        public void AutopropertiesLastIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesLastIsField>();

            Check.That(engine.Options.FieldCount).IsEqualTo(3);

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("<Tag>k__BackingField");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("<Usuario>k__BackingField");
            Check.That(engine.Options.FieldsNames[2]).IsEqualTo("Field1");
        }


        [Test]
        [Ignore("Mixing properties and fields is not supported")]
        public void AutopropertiesFirstIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesFirstIsField>();

            Check.That(engine.Options.FieldCount).IsEqualTo(3);

            Check.That(engine.Options.FieldsNames[0]).IsEqualTo("Field1");
            Check.That(engine.Options.FieldsNames[1]).IsEqualTo("<Tag>k__BackingField");
            Check.That(engine.Options.FieldsNames[2]).IsEqualTo("<Usuario>k__BackingField");
        }

        [Test]
        [Ignore("Mixing properties and fields is not supported")]
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
            public int Tag { get; set; }
            public string Usuario { get; set; }
            public string Field1;
        }

        [DelimitedRecord("|")]
        public class AutoPropertiesFirstIsField
        {
            public string Field1;
            public int Tag { get; set; }
            public string Usuario { get; set; }
        }

        [DelimitedRecord("|")]
        public class AutoPropertiesMidIsField
        {
            public int Tag { get; set; }
            public string Field1;
            public string Usuario { get; set; }
        }
    }
}