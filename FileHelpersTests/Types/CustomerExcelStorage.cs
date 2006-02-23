using System;
using FileHelpers.DataLink;

namespace FileHelpersTests
{
	/// <summary>
	/// Summary description for CustomerExcelDataLink.
	/// </summary>
	public class CustomerExcelStorage: ExcelStorage
	{
		public CustomerExcelStorage()
		{
			this.StartRow = 3;
			this.StartColumn = 2;
			this.FileName = @"..\data\Customers.xls";
		}

		public override Type RecordType
		{
			get { return typeof(CustomersVerticalBar); }
		}

		// TEMP !!!!
		// IN THE FINAL VERSION YOU DONT NEED TO OVERRIDE THIS 
//		protected override object ValuesToRecord(object[] values)
//		{
//            CustomersVerticalBar res = new CustomersVerticalBar();
//			
//			res.CustomerID = values[0].ToString();
//			res.CompanyName = values[1].ToString();
//			res.ContactName = values[2].ToString();
//			res.ContactTitle = values[3].ToString();
//			res.Address = values[4].ToString();
//			res.City = values[5].ToString();
//			res.Country = values[6].ToString();
//
//			return res;
//
//		}

//			protected override object[] RecordToValues(object record)
//			{
//				CustomersVerticalBar rec = (CustomersVerticalBar) record;
//
//				object[] values = new object[7];
//
//				values[0] = rec.CustomerID;
//				values[1] = rec.CompanyName;
//				values[2] = rec.ContactName;
//				values[3] = rec.ContactTitle;
//				values[4] = rec.Address;
//				values[5] = rec.City;
//				values[6] = rec.Country;
//
//				return values;
//			}


	}
}
