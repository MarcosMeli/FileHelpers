#if ! MINI

using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class TransformEngine
	{
		[Test]
		public void CsvToVerticalDelimited()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass));
			link.TransformFile1To2(TestCommon.TestPath("Good\\Transform1.txt"), TestCommon.TestPath("Good\\transformout.txt"));
			if (File.Exists(TestCommon.TestPath("Good\\transformout.txt"))) File.Delete(TestCommon.TestPath("Good\\transformout.txt"));
		}

		[DelimitedRecord(",")]
		private class FromClass
		{
			public string CustomerId;
			public string CompanyName;
			public string CustomerName;

			[TransformToRecord(typeof(ToClass))]
			public ToClass Transform()
			{
				ToClass res = new ToClass();
				res.CustomerId = CustomerId;
				res.CompanyName = CompanyName;
				res.CustomerName = CustomerName;

				return res;
			}
		}
	
		[FixedLengthRecord()]
		private class ToClass
		{
			[FieldFixedLength(10)]
			public string CustomerId;
			[FieldFixedLength(50)]
			public string CompanyName;
			[FieldFixedLength(60)]
			public string CustomerName;
		}
	
	}
}

#endif