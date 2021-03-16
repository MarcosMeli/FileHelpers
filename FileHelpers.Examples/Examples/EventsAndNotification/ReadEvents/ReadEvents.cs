using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Events;
using NUnit.Framework;

namespace FileHelpers.Examples.EventsAndNotification.ReadEvents
{
    //-> Name: Before/After Read Event Handling
    //-> Description:Show how to implement read events
    public class ReadBeforeEventSample
        : ExampleBase
    {

        //-> Reads input.txt and skips all the records that are not detail records using a simple criteria

        //-> FileIn:input.txt
        /*-----------------------------------------------------
        *              XXX Enterprise
        *-----------------------------------------------------
        *10249   TOMSP  05071996      11.61
        *10250   HANAR  08071996      65.83
        *10251   VICTE  08071996       0.00
        *                                             Page 1
        *-----------------------------------------------------
        *                YYY Enterprise
        *-----------------------------------------------------
        *10269   TOMSP  05071996      11.61
        *10230   HANAR  08071996       0.00
        *10151   VICTE  08071996      41.34
        */
        //-> /File


        //-> File:Report layout.cs

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


		//-> File:RunEngine.cs
        protected override void Run()
        {
            var engine = new FileHelperEngine<OrdersFixed>();
            engine.BeforeReadRecord += BeforeEvent;
			engine.AfterReadRecord += AfterEvent;

			var result = engine.ReadFile("input.txt");

            foreach (var value in result)
                Console.WriteLine("Customer: {0} Freight: {1}", value.CustomerID, value.Freight);

        }

        private void BeforeEvent(EngineBase engine, BeforeReadEventArgs<OrdersFixed> e)
        {
            if (e.RecordLine.StartsWith(" ") ||
                e.RecordLine.StartsWith("-"))
                e.SkipThisRecord = true;

            //  Sometimes changing the record line can be useful, for example to correct for
            //  a bad data layout.  Here is an example of this, commented out for this example

            //if (e.RecordLine.StartsWith(" "))
            //   e.RecordLine = "Be careful!";
        }


		private void AfterEvent(EngineBase engine, AfterReadEventArgs<OrdersFixed> e)
		{
			//  we want to drop all records with no freight
			if (e.Record.Freight == 0)
				e.SkipThisRecord = true;
		}
        //-> /File

		//-> Console

		//-> <b>Important</b>You can use lambda expressions instead of event methods, for example:

		//-> File:RunEngineLambda.cs
		public void RunLambda()
		{
			var engine = new FileHelperEngine<OrdersFixed>();
			engine.BeforeReadRecord += (eng, e) => {
				if (e.RecordLine.StartsWith (" ") ||
			        e.RecordLine.StartsWith ("-"))
					e.SkipThisRecord = true;
			};
			engine.AfterReadRecord +=  (eng, e) => {
				if (e.Record.Freight == 0)
					e.SkipThisRecord = true;
			};

			var result = engine.ReadFile("input.txt");

			foreach (var value in result)
				Console.WriteLine("Customer: {0} Freight: {1}", value.CustomerID, value.Freight);

		}

		//-> /File

    }
}