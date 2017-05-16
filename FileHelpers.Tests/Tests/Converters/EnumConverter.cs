using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    public class EnumConverter
    {
        [Test]
        public void EnumSingleCase()
        {
            var res = FileTest.Good.EnumConverter2.ReadWithEngine<EnumType2>();

            Assert.AreEqual(5, res.Length);

            Assert.AreEqual(Enum2.One, res[0].EnumValue);
            Assert.AreEqual(Enum2.One, res[1].EnumValue);
            Assert.AreEqual(Enum2.Two, res[2].EnumValue);
            Assert.AreEqual(Enum2.Three, res[3].EnumValue);
            Assert.AreEqual(Enum2.Three, res[4].EnumValue);
        }

        [Test]
        public void EnumexplicitConverter()
        {
            var res = FileTest.Good.EnumConverter2.ReadWithEngine<EnumType3>();

            Assert.AreEqual(5, res.Length);

            Assert.AreEqual(Enum2.One, res[0].EnumValue);
            Assert.AreEqual(Enum2.One, res[1].EnumValue);
            Assert.AreEqual(Enum2.Two, res[2].EnumValue);
            Assert.AreEqual(Enum2.Three, res[3].EnumValue);
            Assert.AreEqual(Enum2.Three, res[4].EnumValue);
        }

        [Test]
        public void EnumMulticase()
        {
            var res = FileTest.Good.EnumConverter1.ReadWithEngine<EnumType1>();

            Assert.AreEqual(5, res.Length);

            Assert.AreEqual(Enum1.ONe, res[0].EnumValue);
            Assert.AreEqual(Enum1.ONe, res[1].EnumValue);
            Assert.AreEqual(Enum1.two, res[2].EnumValue);
            Assert.AreEqual(Enum1.ThreE, res[3].EnumValue);
            Assert.AreEqual(Enum1.ThreE, res[4].EnumValue);
        }

        [Test]
        public void EnumValueNotFound()
        {
            var engine = new FileHelperEngine<EnumType2>();
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            EnumType2[] res = TestCommon.ReadTest<EnumType2>(engine, "Good", "EnumConverter3.txt");

            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
            Assert.AreEqual(3, engine.ErrorManager.Errors[0].LineNumber);
            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());

            Assert.AreEqual(4, res.Length);


            Assert.AreEqual(Enum2.One, res[0].EnumValue);
            Assert.AreEqual(Enum2.Two, res[1].EnumValue);
            Assert.AreEqual(Enum2.Three, res[2].EnumValue);
            Assert.AreEqual(Enum2.Three, res[3].EnumValue);
        }

        [Test]
        public void EnumString()
        {
            var engine = new FileHelperEngine<EnumType4>();

            using (var temp = new TempFileFactory())
            {
                EnumType4[] data = new EnumType4[] { new EnumType4() { EnumValue = Enum2.One } };

                //write data to file using approprppriate conversion style
                engine.WriteFile(temp.FileName, data);

                //read data back
                data = engine.ReadFile(temp.FileName);

                Assert.IsAssignableFrom(typeof(EnumType4), data[0]);
                Assert.AreEqual(data[0].EnumValue, Enum2.One);

            }
        }

        [Test]
        public void EnumNumeric()
        {
            var engine = new FileHelperEngine<EnumType5>();
            using (var temp = new TempFileFactory())
            {
                EnumType5[] data = new EnumType5[] { new EnumType5() { EnumValue = Enum2.One } };

                //write data to file using approprppriate conversion style
                engine.WriteFile(temp.FileName, data);

                //read data back
                data = engine.ReadFile(temp.FileName);

                Assert.IsAssignableFrom(typeof(EnumType5), data[0]);
                Assert.AreEqual(data[0].EnumValue, Enum2.One);
            }
        }
    }


    public enum Enum1
    {
        ONe,
        two,
        ThreE
    };

    public enum Enum2
    {
        One,
        Two,
        Three
    };

    [DelimitedRecord(",")]
    public class EnumType1
    {
        public Enum1 EnumValue;
    }

    [DelimitedRecord(",")]
    public class EnumType2
    {
        public Enum2 EnumValue;
    }

    [DelimitedRecord(",")]
    public class EnumType3
    {
        [FieldConverter(typeof (Enum2))]
        public Enum2 EnumValue;
    }

    [DelimitedRecord(",")]
    public class EnumType4
    {
        [FieldConverter(typeof(Enum2),"s")]
        public Enum2 EnumValue;
    }

    [DelimitedRecord(",")]
    public class EnumType5
    {
        [FieldConverter(typeof(Enum2), "n")]
        public Enum2 EnumValue;
    }


}