#if ! MINI

using System;
using FileHelpers.DataLink;

namespace FileHelpersTests
{
	public class OrdersLinkProvider : AccessStorage
	{
		private string mAccessFileName = @"..\data\TestData.mdb";

		#region "  Constructors  "

		public OrdersLinkProvider()
		{
		}

		public OrdersLinkProvider(string fileName)
		{
			mAccessFileName = fileName;
		}

		#endregion

		#region "  RecordType  "

		public override Type RecordType
		{
			get { return typeof (OrdersFixed); }
		}

		#endregion

		#region "  FillRecord  "

		protected override object FillRecord(object[] fields)
		{
			OrdersFixed record = new OrdersFixed();

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

			return record;
		}

		#endregion

		#region "  GetSelectSql  "

		protected override string GetSelectSql()
		{
			return "SELECT * FROM Orders";
		}

		#endregion

		#region "  GetInsertSql  "

		protected override string GetInsertSql(object record)
		{
			OrdersFixed obj = (OrdersFixed) record;

			return String.Format("INSERT INTO Orders (CustomerID, EmployeeID, Freight, OrderDate, OrderID, RequiredDate, ShippedDate, ShipVia) " +
				" VALUES ( \"{0}\" , \"{1}\" , \"{2}\" , \"{3}\" , \"{4}\" , \"{5}\" , \"{6}\" , \"{7}\"  ) ",
			                     obj.CustomerID,
			                     obj.EmployeeID,
			                     obj.Freight,
			                     obj.OrderDate,
			                     obj.OrderID,
			                     obj.RequiredDate,
			                     obj.ShippedDate,
			                     obj.ShipVia
				);

		}

		#endregion

		public override string MdbFileName
		{
			get { return mAccessFileName; }
		}

	}
}

#endif