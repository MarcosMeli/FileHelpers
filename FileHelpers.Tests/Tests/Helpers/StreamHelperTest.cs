using System;
using System.IO;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests.Helpers
{
    /// <summary>
    /// FileHelpers Helpers StreamHelper tests
    /// Stream Helper opens files and optionally trims off blank lines at the end.
    /// We access this through the engine with append lines to send an append command
    /// </summary>
    [TestFixture]
    internal class StreamHelperTest
    {
        [Test(Description = "A basic Windows windows CR/LF file")]
        public void TestDosFile()
        {
            var testdata = new SimpleData {
                Record = "one record only"
            };

            var engine = new DelimitedFileEngine<SimpleData>();
            using (var filename = new TempFileFactory()) {
                string twoRecords = testdata.Record + Environment.NewLine + testdata.Record + Environment.NewLine;

                ProcessAppend(testdata, engine, filename, twoRecords, "\r\n\r\n");
                ProcessAppend(testdata, engine, filename, twoRecords, "\n\n\n");
                ProcessAppend(testdata, engine, filename, twoRecords, "\r\r\r");

                GC.Collect(); //  Clean up the loose file stream from the testing
            }
        }

        private static void ProcessAppend(SimpleData testdata,
            IFileHelperEngine<SimpleData> engine,
            string filename,
            string twoRecords,
            string lineEnds)
        {
            using (var fs = new StreamWriter(filename)) {
                fs.Write(testdata.Record);
                fs.Write(lineEnds); // lots of blanks lines to trim
                fs.Close();
            }

            engine.AppendToFile(filename, testdata);

            using (var input = new StreamReader(filename)) {
                string result = input.ReadToEnd();
                Check.That(result).IsEqualTo(twoRecords);
                input.Close();
            }
        }

        [DelimitedRecord(";")]
        private class SimpleData
        {
            internal string Record;
        }
    }
}