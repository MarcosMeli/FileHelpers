using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

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
            var testdata = new SimpleData() {
                Record = "one record only"
            };

            var engine = new DelimitedFileEngine<SimpleData>();
            using (var filename = new TempFileFactory()) {
                String twoRecords = testdata.Record + StringHelper.NewLine + testdata.Record + StringHelper.NewLine;

                ProcessAppend(testdata, engine, filename, twoRecords, "\r\n\r\n", "Dos");
                ProcessAppend(testdata, engine, filename, twoRecords, "\n\n\n", "Unix");
                ProcessAppend(testdata, engine, filename, twoRecords, "\r\r\r", "Macintosh");

                GC.Collect(); //  Clean up the loose file stream from the testing
            }
        }

        private static void ProcessAppend(SimpleData testdata,
            DelimitedFileEngine<SimpleData> engine,
            String filename,
            String twoRecords,
            string LineEnds,
            string testname)
        {
            using (var fs = new StreamWriter(filename)) {
                fs.Write(testdata.Record);
                fs.Write(LineEnds); // lots of blanks lines to trim
                fs.Close();
            }

            engine.AppendToFile(filename, testdata);

            using (var input = new StreamReader(filename)) {
                String result = input.ReadToEnd();
                result.AssertEqualTo<String>(twoRecords, testname + ": Expected two records only on output");
                input.Close();
            }
        }


        [DelimitedRecord(";")]
        private class SimpleData
        {
            internal String Record;
        }
    }
}