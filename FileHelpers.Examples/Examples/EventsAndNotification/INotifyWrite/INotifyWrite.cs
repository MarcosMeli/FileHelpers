using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Events;

namespace FileHelpers.Examples.EventsAndNotification.INotifyWrite
{
    //-> Name: INotifyWrite Interface
    //-> Description: Get Before/After Write events with the INotifyWrite interface

    public class NotifyWriteSample
        : ExampleBase
    {

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
            :FileHelpers.Events.INotifyWrite
		{
			[FieldFixedLength(7)]
			public int OrderID;

			[FieldFixedLength(8)]
			public string CustomerID;

			[FieldFixedLength(8)]
			public DateTime OrderDate;

			[FieldFixedLength(11)]
			public decimal Freight;

            public void BeforeWrite(BeforeWriteEventArgs e)
            {  
                //  We only want clients with large frieght values
                if (Freight < 40)
                    e.SkipThisRecord = true;
            }

            public void AfterWrite(AfterWriteEventArgs e)
            {
                //  Hide a line
                if (CustomerID.Trim() == "HANAR")
                    e.RecordLine = "-- Insufficient Access";
            }
		}

		//-> /File

		//-> Run a record through engine using the write event to filter out unwanted details

        public override void Run()
        {
            //-> File:RunEngine.cs

            var engine = new FileHelperEngine<OrdersFixed>();

            var result = engine.ReadFile("Input.txt");

            engine.WriteFile("output.txt", result);
            //-> /File

        }
        
        //-> FileOut:output.txt
        




    }
}