using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFx
{
    public class EnumConverterExample : ExampleBase
    {
        //-> Name:Enum Converter
        //-> Description:When you have a string field in your files that can be better handled if you map it to an enum.


        //-> FileIn:Input.txt
        /*ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
        ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner
        FRANR|France restauration|Carine Schmitt|MarketingManager
        ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner*/
        //-> /File

        //-> File:CustomerTitle.cs
        public enum CustomerTitle
        {
            Owner,
            SalesRepresentative,
            MarketingManager
        }
        //-> /File


        //-> File:Customers with Enum.cs
        [DelimitedRecord("|")]
        public class Customer
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            
            // Notice last feild is our enumerator
            public CustomerTitle ContactTitle;
        }

        //-> /File

        //-> File:RunEngine.cs
        public override void Run()
        {
            var engine = new DelimitedFileEngine<Customer>();

            //  Read input records, enumeration automatically converted
            Customer[] customers = engine.ReadFile("Input.txt");

            foreach (var cust in customers)
                Console.WriteLine("Customer name {0} is a {1}", cust.ContactName, cust.ContactTitle);
        }

        //-> /File

    }
}