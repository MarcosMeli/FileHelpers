using System;
using System.Collections;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class Autoproperties
    {
        [Test]
        [Ignore]
        public void AutopropertiesSimple()
        {
            var engine = new FileHelperEngine<AutoPropertiesSimple>();

            engine.Options.FieldCount.AssertEqualTo(2);

            engine.Options.FieldsNames[0].AssertEqualTo("<Tag>k__BackingField");
            engine.Options.FieldsNames[1].AssertEqualTo("<Usuario>k__BackingField");
        }


        [Test]
        [Ignore]
        public void AutopropertiesLastIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesLastIsField>();

            engine.Options.FieldCount.AssertEqualTo(3);

            engine.Options.FieldsNames[0].AssertEqualTo("<Tag>k__BackingField");
            engine.Options.FieldsNames[1].AssertEqualTo("<Usuario>k__BackingField");
            engine.Options.FieldsNames[2].AssertEqualTo("Field1");
        }



        [Test]
        [Ignore]
        public void AutopropertiesFirstIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesFirstIsField>();

            engine.Options.FieldCount.AssertEqualTo(3);

            engine.Options.FieldsNames[0].AssertEqualTo("Field1");
            engine.Options.FieldsNames[1].AssertEqualTo("<Tag>k__BackingField");
            engine.Options.FieldsNames[2].AssertEqualTo("<Usuario>k__BackingField");
        }

        [Test]
        [Ignore]
        public void AutopropertiesMidIsField()
        {
            var engine = new FileHelperEngine<AutoPropertiesMidIsField>();

            engine.Options.FieldCount.AssertEqualTo(3);

            engine.Options.FieldsNames[0].AssertEqualTo("<Tag>k__BackingField");
            engine.Options.FieldsNames[1].AssertEqualTo("Field1");
            engine.Options.FieldsNames[2].AssertEqualTo("<Usuario>k__BackingField");
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