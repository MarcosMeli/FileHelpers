using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.SamplesDashboard
{
    public class DemoFactory
    {
        public static IEnumerable<DemoCode> GetDemos()
        {
            var res = new List<DemoCode>();
            DemoCode demo;

            demo = new DemoCode("Read Asyncronous", "Basic/Read");
            demo.Files.Add(new DemoFile());
            demo.LastFile.Contents = "Bla bla .bla";
            res.Add(demo);


            // Events
            demo = new DemoCode("Implementing INotifyRead", "Events/Interfaces");
            demo.Files.Add(new DemoFile());
            demo.LastFile.Contents = "Bla bla .bla";
            res.Add(demo);

            demo = new DemoCode("Implementing INotifyWrite", "Events/Interfaces");
            demo.Files.Add(new DemoFile());
            demo.LastFile.Contents = "Bla bla .bla";

            res.Add(demo);

            return res;
        }
    }
}
