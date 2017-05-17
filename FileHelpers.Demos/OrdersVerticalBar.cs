using FileHelpers;

namespace FileHelpersSamples
{
    /// <summary>
    /// Sample Orders class delimited by a vertical bar.
    /// Used by most examples to break up data and display
    /// it
    /// </summary>
    [DelimitedRecord("|")]
    public sealed class OrdersVerticalBar
    {
        public string OrderID;

        public string CustomerID;

        public string EmployeeID;

        public string OrderDate;

        public string RequiredDate;

        public string ShippedDate;

        public string ShipVia;

        public string Freight;

        public string OrderID2;

        public string CustomerID2;

        public string EmployeeID2;

        public string OrderDate2;

        public string RequiredDate2;

        public string ShippedDate2;

        public string ShipVia2;

        public string Freight2;
    }
}