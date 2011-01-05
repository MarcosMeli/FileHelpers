using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    //-> {Example.Name:Read Delimited File}
    //-> {Example.Description:Example of how to read a Delimited File}

    /// <summary>
    /// Example of reading a simple file delimited by | using the Generic Engine
    /// </summary>
    public class ReadFile
        :IDemo
    {

        //-> {Example.File:Example.cs}

        /// <summary>
        /// Execute the engine and get some results
        /// </summary>
        public void Run()
        {
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("Input.txt");

            foreach (var record in records)
            {
                Console.WriteLine(record.CustomerID);
                Console.WriteLine(record.OrderDate.ToString("dd/MM/yyyy"));
                Console.WriteLine(record.Freight);
            }
        }
        //-> {/Example.File}

        //-> {Example.File:RecordClass.cs}

        /// <summary>
        /// Our class we are reading using FileHelpers,  the record breakdown
        /// </summary>
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

        //-> {Example.File:Input.txt}
        /*10248|VINET|04071996|32.38
        10249|TOMSP|05071996|11.61
        10250|HANAR|08071996|65.83
        10251|VICTE|08071996|41.34*/
        //-> {/Example.File}
    }

}
