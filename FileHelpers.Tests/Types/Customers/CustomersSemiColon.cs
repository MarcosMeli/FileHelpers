namespace FileHelpers.Tests
{
    /// <summary>
    /// Create a file where records are delimitted by semi colon
    /// </summary>
    [DelimitedRecord(";")]
    public class CustomersSemiColon
    {
        /// <summary>
        /// Field position 1
        /// </summary>
        public string CustomerID;

        /// <summary>
        /// Field position 2
        /// </summary>
        public string CompanyName;

        /// <summary>
        /// field position 3
        /// </summary>
        public string ContactName;

        /// <summary>
        /// field position 4
        /// </summary>
        public string ContactTitle;

        /// <summary>
        /// field position 5
        /// </summary>
        public string Address;

        /// <summary>
        /// field positon 6
        /// </summary>
        public string City;

        /// <summary>
        /// field position 7
        /// </summary>
        public string Country;
    }
}