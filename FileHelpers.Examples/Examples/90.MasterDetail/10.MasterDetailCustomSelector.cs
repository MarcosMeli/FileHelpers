﻿using System;
using FileHelpers;
using FileHelpers.MasterDetail;

namespace ExamplesFx
{
    public class SimpleMasterDetailSample
        : ExampleBase
    {
        //-> Name: Master Detail Custom Selector
        //-> Description:Show how to implement Master detail reading using a selection subroutine

        //-> File:RunEngine.cs
        /// <summary>
        /// Run a record through engine using a selector to create a master detail input
        /// </summary>
        public override void Run()
        {
            var engine = new MasterDetailEngine<Customers, Orders>(new MasterDetailSelector(ExampleSelector));

            var result = engine.ReadFile("Input.txt");

            foreach (var group in result) {
                Console.WriteLine("Customer: {0}", group.Master.CustomerID);
                foreach (var detail in group.Details)
                    Console.WriteLine("    Freight: {0}", detail.Freight);
            }
        }

        /// <summary>
        /// Selector to determine whether we have a master or
        /// detail record to import
        /// </summary>
        /// <param name="record">Alpha characters coming in</param>
        /// <returns>Selector for master or detail record</returns>
        private RecordAction ExampleSelector(string record)
        {
            if (record.Length < 2)
                return RecordAction.Skip;

            if (Char.IsLetter(record[0]))
                return RecordAction.Master;
            else
                return RecordAction.Detail;
        }

        //-> /File

        //-> File:Input.txt
        /*ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
        10248|ALFKI|5|04071996|01081996|16071996|3|32.38
        10249|ALFKI|6|05071996|16081996|10071996|1|11.61
        10251|ALFKI|3|08071996|05081996|15071996|1|41.34
        ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner|Avda. de la Constitución 2222|México D.F.|Mexico
        10252|ANATR|4|09071996|06081996|11071996|2|51.3
        ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
        */
        //-> /File

        //-> File:output.txt
        //-> /File

        //-> File:Master layout.cs
        /// <summary>
        /// Layout of the master records beginning with alpha characters in input
        /// </summary>
        [DelimitedRecord("|")]
        public class Customers
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;
        }

        //-> /File

        //-> File:Detail layout.cs
        /// <summary>
        /// Layout of the detail records beginning with numerics in input
        /// </summary>
        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;
            public string CustomerID;
            public int EmployeeID;
            public DateTime OrderDate;
            public DateTime RequiredDate;
            public DateTime ShippedDate;
            public int ShipVia;
            public decimal Freight;
        }

        //-> /File
    }
}