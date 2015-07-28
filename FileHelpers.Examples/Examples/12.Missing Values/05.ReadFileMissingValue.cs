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
		//-> If your files contain a field that can be empty 
        //-> FileIn:Input.txt
/*10248|VINET|04071996|32.38
10249|TOMSP||11.61
10250|HANAR|08071996|65.83
10251|VICTE||41.34*/
        //-> /File

		// And the field type needs a value (int, DateTime, etc), you can make that fields Nullable<T>

        //-> File:RecordClass.cs
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime? OrderDate;

            public decimal? Freight;
        }

        //-> /File


        public override void Run()
        {
            //-> File:Example.cs
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("Input.txt");

            foreach (var record in records) {
                Console.WriteLine(record.CustomerID);
                Console.WriteLine(record.OrderDate);
                Console.WriteLine(record.Freight);
            }
            //-> /File
        }

		//-> Console

     
    }
}