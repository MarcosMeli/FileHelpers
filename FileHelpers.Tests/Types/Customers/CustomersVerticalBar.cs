using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Tests
{
    [DelimitedRecord("|")]
    public class CustomersVerticalBar
        : IComparableRecord<CustomersVerticalBar>
    {
        public string CustomerID;
        public string CompanyName;
        public string ContactName;
        public string ContactTitle;
        public string Address;
        public string City;
        public string Country;

        public bool IsEqualRecord(CustomersVerticalBar record)
        {
            return this.CustomerID == record.CustomerID;
        }
    }
}