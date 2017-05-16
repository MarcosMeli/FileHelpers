using System;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name: FieldOrder
    //-> Description: Force field order with [FieldOrder] attribute

    public class DemoFieldOrder
        : ExampleBase
    {

        //-> FileIn:Input.txt
/*10248|VINET|04071996|32.38
10249|TOMSP|05071996|11.61
10250|HANAS|08071996|65.83
10251|VICTE|08071996|41.34*/
//-> /FileIn

        //-> File:RecordClass.cs
        [DelimitedRecord("|")]
        public class Orders
        {
            [FieldOrder(20)]
            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            [FieldOrder(30)]
            public DateTime OrderDate;

            [FieldConverter(ConverterKind.Decimal, ".")] // The decimal separator is .
            [FieldOrder(40)]
            public decimal Freight;

            [FieldOrder(10)]
            public int OrderID;

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