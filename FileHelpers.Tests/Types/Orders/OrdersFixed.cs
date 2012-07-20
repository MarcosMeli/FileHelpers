using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Tests
{
	[FixedLengthRecord]
	public class OrdersFixed
	{
		[FieldFixedLength(7)] 
		public int OrderID;

		[FieldFixedLength(12)] 
		public string CustomerID;

		[FieldFixedLength(3)] 
		public int EmployeeID;

		[FieldFixedLength(10)] 
		public DateTime OrderDate;

		[FieldFixedLength(10)]
		public DateTime RequiredDate;

		[FieldFixedLength(10)]
		[FieldNullValue(typeof (DateTime), "2005-1-1")] 
		public DateTime ShippedDate;

		[FieldFixedLength(3)] 
		public int ShipVia;

		[FieldFixedLength(10)] 
		public decimal Freight;
	}


}