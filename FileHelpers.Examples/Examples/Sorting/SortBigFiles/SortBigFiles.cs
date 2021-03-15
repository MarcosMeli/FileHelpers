using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Examples.Sorting.SortBigFiles
{
    //-> Name:Sort Big File with Record Class
    //-> Description:Shows how to sort a big file using a record class
    public class BigFileSort
        : ExampleBase
    {

        //-> If you need to sort a really big file (20Mb and more) you have the BigFileSorter

        //-> Implements <a href="http://en.wikipedia.org/wiki/External_sorting">External Sorting (wikipedia)</a>

        //-> The Sorter will split the file in blocks, write them to temp files, and finally join all in a sorted file

        protected override void Run()
        {
            //-> File:SortingWithRecord.cs

            // OrdersTab must be IComparable<OrdersTab>
            
            // We recommend to split in blocks between 1 and 40 Mb
            var sorter = new BigFileSorter<OrdersTab>(10*1024*1024); // 10 Mb blocks

            sorter.Sort("input.txt", "sorted.txt");

            //-> /File
        }



        //-> File:OrdersTab.cs
        [DelimitedRecord(",")]
        public class OrdersTab
            : IComparable<OrdersTab>
        {
            public int OrderId;

            public string CustomerId;

            [FieldConverter(ConverterKind.Date, "d-M-yyyy")]
            public DateTime OrderDate;

            public DateTime RequiredDate;

            #region IComparable<OrdersTab> Members

            public int CompareTo(OrdersTab other)
            {
                return OrderId.CompareTo(other.OrderId);
            }

            #endregion
        }

        //-> /File
    }
}