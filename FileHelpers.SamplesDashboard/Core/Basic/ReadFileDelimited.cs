using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    //-> {Example.Name:Read Delimited File}
    //-> {Example.Description:Example of how to read a Delimited File}

    public class ReadFile
        :IDemo
    {
        public void Run()
        {
            //-> {Example.File:Example.cs}
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("");

            foreach (var record in records)
            {
                Console.WriteLine(record.CustomerID);
                Console.WriteLine(record.OrderDate.ToString("dd/MM/yyyy"));
                Console.WriteLine(record.Freight);
            }
            //-> {/Example.File}
        }

        //-> {Example.File:OrdersRecord.cs}
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            public decimal Freight;
        }
        //-> {/Example.File}

    }


}
