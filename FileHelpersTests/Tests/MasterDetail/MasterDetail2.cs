using System;
using FileHelpers;
using FileHelpers.MasterDetail;
using NUnit.Framework;
using System.IO;

namespace FileHelpersTests.MasterDetail
{
	[TestFixture]
	public class MasterDetail2
	{
		MasterDetailEngine engine;

		[Test]
		public void CustomerOrdersRead()
		{
            engine = new MasterDetailEngine(typeof(CustomersVerticalBar), typeof(OrdersVerticalBar), CommonActions.DetailIfContains, "@");

            MasterDetails[] res = TestCommon.ReadTest(engine, @"Good\MasterDetail2.txt");

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(4, res[0].Details.Length);
            Assert.AreEqual(3, res[1].Details.Length);
            Assert.AreEqual(2, res[2].Details.Length);
            Assert.AreEqual(0, res[3].Details.Length);

            Assert.AreEqual("ALFKI", ((CustomersVerticalBar)res[0].Master).CustomerID);
            Assert.AreEqual(10248, ((OrdersVerticalBar)res[0].Details[0]).OrderID);
            Assert.AreEqual(10249, ((OrdersVerticalBar)res[0].Details[1]).OrderID);
            Assert.AreEqual(10250, ((OrdersVerticalBar)res[0].Details[2]).OrderID);
            Assert.AreEqual(10251, ((OrdersVerticalBar)res[0].Details[3]).OrderID);
            
            Assert.AreEqual("ANATR", ((CustomersVerticalBar)res[1].Master).CustomerID);
            Assert.AreEqual(10252, ((OrdersVerticalBar)res[1].Details[0]).OrderID);
            Assert.AreEqual(10253, ((OrdersVerticalBar)res[1].Details[1]).OrderID);
            Assert.AreEqual(10254, ((OrdersVerticalBar)res[1].Details[2]).OrderID);

            Assert.AreEqual("ANTON", ((CustomersVerticalBar)res[2].Master).CustomerID);
            Assert.AreEqual(10257, ((OrdersVerticalBar)res[2].Details[0]).OrderID);
            Assert.AreEqual(10258, ((OrdersVerticalBar)res[2].Details[1]).OrderID);

            Assert.AreEqual("DUMON", ((CustomersVerticalBar)res[3].Master).CustomerID);

		}

        [Test]
        public void CustomerOrdersRead2()
        {
            engine = new MasterDetailEngine(typeof(CustomersVerticalBar), typeof(OrdersVerticalBar), CommonActions.MasterIfContains, "@");

            MasterDetails[] res = TestCommon.ReadTest(engine, @"Good\MasterDetail3.txt");

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(4, res[0].Details.Length);
            Assert.AreEqual(3, res[1].Details.Length);
            Assert.AreEqual(2, res[2].Details.Length);
            Assert.AreEqual(0, res[3].Details.Length);

            Assert.AreEqual("ALFKI", ((CustomersVerticalBar)res[0].Master).CustomerID);
            Assert.AreEqual(10248, ((OrdersVerticalBar)res[0].Details[0]).OrderID);
            Assert.AreEqual(10249, ((OrdersVerticalBar)res[0].Details[1]).OrderID);
            Assert.AreEqual(10250, ((OrdersVerticalBar)res[0].Details[2]).OrderID);
            Assert.AreEqual(10251, ((OrdersVerticalBar)res[0].Details[3]).OrderID);

            Assert.AreEqual("ANATR", ((CustomersVerticalBar)res[1].Master).CustomerID);
            Assert.AreEqual(10252, ((OrdersVerticalBar)res[1].Details[0]).OrderID);
            Assert.AreEqual(10253, ((OrdersVerticalBar)res[1].Details[1]).OrderID);
            Assert.AreEqual(10254, ((OrdersVerticalBar)res[1].Details[2]).OrderID);

            Assert.AreEqual("ANTON", ((CustomersVerticalBar)res[2].Master).CustomerID);
            Assert.AreEqual(10257, ((OrdersVerticalBar)res[2].Details[0]).OrderID);
            Assert.AreEqual(10258, ((OrdersVerticalBar)res[2].Details[1]).OrderID);

            Assert.AreEqual("DUMON", ((CustomersVerticalBar)res[3].Master).CustomerID);

        }
       


	}
}