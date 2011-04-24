using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFramework
{
    //-> {Example.Name:Write Fixed File}
    //-> {Example.Description:Example of how to write a Fixed Record File}
    //-> {Example.AutoRun:true}

    public class WriteFileFixed
        : ExampleBase
    {
        /// <summary>
        /// Execute engine and write out records we define in memory delimited by |
        /// </summary>
        public override void Run()
        {
            //-> {Example.File:Example.cs}
            var engine = new FileHelperEngine<Customer>();

            var customers = new List<Customer>();

            var order1 = new Customer() { CustId = 1, Name = "Antonio Moreno Taquería", AddedDate = new DateTime(2009, 05, 01) };
            var order2 = new Customer() { CustId = 2, Name = "Berglunds snabbköp", AddedDate = new DateTime(2009, 05, 02) };

            customers.Add(order1);
            customers.Add(order2);

            engine.WriteFile("Output.Txt", customers);

            //-> {/Example.File}
            Console.WriteLine(engine.WriteString(customers));

        }

        //-> {Example.File: Output.Txt}
        //-> {/Example.File}

        //-> {Example.File:RecordClass.cs}
        [FixedLengthRecord()]
        public class Customer
        {
            [FieldFixedLength(5)]
            public int CustId;

            [FieldFixedLength(30)]
            [FieldTrim(TrimMode.Both)]
            public string Name;

            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime AddedDate;

        }
        //-> {/Example.File}

     
    }
}
