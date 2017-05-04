using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    public class DecimalNumbers
    {
        [Test]
        public void Decimals1()
        {
            var engine = new FileHelperEngine<DecimalType>();

            var res = TestCommon.ReadTest<DecimalType>(engine, "Good", "NumberFormat.txt");

            Assert.AreEqual(10, res.Length);

            CheckDecimal((decimal) 32.38, res[0]);
            CheckDecimal((decimal) 11.61, res[1]);
            CheckDecimal((decimal) 65.83, res[2]);
            CheckDecimal((decimal) 41.34, res[3]);
            CheckDecimal((decimal) 51.3, res[4]);
            CheckDecimal((decimal) 58.17, res[5]);
            CheckDecimal((decimal) 22.98, res[6]);
            CheckDecimal((decimal) 148.33, res[7]);
            CheckDecimal((decimal) 13.97, res[8]);
            CheckDecimal((decimal) 81.91, res[9]);
        }

        private static void CheckDecimal(decimal dec, DecimalType res)
        {
            Assert.AreEqual((decimal) dec, res.DecimalField);
            Assert.AreEqual((double) dec, res.DoubleField);
            Assert.AreEqual((float) dec, res.FloatField);
        }


        [Test]
        public void NegativeNumbers()
        {
            var engine = new FileHelperEngine<DecimalType>();

            var res = TestCommon.ReadTest<DecimalType>(engine, "Good", "NumberNegative.txt");

            Assert.AreEqual(10, res.Length);

            CheckDecimal((decimal) 32.38, res[0]);
            CheckDecimal((decimal) -11.61, res[1]);
            CheckDecimal((decimal) -65.83, res[2]);
            CheckDecimal((decimal) 41.34, res[3]);
            CheckDecimal((decimal) 51.3, res[4]);
            CheckDecimal((decimal) -58.17, res[5]);
            CheckDecimal((decimal) 22.98, res[6]);
            CheckDecimal((decimal) -148.33, res[7]);
            CheckDecimal((decimal) 13.97, res[8]);
            CheckDecimal((decimal) 81.91, res[9]);
        }


        [DelimitedRecord("|")]
        public class DecimalType
        {
            public int IntField;
            public float FloatField;
            public double DoubleField;
            public decimal DecimalField;
        }

        [Test]
        public void DecimalsWithExponents()
        {
            var engine = new FileHelperEngine<DecimalType>();

            DecimalType[] res;
            res = TestCommon.ReadTest<DecimalType>(engine, "Good", "NumberFormat2.txt");

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual(10248, res[0].IntField);
            Assert.AreEqual(1024900, res[1].IntField);

            CheckDecimal((decimal) 32.38, res[0]);
            CheckDecimal((decimal) 11.61E+03, res[1]);
            CheckDecimal((decimal) 81.91, res[2]);
            CheckDecimal((decimal) 65.83E+02, res[3]);
        }
    }
}