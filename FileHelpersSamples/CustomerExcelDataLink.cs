using System;
using FileHelpers.DataLink;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for CustomerExcelDataLink.
	/// </summary>
	public class CustomerExcelDataLink: ExcelStorage
	{
		public CustomerExcelDataLink()
		{
			this.StartRow = 3;
			this.StartColumn = 2;
			this.FileName = @"D:\__Working\Posts\FileHelpers\CurrentSource\Source\FileHelpersTests\Data\Customers.xls";
			this.mNumberOfFields = 7;
		}


		public override Type RecordType
		{
			get { return typeof(CustomersVerticalBar); }
		}

		protected override object ValuesToRecord(object[] values)
		{
            CustomersVerticalBar res = new CustomersVerticalBar();
			
			res.CustomerID = values[0].ToString();
			res.CompanyName = values[1].ToString();
			res.ContactName = values[2].ToString();
			res.ContactTitle = values[3].ToString();
			res.Address = values[4].ToString();
			res.City = values[5].ToString();
			res.Country = values[6].ToString();

			return res;

		}

		protected override object[] RecordToValues(object record)
		{
			CustomersVerticalBar rec = (CustomersVerticalBar) record;

			object[] values = new object[7];

			values[0] = rec.CustomerID;
			values[1] = rec.CompanyName;
			values[2] = rec.ContactName;
			values[3] = rec.ContactTitle;
			values[4] = rec.Address;
			values[5] = rec.City;
			values[6] = rec.Country;

			return values;
		}

	}
}
