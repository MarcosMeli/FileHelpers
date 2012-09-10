﻿using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;
using FileHelpers.Events;

namespace ExamplesFramework
{
    /// <summary>
    /// Example of the Write After event
    /// </summary>
    public class WriteAfterEventSample
        : ExampleBase
    {

        //-> Name:After Write Event Handling
        //-> Description:Show how to implement write after event

        //-> File:RunEngine.cs
        /// <summary>
        /// Run a record through engine using the write event to filter out unwanted details
        /// </summary>
        public override void Run()
        {
            var engine = new FileHelperEngine<OrdersFixed>();

            var result = engine.ReadFile("Input.txt");

            //  add our filter logic.
            engine.AfterWriteRecord += AfterWriteEvent;
            engine.WriteFile("output.txt", result);
        }

        private void AfterWriteEvent(EngineBase engine, AfterWriteEventArgs<OrdersFixed> e)
        {
           //  We only want clients with large frieght values
            if (e.Record.CustomerID == "HANAR" )
                e.RecordLine = "Insufficient Access";
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

        //-> File:output.txt
        //-> /File

        //-> File:Report layout.cs
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
        //-> /File

    }
}