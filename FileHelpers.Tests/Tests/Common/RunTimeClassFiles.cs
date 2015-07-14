using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
	[Category("Dynamic")]
	public class RunTimeClassesFiles
    {
        //FileHelperEngine engine;

        [Test]
        public void LoadFromXML()
        {
            ClassBuilder cb = ClassBuilder.LoadFromXml(TestCommon.GetPath("RunTime", "VendorImport.xml"));
            Type t = cb.CreateRecordClass(); // this line generates an error in the FH library 

            using (var engine = new FileHelperAsyncEngine(t)) {
                engine.BeginReadString("");

                while (engine.ReadNext() != null) {}
            }
        }
    }
}