using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FileHelpers;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class FastReadCsv
	{

		[Test]
		public void ReadFileComma()
		{
            string file = TestCommon.GetPath("Good", "RealCsvComma1.txt");

            List<RecordIndexer> res = new List<RecordIndexer>();
            
            foreach (RecordIndexer record in CommonEngine.ReadCsv(file))
		    {
                res.Add(record);
		    }

            Assert.AreEqual(21, res.Count);
            Assert.AreEqual("CustomerID", res[0][0]);
            Assert.AreEqual("ALFKI", res[1][0]);
            Assert.AreEqual("ERNSH", res[20][0]);

            Assert.AreEqual("Country", res[0][6]);
            Assert.AreEqual("Germany", res[1][6]);
            Assert.AreEqual("Austria", res[20][6]);

		}


        [Test]
        public void ReadFileHeader1()
        {
            string file = TestCommon.GetPath("Good", "RealCsvComma1.txt");

            List<RecordIndexer> res = new List<RecordIndexer>();

            foreach (RecordIndexer record in CommonEngine.ReadCsv(file, ',', 1))
            {
                res.Add(record);
            }

            Assert.AreEqual(20, res.Count);
            Assert.AreEqual("ALFKI", res[0][0]);
            Assert.AreEqual("ERNSH", res[19][0]);

            Assert.AreEqual("Germany", res[0][6]);
            Assert.AreEqual("Austria", res[19][6]);

            //Assert.AreEqual("CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Country", res[0].Header);
            //Assert.AreEqual("CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Country", res[19].Header);

        }

        public void ReadFileTab()
        {
            string file = TestCommon.GetPath("Good", "RealCsvTab1.txt");

            List<RecordIndexer> res = new List<RecordIndexer>();

            foreach (RecordIndexer record in CommonEngine.ReadCsv(file, '\t', 1))
            {
                res.Add(record);
            }

            Assert.AreEqual(20, res.Count);
            Assert.AreEqual("ALFKI", res[0][0]);
            Assert.AreEqual("ERNSH", res[19][0]);

            Assert.AreEqual("Germany", res[0][6]);
            Assert.AreEqual("Austria", res[19][6]);

            //Assert.AreEqual("CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Country", res[0].Header);
            //Assert.AreEqual("CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Country", res[19].Header);

        }

        [Test]
        public void ReadFileTab2()
        {
            string file = TestCommon.GetPath("Good", "RealCsvTab2.txt");

            List<RecordIndexer> res = new List<RecordIndexer>();

            foreach (RecordIndexer record in CommonEngine.ReadCsv(file, '\t', 0))
            {
                res.Add(record);
            }

            Assert.AreEqual(21, res.Count);
            Assert.AreEqual(9, res[0].FieldCount);

            Assert.AreEqual("EP_CRUDO", res[0][0]);
            Assert.AreEqual("REL_EP_FINAL_7", res[0][8]);

            
            Assert.AreEqual("ACUSA REBELDIA ADMITEN Y DESAHOGAN PRUEBAS Y SE ABRE JUICIO A ALEGATOS PORDOS DIAS", res[1][0]);
            Assert.AreEqual("REBELDIA", res[1][1]);


        }



	}
}