using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFramework
{
    /// <summary>
    /// Simple class with use the error mode for a value not in the enumerator
    /// </summary>
    public class IgnoreModeErrorHandlingExample
        : ExampleBase
    {
        //-> Name:Ignore Mode Error handling
        //-> Description:Read the file dropping bad records

        //-> File:RunEngine.cs
        /// <summary>
        /// Run an example of running a file with an error using the
        /// IgnoreMode option to silently drop bad records
        /// </summary>
        public override void Run()
        {
            var engine = new DelimitedFileEngine<Customer>();

            // Switch error mode on
            engine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;

            //  This fails with not in enumeration error
            Customer[] customers = engine.ReadFile("Input.txt");

            // This wont display anything, we have dropped it
            foreach (var err in engine.ErrorManager.Errors)
            {
                Console.WriteLine();
                Console.WriteLine("Error on Line number: {0}", err.LineNumber);
                Console.WriteLine("Record causing the problem: {0}", err.RecordString);
                Console.WriteLine("Complete exception information: {0}", err.ExceptionInfo.ToString());
            }

            // This will display only 3 of the four records
            foreach (var cust in customers)
            {
                Console.WriteLine("Customer name {0} is a {1}", cust.ContactName, cust.ContactTitle);
            }
        }
        //-> /File

        //-> File:Customers with Enum.cs
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
        //-> /File

        //-> File:Input.txt
        /*ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
        ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
        FRANR|France restauration|Carine Schmitt|MarketingManager
        ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner*/
        //-> /File

        //-> File:TheEnumerator.cs
        /// <summary>
        /// Different titles describing position in company
        /// </summary>
        public enum CustomerTitle
        {
            Owner,
            SalesRepresentative,
            MarketingManager
        }
        //-> /File

        //-> File:example_errors_ignore.html
        /* <h2>Ignore and Continue Error Handling</h2>
         * <p>Another option is to ignore the errors and continue. Here is an example:</p>
         * ${RunEngine.cs}
         * <p>In the records array you only have the good records.</p>
         */
        //-> /File
    }
}
