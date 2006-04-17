using System;
using FileHelpers;
using FileHelpers.MasterDetail;
using NUnit.Framework;
using System.IO;

namespace FileHelpersTests
{
	[TestFixture]
	public class MultiRecords
	{
		MultiRecordEngine engine;

		[Test]
		public void MultpleRecordsFile()
		{
			engine = new MultiRecordEngine(new Type[] {typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType)}, new RecordTypeSelector(Test1Selector));

            object[] res = engine.ReadFile(TestCommon.TestPath(@"Good\MultiRecord1.txt"));

            Assert.AreEqual(12, res.Length);

            Assert.AreEqual(12, engine.TotalRecords);

		}

        Type Test1Selector(string record)
        {
            if (Char.IsLetter(record[0]))
                return typeof(CustomersSemiColon);
			else if (record.Length == 14)
				return typeof(SampleType);
            else
                return typeof(OrdersVerticalBar);
        }

        


	}
}