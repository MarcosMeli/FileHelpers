using System.Collections.Generic;
using FileHelpers.MasterDetail;
using NUnit.Framework;
using MasterDetails = FileHelpers.MasterDetail.MasterDetails<object, object>;

namespace FileHelpers.Tests.MasterDetail
{
    [TestFixture]
    public class MasterDetail2
    {
        [Test]
        public void CustomerOrdersRead()
        {
            var engine = new MasterDetailEngine(typeof (CustomersVerticalBar),
                typeof (OrdersVerticalBar),
                CommonSelector.DetailIfContains,
                "@");

            MasterDetails[] res = engine.ReadFile(FileTest.Good.MasterDetail2.Path);

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(4, res[0].Details.Length);
            Assert.AreEqual(3, res[1].Details.Length);
            Assert.AreEqual(2, res[2].Details.Length);
            Assert.AreEqual(0, res[3].Details.Length);

            Assert.AreEqual("ALFKI", ((CustomersVerticalBar) res[0].Master).CustomerID);
            Assert.AreEqual(10248, ((OrdersVerticalBar) res[0].Details[0]).OrderID);
            Assert.AreEqual(10249, ((OrdersVerticalBar) res[0].Details[1]).OrderID);
            Assert.AreEqual(10250, ((OrdersVerticalBar) res[0].Details[2]).OrderID);
            Assert.AreEqual(10251, ((OrdersVerticalBar) res[0].Details[3]).OrderID);

            Assert.AreEqual("ANATR", ((CustomersVerticalBar) res[1].Master).CustomerID);
            Assert.AreEqual(10252, ((OrdersVerticalBar) res[1].Details[0]).OrderID);
            Assert.AreEqual(10253, ((OrdersVerticalBar) res[1].Details[1]).OrderID);
            Assert.AreEqual(10254, ((OrdersVerticalBar) res[1].Details[2]).OrderID);

            Assert.AreEqual("ANTON", ((CustomersVerticalBar) res[2].Master).CustomerID);
            Assert.AreEqual(10257, ((OrdersVerticalBar) res[2].Details[0]).OrderID);
            Assert.AreEqual(10258, ((OrdersVerticalBar) res[2].Details[1]).OrderID);

            Assert.AreEqual("DUMON", ((CustomersVerticalBar) res[3].Master).CustomerID);
        }

        [Test]
        public void CustomerOrdersRead2()
        {
            var engine = new MasterDetailEngine(typeof (CustomersVerticalBar),
                typeof (OrdersVerticalBar),
                CommonSelector.MasterIfContains,
                "@");

            MasterDetails[] res = engine.ReadFile(FileTest.Good.MasterDetail3.Path);

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(4, res[0].Details.Length);
            Assert.AreEqual(3, res[1].Details.Length);
            Assert.AreEqual(2, res[2].Details.Length);
            Assert.AreEqual(0, res[3].Details.Length);

            Assert.AreEqual("ALFKI", ((CustomersVerticalBar) res[0].Master).CustomerID);
            Assert.AreEqual(10248, ((OrdersVerticalBar) res[0].Details[0]).OrderID);
            Assert.AreEqual(10249, ((OrdersVerticalBar) res[0].Details[1]).OrderID);
            Assert.AreEqual(10250, ((OrdersVerticalBar) res[0].Details[2]).OrderID);
            Assert.AreEqual(10251, ((OrdersVerticalBar) res[0].Details[3]).OrderID);

            Assert.AreEqual("ANATR", ((CustomersVerticalBar) res[1].Master).CustomerID);
            Assert.AreEqual(10252, ((OrdersVerticalBar) res[1].Details[0]).OrderID);
            Assert.AreEqual(10253, ((OrdersVerticalBar) res[1].Details[1]).OrderID);
            Assert.AreEqual(10254, ((OrdersVerticalBar) res[1].Details[2]).OrderID);

            Assert.AreEqual("ANTON", ((CustomersVerticalBar) res[2].Master).CustomerID);
            Assert.AreEqual(10257, ((OrdersVerticalBar) res[2].Details[0]).OrderID);
            Assert.AreEqual(10258, ((OrdersVerticalBar) res[2].Details[1]).OrderID);

            Assert.AreEqual("DUMON", ((CustomersVerticalBar) res[3].Master).CustomerID);
        }


        private readonly OrdersVerticalBar[] colDetails = new OrdersVerticalBar[] {};

        [Test]
        public void CustomerOrdersWrite2()
        {
            var masterDetEng = new MasterDetailEngine<CustomersVerticalBar, OrdersVerticalBar>();

            MasterDetails<CustomersVerticalBar, OrdersVerticalBar> record;
            var records = new List<MasterDetails<CustomersVerticalBar, OrdersVerticalBar>>();

            // Create the master detail item
            record = new MasterDetails<CustomersVerticalBar, OrdersVerticalBar>();
            records.Add(record);

            // Create the master object
            record.Master = new CustomersVerticalBar();
            record.Master.CustomerID = "ALFKI";
            record.Master.Country = "Argentina";

            // Create the details object
            var orders = new List<OrdersVerticalBar>();
            foreach (var det in colDetails)
                orders.Add(det);

            record.Details = orders.ToArray();


            // We can create a second master object
            record = new MasterDetails<CustomersVerticalBar, OrdersVerticalBar>();
            records.Add(record);

            record.Master = new CustomersVerticalBar();
            record.Master.CustomerID = "ALFKI";
            record.Master.Country = "Argentina";

            orders = new List<OrdersVerticalBar>();
            foreach (var det in colDetails)
                orders.Add(det);
            record.Details = orders.ToArray();

            // And now write it to a file

            masterDetEng.WriteFile("myMDfile.txt", records.ToArray());
        }
    }
}