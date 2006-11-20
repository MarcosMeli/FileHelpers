using System;
using System.Data;
using FileHelpers;
using FileHelpers.Mapping;
using NUnit.Framework;

namespace FileHelpersTests.Mapping
{
	[TestFixture]
	public class SimpleMapping
	{
		DataMapper mapper;

		[Test]
		public void Map1()
		{
			mapper = new DataMapper(typeof(OrdersMap));
			
			mapper.AddMapping(1, "OrderId");
			mapper.AddMapping(2, "EmployeeId");
			mapper.AddMapping(0, "OrderDate");
			
			
			DataTable dt = new DataTable();
			dt.Columns.Add("Date", typeof(DateTime));
			dt.Columns.Add("Order", typeof(int));
			dt.Columns.Add("Employee", typeof(short));
			
			dt.Rows.Add(new object[] { new DateTime(2006,4,1), 3, (short)1 });
			dt.Rows.Add(new object[] { new DateTime(2006,6,1), 10, (short)11 });
			dt.Rows.Add(new object[] { new DateTime(2006,9,1), 20, (short)21 });
			
			OrdersMap[] res = (OrdersMap[]) mapper.MapRecords(dt);
			
			Assert.AreEqual(3, res[0].OrderID);
			Assert.AreEqual(1, res[0].EmployeeID);
			Assert.AreEqual(new DateTime(2006,4,1), res[0].OrderDate);
			
			Assert.AreEqual(10, res[1].OrderID);
			Assert.AreEqual(11, res[1].EmployeeID);
			Assert.AreEqual(new DateTime(2006,6,1), res[1].OrderDate);
			
			
		}


		[DelimitedRecord("\t")]
		public class OrdersMap
		{
			public int OrderID;

			public int EmployeeID;

			public DateTime OrderDate;

		}



	}


}