using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name: Read File with Nullable Types
    //-> Description:Example of how to read a file with some missing values and use <b>Nullable Types</b>

    public class ReadFileNullable
        : ExampleBase
    {

        public override void Run()
        {
            //-> File:Example.cs
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("Input.txt");

            foreach (var record in records)
            {
                Console.WriteLine(record.CustomerID);
                if (record.OrderDate.HasValue)
                    Console.WriteLine(record.OrderDate.Value.ToString("dd/MM/yyyy"));
                else
                    Console.WriteLine("No Date");
                Console.WriteLine(record.Freight);
            }
            //-> /File
        }

        //-> File:RecordClass.cs
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime? OrderDate;

            public decimal Freight;
        }
        //-> /File

        //-> File:Input.txt
        /*10248|VINET|04071996|32.38
        10249|TOMSP||11.61
        10250|HANAR|08071996|65.83
        10251|VICTE|08071996|41.34*/
        //-> /File

        //-> File:example_easy.html
        /*<h2>Easy Example </h2>
         * <blockquote>
         * <p>If you have a source file like this, separated by a |:</p>
         * ${Input.txt}
         * <p>You first declare a Record Mapping Class:</p>
         * ${RecordClass.cs}
         * <p>Finally you must to instantiate a FileHelperEngine and read or write files:</p>
         * ${Example.cs}
         * <p>Now you have an Orders array named <span class="cs-literal">res</span> where
         * every item in the array is an Order object. If you want to access one of the fields
         * let the Visual Studio IntelliSense bring up the field names for you.</p>
         * <blockquote>
         * <img height="93" src="${URL}vs_orders.png" width="165" alt="Visual studio intellisense"/>
         * </blockquote>
         */
        //-> /File
    }
}
