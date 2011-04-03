using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace Demos
{
    public class EnumConverterDemo : DemoParent
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
        public override void Run()
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

        //-> {Example.File:example_enumconverter.html}
        /* <h2>Enum Converter Example</h2>
         * <blockquote>
         * <p>Sometimes you have a string field in your files that can be better handled if you map it to an enum.</p>
         * <p>Thanks to <b>Derek Fluker,</b> you can automatically use an enum without defining any
         * converter. The FileHelpers library parses the field and performs a case insensitive
         * comparison to the enum values and assigns the correct one.</p>
         * <p>The customer file is an excellent sample:</p>
         * ${Input.txt}
         * <p>The enum is:</p>
         * ${TheEnumerator.cs}
         * <p>When defining your record class use the enum:</p>
         * ${Customers with Enum.cs}
         * <p>Done !! you parse the file with:</p>
         * ${RunEngine.cs}
         * </blockquote>
         */
        //-> {/Example.File}
    }

}