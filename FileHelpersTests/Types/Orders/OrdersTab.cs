using System;
using FileHelpers;

namespace FileHelpersTests
{
	[DelimitedRecord("\t")]
	public class OrdersTab
	{
		public int OrderID;

		public string CustomerID;

		public int EmployeeID;

		public DateTime OrderDate;

		[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime RequiredDate;

		[FieldNullValue(typeof (DateTime), "2005-1-1")] public DateTime ShippedDate;

		public int ShipVia;

		public decimal Freight;
	}
}