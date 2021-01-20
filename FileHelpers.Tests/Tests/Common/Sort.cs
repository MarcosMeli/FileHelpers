#if !NETCOREAPP
using System;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class SortRecords
    {
        [Test]
        public void Sort1()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();

            CustomersVerticalBar[] res = engine.ReadFile(FileTest.Good.Sort1.Path);

            Assert.AreEqual(8, res.Length);

            CommonEngine.SortRecordsByField(res, "CompanyName");

            Assert.AreEqual(8, res.Length);

            Assert.AreEqual("Alfreds Futterkiste", res[0].CompanyName);
            Assert.AreEqual("La maison d'Asie", res[1].CompanyName);
            Assert.AreEqual("Tortuga Restaurante", res[2].CompanyName);
        }

        [Test]
        public void Sort2()
        {
            var engine = new FileHelperEngine<CustomersSort>();

            var res = engine.ReadFile(FileTest.Good.Sort1.Path) as CustomersSort[];

            Assert.AreEqual(8, res.Length);

            CommonEngine.SortRecordsByField(res, "CompanyName");

            Assert.AreEqual(8, res.Length);

            Assert.AreEqual("Alfreds Futterkiste", res[0].CompanyName);
            Assert.AreEqual("La maison d'Asie", res[1].CompanyName);
            Assert.AreEqual("Tortuga Restaurante", res[2].CompanyName);
        }

        [Test]
        public void Sort3()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();

            var res = engine.ReadFile(FileTest.Good.Sort1.Path);

            Assert.Throws<BadUsageException>(()
                => CommonEngine.SortRecords(res));
        }

        [Test]
        public void Sort4()
        {
            var engine = new FileHelperEngine<CustomersSort>();

            var res = engine.ReadFile(FileTest.Good.Sort1.Path) as CustomersSort[];

            Assert.AreEqual(8, res.Length);

            CommonEngine.SortRecords(res);

            Assert.AreEqual(8, res.Length);

            Assert.AreEqual("Alfreds Futterkiste", res[0].CompanyName);
            Assert.AreEqual("La maison d'Asie", res[1].CompanyName);
            Assert.AreEqual("Tortuga Restaurante", res[2].CompanyName);
        }

        [Test]
        public void Sort5()
        {
            var engine = new FileHelperEngine<CustomersSort>();

            CustomersSort[] res = engine.ReadFile(FileTest.Good.Sort1.Path);
            Assert.AreEqual(8, res.Length);

            Assert.Throws<BadUsageException>(()
                => CommonEngine.SortRecordsByField(res, "CompanyNameNoExistHere"));
        }

        [DelimitedRecord("|")]
        private class CustomersSort : IComparable
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;

            #region IComparable Members

            public int CompareTo(object obj)
            {
                if (this == obj)
                    return 0;

                var to = (CustomersSort) obj;

                if (to == null)
                    return int.MaxValue;

                return CompanyName.CompareTo(to.CompanyName);
            }

            #endregion
        }
    }
}
#endif
