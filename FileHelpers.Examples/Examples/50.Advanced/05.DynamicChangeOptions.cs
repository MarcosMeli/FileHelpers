using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name: Engine Options
    //-> Description: Change the options of the engines at run time

    public class EngineOptions
        : ExampleBase
    {

        public override void Run()
        {
            var customers = CreateCustomers();

            var engine = new DelimitedFileEngine<CustomersVerticalBar>();

            engine.Options.Fields[2].TrimMode = TrimMode.Both;
            engine.Options.RemoveField("DummyField");
            //engine.Options.Fields[3].IsOptional

            //engine.ReadString(customers)

        }
        //-> /File

        //-> File:CreateCustomers.cs
        /// <summary>
        /// This routine reads the data and creates an array of Customers for our samples
        /// </summary>
        /// <returns>Array of customers</returns>
        private CustomersVerticalBar[] CreateCustomers()
        {
            //  6 records of sample data to parse
            string tempCustomers = @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain";

            // use the common engine to break down the records above
            return CommonEngine.ReadString<CustomersVerticalBar>(tempCustomers);
        }
        //-> /File

        //-> File:CustomersVerticalBar.cs
        /// <summary>
        /// Sample class that is delimited by | default
        /// </summary>
        /// <remarks>
        /// Order of fields in the class is the same as the order in the file
        /// </remarks>
        [DelimitedRecord("|")]
        public class CustomersVerticalBar
        {
            public string CustomerID;
            public string DummyField;
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

    }
}
