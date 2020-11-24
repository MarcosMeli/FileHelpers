using System;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name:Write Delimited File
    //-> Description:Example of how to write a Delimited File
    //-> AutoRun:true

    public class PaddingConverterExample
        : ExampleBase
    {
        //-> To write an output file (separated by a "|"):

        //-> FileOut: Output.txt

        // -> You use the same Record Mapping Class as you would to read it:
        //-> File:RecordClass.cs
        /// <summary>
        /// Layout for a file delimited by |
        /// </summary>
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            [FieldConverter(ConverterKind.Padding, 8, PaddingMode.Left, '0')]
            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            [FieldConverter(ConverterKind.Decimal, ".")] // The decimal separator is .
            public decimal Freight;
        }

        //-> /File

        //-> Instantiate a FileHelperEngine and write the file:

        public override void Run()
        {
            //-> File:Example.cs
            var engine = new FileHelperEngine<Orders>();

            var orders = new List<Orders>();

            orders.Add(new Orders()
            {
                OrderID = 1,
                CustomerID = "9001",
                Freight = 82.43M,
                OrderDate = new DateTime(2009, 05, 01)
            });

            orders.Add(new Orders()
            {
                OrderID = 2,
                CustomerID = "9002",
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
