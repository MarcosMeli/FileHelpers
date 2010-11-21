using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    //-> {Example.Name:Read Delimited File}
    //-> {Example.Description:Example of how to read a Delimited File}

    public class ReadFile
        :IDemo
    {

        //-> {Example.File:Example.cs}

        public void Run()
        {
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("Input.txt");

            foreach (var record in records)
            {
                Console.WriteLine(record.CustomerID);
                Console.WriteLine(record.OrderDate.ToString("dd/MM/yyyy"));
                Console.WriteLine(record.Freight);
            }
        }

        [DelimitedRecord("|")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            public decimal Freight;
        }
        //-> {/Example.File}

        //-> {Example.File:Input.txt}
/*ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
AROUT|Around the Horn|Thomas Hardy|Sales Representative|120 Hanover Sq.|London|UK
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BLONP|Blondesddsl père et fils|Frédérique Citeaux|Manager|24, Kléber|Strasbourg|France
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain*/
        //-> {/Example.File}
    }

    


}
