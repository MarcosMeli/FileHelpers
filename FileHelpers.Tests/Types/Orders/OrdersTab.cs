using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Tests
{
	[DelimitedRecord("\t")]
	public class OrdersTab 
        : IComparable<OrdersTab>
	{
		public int OrderID;

		public string CustomerID;

		public int EmployeeID;

		public DateTime OrderDate;

		[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime RequiredDate;

		[FieldNullValue(typeof (DateTime), "2005-1-1")] public DateTime ShippedDate;

		public int ShipVia;

		public decimal Freight;
	    
        public int CompareTo(OrdersTab other)
	    {
	        return this.OrderID.CompareTo(other.OrderID);
	    }
	}
}