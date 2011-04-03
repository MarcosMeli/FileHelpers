using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    /// <summary>
    /// Simple class with try catch and a value not in the enumerator
    /// </summary>
    public class SimpleErrorHandlingDemo
        : DemoParent
    {
        //-> {Example.Name:Simple Error handling}
        //-> {Example.Description:Read the file or reject the whole file}

        //-> {Example.File:RunEngine.cs}
        /// <summary>
        /// Run an example of running a file with an error through and
        /// using a try catch to collect the error.
        /// </summary>
        /// <remarks>
        /// In the standard mode you can catch the exceptions when something fails.
        /// </remarks>
        public override void Run()
        {
            try
            {
                var engine = new DelimitedFileEngine<Customer>();

                //  This fails with not in enumeration error
                Customer[] customers = engine.ReadFile("Input.txt");

                // this will not happen because of the exception
                foreach (var cust in customers)
                {
                    Console.WriteLine("Customer name {0} is a {1}",
                                      cust.ContactName,
                                      cust.ContactTitle);
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.ToString()); // with stack trace
                this.Console.WriteLine(ex.Message);
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
        ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
        FRANR|France restauration|Carine Schmitt|MarketingManager
        ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner*/
        //-> {/Example.File}

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

        //-> {Example.File:example_errors_simple.html}
        /* <h2>Simple Error Handling</h2>
         * <blockquote>
         * <p>In the standard mode you can catch the exceptions when something fail.</p>
         * ${RunEngine.cs}
         * <p>This approach not is bad but you lose some information about the current record
         * and you can't use the records array because is not assigned.</p>
         * <p>Example exception is:</p>
         * ${Console}
         * </blockquote>
         */
        //-> {/Example.File}
    }
}
