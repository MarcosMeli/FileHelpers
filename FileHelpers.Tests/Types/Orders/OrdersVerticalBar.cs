using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Tests
{
    [DelimitedRecord("|")]
    public class OrdersVerticalBar
    {
        public int OrderID;

        public string CustomerID;

        public int EmployeeID;

        public DateTime OrderDate;

        public DateTime RequiredDate;

        [FieldNullValue(typeof (DateTime), "2005-1-1")]
        public DateTime ShippedDate;

        public int ShipVia;

        public decimal Freight;
    }
}