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

        public void Run()
        {
            //-> {Example.File:Example.cs}
            var customers = CreateCustomers();

            DelimitedFileEngine<CustomersVerticalBar> engine = new DelimitedFileEngine<CustomersVerticalBar>();
            engine.WriteFile("Out_Vertical.txt", customers);

            engine.Options.Delimiter = ";";
            engine.WriteFile("Out_SemiColon.txt", customers);

            engine.Options.Delimiter = "\t";
            engine.WriteFile("Out_Tab.txt", customers);

            //-> {/Example.File}
        }

        private CustomersVerticalBar[] CreateCustomers()
        {
            string tempCustomers = @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain";
            return CommonEngine.ReadString<CustomersVerticalBar>(tempCustomers);
        }

        //-> {Example.File:CustomersVerticalBar.cs}
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
