using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.Examples.Advanced.MultipleDelimiters
{
    //-> Name:Multiple Delimiters
    //-> Description:Write a file with different delimiters using the same record

    public class MultipleDelimiters
        : ExampleBase
    {

        //-> File:CustomersVerticalBar.cs
        /// <summary> Sample class that is delimited by | default </summary>
        [DelimitedRecord("|")]
        public class CustomersVerticalBar
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;

        }

        //-> /File


        public override void Run()
        {
            //-> File:RunEngine.cs

            var customers = CreateCustomers();

            var engine = new DelimitedFileEngine<CustomersVerticalBar>(Encoding.UTF8);
            //  write out customers using a vertical bar delimiter (default)
            engine.WriteFile("Out_Vertical.txt", customers);

            // Change the delimiter to semicolon and write that out
            engine.Options.Delimiter = ";";
            engine.WriteFile("Out_SemiColon.txt", customers);

            // Change the delimiter to a tab and write that out
            engine.Options.Delimiter = "\t";
            engine.WriteFile("Out_Tab.txt", customers);

            //-> /File
        }


        private CustomersVerticalBar[] CreateCustomers()
        {
            //  6 records of sample data to parse
            string tempCustomers =
                @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain";

            // use the common engine to break down the records above
            return CommonEngine.ReadString<CustomersVerticalBar>(tempCustomers);
        }


        //-> FileOut: Out_Vertical.txt

        //-> FileOut: Out_SemiColon.txt

        //-> FileOut: Out_Tab.txt
    }
}