using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;
using FileHelpers.Events;

namespace ExamplesFx
{
    /// <summary>
    /// Example of the Read After event
    /// </summary>
    public class ReadAfterEventSample
        : ExampleBase
    {
        //-> Name:After Read Event Handling
        //-> Description:Show how to implement read after event

        //-> File:RunEngine.cs
        /// <summary>
        /// Read a simple file and ignore zero value freight using a Read After Event
        /// </summary>
        public override void Run()
        {
            var engine = new FileHelperEngine<OrdersFixed>();
            engine.AfterReadRecord += AfterEvent;

            var result = engine.ReadFile("Input.txt");

            foreach (var value in result)
                Console.WriteLine("Customer: {0} Freight: {1}", value.CustomerID, value.Freight);
        }

        private void AfterEvent(EngineBase engine, AfterReadEventArgs<OrdersFixed> e)
        {
            //  we want to drop all records with no freight
            if (e.Record.Freight == 0)
                e.SkipThisRecord = true;
        }

        //-> /File

        //-> File:Input.txt
        /*10249   TOMSP  05071996      11.61
        10250   HANAR  08071996       0.00
        10251   VICTE  08071996      41.34
        10269   TOMSP  05071996      11.61
        10230   HANAR  08071996      65.83
        10151   VICTE  08071996      41.34
        */
        //-> /File

        //-> File:Report layout.cs
        /// <summary>
        /// Layout of the all input records from Input.txt
        /// </summary>
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

        //-> /File
    }
}