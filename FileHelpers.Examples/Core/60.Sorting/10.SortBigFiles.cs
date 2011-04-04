using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFramework
{
    //-> {Example.Name:Sort Big File with Record Class}
    //-> {Example.Runnable:false}
    //-> {Example.Description:Shows how to sort a big file using a record class}

    public class BigFileSort
        : ExampleBase
    {

        //-> {Example.File:SortingWithRecord.cs}
        public override void Run()
        {
            // Implements http://en.wikipedia.org/wiki/External_sorting
            // OrdersTab is IComparable<OrdersTab>

            var sorter = new BigFileSorter<OrdersTab>(10 * 1024 * 1024); // 10 Mb
            sorter.DeleteTempFiles = true;
            sorter.Sort("unsorted.txt", "sorted.txt");
        }

        //-> {/Example.File}


        //-> {Example.File:OrdersTab.cs}
        /// <summary>
        /// Sample class that is delimited by tab
        /// </summary>
        [DelimitedRecord("\t")]
        public class OrdersTab
            :IComparable<OrdersTab>
        {
            public int OrderID;

            public string CustomerID;

            public int EmployeeID;

            public DateTime OrderDate;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime RequiredDate;

            [FieldNullValue(typeof(DateTime), "2005-1-1")]
            public DateTime ShippedDate;

            public int ShipVia;

            public decimal Freight;


            #region IComparable<OrdersTab> Members

            public int CompareTo(OrdersTab other)
            {
                return this.OrderID.CompareTo(other.OrderID);
            }

            #endregion
        }
        //-> {/Example.File}

    }
}
