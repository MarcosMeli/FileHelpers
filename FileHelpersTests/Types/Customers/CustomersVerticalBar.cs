using System;
using FileHelpers;

namespace FileHelpersTests
{
	[DelimitedRecord("|")]
	public class CustomersVerticalBar: IComparableRecord
	{
		public string CustomerID;
		public string CompanyName;
		public string ContactName;
		public string ContactTitle;
		public string Address;
		public string City;
		public string Country;

		public bool IsEqualRecord(object record)
		{
			CustomersVerticalBar rec = (CustomersVerticalBar) record;
			return this.CustomerID == rec.CustomerID;
		}
	}

}