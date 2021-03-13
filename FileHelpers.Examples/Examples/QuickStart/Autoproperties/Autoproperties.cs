using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Examples.QuickStart.Autoproperties
{
    //-> Name: Autoproperties
    //-> Description: You can use autoproperties instead of fields:

    public class AutopropertiesSample
        : ExampleBase
    {
      
        //-> If you have a source file like this, separated by a "|":
      
        //-> FileIn:Input.txt
/*10248|VINET|04071996|32.38
10249|TOMSP|05071996|11.61
10250|HANAS|08071996|65.83
10251|VICTE|08071996|41.34*/
        //-> /FileIn


        //-> You can use autoproperties but for the moment you can't use Converters:

        //-> File:RecordClass.cs
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID { get; set; }

            public string CustomerID { get; set; }

            public string OrderDate { get; set; }

            public string Freight { get; set; }
        }

        //-> /File
        //-> Instantiate the FileHelperEngine and iterate over the records:
        protected override void Run()
        {
            //-> File:Example.cs
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("Input.txt");

            foreach (var record in records)
            {
                Console.WriteLine(record.CustomerID);
                Console.WriteLine(record.OrderDate);
                Console.WriteLine(record.Freight);
            }
            //-> /File
        }

    }
}
