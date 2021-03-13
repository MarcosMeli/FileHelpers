using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Examples.QuickStart.WriteFileDelimited
{
    //-> Name:Write Delimited File
    //-> Description:Example of how to write a Delimited File
    //-> AutoRun:true

    public class WriteFileDelimited
        : ExampleBase
    {
        //-> To write an output file, separated by a "|":

        //-> FileOut: Output.txt

        // -> You use the same Record Mapping Class as you would to read it:
        //-> File:RecordClass.cs
        /// <summary>
        /// Layout for a file delimited by "|"
        /// </summary>
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            [FieldConverter(ConverterKind.Decimal, ".")] // The decimal separator is "."
            public decimal Freight;
        }

        //-> /File

        //-> Instantiate a FileHelperEngine and write the file:

        public override void Run()
        {
            //-> File:Example.cs
            var engine = new FileHelperEngine<Orders>();

            var orders = new List<Orders>();

            orders.Add(new Orders() {
                OrderID = 1,
                CustomerID = "AIRG",
                Freight = 82.43M,
                OrderDate = new DateTime(2009, 05, 01)
            });

            orders.Add(new Orders() {
                OrderID = 2,
                CustomerID = "JSYV",
                Freight = 12.22M,
                OrderDate = new DateTime(2009, 05, 02)
            });

            //engine now supports custom EOL for writing
            engine.NewLineForWrite = "\n";

            engine.WriteFile("Output.Txt", orders);

            //-> /File

            Console.WriteLine(engine.WriteString(orders));
        }

        //-> The classes you use could come from anywhere: LINQ to Entities, SQL database reads, or in this case, classes created within an application.
    }
}
