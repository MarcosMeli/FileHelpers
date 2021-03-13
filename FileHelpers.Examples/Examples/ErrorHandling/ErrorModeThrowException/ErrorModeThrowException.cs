using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Examples.ErrorHandling.ErrorModeThrowException
{
    /// <summary>
    /// Simple class with try catch and a value not in the enumerator
    /// </summary>
    public class ErrorModeThrowException
        : ExampleBase
    {
        //-> Name: ErrorMode.ThrowException
        //-> Description: Default Behavior. Read the file or reject the whole file

        //-> Run an example of running a file with an error through and using a try catch to collect the error.
        //-> In the standard mode you can catch the exceptions when something fails.


        //-> FileIn:Input.txt
        /*ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
        ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
        FRANR|France restauration|Carine Schmitt|MarketingManager
        ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner
*/
        //-> /File

        //-> File:Customers with Enum.cs

        [DelimitedRecord("|")]
        public class Customer
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public CustomerTitle ContactTitle;
        }

        public enum CustomerTitle
        {
            Owner,
            SalesRepresentative,
            MarketingManager
        }


        //-> /File

        protected override void Run()
        {
            //-> File:Example.cs
            try
            {
                var engine = new DelimitedFileEngine<Customer>();
                
                //  This fails with not in enumeration error
                var customers = engine.ReadFile("Input.txt");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); // with stack trace
            }
            //-> /File

        }

      
        

    }
}