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

            // Notice last field is our enumeration
            // argument "s" means converting to string representation of enum value
            // argument "n" means converting as integer representation of enum value
            // omitting FieldConverterAttribute means that enum members will be written
            //   as their string representation
            // Note: this attribute makes sense only when writing records - when reading, 
            //   converter automatically supports both string and integer representation
            //   of enum members
            [FieldConverter(typeof(CustomerTitle),"s")]
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
