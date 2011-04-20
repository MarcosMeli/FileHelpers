using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using FileHelpers.Events;

namespace ExamplesFramework
{
    /// <summary>
    /// Example of the Read Before event
    /// </summary>
    public class ReadBeforeEventSample
        : ExampleBase
    {

        //-> {Example.Name:Read Before Event Handling}
        //-> {Example.Description:Show how to implement read before event}

        //-> {Example.File:RunEngine.cs}
        /// <summary>
        /// reads report.inp and skips all the records that are not detail records using a simple criteria
        /// </summary>
        public override void Run()
        {
            var engine = new FileHelperEngine<OrdersFixed>();
            engine.BeforeReadRecord += new BeforeReadHandler<OrdersFixed>(BeforeEvent);
            var result = engine.ReadFile("report.inp");

            foreach (var value in result)
            {
                Console.WriteLine("Customer: {0} Freight: {1}", value.CustomerID, value.Freight);
            }
        }
        //-> {/Example.File}

        //-> {Example.File:report.inp}
        /*-----------------------------------------------------
                      XXX Enterprise
        -----------------------------------------------------
        10249   TOMSP  05071996      11.61
        10250   HANAR  08071996      65.83
        10251   VICTE  08071996      41.34
                                               Page 1
        -----------------------------------------------------
                      YYY Enterprise
        -----------------------------------------------------
        10269   TOMSP  05071996      11.61
        10230   HANAR  08071996      65.83
        10151   VICTE  08071996      41.34
        */
        //-> {/Example.File}

        //-> {Example.File:Report layout.cs}
        /// <summary>
        /// Layout of the records we want for the report in report.inp
        /// </summary>
        /// <remarks>
        /// This only covers the detail records
        /// </remarks>
        [FixedLengthRecord(FixedMode.AllowVariableLength)]
        [IgnoreEmptyLines]
        public class OrdersFixed
        {
            [FieldFixedLength(7)]
            public int OrderID;

            [FieldFixedLength(8)]
            public string CustomerID;

            [FieldFixedLength(8)]
            public DateTime OrderDate;

            [FieldFixedLength(11)]
            public decimal Freight;
        }
        //-> {/Example.File}

        //-> {Example.File:EventHandler.cs}
        private void BeforeEvent(EngineBase engine, BeforeReadEventArgs<OrdersFixed> e)
        {
            if (e.RecordLine.StartsWith(" ") || e.RecordLine.StartsWith("-"))
                e.SkipThisRecord = true;

            //  Sometimes changing the record line can be useful, for example to correct for
            //  a bad data layout.  Here is an example of this, commented out for this example

            //if (e.RecordLine.StartsWith(" "))
            //   e.RecordLine = "Be careful!";
        }

        //-> {/Example.File}


    }
}
