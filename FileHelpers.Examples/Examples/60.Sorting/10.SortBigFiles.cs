using System;
using FileHelpers;
using FileHelpers.Converters;

namespace ExamplesFx
{
    //-> Name:Sort Big File with Record Class
    //-> Runnable:false
    //-> Description:Shows how to sort a big file using a record class

    public class BigFileSort
        : ExampleBase
    {

        //-> If you need to sort a really big file (20Mb and more) you have the BigFileSorter

        //-> Implements <a href="http://en.wikipedia.org/wiki/External_sorting">External Sorting (wikipedia)</a>

        //-> The Sorter will split the file in blocks, write them to temp files, and finally join all in a sorted file

        public override void Run()
        {
            //-> File:SortingWithRecord.cs

            // OrdersTab must be IComparable<OrdersTab>
            
            // We recommend to split in blocks between 1 and 40 Mb
            var sorter = new BigFileSorter<OrdersTab>(10*1024*1024); // 10 Mb blocks

            sorter.Sort("unsorted.txt", "sorted.txt");

            //-> /File
        }



        //-> File:OrdersTab.cs
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

            #region IComparable<OrdersTab> Members

            public int CompareTo(OrdersTab other)
            {
                return OrderID.CompareTo(other.OrderID);
            }

            #endregion
        }

        //-> /File
    }
}