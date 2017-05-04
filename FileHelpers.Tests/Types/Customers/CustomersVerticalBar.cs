using System;

namespace FileHelpers.Tests
{
    [DelimitedRecord("|")]
    public class CustomersVerticalBar
      : IComparable<CustomersVerticalBar>
    {
        public string CustomerID;

        public string CompanyName;

        public string ContactName;

        public string ContactTitle;

        public string Address;
        public string City;
        public string Country;

        public int CompareTo(CustomersVerticalBar other)
        {
            return CustomerID.CompareTo(other.CustomerID);

        }
    }
}