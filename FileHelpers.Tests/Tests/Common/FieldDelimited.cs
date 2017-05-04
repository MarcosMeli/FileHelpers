using System;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class FieldDelimited
    {
        [Test]
        public void CustomDelimiter()
        {
            var engine = new FileHelperEngine<SOXLog>();

            SOXLog[] res = TestCommon.ReadTest<SOXLog>(engine, "Good", "FieldHiddenAdvanced.txt");
            Assert.AreEqual(5, res.Length);

            Assert.AreEqual(ActionEnum.Deleted, res[0].ActionType);
            Assert.AreEqual(ActionEnum.Created, res[2].ActionType);
            Assert.AreEqual("6/3/2006 5:18:18 AM", res[0].TimeStamp);
        }


        [DelimitedRecord(" - ")]
        private class SOXLog
        {
            [FieldDelimiter(": ")]
            internal String DummyField;

            public ActionEnum ActionType;
            public String TimeStamp;
            public String FileName;
        }

        /// <summary> 
        /// Enumeration of the types of Actions permitted. 
        /// </summary> 
        private enum ActionEnum
        {
            Created,
            Deleted,
            Changed
        }

        // TEST CLASS
        [DelimitedRecord("|")]
        public class AnotherDelimiterType
        {
            [FieldDelimiter(" - ")]
            public string Street;

            [FieldDelimiter(",")]
            public string Number;

            [FieldTrim(TrimMode.Both)]
            public string City;

            public int Age;
        }

        [Test]
        public void AnotherDelimiterTest()
        {
            var engine = new FileHelperEngine<AnotherDelimiterType>();

            var res = TestCommon.ReadTest<AnotherDelimiterType>(engine, "Good", "CustomConverter2.txt");

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual("Bahia Blanca", res[0].City);
            Assert.AreEqual("Sin Nombre", res[0].Street);
            Assert.AreEqual("13", res[0].Number);

            Assert.AreEqual("Saavedra", res[1].City);
            Assert.AreEqual("Florencio Sanches", res[1].Street);
            Assert.AreEqual("s/n", res[1].Number);

            Assert.AreEqual("Bs.As", res[2].City);
            Assert.AreEqual("12 de Octubre", res[2].Street);
            Assert.AreEqual("4", res[2].Number);

            Assert.AreEqual("Chilesito", res[3].City);
            Assert.AreEqual("Pololo", res[3].Street);
            Assert.AreEqual("5421", res[3].Number);
        }
    }
}