using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace ExamplesFramework
{
    //-> Name:Multi Record Processing
    //-> Description:Read or write a file with many different layouts

    public class MultiRecordSample
        : ExampleBase
    {
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

        //-> File:RunEngine.cs
        /// <summary>
        /// The idea of this engine is to parse files with different record types
        /// (this engine doesn't use any hierarical structure like the
        /// master-details, all the records are in linear relation for it).
        /// With the MultiRecordEngine you can parse also mixed delimited and
        /// fixed length records.
        /// </summary>
        public override void Run()
        {
            MultiRecordEngine engine;

            engine = new MultiRecordEngine(typeof(Orders),
                                            typeof(Customer),
                                            typeof(SampleType));
            engine.RecordSelector = new RecordTypeSelector(CustomSelector);

            object[] res = engine.ReadFile("Input.txt");

            foreach (object rec in res)
                this.Console.WriteLine(res.ToString());

        }
        //-> /File

        //-> File:Selector.cs
        /// <summary>
        /// This is the selector that determines the record type based on
        /// whatever criteria you write
        /// </summary>
        /// <param name="engine">Engine that is processing file</param>
        /// <param name="record">Record read from input</param>
        /// <returns>Record to accept this record</returns>
        Type CustomSelector(MultiRecordEngine engine, string record)
        {
            if (record.Length == 0)
                return null;

            if (Char.IsLetter(record[0]))
                return typeof(Customer);
            else if (record.Length == 14)
                return typeof(SampleType);
            else
                return typeof(Orders);
        }
        //-> /File

        //-> File:Customer.cs
        /// <summary>
        /// Sample class that is delimited by | default
        /// </summary>
        /// <remarks>
        /// Order of fields in the class is the same as the order in the file
        /// </remarks>
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

            //-> To display in the PropertyGrid.
            public override string ToString()
            {
                return CustomerID + " - " + CompanyName + ", " + ContactName;
            }
        }
        //-> /File

        //-> File:SampleType.cs
        /// <summary>
        /// Sample class that is Fixed length and has vrious data
        /// </summary>
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
        }
        //-> /File


        //-> File:Orders.cs
        /// <summary>
        /// Sample class that is breaks up a vertical bar delimitted file
        /// </summary>
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
        }
        //-> /File

        //-> File:example_multirecords.html
        /*<h2>Multi Record Engine Example</h2>
         * <blockquote>
         * <p>The idea of this engine is to parse files with different record types
         * (this engine doesn't use any hierarical structure like the master-details,
         * all the records are in linear relation for it).</p>
         * <p>With the MultiRecordEngine you can parse also mixed delimited and fixed
         * length records. For example, you can parse this strange file:</p>
         * ${Input.txt}
         * <p>This file contains <b>three</b> record types; Customers (begins with letters, | delimited),
         * Orders (begin with numbers, ';' delimited) and the sample type of the first example.</p>
         * <p>To work with this engine you must create one instance of it in this way:</p>
         * ${RunEngine.cs}
         * <p>In the res array you have all the records in the file, each one with the corresponding type.</p>
         * <p>And the Selector Method looks like this:</p>
         * ${Selector.cs}
         * <p> here are the three classes that read the different records</p>
         * ${Customer.cs}
         * <br/><br/>
         * ${SampleType.cs}
         * <br/><br/>
         * ${Orders.cs}
         * <p>Hope you find this useful.</p>
         * </blockquote>
         */
        //-> /File


    }
}
