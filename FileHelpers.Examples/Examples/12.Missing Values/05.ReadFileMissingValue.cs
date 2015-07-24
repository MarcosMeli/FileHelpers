using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name: Error when reading file with missing values
    //-> Description:Example of the error you get when read a file with some missing values and use the FieldNullValue attribute

    public class ReadFileMissingValue
        : ExampleBase
    {

        //-> FileIn:Input.txt
/*10248|VINET|04071996|32.38
10249|TOMSP||11.61
10250|HANAR|08071996|65.83
10251|VICTE|08071996|41.34*/
        //-> /File


        //-> File:RecordClass.cs
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

            foreach (var record in records) {
                Console.WriteLine(record.CustomerID);
                Console.WriteLine(record.OrderDate.ToString("dd/MM/yyyy"));
                Console.WriteLine(record.Freight);
            }
            //-> /File
        }


     
    }
}