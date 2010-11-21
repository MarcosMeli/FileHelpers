using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    public class ReadFile
        :IDemo
    {
        public void Run()
        {
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("");

            foreach (var record in records)
            {
                Console.WriteLine(record.CustomerID);
                Console.WriteLine(record.OrderDate.ToString("dd/MM/yyyy"));
                Console.WriteLine(record.Freight);
            }
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
