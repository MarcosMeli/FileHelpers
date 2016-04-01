using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using FileHelpers;

namespace FileHelpers.Benchmarks
{
    /// <summary>
    /// Benchmark the application so that we can check performance
    /// </summary>
    internal class Benchmark
    {
        /// <summary>
        /// Performs a series of tests to show time differences
        /// </summary>
        [STAThread]
        private static void Main()
        {
            string file = Path.GetTempFileName();
            var records = 250000; // 

            Console.WriteLine("Generating file with " + records + " records");
            CreateSampleFixedString(file, records);

            //var engine = new FileHelperAsyncEngine<FixedSampleRecord>();
            //TestFixedLengthRecord(engine, file);

            //engine.AfterReadRecord += new FileHelpers.Events.AfterReadHandler<FixedSampleRecord>(engine_AfterReadRecord);
            //engine.BeforeReadRecord += new FileHelpers.Events.BeforeReadHandler<FixedSampleRecord>(engine_BeforeReadRecord);
            //engine.Progress += new EventHandler<FileHelpers.Events.ProgressEventArgs>(engine_Progress);
            //engine.AfterWriteRecord += new FileHelpers.Events.AfterWriteHandler<FixedSampleRecord>(engine_AfterWriteRecord);
            //engine.BeforeWriteRecord += new FileHelpers.Events.BeforeWriteHandler<FixedSampleRecord>(engine_BeforeWriteRecord);

            // Read one time to load it on memoty

            Console.WriteLine("File generated, precaching...");

            //TestFixedLengthRecord(engine, file);
            

            //System.Threading.Thread.Sleep(2000);

            //Console.WriteLine("Start Test...");
            //long start = DateTime.Now.Ticks;
            //TestFixedLengthRecord(engine, file);

            //var ts = new TimeSpan(DateTime.Now.Ticks - start);

            //Console.WriteLine("Records: " + records);
            //Console.WriteLine("Total Time: " + Math.Round(ts.TotalSeconds, 3) + " seconds");
            //Console.WriteLine(Math.Round(ts.TotalMilliseconds / records, 3) + " ms per record");

            var fi = new FileInfo(file);
            Console.WriteLine("Filesize: " + fi.Length + " Bytes | " + (fi.Length / (1024 * 1024)) + " MBytes");
            Console.WriteLine();
            Console.WriteLine("Start Test 1, Auto Block Size");
            RunBigFileSortTest(file, file + "_test1", 0);
            Console.WriteLine();
            Console.WriteLine("Start Test 2, 0.5 MB Block Size");
            RunBigFileSortTest(file, file + "_test2", (int)(0.5 * 1024 * 1024));
            Console.WriteLine();
            Console.WriteLine("Start Test 2, 10 MB Block Size");
            RunBigFileSortTest(file, file + "_test2", (10 * 1024 * 1024));
            Console.WriteLine();
            Console.WriteLine("Start Test 2, 40 MB Block Size");
            RunBigFileSortTest(file, file + "_test3", (40 * 1024 * 1024));
            Console.WriteLine();
            Console.WriteLine("Finished press any key to close");

            Console.ReadKey();
        }

        private static void engine_BeforeWriteRecord(EngineBase engine,
            FileHelpers.Events.BeforeWriteEventArgs<FixedSampleRecord> e) {}

        private static void engine_AfterWriteRecord(EngineBase engine,
            FileHelpers.Events.AfterWriteEventArgs<FixedSampleRecord> e) {}

        private static void engine_BeforeReadRecord(EngineBase engine,
            FileHelpers.Events.BeforeReadEventArgs<FixedSampleRecord> e) {}

        private static void engine_AfterReadRecord(EngineBase engine,
            FileHelpers.Events.AfterReadEventArgs<FixedSampleRecord> e) {}

        private static void engine_Progress(object sender, FileHelpers.Events.ProgressEventArgs e) {}


        private static void RunBigFileSortTest(string inputFile, string outputFile, int requestedBlockSize) {

            long start = DateTime.Now.Ticks;

            var blockSize = TestBigFileSorter(inputFile, outputFile, requestedBlockSize); 

            var ts = new TimeSpan(DateTime.Now.Ticks - start);

            
            Console.WriteLine("Block Size: " + blockSize + " Bytes | " + ((float)blockSize / (1024 * 1024)) + " MBytes");
            Console.WriteLine("Total Time: " + Math.Round(ts.TotalSeconds, 3) + " seconds");
        }

        /// <summary>
        /// Use BigFileSorter to sort the generated file 
        /// </summary>
        /// <param name="file">File to process</param>
        private static int TestBigFileSorter(string inputFile, string outputFile, int blockSize)
        {
            var sorter = new BigFileSorter<FixedSampleRecordSortable>(blockSize); // 10 Mb blocks

            sorter.Sort(inputFile, outputFile);
            return sorter.BlockFileSizeInBytes;
        }

        /// <summary>
        /// Use a simple loop mechanism to read a file sequentially
        /// </summary>
        /// <param name="engine">Engine to parse record</param>
        /// <param name="file">File to process</param>
        private static void TestFixedLengthRecord<T>(FileHelperAsyncEngine<T> engine, string file) where T : class
        {
            using (engine.BeginReadFile(file))
                while (engine.ReadNext() != null) {}
        }

        /// <summary>
        /// Sample data that will be repeated until we have the right count of records
        /// </summary>
        private static string mFixedSample =
            @"20000000109PANIAGUA JOSE                                                                                                                                                   0     
20000000125ACOSTA MARCOS                                                                                                                                                   0     
20000000141GONZALEZ DOMINGO                                                                                                                                                0     
20000000168SENA RAUL                                                                                                                                                       0     
20000000192BITTAR RUSTON MUSA                                                                                                                                              0     
20000000206CORDON SERGIO ALFREDO                                                                                                                                           0     
20000000222CAVAGNARO ERNESTO RODAS GUILLERMO Y RIV                                                                                                                         0     
20000000338BRUCE LUIS                                                                                                                                                      0     
20000000354CHAVEZ DAMIAN JOSE                                                                                                                                              0     
20000000389ZINGARETTI SANTIAGO ENRIQUE ARDUINO                                                                                                                             0     
20000000400PEREYRA ARNOTI LEONARDO ANDRES                                                                                                                                  0     
20000000516VELAZQUEZ ANIBAL ARISTOBU                                                                                                                                       0     
20000000532GONZALEZ DOMINGO                                                                                                                                                0     
20000000613CANALE S A CTA RECAUDADORA                                                                                                                                      0     
20000000656MU#OZ FERNANDEZ ALEJANDRO                                                                                                                                       0     
20000000745FERNANDEZ MONTOYA CHARLES JAIME                                                                                                                                 0     
20000000869MOSCHION DANILO JUAN                                                                                                                                            602290
20000000885CHOQUE RAMON FELIX                                                                                                                                              0     
20000000923AQUINO VILLASANTI NICASIO                                                                                                                                       0     
";

        /// <summary>
        /// Create a file containing records base on count
        /// </summary>
        /// <param name="file">File to create with sample</param>
        /// <param name="records">Number of records to create</param>
        private static void CreateSampleFixedString(string file, int records)
        {
            using (var stream = new StreamWriter(file)) {
                var loops = records/20;

                //var res = new StringBuilder(mFixedSample.Length*(loops + 1));
                for (int i = 0; i < loops; i++)
                    stream.Write(mFixedSample);
            }
        }
    }
}