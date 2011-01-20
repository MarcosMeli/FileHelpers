using FileHelpers;

namespace FileHelpers.Tests
{
    /// <summary>
    /// Customer record that is separated by tabs
    /// </summary>
	[DelimitedRecord("\t")]
	public class CustomersTab
	{
        /// <summary>
        /// Field postion 1
        /// </summary>
		public string CustomerID;
        /// <summary>
        /// Field position 2
        /// </summary>
		public string CompanyName;
        /// <summary>
        /// Field position 3
        /// </summary>
		public string ContactName;
        /// <summary>
        /// Field position 4
        /// </summary>
		public string ContactTitle;
        /// <summary>
        /// Field position 5
        /// </summary>
		public string Address;
        /// <summary>
        /// Field position 6
        /// </summary>
		public string City;
        /// <summary>
        /// Field position 7
        /// </summary>
		public string Country;
	}

}