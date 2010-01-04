using FileHelpers;

namespace FileHelpers.Tests
{
	[FixedLengthRecord]
	public class CustomersFixed
	{
		[FieldFixedLength(11)]
		public string CustomerID;

		[FieldFixedLength(50 - 12)]
		public string CompanyName;

		[FieldFixedLength(72 - 50)]
		public string ContactName;

		[FieldFixedLength(110 - 72)]
		public string ContactTitle;

		[FieldFixedLength(151 - 110)]
		public string Address;

		[FieldFixedLength(169 - 151)]
		public string City;

		[FieldFixedLength(15)]
		public string Country;
	}
}