using FileHelpers.Converters;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    // SPECIAL FIELD
    public class AddressField
    {
        public string Street;
        public string Number;
        public string City;

        public override string ToString()
        {
            return Street + " - " + Number + ", " + City;
        }
    }

    // CUSTOM CONVERTER
    public class AddressConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            string[] splited1 = from.Split(',');
            string[] splited2 = splited1[0].Split('-');

            var res = new AddressField();
            res.Street = splited2[0].Trim();
            res.Number = splited2[1].Trim();
            res.City = splited1[1].Trim();

            return res;
        }
    }

    // CUSTOM CONVERTER
    public class AddressConverter2 : ConverterBase
    {
        private readonly char mSep1;
        private readonly char mSep2;

        public AddressConverter2(string sep1, string sep2)
        {
            mSep1 = sep1[0];
            mSep2 = sep2[0];
        }

        public override object StringToField(string from)
        {
            string[] splited1 = from.Split(mSep1);
            string[] splited2 = splited1[0].Split(mSep2);

            var res = new AddressField();
            res.Street = splited2[0].Trim();
            res.Number = splited2[1].Trim();
            res.City = splited1[1].Trim();

            return res;
        }
    }


    // TEST CLASS
    [DelimitedRecord("|")]
    public class AddressConvClass
    {
        [FieldConverter(typeof (AddressConverter))]
        public AddressField Address;

        public int Age;
    }

    // TEST CLASS
    [DelimitedRecord("|")]
    public class AddressConvClass2
    {
        [FieldConverter(typeof (AddressConverter2), ",", "-")]
        public AddressField Address;

        public int Age;
    }

    // NUNIT TESTS
    [TestFixture]
    public class CustomConvertAddress
    {
        [Test]
        public void NameConverterTest()
        {
            var engine = new FileHelperEngine<AddressConvClass>();

            AddressConvClass[] res = TestCommon.ReadTest<AddressConvClass>(engine, "Good", "CustomConverter2.txt");

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual("Bahia Blanca", res[0].Address.City);
            Assert.AreEqual("Sin Nombre", res[0].Address.Street);
            Assert.AreEqual("13", res[0].Address.Number);

            Assert.AreEqual("Saavedra", res[1].Address.City);
            Assert.AreEqual("Florencio Sanches", res[1].Address.Street);
            Assert.AreEqual("s/n", res[1].Address.Number);

            Assert.AreEqual("Bs.As", res[2].Address.City);
            Assert.AreEqual("12 de Octubre", res[2].Address.Street);
            Assert.AreEqual("4", res[2].Address.Number);

            Assert.AreEqual("Chilesito", res[3].Address.City);
            Assert.AreEqual("Pololo", res[3].Address.Street);
            Assert.AreEqual("5421", res[3].Address.Number);
        }


        [Test]
        public void NameConverterTest2()
        {
            var engine = new FileHelperEngine<AddressConvClass2>();

            AddressConvClass2[] res = TestCommon.ReadTest<AddressConvClass2>(engine, "Good", "CustomConverter2.txt");

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual("Bahia Blanca", res[0].Address.City);
            Assert.AreEqual("Sin Nombre", res[0].Address.Street);
            Assert.AreEqual("13", res[0].Address.Number);

            Assert.AreEqual("Saavedra", res[1].Address.City);
            Assert.AreEqual("Florencio Sanches", res[1].Address.Street);
            Assert.AreEqual("s/n", res[1].Address.Number);

            Assert.AreEqual("Bs.As", res[2].Address.City);
            Assert.AreEqual("12 de Octubre", res[2].Address.Street);
            Assert.AreEqual("4", res[2].Address.Number);

            Assert.AreEqual("Chilesito", res[3].Address.City);
            Assert.AreEqual("Pololo", res[3].Address.Street);
            Assert.AreEqual("5421", res[3].Address.Number);
        }

        // TEST CLASS
        [DelimitedRecord("|")]
        public class AddressBadClass1
        {
            [FieldConverter(typeof (AddressConverter2), ",")]
            public AddressField Address;

            public int Age;
        }

        [DelimitedRecord("|")]
        public class AddressBadClass2
        {
            [FieldConverter(typeof (AddressConverter2))]
            public AddressField Address;

            public int Age;
        }

        [DelimitedRecord("|")]
        public class AddressBadClass3
        {
            [FieldConverter(typeof (AddressConverter2), 3, 58.25)]
            public AddressField Address;

            public int Age;
        }

        [Test]
        public void NameConverterBad1()
        {
            var ex = Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<AddressBadClass1>());

            Assert.AreEqual(
                "Constructor for converter: AddressConverter2 with these arguments: (String) was not found. You must add a constructor with this signature (can be public or private)"
                ,
                ex.Message);
        }

        [Test]
        public void NameConverterBad2()
        {
            var ex = Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<AddressBadClass2>());

            Assert.AreEqual(
                "Empty constructor for converter: AddressConverter2 was not found. You must add a constructor without args (can be public or private)"
                ,
                ex.Message);
        }

        [Test]
        public void NameConverterBad3()
        {
            var ex = Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<AddressBadClass3>());

            Assert.AreEqual(
                "Constructor for converter: AddressConverter2 with these arguments: (Int32, Double) was not found. You must add a constructor with this signature (can be public or private)"
                ,
                ex.Message);
        }
    }
}