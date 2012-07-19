using System;
using System.Collections;
using System.Collections.Generic;
using ExamplesFramework.Properties;
using FileHelpers;

namespace ExamplesFramework
{
    //-> Name: Read Delimited File
    //-> Description: Example of how to read a Delimited File
    //-> AutoRun: true

    public class ReadFile
        : ExampleBase
    {
        public ReadFile()
        {
            HtmlBody = 
@"<p>If you have a source file like this, separated by a |:</p>
          ${Input.txt}
          <p>You first declare a Record Mapping Class:</p>
          ${RecordClass.cs}
          <p>Finally you must to instantiate a FileHelperEngine and read or write files:</p>
          ${Example.cs}
          <p>Now you have an Orders array named <span class=""cs-literal"">res</span> where
          every item in the array is an Order object. If you want to access one of the fields
          let the Visual Studio IntelliSense bring up the field names for you.</p>
          <blockquote>
          <img height='93' src='${URL}vs_orders.png' width='165' alt='Visual studio intellisense'/>
";

        }

        //->Html: <p>If you have a source file like this, separated by a |:</p>

        //-> File:Input.txt
        /*10248|VINET|04071996|32.38
        10249|TOMSP|05071996|11.61
        10250|HANAS|08071996|65.83
        10251|VICTE|08071996|41.34*/
        //-> /File

        //->Html: <p>You first declare a Record Mapping Class:</p>

        //-> File:RecordClass.cs
        /// <summary> Our class we are reading using FileHelpers,  the record breakdown </summary>
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            public decimal Freight;
        }
        //-> /File

       

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


    }
}
