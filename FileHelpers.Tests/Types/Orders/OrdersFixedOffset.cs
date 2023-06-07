using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FileHelpers.Tests.Types.Orders
{
    [FixedLengthRecord]
    public class OrdersFixedOffset
    {
        [FieldFixedLength(7)]
        public int OrderID;

        [FieldFixedLength(12, Offset = 2)]
        public string CustomerID;

        [FieldFixedLength(3, Offset = 3)]
        public int EmployeeID;

        [FieldFixedLength(10)]
        public DateTime OrderDate;

        [FieldFixedLength(10)]
        public DateTime RequiredDate;

        [FieldFixedLength(10)]
        [FieldNullValue(typeof(DateTime), "2005-1-1")]
        public DateTime ShippedDate;

        [FieldFixedLength(3)]
        public int ShipVia;

        [FieldFixedLength(10)]
        public decimal Freight;
    }
}
