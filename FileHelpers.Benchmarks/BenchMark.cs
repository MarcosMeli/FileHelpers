using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using FileHelpers;

namespace FileHelpers.Benchmarks
{
	class Benchmark
	{


		[STAThread]
		static void Main(string[] args)
		{
		    string file = Path.GetTempFileName();
            CreateSampleFixedString(file, 200000);
            var engine = new FileHelperAsyncEngine<FixedSampleRecord>();
            //TestFixedLengthRecord(engine, file);

            engine.AfterReadRecord += new FileHelpers.Events.AfterReadHandler<FixedSampleRecord>(engine_AfterReadRecord);
            engine.BeforeReadRecord += new FileHelpers.Events.BeforeReadHandler<FixedSampleRecord>(engine_BeforeReadRecord);
            engine.Progress += new EventHandler<FileHelpers.Events.ProgressEventArgs>(engine_Progress);
            engine.AfterWriteRecord += new FileHelpers.Events.AfterWriteHandler<FixedSampleRecord>(engine_AfterWriteRecord);
            engine.BeforeWriteRecord += new FileHelpers.Events.BeforeWriteHandler<FixedSampleRecord>(engine_BeforeWriteRecord);

            System.Threading.Thread.Sleep(2000);
            long start = DateTime.Now.Ticks;

		    TestFixedLengthRecord(engine, file);

            var ts = new TimeSpan(DateTime.Now.Ticks - start);

            Console.WriteLine("Total Time: " + Math.Round(ts.TotalSeconds, 2));
            if (args.Length > 0)
		        Console.ReadLine();
		}

        static void engine_BeforeWriteRecord(EngineBase engine, FileHelpers.Events.BeforeWriteEventArgs<FixedSampleRecord> e)
        {
            
        }

        static void engine_AfterWriteRecord(EngineBase engine, FileHelpers.Events.AfterWriteEventArgs<FixedSampleRecord> e)
        {
            
        }

        static void engine_BeforeReadRecord(EngineBase engine, FileHelpers.Events.BeforeReadEventArgs<FixedSampleRecord> e)
        {

        }

        static void engine_AfterReadRecord(EngineBase engine, FileHelpers.Events.AfterReadEventArgs<FixedSampleRecord> e)
        {
            
        }

        static void engine_Progress(object sender, FileHelpers.Events.ProgressEventArgs e)
        {
        }

	    private static void TestFixedLengthRecord(FileHelperAsyncEngine<FixedSampleRecord> engine, string file)
	    {
			using (engine.BeginReadFile(file))
			{
                while (engine.ReadNext() != null)
                {

                }
            }
	    }

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
        private static void CreateSampleFixedString(string file, int records)
	    {
	        using (var stream = new StreamWriter(file))
	        {
                var loops = records / 20;

                //var res = new StringBuilder(mFixedSample.Length*(loops + 1));
                for (int i = 0; i < loops; i++)
                {
                    stream.Write(mFixedSample);
                }
	            
	        }
	    }
	}
	
	internal interface IRecordAssigner
			{
				object CreateAndAssign(object[] values);
			}
	
	
}
