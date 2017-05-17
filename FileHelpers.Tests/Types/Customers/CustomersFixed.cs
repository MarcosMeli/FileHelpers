namespace FileHelpers.Tests
{
    /// <summary>
    /// Sample fixed length record layout
    /// </summary>
    [FixedLengthRecord]
    public class CustomersFixed
    {
        /// <summary>
        /// Customer id is first 11 characters
        /// </summary>
        [FieldFixedLength(11)]
        public string CustomerID;

        /// <summary>
        /// Company name is from position 12 through 50
        /// </summary>
        [FieldFixedLength(50 - 12)]
        public string CompanyName;

        /// <summary>
        /// Contact name is from position 50 through 72
        /// </summary>
        [FieldFixedLength(72 - 50)]
        public string ContactName;

        /// <summary>
        /// Contact title is position 72 through 110
        /// </summary>
        [FieldFixedLength(110 - 72)]
        public string ContactTitle;

        /// <summary>
        /// Address is from 110 through 151
        /// </summary>
        [FieldFixedLength(151 - 110)]
        public string Address;

        /// <summary>
        /// City is position 151 through 169
        /// </summary>
        [FieldFixedLength(169 - 151)]
        public string City;

        /// <summary>
        /// Country is just last 15 characters, no magic subtractions
        /// </summary>
        [FieldFixedLength(15)]
        public string Country;
    }
}