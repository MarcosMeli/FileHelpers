using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    public class WriteFile
        :IDemo
    {
        public void Run()
        {
            var engine = new FileHelperEngine<Orders>();
            
            var orders = new List<Orders>();

            var order1 = new Orders() {OrderID = 1, CustomerID = "AIRG", Freight = 82.43M, OrderDate = new DateTime(2009,05,01)};
            var order2 = new Orders() {OrderID = 2, CustomerID = "JSYV", Freight = 12.22M, OrderDate = new DateTime(2009,05,02)};

            orders.Add(order1);
            orders.Add(order2);

            engine.WriteFile("", orders);
        }

        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            public decimal Freight;
        }

    }


}
