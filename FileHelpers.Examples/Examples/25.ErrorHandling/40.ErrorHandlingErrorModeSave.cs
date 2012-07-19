using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFramework
{
    /// <summary>
    /// Simple class with use the error mode for a value not in the enumerator
    /// Saving error output and recovering it
    /// </summary>
    public class ErrorSaveErrorHandlingExample
        : ExampleBase
    {
        //-> Name:ErrorMode saving Errors
        //-> Description:Read the file saving bad records

        //-> File:RunEngine.cs
        /// <summary>
        /// Run an example of running a file with an error using the
        /// ErrorMode option to capture bad records and then saving them
        /// </summary>
        public override void Run()
        {
            var engine = new DelimitedFileEngine<Customer>();

            // Switch error mode on
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            //  This fails with not in enumeration error
            Customer[] customers = engine.ReadFile("Input.txt");

            if (engine.ErrorManager.HasErrors)
                engine.ErrorManager.SaveErrors("errors.out");
            LoadErrors();
        }
        //-> /File

        //-> File:LoadErrors.cs
        /// <summary>
        /// Load errors and display on console
        /// </summary>
        private void LoadErrors()
        {

            // sometime later you can read it back
            ErrorInfo[] errors = ErrorManager.LoadErrors("errors.out");

            // This will display error from line 2 of the file.
            foreach (var err in errors)
            {
                this.Console.WriteLine();
                this.Console.WriteLine("Error on Line number: {0}", err.LineNumber);
                this.Console.WriteLine("Record causing the problem: {0}", err.RecordString);
                this.Console.WriteLine("Complete exception information: {0}", err.ExceptionInfo.ToString());
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

        //-> File:Errors.out
        //-> /File

        //-> File:example_errors_save.html
        /* <h2>Saving and Loading Errors</h2>
         * <blockquote>
         * <p>One interesting feature is the method in the ErrorManager to save the errors to a file,
         * you can do this as follows:</p>
         * ${RunEngine.cs}
         * <p>To load a file with errors you can use the static method:</p>
         * ${LoadErrors.cs}
         * </blockquote>
         */
        //-> /File

    }
}
