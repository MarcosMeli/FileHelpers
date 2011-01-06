using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace Demos
{
    public class EnumConverterDemo : IDemo
    {
        //-> {Example.Name:Enum Converter Example}
        //-> {Example.Description:When you have a string field in your files that can be better handled if you map it to an enum.}

        //-> {Example.File:TheEnumerator.cs}
        /// <summary>
        /// Different titles describing position in company
        /// </summary>
        public enum CustomerTitle
        {
            Owner,
            SalesRepresentative,
            MarketingManager
        }
        //-> {/Example.File}


        //-> {Example.File:RunEngine.cs}
        /// <summary>
        /// Run an example of writing a delimited file and 
        /// changing the delimiter to show how it is done.
        /// </summary>
        public void Run()
        {
            var engine = new DelimitedFileEngine<Customer>();

            //  Read input records, enumeration automatically converted
            Customer[] customers =  engine.ReadFile("Input.txt");

            foreach (var cust in customers)
            {
                Console.WriteLine("Customer name {0} is a {1}", cust.ContactName, cust.ContactTitle);
            }
        }
        //-> {/Example.File}

        //-> {Example.File:Customers with Enum.cs}
        /// <summary>
        /// Sample customer class that is delimited by | default
        /// </summary>
        /// <remarks>
        /// Notice last feild is our enumerator
        /// </remarks>
        [DelimitedRecord("|")]
        public class Customer
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public CustomerTitle ContactTitle;
        }
        //-> {/Example.File}

        //-> {Example.File:Input.txt}
        /*ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
        ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner
        FRANR|France restauration|Carine Schmitt|MarketingManager
        ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner*/
        //-> {/Example.File}
    }

}