using System;
using System.Data;
using System.IO;
using FileHelpers;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests
{
	[TestFixture]
	public class RunTimeClassesFiles
	{
		//FileHelperEngine engine;

        [Test]
		public void LoadFromXML()
		{
            ClassBuilder cb = ClassBuilder.LoadFromXml(TestCommon.GetPath("RunTime", "VendorImport.xml"));
            Type t = cb.CreateRecordClass(); // this line generates an error in the FH library 

            using (FileHelperAsyncEngine engine = new FileHelperAsyncEngine(t))
            {
                engine.BeginReadString("");

                while (engine.ReadNext() != null)
                {
                }
            } 
		}

	}
}