using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class FieldValueDiscarded
    {
        [Test]
        public void DiscardFirst()
        {
            var res = FileTest.Good.CustomersTab.ReadWithEngine<CustomersTabDiscardedFirst>();

            res.Length.AssertEqualTo(Array.FindAll(res, 
                (x) => x.CustomerID == null).Length);
        }


        [Test]
        public void DiscardSecond()
        {
            var res = FileTest.Good.CustomersTab.ReadWithEngine<CustomersTabDiscardedSecond>();

            res.Length.AssertEqualTo(Array.FindAll(res, (x) => x.CompanyName == null).Length);
        }

        [Test]
        public void DiscardMiddle()
        {
            var res = FileTest.Good.CustomersTab.ReadWithEngine<CustomersTabDiscardedMiddle>();

            res.Length.AssertEqualTo(Array.FindAll(res, (x) => x.Address == null).Length);
        }


        [Test]
        public void DiscardLast()
        {
            var res = FileTest.Good.CustomersTab.ReadWithEngine<CustomersTabDiscardedLast>();

            res.Length.AssertEqualTo(Array.FindAll(res, (x) => x.Country == null).Length);
        }

        [DelimitedRecord("\t")]
        public class CustomersTabDiscardedFirst
        {
            [FieldValueDiscarded()]
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;
        }


        [DelimitedRecord("\t")]
        public class CustomersTabDiscardedSecond
        {
            public string CustomerID;
            [FieldValueDiscarded()]
            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;
        }

        [DelimitedRecord("\t")]
        public class CustomersTabDiscardedMiddle
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            [FieldValueDiscarded()]
            public string Address;
            public string City;
            public string Country;
        }

        [DelimitedRecord("\t")]
        public class CustomersTabDiscardedLast
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            public string Address;
            public string City;
            [FieldValueDiscarded()]
            public string Country;
        }



        [Test]
        public void DiscardedBad()
        {
            Assert.Throws<BadUsageException>(() =>
                                             FileTest.Good.OrdersSmallVerticalBar
                                                 .ReadWithEngine<OrdersDiscardBad>());
        }

        [Test]
        public void OrdersAllDiscarded()
        {
            var res = FileTest.Good.OrdersSmallVerticalBar
                            .ReadWithEngine<OrdersAllDiscard>();

            Array.FindAll(res, (x) => x.CustomerID == null
                && x.OrderID == -1
                && x.OrderDate == new DateTime(2000,1,2)
                && x.Freight == 0).Length.AssertEqualTo(res.Length);
        }

        [Test]
        public void OrdersLastNotDiscarded()
        {
            var res = FileTest.Good.OrdersSmallVerticalBar
                            .ReadWithEngine<OrdersLastNotDiscard>();

            Array.FindAll(res, (x) => x.CustomerID == null
                && x.OrderID == -1
                && x.OrderDate == new DateTime(2000, 1, 2)
                && x.Freight != 0).Length.AssertEqualTo(res.Length);
        }

        [Test]
        public void OrdersLastTwoNotDiscarded()
        {
            var res = FileTest.Good.OrdersSmallVerticalBar
                            .ReadWithEngine<OrdersLastTwoNotDiscard>();

            Array.FindAll(res, (x) => x.CustomerID == null
                && x.OrderID == -1
                && x.OrderDate != new DateTime(2000, 1, 2)
                && x.Freight != 0).Length.AssertEqualTo(res.Length);
        }
        [DelimitedRecord("|")]
        public class OrdersDiscardBad
        {
            [FieldValueDiscarded]
            public int OrderID;

            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            public decimal Freight;
        }


        [DelimitedRecord("|")]
        public class OrdersAllDiscard
        {
            [FieldValueDiscarded]
            [FieldNullValue(-1)]
            public int OrderID;

            [FieldValueDiscarded]
            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            [FieldValueDiscarded]
            [FieldNullValue(typeof(DateTime), "2000-01-02")]
            public DateTime OrderDate;

            [FieldValueDiscarded]
            [FieldNullValue(typeof(decimal), "0")]
            public decimal Freight;
        }

        [DelimitedRecord("|")]
        public class OrdersLastNotDiscard
        {
            [FieldValueDiscarded]
            [FieldNullValue(-1)]
            public int OrderID;

            [FieldValueDiscarded]
            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            [FieldValueDiscarded]
            [FieldNullValue(typeof(DateTime), "2000-01-02")]
            public DateTime OrderDate;

            public decimal Freight;
        }

          [DelimitedRecord("|")]
        public class OrdersLastTwoNotDiscard
        {
            [FieldValueDiscarded]
              [FieldNullValue(-1)]
              public int OrderID;

            [FieldValueDiscarded]
            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            public decimal Freight;
        }

    }


}