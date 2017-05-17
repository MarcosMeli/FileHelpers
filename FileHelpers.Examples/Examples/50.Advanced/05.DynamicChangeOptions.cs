using FileHelpers;

namespace ExamplesFx
{
    //-> Name: Dynamic Engine Options
    //-> Description: Change the options of the engines at run time

    public class EngineOptions
        : ExampleBase
    {
        //-> FileIn:Input.txt
        /*ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain
*/

        //-> /File

        //-> File:CustomersVerticalBar.cs
        [DelimitedRecord("|")]
        public class CustomersVerticalBar
        {
            public string CustomerID;
            
            // Will be excluded at run time
            public string DummyField;

            public string CompanyName;
            public string ContactName;
            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;
        }

        //-> /File

        public override void Run()
        {
            //-> File:Example.txt

            var engine = new DelimitedFileEngine<CustomersVerticalBar>();

            engine.Options.Fields[2].TrimMode = TrimMode.Both;
            engine.Options.RemoveField("DummyField");
            
            // City is optional
            engine.Options.Fields[engine.Options.Fields.Count - 1].IsOptional = true;

            engine.ReadFile("Input.txt");

            //-> /File

        }
        
      
    }
}