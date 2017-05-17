using System;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name: Multi Record Processing
    //-> Description: Read or write a file with many different layouts

    public class MultiRecordSample
        : ExampleBase
    {
        //-> The idea of this engine is to parse files with different record types (this engine doesn't use any hierarchical structure like the master-details, all the records are in linear relation for it).
        //-> With the MultiRecordEngine you can parse also mixed delimited and fixed length records.
        //-> For example, you can parse this strange file:

        //-> File:Input.txt
        /*10248|VINET|5|04071996|01081996|16071996|3|32.38  
10249|TOMSP|6|05071996|16081996|10071996|1|11.61
ALFKI;Alfreds Futterkiste;Maria Anders;Sales Representative;Obere Str. 57;Berlin;Germany
ANATR;Ana Trujillo Emparedados y helados;Ana Trujillo;Owner;Avda. de la Constitución 2222;México D.F.;Mexico
10250|HANAR|4|08071996|05081996|12071996|2|65.83
10111314012345
11101314123456
10251|VICTE|3|08071996|05081996|15071996|1|41.34
11121314901234
10101314234567
ANTON;Antonio Moreno Taquería;Antonio Moreno;Owner;Mataderos  2312;México D.F.;Mexico
BERGS;Berglunds snabbköp;Christina Berglund;Order Administrator;Berguvsvägen  8;Luleå;Sweden
*/
        //-> /File

        //-> This file contains<b> three</b> record types; Customers(begins with letters, | delimited), Orders(begin with numbers, ';' delimited) and the sample type of the first example.


        //-> File:Customer.cs
        [DelimitedRecord(";")]
        public class Customer
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;

            public override string ToString()
            {
                return "Customer: " + CustomerID + " - " + CompanyName + ", " + ContactName;
            }
        }

        //-> /File

        //-> File:SampleType.cs
        [FixedLengthRecord]
        public class SampleType
        {
            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime Field1;

            [FieldFixedLength(3)]
            [FieldAlign(AlignMode.Left, ' ')]
            [FieldTrim(TrimMode.Both)]
            public string Field2;

            [FieldFixedLength(3)]
            [FieldAlign(AlignMode.Right, '0')]
            [FieldTrim(TrimMode.Both)]
            public int Field3;

            public override string ToString()
            {
                return "SampleType: " + Field2 + " - " + Field3;
            }
        }

        //-> /File


        //-> File:Orders.cs
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;

            public int EmployeeID;

            public DateTime OrderDate;

            public DateTime RequiredDate;

            [FieldNullValue(typeof(DateTime), "2005-1-1")]
            public DateTime ShippedDate;

            public int ShipVia;

            public decimal Freight;

            public override string ToString()
            {
                return "Orders: " + OrderID + " - " + CustomerID + " - " + Freight;
            }
        }

        //-> /File

        //-> To work with this engine you must create one instance of it in this way:

        public override void Run()
        {
            //-> File:RunEngine.cs

            var engine = new MultiRecordEngine(typeof (Orders),
                typeof (Customer),
                typeof (SampleType));

            engine.RecordSelector = new RecordTypeSelector(CustomSelector);

            var res = engine.ReadFile("Input.txt");

            foreach (var rec in res)
                Console.WriteLine(rec.ToString());

            //-> /File

        }

        //-> This is the selector that determines the record type based on whatever criteria you write

        //-> File:Selector.cs
        private Type CustomSelector(MultiRecordEngine engine, string recordLine)
        {
            if (recordLine.Length == 0)
                return null;

            if (Char.IsLetter(recordLine[0]))
                return typeof (Customer);
            else if (recordLine.Length == 14)
                return typeof (SampleType);
            else
                return typeof (Orders);
        }

        //-> /File


    }
}