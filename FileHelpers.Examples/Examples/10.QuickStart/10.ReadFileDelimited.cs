using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name: Read Delimited File
    //-> Description: How to read a Delimited File
    //-> AutoRun: true

    public class ReadFileDelimited
        : ExampleBase
    {
      
        //-> If you have a source file like this, separated by a |:
      
        //-> FileIn:Input.txt
/*10248|VINET|04071996|32.38
10249|TOMSP|05071996|11.61
10250|HANAS|08071996|65.83
10251|VICTE|08071996|41.34*/
        //-> /FileIn


        //-> You first declare a Record Mapping Class:

        //-> File:RecordClass.cs
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            [FieldConverter(ConverterKind.Decimal, ".")] // The decimal separator is .
            public decimal Freight;
        }

        //-> /File

        //->  Instantiate a FileHelperEngine and read or write files:

        public override void Run()
        {
            //-> File:Example.cs
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("Input.txt");

            foreach (var record in records)
            {
                Console.WriteLine(record.CustomerID);
                Console.WriteLine(record.OrderDate.ToString("dd/MM/yyyy"));
                Console.WriteLine(record.Freight);
            }
            //-> /File
        }

        /*->Html: <p>Now you have an Orders array named <span class="code">res</span> where
                  every item in the array is an Order object. If you want to access one of the fields
                  let the Visual Studio IntelliSense bring up the field names for you.</p>
                  <div class="indent">
                     <img alt="Visual studio intellisense" src="data:image/png;base64,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" />
                  </div>
         */
    }
}