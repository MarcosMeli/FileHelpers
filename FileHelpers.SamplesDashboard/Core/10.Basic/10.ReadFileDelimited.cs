using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    //-> {Example.Name:Read Delimited File}
    //-> {Example.Description:Example of how to read a Delimited File}

    /// <summary>
    /// Example of reading a simple file delimited by | using the Generic Engine
    /// </summary>
    public class ReadFile
        : DemoParent
    {

        //-> {Example.File:Example.cs}
        /// <summary>
        /// Execute the engine and get some results
        /// </summary>
        public override void Run()
        {
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("Input.txt");

            foreach (var record in records)
            {
                this.Console.WriteLine(record.CustomerID);
                this.Console.WriteLine(record.OrderDate.ToString("dd/MM/yyyy"));
                this.Console.WriteLine(record.Freight);
            }
        }
        //-> {/Example.File}

        //-> {Example.File:RecordClass.cs}

        /// <summary>
        /// Our class we are reading using FileHelpers,  the record breakdown
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

        //-> {Example.File:Input.txt}
        /*10248|VINET|04071996|32.38
        10249|TOMSP|05071996|11.61
        10250|HANAR|08071996|65.83
        10251|VICTE|08071996|41.34*/
        //-> {/Example.File}

        //-> {Example.File:example_easy.html}
        /*<h2>Easy Example </h2>
         * <blockquote>
         * <p>If you have a source file like this, separated by a |:</p>
         * ${Input.txt}
         * <p>You first declare a Record Mapping Class:</p>
         * ${RecordClass.cs}
         * <p>Finally you must to instantiate a FileHelperEngine and read or write files:</p>
         * ${Example.cs}
         * <p>Now you have an Orders array named <span class="cs-literal">res</span> where
         * every item in the array is an Order object. If you want to access one of the fields
         * let the Visual Studio IntelliSense bring up the field names for you.</p>
         * <blockquote>
         * <img height="93" src="${URL}vs_orders.png" width="165" alt="Visual studio intellisense"/>
         * </blockquote>
         */
        //-> {/Example.File}
    }
}
