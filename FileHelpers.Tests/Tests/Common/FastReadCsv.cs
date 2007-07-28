using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FileHelpers;
using FileHelpers.RunTime;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class FastReadCsv
	{

		[Test]
		public void ReadFileComma()
		{
            string file = Common.TestPath(@"Good\RealCsvComma1.txt");

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
            string file = Common.TestPath(@"Good\RealCsvComma1.txt");

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
            string file = Common.TestPath(@"Good\RealCsvTab1.txt");

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


	}
}