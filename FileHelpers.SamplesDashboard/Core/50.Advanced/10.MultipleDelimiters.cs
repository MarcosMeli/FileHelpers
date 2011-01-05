using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    //-> {Example.Name:Multiple Delimiters}
    //-> {Example.Description:Write a file with different delimiters using the same record}

    public class MultipleDelimiters
        :IDemo
    {

        //-> {Example.File:RunEngine.cs}
        /// <summary>
        /// Run an example of writing a delimited file and 
        /// changing the delimiter to show how it is done.
        /// </summary>
        public void Run()
        {
            var customers = CreateCustomers();

            var engine = new DelimitedFileEngine<CustomersVerticalBar>();
            //  write out customers using a vertical bar delimiter (default)
            engine.WriteFile("Out_Vertical.txt", customers);

            // Change the delimiter to semicolon and write that out
            engine.Options.Delimiter = ";";
            engine.WriteFile("Out_SemiColon.txt", customers);

            // Change the delimiter to a tab and write that out
            engine.Options.Delimiter = "\t";
            engine.WriteFile("Out_Tab.txt", customers);

        }
        //-> {/Example.File}

        //-> {Example.File:CreateCustomers.cs}
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
        //-> {/Example.File}

        //-> {Example.File:CustomersVerticalBar.cs}
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
        //-> {/Example.File}
    }
}
