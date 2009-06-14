using System;
using FileHelpers;
using FileHelpers.MasterDetail;
using NUnit.Framework;
using System.IO;
using MasterDetails = FileHelpers.MasterDetail.MasterDetails<object, object>;

namespace FileHelpersTests.MasterDetail
{
	[TestFixture]
	public class MasterDetail
	{
		MasterDetailEngine engine;

		[Test]
		public void CustomerOrdersRead()
		{
            engine = new MasterDetailEngine(typeof(CustomersVerticalBar), typeof(OrdersVerticalBar), new MasterDetailSelector(Test1Selector));

            MasterDetails[] res = TestCommon.ReadTest(engine, @"Good\MasterDetail1.txt");

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
        public void CustomerOrdersWrite()
        {
            engine = new MasterDetailEngine(typeof(CustomersVerticalBar), typeof(OrdersVerticalBar), new MasterDetailSelector(Test1Selector));
            MasterDetails[] resTmp = TestCommon.ReadTest(engine, @"Good\MasterDetail1.txt");
            Assert.AreEqual(4, resTmp.Length);

            engine.WriteFile("tempmd.txt", resTmp);


            MasterDetails[] res = engine.ReadFile("tempmd.txt");

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


            if (File.Exists("tempmd.txt")) File.Delete("tempmd.txt");

        }

        RecordAction Test1Selector(string record)
        {
            if (Char.IsLetter(record[0]))
                return RecordAction.Master;
            else
                return RecordAction.Detail;
        }

        


	}
}