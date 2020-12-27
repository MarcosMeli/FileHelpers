using System.IO;
using FileHelpers.Converters;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    // SPECIAL FIELD
    public class NameField
    {
        public string FirstName;
        public string LastName;

        public override string ToString()
        {
            return LastName + ", " + FirstName;
        }
    }

    // CUSTOM CONVERTER
    public class NameConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            string[] splited = from.Split(',');

            var res = new NameField();
            res.LastName = splited[0].Trim();
            res.FirstName = splited[1].Trim();

            return res;
        }
    }

    // TEST CLASS
    [DelimitedRecord("|")]
    public class CustomConvClass
    {
        public string Country;

        [NameConverter]
        public NameField Names;

        public int Age;
    }


    // NUNIT TESTS
    [TestFixture]
    public class CustomConverterName
    {
        [Test]
        public void NameConverterTest()
        {
            var engine = new FileHelperEngine<CustomConvClass>();

            CustomConvClass[] res = TestCommon.ReadTest<CustomConvClass>(engine, "Good", "CustomConverter1.txt");

            Assert.AreEqual(5, res.Length);

            for (int i = 0; i < 5; i++) {
                Assert.AreEqual("Argentina", res[i].Country);
                Assert.AreEqual("Meli", res[i].Names.LastName);
                Assert.AreEqual("Marcos", res[i].Names.FirstName);
                Assert.AreEqual(25, res[i].Age);
            }
        }

        [Test]
        public void NameConverterTest2()
        {
            var engine = new FileHelperEngine<CustomConvClass>();

            CustomConvClass[] res = TestCommon.ReadTest<CustomConvClass>(engine, "Good", "CustomConverter1.txt");
            Assert.AreEqual(5, res.Length);

            engine.WriteFile("tmpCC.txt", res);
            res = (CustomConvClass[]) engine.ReadFile("tmpCC.txt");
            Assert.AreEqual(5, res.Length);

            for (int i = 0; i < 5; i++) {
                Assert.AreEqual("Argentina", res[i].Country);
                Assert.AreEqual("Meli", res[i].Names.LastName);
                Assert.AreEqual("Marcos", res[i].Names.FirstName);
                Assert.AreEqual(25, res[i].Age);
            }

            if (File.Exists("tmpCC.txt"))
                File.Delete("tmpCC.txt");
        }
    }
}