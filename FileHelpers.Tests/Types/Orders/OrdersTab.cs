using System;
using FileHelpers.Converters;

namespace FileHelpers.Tests
{
    [DelimitedRecord("\t")]
    public class OrdersTab
        : IComparable<OrdersTab>
    {
        public int OrderID;

        public string CustomerID;

        public int EmployeeID;

        public DateTime OrderDate;

        [DateTimeConverter("ddMMyyyy")]
        public DateTime RequiredDate;

        [FieldNullValue(typeof (DateTime), "2005-1-1")]
        public DateTime ShippedDate;

        public int ShipVia;

        public decimal Freight;

        public int CompareTo(OrdersTab other)
        {
            return OrderID.CompareTo(other.OrderID);
        }
    }
}