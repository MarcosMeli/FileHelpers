using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using FileHelpers.Events;

namespace Demos
{
    /// <summary>
    /// Example of the Write After event
    /// </summary>
    public class WriteAfterEventSample
        : IDemo
    {

        //-> {Example.Name:Write After Event Handling}
        //-> {Example.Description:Show how to implement write after event}

        //-> {Example.File:RunEngine.cs}
        /// <summary>
        /// Run a record through engine using the write event to filter out unwanted details
        /// </summary>
        public void Run()
        {
            var engine = new FileHelperEngine<OrdersFixed>();

            var result = engine.ReadFile("Input.txt");

            //  add our filter logic.
            engine.AfterWriteRecord += new AfterWriteHandler<OrdersFixed>(AfterWriteEvent);
            engine.WriteFile("output.txt", result);
        }
        //-> {/Example.File}

        //-> {Example.File:EventHandler.cs}
        private void AfterWriteEvent(EngineBase engine, AfterWriteEventArgs<OrdersFixed> e)
        {
           //  We only want clients with large frieght values
            if (e.Record.CustomerID == "HANAR" )
                e.RecordLine = "Insufficient Access";
        }
        //-> {/Example.File}

        //-> {Example.File:Input.txt}
        /*10249   TOMSP  05071996      11.61
        10250   HANAR  08071996       0.00
        10251   VICTE  08071996      41.34
        10269   TOMSP  05071996      11.61
        10230   HANAR  08071996      65.83
        10151   VICTE  08071996      41.34
        */
        //-> {/Example.File}

        //-> {Example.File:output.txt}
        //-> {/Example.File}

        //-> {Example.File:Report layout.cs}
        /// <summary>
        /// Layout of the records we want for the report in Input.txt
        /// </summary>
        [FixedLengthRecord(FixedMode.AllowVariableLength)]
        [IgnoreEmptyLines]
        public class OrdersFixed
        {
            [FieldFixedLength(7)]
            public int OrderID;

            [FieldFixedLength(8)]
            [FieldTrim(TrimMode.Both)]
            public string CustomerID;

            [FieldFixedLength(8)]
            public DateTime OrderDate;

            [FieldFixedLength(11)]
            public decimal Freight;
        }
        //-> {/Example.File}

    }
}