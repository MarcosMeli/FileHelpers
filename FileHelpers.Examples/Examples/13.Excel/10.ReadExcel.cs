using FileHelpers;
using FileHelpers.DataLink;
using FileHelpers.ExcelNPOIStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamplesFx
{
    //-> Name: Create workbook and Insert records 
    //-> Runnable: true
    //-> Description: Shows how to sort a big file without a record class

    public class ReadExcel :
    ExampleBase
    {
        //-> If you need to sort a really big file (20Mb and more) you have the BigFileSorter

        //-> Implements <a href="http://en.wikipedia.org/wiki/External_sorting">External Sorting (wikipedia)</a>

        //-> The Sorter will split the file in blocks, write them to temp files, and finally join all in a sorted file

        public override void Run()
        {
            //-> File:ExcelExample.cs

            // Create a record first

            ExcelNPOIStorage storage = new ExcelNPOIStorage(typeof(Customer), "ExcelTest.xlsx", 1, 1);

            // Create excel file
            //ExcelStorage storage = new ExcelStorage(typeof(Customer), "ExcelTest.xlsx", 1, 1);
            storage.SheetName = "Sheet 1";

            int count = 3;
            Customer[] data = new Customer[count];


            data[0] = new Customer();
            data[0].CustId = 0;
            data[0].Name = "Prvi";

            data[1] = new Customer();
            data[1].CustId = 1;
            data[1].Name = "Drugi";

            data[2] = new Customer();
            data[2].CustId = 3;
            data[2].Name = "Treci";


            // Insert data to excel file and save it
            storage.InsertRecords(data);

           


            //-> /File
        }


        //-> Customer.cs
        [DelimitedRecord("")]
        public class Customer
        {

            public int CustId;

  
            [FieldTrim(TrimMode.Both)]
            public string Name;
        }
        //-> /File



        //-> File:ExcelTest.xlsx
        //-> /File
   



    }
}
