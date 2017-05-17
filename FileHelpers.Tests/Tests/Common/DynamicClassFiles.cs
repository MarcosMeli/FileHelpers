using System;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
	[Category("Dynamic")]
	public class DynamicClassesFiles
    {
        //FileHelperEngine engine;

        [Test]
        public void LoadFromXML()
        {
            ClassBuilder cb = ClassBuilder.LoadFromXml(TestCommon.GetPath("Dynamic", "VendorImport.xml"));
            Type t = cb.CreateRecordClass(); // this line generates an error in the FH library 

            using (var engine = new FileHelperAsyncEngine(t)) {
                engine.BeginReadString("");

                while (engine.ReadNext() != null) {}
            }
        }
    }
}