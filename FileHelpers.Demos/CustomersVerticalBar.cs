using System.ComponentModel;
using FileHelpers;

namespace FileHelpersSamples
{
    /// <summary>
    /// Sample class with a few delimited fields so we can load data into it.
    /// </summary>
	[DelimitedRecord("|")]
	[TypeConverter(typeof (ExpandableObjectConverter))]
	public class CustomersVerticalBar
	{
		private string mCustomerID;
		private string mCompanyName;
		private string mContactName;
		private string mContactTitle;
		private string mAddress;
		private string mCity;
		private string mCountry;

		public string CustomerID
		{
			get { return mCustomerID; }
			set { mCustomerID = value; }
		}

		public string CompanyName
		{
			get { return mCompanyName; }
			set { mCompanyName = value; }
		}

		public string ContactName
		{
			get { return mContactName; }
			set { mContactName = value; }
		}

		public string ContactTitle
		{
			get { return mContactTitle; }
			set { mContactTitle = value; }
		}

		public string Address
		{
			get { return mAddress; }
			set { mAddress = value; }
		}

		public string City
		{
			get { return mCity; }
			set { mCity = value; }
		}

		public string Country
		{
			get { return mCountry; }
			set { mCountry = value; }
		}


		//-> To display in the PropertyGrid.
		public override string ToString()
		{
			return CustomerID + " - " + CompanyName + ", " + ContactName;
		}
	}
}