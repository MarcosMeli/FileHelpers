using FileHelpers;

namespace ExamplesFx
{
    /// <summary>
    /// Simple class with use the error mode for a value not in the enumerator
    /// Saving error output and recovering it
    /// </summary>
    public class ErrorModeSaveAndContinue
        : ExampleBase
    {
        //-> Name: ErrorMode SaveAndContinue
        //-> Description:Read the file saving bad records


        //-> File:Input.txt
        /*ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
        ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
        FRANR|France restauration|Carine Schmitt|MarketingManager
        ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner*/
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

        //-> File:RunEngine.cs

        public override void Run()
        {
            var engine = new DelimitedFileEngine<Customer>();

            // Switch error mode on
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            //  This fails with not in enumeration error
            var customers = engine.ReadFile("Input.txt");

            if (engine.ErrorManager.HasErrors)
                engine.ErrorManager.SaveErrors("errors.out");

            LoadErrors();
        }

        private void LoadErrors()
        {
            // sometime later you can read it back using:
            ErrorInfo[] errors = ErrorManager.LoadErrors("errors.out");

            // This will display error from line 2 of the file.
            foreach (var err in errors) {
                Console.WriteLine();
                Console.WriteLine("Error on Line number: {0}", err.LineNumber);
                Console.WriteLine("Record causing the problem: {0}", err.RecordString);
                Console.WriteLine("Complete exception information: {0}", err.ExceptionInfo.ToString());
            }
        }

        //-> /File
        
        //-> FileOut:Errors.out

    }
}