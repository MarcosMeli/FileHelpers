using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class WritersFieldIndexers
	{
	    readonly string data = "11121314901234" + Environment.NewLine +
			"10111314012345" + Environment.NewLine +
			"11101314123456" + Environment.NewLine +
			"10101314234567" + Environment.NewLine;


		[Test] 
		public void AsyncFieldIndex1()
		{

			var engine = new FileHelperAsyncEngine(typeof(SampleType));

		    var sw = new StringWriter();
            engine.BeginWriteStream(sw);

		    engine[0] = new DateTime(2003, 2, 1);
		    engine[1] = "B";
            engine[2] = 18;

		    engine.WriteNextValues();

		    engine.Close();

		    engine.BeginReadString(sw.GetStringBuilder().ToString());
		    engine.ReadNext();

		    Assert.AreEqual(new DateTime(2003, 2, 1), engine[0]);
		    Assert.AreEqual("B", engine[1]);
		    Assert.AreEqual(18, engine[2]);

            Assert.AreEqual(new DateTime(2003, 2, 1), engine.LastRecordValues[0]);
            Assert.AreEqual("B", engine.LastRecordValues[1]);
            Assert.AreEqual(18, engine.LastRecordValues[2]);

            engine.ReadNext();

		    Assert.IsNull(engine.LastRecord);
            Assert.IsNull(engine.LastRecordValues);
		}

        [Test]
        public void AsyncFieldIndexBad1()
        {
            var engine = new FileHelperAsyncEngine(typeof(SampleType));
            Assert.Throws<BadUsageException>(()
                 => engine[0] = new DateTime(2003, 2, 1));
        }

        [Test]
        public void AsyncFieldIndexBad2()
        {
            var engine = new FileHelperAsyncEngine(typeof(SampleType));
            
            Assert.Throws<BadUsageException>(()
                 => engine.WriteNextValues());
        }

        [Test]
        public void AsyncFieldIndexBad3()
        {
            var engine = new FileHelperAsyncEngine(typeof(SampleType));
            var sw = new StringWriter();
            engine.BeginWriteStream(sw);
            
            Assert.Throws<BadUsageException>(()
                 => engine.WriteNextValues());
        }


	}
}