using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFramework
{
    //-> {Example.Name:Write Delimited File}
    //-> {Example.Description:Example of how to write a Delimited File}

    /// <summary>
    /// Example of writing a delimited file using the generic engine
    /// </summary>
    public class WriteFile
        : ExampleBase
    {
        /// <summary>
        /// Execute engine and write out records we define in memory delimited by |
        /// </summary>
        public override void Run()
        {
            //-> {Example.File:Example.cs}
            var engine = new FileHelperEngine<Orders>();
            
            var orders = new List<Orders>();

            var order1 = new Orders() {OrderID = 1, CustomerID = "AIRG", Freight = 82.43M, OrderDate = new DateTime(2009,05,01)};
            var order2 = new Orders() {OrderID = 2, CustomerID = "JSYV", Freight = 12.22M, OrderDate = new DateTime(2009,05,02)};

            orders.Add(order1);
            orders.Add(order2);

            engine.WriteFile("Output.Txt", orders);
            //-> {/Example.File}
        }

        //-> {Example.File:RecordClass.cs}
        /// <summary>
        /// Layout for a file delimited by |
        /// </summary>
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            public decimal Freight;
        }
        //-> {/Example.File}

        //-> {Example.File: Output.Txt}
        //-> {/Example.File}

        //-> {Example.File: example_easy_write.html}
        /*<h2>Easy Write Example</h2>
         * <blockquote>
         * <p>To write an output file separated by a |:</p>
         * ${Output.Txt}
         * <p>You use the same Record Mapping Class as you would to read it:</p>
         * ${RecordClass.cs}
         * <p>Finally you must to instantiate a FileHelperEngine and write the file:</p>
         * ${Example.cs}
         * <p>The classes you use could come from anywhere,  Linq to Entities,
         * SQL database reads, or in this case classes created within an application.
        */
        //-> {/Example.File}
    }
}
