using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Tests
{
    [DelimitedRecord("|")]
    public class CustomersVerticalBarWithFieldCaption
        : IComparable<CustomersVerticalBar>
    {
        [FieldCaption("Customer ID")]
        public string CustomerID;

        [FieldCaption("Company Name")]
        public string CompanyName;

        [FieldCaption("Contact Name")]
        public string ContactName;

        [FieldCaption("Contact Title")]
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