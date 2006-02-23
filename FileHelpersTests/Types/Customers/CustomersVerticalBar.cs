using FileHelpers;

namespace FileHelpersTests
{
	[DelimitedRecord("|")]
	public class CustomersVerticalBar
	{
		public string CustomerID;
		public string CompanyName;
		public string ContactName;
		public string ContactTitle;
		public string Address;
		public string City;
		public string Country;
	}

}