using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;
using FileHelpers.Events;

namespace ExamplesFx
{
    public class WriteEvents
        : ExampleBase
    {
        //-> Name:Before/After Write Event Handling
        //-> Description:Show how to implement write events

		//-> FileIn:Input.txt
		/*10249   TOMSP  05071996      11.61
10250   HANAR  08071996       0.00
10251   VICTE  08071996      41.34
10269   TOMSP  05071996      11.61
10230   HANAR  08071996      65.83
10151   VICTE  08071996      41.34
*/
		//-> /File

		//-> File:Report layout.cs
        [FixedLengthRecord]
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

		//-> Run a record through engine using the write event to filter out unwanted details

        //-> File:RunEngine.cs
        public override void Run()
        {
            var engine = new FileHelperEngine<OrdersFixed>();

            var result = engine.ReadFile("Input.txt");

            //  add our filter logic.
            engine.BeforeWriteRecord += BeforeWriteEvent;
			engine.AfterWriteRecord += AfterWriteEvent;

            engine.WriteFile("output.txt", result);
        }

        private void BeforeWriteEvent(EngineBase engine, BeforeWriteEventArgs<OrdersFixed> e)
        {
            //  We only want clients with large frieght values
            if (e.Record.Freight < 40)
                e.SkipThisRecord = true;
        }

		private void AfterWriteEvent(EngineBase engine, AfterWriteEventArgs<OrdersFixed> e)
		{
			//  Hide a line
			if (e.Record.CustomerID.Trim() == "HANAR")
				e.RecordLine = "-- Insufficient Access";
		}
        //-> /File

        //-> FileOut:output.txt

		//-> <b>Important</b>You can use lambda expressions instead of event methods, for example:

		//-> File:RunEngineLambda.cs
		public void RunLambda()
		{
			var engine = new FileHelperEngine<OrdersFixed>();

			var result = engine.ReadFile("Input.txt");

			//  add our filter logic.
			engine.BeforeWriteRecord += (eng, e) => {
				if (e.Record.Freight < 40)
					e.SkipThisRecord = true;
			};
			engine.AfterWriteRecord += (eng, e) => {
				if (e.Record.CustomerID == "HANAR")
					e.RecordLine = "Insufficient Access";
			};

			engine.WriteFile("output.txt", result);
		}

		//-> /File





    }
}