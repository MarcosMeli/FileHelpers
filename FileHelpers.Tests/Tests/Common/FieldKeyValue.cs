using NUnit.Framework;

namespace FileHelpers.Tests.Tests.Common
{
    [TestFixture]
    public class FieldKeyValue
    {
        [Test]
        public void FieldKeyValueSimpleTest()
        {
            var engine = new FileHelperEngine<FieldKeyValue1>();
            var res = TestCommon.ReadTest<FieldKeyValue1>(engine, "Good", "FieldKeyValue.txt");
            //var res = TestCommon.ReadTest<FieldKeyValue1>(engine, "Good", @"C:\Users\larz\github\FileHelpers\FileHelpers.Tests\Data\Good\FieldKeyValue.txt");

            Assert.AreEqual(res[0].ContactTitle,1);
            Assert.AreEqual(res[1].ContactTitle, 2);
            Assert.AreEqual(res[2].ContactTitle, 3);

            Assert.AreEqual(res[0].Country, "Germany");
            Assert.AreEqual(res[1].Country, "Mexico");
            Assert.AreEqual(res[2].Country, "Sweden");
        }

        //The first attribute maps string -> string and the second from string->int.
        [FixedLengthRecord]
        private class FieldKeyValue1
        {
            //[FieldKeyValue("Sales Representative","SR"), FieldKeyValue("Owner", "OW"), FieldKeyValue("Order Administrator", "OA")]
            [FieldKeyValue("Sales Representative", 1), FieldKeyValue("Owner", 2), FieldKeyValue("Order Administrator", 3)]
            [FieldFixedLength(38)]
            [FieldTrim(TrimMode.Both)]
            public int ContactTitle;

            [FieldFixedLength(15)]
            [FieldTrim(TrimMode.Both)]
            public string Country;
        }
    }
}
