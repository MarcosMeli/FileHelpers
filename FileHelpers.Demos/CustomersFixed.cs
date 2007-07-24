using System.ComponentModel;
using FileHelpers;

namespace FileHelpersSamples
{
	[FixedLengthRecord]
	[TypeConverter(typeof (ExpandableObjectConverter))]
	public class CustomersFixed
	{
		[FieldFixedLength(11)] public string mCustomerID;

		[FieldFixedLength(38)] public string mCompanyName; 

		[FieldFixedLength(22)] public string mContactName;

		[FieldFixedLength(38)] public string mContactTitle;

		[FieldFixedLength(41)] public string mAddress;

		[FieldFixedLength(18)] public string mCity;

		[FieldFixedLength(10)] public string mCountry;

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