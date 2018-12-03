using FileHelpers;

namespace ExamplesFx
{
    /// <summary>
    /// Simple class with use the error mode for a value not in the enumerator
    /// </summary>
    public class ErrorModeIgnoreAndContinue
        : ExampleBase
    {
        //-> Name: ErrorMode.IgnoreAndContinue
        //-> Description:Read the file dropping bad records

        //-> Another option is to ignore the errors and continue. Here is an example:

        //-> FileIn:Input.txt
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

        public override void Run()
        {
            //-> File:RunEngine.cs
            var engine = new DelimitedFileEngine<Customer>();

            // Switch error mode on
            engine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;

            //  This fails with not in enumeration error
            var customers = engine.ReadFile("Input.txt");

            // This wont display anything, we have dropped it
            foreach (var err in engine.ErrorManager.Errors) {
                Console.WriteLine();
                Console.WriteLine("Error on Line number: {0}", err.LineNumber);
                Console.WriteLine("Using record type: {0}", err.RecordTypeName);
                Console.WriteLine("Record causing the problem: {0}", err.RecordString);
                Console.WriteLine("Complete exception information: {0}", err.ExceptionInfo.ToString());
            }

            // This will display only 3 of the four records
            foreach (var cust in customers)
                Console.WriteLine("Customer name {0} is a {1}", cust.ContactName, cust.ContactTitle);

            //-> /File

        }

     


   
    }
}