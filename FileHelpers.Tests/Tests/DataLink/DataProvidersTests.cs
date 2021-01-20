#if !NETCOREAPP
using System;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpers.Tests.DataLink
{
    [TestFixture]
    [Explicit]
    [Category("Advanced")]
    public class DataProviders
    {
        [Test]
        public void OrdersProvider()
        {
            var storage = new AccessStorage(typeof (OrdersFixed), @"..\data\TestData.mdb");
            storage.SelectSql = "SELECT * FROM Orders";
            storage.FillRecordCallback = new FillRecordHandler(FillRecordOrder);

            object[] res = storage.ExtractRecords();

            Assert.AreEqual(830, res.Length);
            Assert.AreEqual(typeof (OrdersFixed), res[0].GetType());
        }


        protected void FillRecordOrder(object rec, object[] fields)
        {
            var record = (OrdersFixed) rec;

            record.OrderID = (int) fields[0];
            record.CustomerID = (string) fields[1];
            record.EmployeeID = (int) fields[2];
            record.OrderDate = (DateTime) fields[3];
            record.RequiredDate = (DateTime) fields[4];
            if (fields[5] != DBNull.Value)
                record.ShippedDate = (DateTime) fields[5];
            else
                record.ShippedDate = DateTime.MinValue;
            record.ShipVia = (int) fields[6];
            record.Freight = (decimal) fields[7];
        }

        private void FillRecordCust(object rec, object[] fields)
        {
            var record = (CustomersVerticalBar) rec;

            record.CustomerID = (string) fields[0];
            record.CompanyName = (string) fields[1];
            record.ContactName = (string) fields[2];
            record.ContactTitle = (string) fields[3];
            record.Address = (string) fields[4];
            record.City = (string) fields[5];
            record.Country = (string) fields[6];
        }

        [Test]
        public void CustomersProvider()
        {
            var storage = new AccessStorage(typeof (CustomersVerticalBar), @"..\data\TestData.mdb");
            storage.SelectSql = "SELECT * FROM Customers";
            storage.FillRecordCallback = new FillRecordHandler(FillRecordCust);

            object[] res = storage.ExtractRecords();

            Assert.AreEqual(91, res.Length);
            Assert.AreEqual(typeof (CustomersVerticalBar), res[0].GetType());
        }
    }
}
#endif
