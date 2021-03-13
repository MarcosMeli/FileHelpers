using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Events;

namespace FileHelpers.Examples.EventsAndNotification.INotifyRead
{
    //-> Name: INotifyRead Interface
    //-> Description: Get Before/After Read events with the INotifyRead interface

    public class NotifyReadSample
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
            :FileHelpers.Events.INotifyRead
		{
			[FieldFixedLength(7)]
			public int OrderID;

			[FieldFixedLength(8)]
			public string CustomerID;

			[FieldFixedLength(8)]
			public DateTime OrderDate;

			[FieldFixedLength(11)]
			public decimal Freight;


            public void BeforeRead(BeforeReadEventArgs e)
            {
                if (e.RecordLine.StartsWith(" ") ||
                   e.RecordLine.StartsWith("-"))
                    e.SkipThisRecord = true;
            }

            public void AfterRead(AfterReadEventArgs e)
            {   
                //  we want to drop all records with no freight
                if (Freight == 0)
                    e.SkipThisRecord = true;

            }

		}

		//-> /File


        public override void Run()
        {
            //-> File:RunEngine.cs

            var engine = new FileHelperEngine<OrdersFixed>();
            var result = engine.ReadFile("input.txt");

            foreach (var value in result)
                Console.WriteLine("Customer: {0} Freight: {1}", value.CustomerID, value.Freight);
            //-> /File

        }

        //-> Console
        
    }
}