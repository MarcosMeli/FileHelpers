using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name: Smart Format Detector
    //-> Description: Detect the format from a flat file

    public class AutoFormatDetectorExample
        : ExampleBase
    {
        //
        //-> FileIn:input.txt
        /*Id|Company Name|Representative|Position|Address|City|Country
ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain*/
        //-> /File


        public override void Run()
        {
            //-> File:Example.cs

            var detector = new FileHelpers.Detection.SmartFormatDetector();
            var formats = detector.DetectFileFormat("input.txt");

            foreach (var format in formats)
            {
                Console.WriteLine("Format Detected, confidence:" + format.Confidence + "%");
                var delimited = format.ClassBuilderAsDelimited;

                Console.WriteLine("    Delimiter:" + delimited.Delimiter);
                Console.WriteLine("    Fields:");

                foreach (var field in delimited.Fields)
                {
                   Console.WriteLine("        " + field.FieldName + ": " + field.FieldType);    
                }
                
                
            }
            //-> /File
        }
        
    }
}
 